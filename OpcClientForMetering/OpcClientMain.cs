using Base.kit;
using Common.log;
using SkKit.kit;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UcAsp.Opc;

namespace OpcClientForMetering
{
    class OpcClientMain
    {
        readonly NLOG logger = new NLOG("OpcClientMain");
        OpcClient OClient = null;
        public TagEventHandler OpcSetTagChanged;
        public OpcClientMain(string ClientHandle)
        {
            if (ClientHandle == null) {
                return;
            }
            logger.Debug("opc name [{}]", ClientHandle);
            OClient = new OpcClient(new Uri(ClientHandle));
            if (OClient.Connect == OpcStatus.Connected) {
                logger.Debug("Connected success");
            }
            else {
                logger.Debug("Connected fail!!");
            }
        }
        public void OpcClientMainRead(ref ConcurrentDictionary<string, DataItem> ReadTagList)
        {
            logger.Debug("OpcClientMainRead--Count[{}]", ReadTagList.Count);
            foreach (KeyValuePair<string, DataItem> one in ReadTagList)
            {
                logger.Debug("name[{}]", one.Key);
                DataItem TagData = one.Value;
                OpcItemValue data =  this.OClient.ReadOneTag<string>(TagData.TagName);
                logger.Debug("ItemId[{}]Value[{}]Timestamp[{}]", data.ItemId, data.Value, data.Timestamp);
                TagData.Value = data.Value;
                TagData.DataTime = data.Timestamp.ToString();
            }
        }
        OpcGroup OpcSetSubGroup = null;
        string OpcSetClientGName = "OpcSetClient";
        public void OpcClientMainSubscription(string[] Tagname)
        {
            string msg;
            logger.Debug("OpcClientMainSubscription---Length[{}]", Tagname.Length);
            if (this.OpcSetSubGroup == null) {
                this.OpcSetSubGroup = this.OClient.AddGroup(OpcSetClientGName);
                this.OpcSetSubGroup.DataChange += OpcSetGroup_DataChange;
            }
            this.OClient.AddItems(OpcSetClientGName, Tagname, out msg);
        }
        private void OpcSetGroup_DataChange(object sender, ItemDataEventArgs e)
        {
            List<DataItem> UPTagList = new List<DataItem>();
            logger.Debug("------------------------------------------------------------");
            foreach (OpcItemValue o in e.Data)
            {
                DataItem one = new DataItem();
                logger.Debug("ItemId[{}]Value[{}]Timestamp[{}]", o.ItemId,o.Value,o.Timestamp);
                one.TagName = o.ItemId;
                one.Value = o.Value;
                one.DataTime = o.Timestamp.ToString();
                UPTagList.Add(one);
            }
            OpcSetTagChanged?.Invoke(UPTagList);
        }
    }
}
