
using Base.kit;
using Common.log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OpcSvr
{
    public class BaseOpcServerConfig
    {
        private static readonly NLOG Logger = new NLOG("BaseOpcServerConfig");
        public string server_name;
        public int serverrate;
        public Dictionary<string , OpcServerGroupInfo> OpcServerCFGs = new Dictionary<string, OpcServerGroupInfo>();
        public BaseOpcServerConfig(string XmlPath)
        {
            if (System.IO.File.Exists(XmlPath) == false) {
                Logger.Debug("this file is not exist!!");
                return;
            }
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(XmlPath);
            ConfigParseXml(xDoc);
            xDoc.Save(XmlPath);
        }
        public void ConfigParseXml(XmlDocument xDoc)
        {
            Logger.Info("ConfigParseXml  strart");
            XmlNode root = xDoc.SelectSingleNode("root");
            server_name = XmlKit.GetByXml("servername", root);
            Logger.Info("  server_name[{}]", server_name);

            foreach (XmlNode node in root.SelectNodes("group"))
            {
                OpcServerGroupInfo OpcServerCFG = new OpcServerGroupInfo();

                OpcServerCFG.rate = int.Parse(XmlKit.GetByXml("rate", node));
                serverrate = OpcServerCFG.rate;
                OpcServerCFG.name = XmlKit.GetByXml("name", node);
                Logger.Info("   server_rate[{}]name[{}]", OpcServerCFG.rate, OpcServerCFG.name);
                foreach (XmlNode n in node.SelectNodes("tag"))
                {
                    DataItem buffer = new DataItem();
                    buffer.TagId = int.Parse(XmlKit.GetByXml("tagid", n));
                    buffer.TagName = XmlKit.GetByXml("tagname", n);
                    OpcServerCFG.tags.Add(buffer.TagId, buffer);
                    Logger.Info("---------------tagid[{}]tagname[{}]", buffer.TagId, buffer.TagName);
                }
                OpcServerCFGs.Add(OpcServerCFG.name, OpcServerCFG);
            }
        }

    }
    public class OpcServerGroupInfo
    {
        public string name;
        public int rate;
        public Dictionary<int, DataItem> tags = new Dictionary<int, DataItem>();
    }
}
