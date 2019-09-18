﻿/***************************************************
*创建人:rixiang.yu
*创建时间:2017/8/28 13:14:32
*功能说明:<Function>
*版权所有:<Copyright>
*Frameworkversion:4.0
*CLR版本：4.0.30319.42000
***************************************************/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using UcAsp.Opc;
using Opc;
using Factory = OpcCom.Factory;
using OpcDa = Opc.Da;
using System.Threading.Tasks;

namespace UcAsp.Opc.Da
{
    /// <summary>
    /// Client Implementation for DA
    /// </summary>
    public partial class DaClient : IClient<DaNode>
    {
        private readonly URL _url;
        private OpcDa.Server _server;
        private long _sub;
        private readonly IDictionary<string, DaNode> _nodesCache = new Dictionary<string, DaNode>();
        private readonly ClientOptions _options = new ClientOptions();
        private Dictionary<string, OpcDa.ISubscription> dic = new Dictionary<string, OpcDa.ISubscription>();

        // default monitor interval in Milliseconds
        private const int DefaultMonitorInterval = 100;

        public event EventHandler<ItemDataEventArgs> DataChange;

        /// <summary>
        /// Initialize a new Data Access Client
        /// </summary>
        /// <param name="serverUrl">the url of the server to connect to</param>
        public DaClient(Uri serverUrl)
        {
            _url = new URL(serverUrl.AbsolutePath)
            {
                Scheme = serverUrl.Scheme,
                HostName = serverUrl.Host
            };
        }

        /// <summary>
        /// Initialize a new Data Access Client
        /// </summary>
        /// <param name="serverUrl">the url of the server to connect to</param>
        public DaClient(Uri serverUrl, ClientOptions options)
        {
            _url = new URL(serverUrl.AbsolutePath)
            {
                Scheme = serverUrl.Scheme,
                HostName = serverUrl.Host
            };
            _options = options;
        }
        /// <summary>
        /// Options to configure the UA client session
        /// </summary>
        public ClientOptions Options
        {
            get { return _options; }
        }
        /// <summary>
        /// Gets the datatype of an OPC tag
        /// </summary>
        /// <param name="tag">Tag to get datatype of</param>
        /// <returns>System Type</returns>
        public System.Type GetDataType(string tag)
        {
            var item = new OpcDa.Item { ItemName = tag };
            OpcDa.ItemProperty result;
            try
            {
                var propertyCollection = _server.GetProperties(new[] { item }, new[] { new OpcDa.PropertyID(1) }, false)[0];
                result = propertyCollection[0];
            }
            catch (NullReferenceException)
            {
                throw new OpcException("Could not find node because server not connected.");
            }
            return result.DataType;
        }

        /// <summary>
        /// OpcDa underlying server object.
        /// </summary>
        protected OpcDa.Server Server
        {
            get
            {
                return _server;
            }
        }

        #region interface methods

        /// <summary>
        /// Connect the client to the OPC Server
        /// </summary>
        public void Connect()
        {
            if (Status == OpcStatus.Connected)
                return;
            _server = new OpcDa.Server(new Factory(), _url);
            _server.Connect();
            var root = new DaNode(string.Empty, string.Empty);
            RootNode = root;
            AddNodeToCache(root);
        }

        /// <summary>
        /// Gets the current status of the OPC Client
        /// </summary>
        public OpcStatus Status
        {
            get
            {
                if (_server == null || _server.GetStatus().ServerState != OpcDa.serverState.running)
                    return OpcStatus.NotConnected;
                return OpcStatus.Connected;
            }
        }

        /// <summary>
        /// Read a tag
        /// </summary>
        /// <typeparam name="T">The type of tag to read</typeparam>
        /// <param name="tag">The fully-qualified identifier of the tag. You can specify a subfolder by using a comma delimited name.
        /// E.g: the tag `foo.bar` reads the tag `bar` on the folder `foo`</param>
        /// <returns>The value retrieved from the OPC</returns>
        public T Read<T>(string tag)
        {
            var item = new OpcDa.Item { ItemName = tag };
            if (Status == OpcStatus.NotConnected)
            {
                throw new OpcException("Server not connected. Cannot read tag.");
            }
            var result = _server.Read(new[] { item })[0];
            CheckResult(result, tag);
            T casted;
            TryCastResult(result.Value, out casted);
            return casted;
        }
        public List<OpcItemValue> Read(string[] tag)
        {
            if (Status == OpcStatus.NotConnected)
            {
                throw new OpcException("Server not connected. Cannot read tag.");
            }
            List<OpcDa.Item> items = new List<OpcDa.Item>();
            for (int i = 0; i < tag.Length; i++)
            {
                items.Add(new OpcDa.Item { ItemName = tag[i] });
            }
            OpcDa.ItemValueResult[] itemresult = _server.Read(items.ToArray());
            List<OpcItemValue> itemvalues = new List<OpcItemValue>();
            for (int i = 0; i < itemresult.Length; i++)
            {
                itemvalues.Add(new OpcItemValue { ItemId = itemresult[i].ItemName, Quality = itemresult[i].Quality.ToString(), Value = itemresult[i].Value, Timestamp = itemresult[i].Timestamp });
            }
            return itemvalues;
        }
        /// <summary>
        /// Write a value on the specified opc tag
        /// </summary>
        /// <typeparam name="T">The type of tag to write on</typeparam>
        /// <param name="tag">The fully-qualified identifier of the tag. You can specify a subfolder by using a comma delimited name.
        /// E.g: the tag `foo.bar` writes on the tag `bar` on the folder `foo`</param>
        /// <param name="item"></param>
        public void Write<T>(string tag, T item)
        {
            var itmVal = new OpcDa.ItemValue
            {
                ItemName = tag,
                Value = item
            };
            var result = _server.Write(new[] { itmVal })[0];
            CheckResult(result, tag);
        }
        public List<Result> Write(string[] tag, object[] values)
        {
            List<Result> results = new List<Result>();
            if (tag.Length != values.Length)
                throw new Exception("item和values个数不一致");
            List<OpcDa.ItemValue> _values = new List<OpcDa.ItemValue>();
            for (int i = 0; i < tag.Length; i++)
            {
                var itmVal = new OpcDa.ItemValue
                {
                    ItemName = tag[i],
                    Value = values[i]
                };
            }
            var writresut = _server.Write(_values.ToArray());
            foreach (IdentifiedResult r in writresut)
            {
                results.Add(new Result { Succeed = r.ResultID == ResultID.S_OK });
            }
            return results;
        }
        /// <summary>
        /// Casts result of monitoring and reading values
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <param name="casted">The casted result</param>
        /// <typeparam name="T">Type of object to try to cast</typeparam>
        public void TryCastResult<T>(object value, out T casted)
        {
            try
            {
                casted = (T)ConvertToT.ConvertT<T>(value);
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException(
                  string.Format(
                    "Could not monitor tag. Cast failed for type \"{0}\" on the new value \"{1}\" with type \"{2}\". Make sure tag data type matches.",
                    typeof(T), value, value.GetType()));
            }
        }


        /// <summary>
        /// Finds a node on the Opc Server
        /// </summary>
        /// <param name="tag">The fully-qualified identifier of the tag. You can specify a subfolder by using a comma delimited name.
        /// E.g: the tag `foo.bar` finds the tag `bar` on the folder `foo`</param>
        /// <returns>If there is a tag, it returns it, otherwise it throws an </returns>
        public DaNode FindNode(string tag)
        {
            // if the tag already exists in cache, return it
            if (_nodesCache.ContainsKey(tag))
                return _nodesCache[tag];

            // try to find the tag otherwise
            var item = new OpcDa.Item { ItemName = tag };
            OpcDa.ItemValueResult result;
            try
            {
                result = _server.Read(new[] { item })[0];
            }
            catch (NullReferenceException)
            {
                throw new OpcException("Could not find node because server not connected.");
            }
            CheckResult(result, tag);
            var node = new DaNode(item.ItemName, item.ItemName, RootNode);
            AddNodeToCache(node);
            return node;
        }

        /// <summary>
        /// Gets the root node of the server
        /// </summary>
        public DaNode RootNode { get; private set; }



        /// <summary>
        /// Explore a folder on the Opc Server
        /// </summary>
        /// <param name="tag">The fully-qualified identifier of the tag. You can specify a subfolder by using a comma delimited name.
        /// E.g: the tag `foo.bar` finds the sub nodes of `bar` on the folder `foo`</param>
        /// <returns>The list of sub-nodes</returns>
        public IEnumerable<DaNode> ExploreFolder(string tag)
        {
            var parent = FindNode(tag);
            OpcDa.BrowsePosition p;
            var nodes = _server.Browse(new ItemIdentifier(parent.Tag), new OpcDa.BrowseFilters(), out p)
              .Select(t => new DaNode(t.Name, t.ItemName, parent))
              .ToList();
            //add nodes to cache
            foreach (var node in nodes)
                AddNodeToCache(node);

            return nodes;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_server != null)
                _server.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        /// Adds a node to the cache using the tag as its key
        /// </summary>
        /// <param name="node">the node to add</param>
        private void AddNodeToCache(DaNode node)
        {
            if (!_nodesCache.ContainsKey(node.Tag))
                _nodesCache.Add(node.Tag, node);
        }

        private static void CheckResult(IResult result, string tag)
        {
            if (result == null)
                throw new OpcException("The server replied with an empty response");
            if (result.ResultID.ToString() != "S_OK")
                throw new OpcException(string.Format("Invalid response from the server. (Response Status: {0}, Opc Tag: {1})", result.ResultID, tag));
        }


        public OpcGroup AddGroup(string groupName)
        {
            OpcGroup _group = new OpcGroup { IsActive = true, Name = groupName, UpdateRate = new TimeSpan(0, 0, 0, 0, _options.DefaultMonitorInterval) };
            var subItem = new OpcDa.SubscriptionState
            {
                Name = (++_sub).ToString(CultureInfo.InvariantCulture),
                Active = true,
                UpdateRate = DefaultMonitorInterval
            };

            var sub = _server.CreateSubscription(subItem);
            Action unsubscribe = () => new Thread(o =>
              _server.CancelSubscription(sub)).Start();

            dic.Add(groupName, sub);
            sub.DataChanged += (subscriptionHandle, requestHandle, values) =>
            {


                List<OpcItemValue> items = new List<OpcItemValue>();
                foreach (OpcDa.ItemValueResult value in values)
                {
                    OpcItemValue item = new OpcItemValue { GroupName = groupName, Value = value.Value, ItemId = value.ItemName, Quality = value.Quality.ToString(), Timestamp = value.Timestamp };
                    items.Add(item);

                }
                _group.SendValue(items);

            };
            return _group;

        }
        public Result AddItems(string groupName, string[] itemName)
        {
            OpcDa.ISubscription sub;
            OpcDa.ItemResult[] itemresult;
            if (dic.TryGetValue(groupName, out sub))
            {
                List<OpcDa.Item> items = new List<OpcDa.Item>();
                for (int i = 0; i < itemName.Length; i++)
                {
                    OpcDa.Item item = new OpcDa.Item { ItemName = itemName[i] };
                    items.Add(item);
                }
                itemresult = sub.AddItems(items.ToArray());
                string _noexit = string.Empty;
                for (int i = 0; i < itemresult.Length; i++)
                {
                    if (itemresult[i].ResultID != ResultID.S_OK)
                    {
                        _noexit += itemName[i];

                    }
                }
                return new Result() { Succeed = true, UserData = _noexit };

            }
            else
            {
                return new Result() { Succeed = false, UserData = groupName + "组不存在" };
            }


        }


    }



}

