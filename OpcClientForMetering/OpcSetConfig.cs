using Common.log;
using System;
using Base.kit;
using SkKit;
using System.Collections.Concurrent;
using System.IO;
using System.Xml;
using System.Collections.Generic;


namespace OpcClientForMetering
{
    class OpcSetConfig
    {
        readonly NLOG logger = new NLOG("OpcSetConfig");
        XmlDocument xDoc = null;
        string XmlPath = null;

        public ConcurrentDictionary<string, DataItem> TagListAll = new ConcurrentDictionary<string, DataItem>();
        public string OpcHandle = null;    //Uri("opcda://127.0.0.1/Matrikon.OPC.Simulation.1")
        public int SocketPort;
        public int SocketPeriod;
        public Dictionary<string, DataItem> TagBannerList = new Dictionary<string, DataItem>();
        public OpcSetConfig()
        {
            string path1 = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            XmlPath = path1 + @"config\OpcSetConfig.xml";
            logger.Debug("xmlpath[{}]", XmlPath);
            if (System.IO.File.Exists(XmlPath) == false)
            {
                logger.Debug("this file is not exist!!");
                return;
            }
            xDoc = new XmlDocument();
        }
        public bool OpcAddIntoTagList(DataItem NewTag)
        {
            bool RetOp = false;
            logger.Debug("OpcAddIntoTagList--TagName[{}]", NewTag.TagName);
            if (this.TagListAll.ContainsKey(NewTag.TagName) == false)
            {
                this.TagListAll.TryAdd(NewTag.TagName, NewTag);
                RetOp = true;
            }
            else
            {
                logger.Debug("this dic had this tag info ,so,back check you config file!!");
            }
            return RetOp;
        }
        private void OpcAddIntoOracleList(DataItem NewTag)
        {
            if (this.TagBannerList.ContainsKey(NewTag.TagName) == false)
            {
                this.TagBannerList.Add(NewTag.TagName, NewTag);
            }
            else
            {
                logger.Debug("name--this dic had this oracle tag info ,so,bach check you config file!!");
            }
        }
        public void OpcSetConfigParseXml()
        {
            if (xDoc == null)
            {
                return;
            }
            try
            {
                xDoc.Load(XmlPath);
                logger.Info("ConfigParseXml  start");
                XmlNode root = xDoc.SelectSingleNode("root");
                logger.Debug("Opc name[{}]", XmlKit.GetByXml("name", root));

                /*清空字典*/
                if (this.TagListAll.Count != 0)
                {
                    logger.Info("clear TagListAll strart");
                    this.TagListAll.Clear();
                }
                if (this.TagBannerList.Count != 0)
                {
                    logger.Info("clear TagBannerList strart");
                    this.TagBannerList.Clear();
                }

                XmlNode SkNode = root.SelectSingleNode("Socket");
                this.SocketPort = int.Parse(XmlKit.GetByXml("port", SkNode));
                this.SocketPeriod = int.Parse(XmlKit.GetByXml("period", SkNode));
                logger.Info("SocketPort[{}]SocketPeriod[{}]", SocketPort, SocketPeriod);

                foreach (XmlNode onenode in root.SelectNodes("Opc"))
                {
                    string Opcstr = "opcda://{0}/{1}";
                    string OpcIp = XmlKit.GetByXml("tip", onenode);
                    string OpcName = XmlKit.GetByXml("name", onenode);
                    this.OpcHandle = string.Format(Opcstr, OpcIp, OpcName);//Uri("opcda://127.0.0.1/Matrikon.OPC.Simulation.1")
                    logger.Debug("OpcIp[{}]OpcName[{}]OpcHandle[{}]", OpcIp, OpcName, this.OpcHandle);
                }
                foreach (XmlNode node in root.SelectNodes("Item"))
                {
                    foreach (XmlNode n in node.SelectNodes("tag"))
                    {
                        DataItem Onetag = new DataItem();
                        Onetag.TagName = XmlKit.GetByXml("tagname", n);
                        Onetag.TagId = int.Parse(XmlKit.GetByXml("id", n));
                        Onetag.Value = "-";
                        logger.Info("tag--parse xml file---------tagname[{}]TagId[{}]", Onetag.TagName, Onetag.TagId);
                        if (OpcAddIntoTagList(Onetag) == true)
                        {
                            //CfgTagList.Add(Onetag);
                        }
                    }
                }
                foreach (XmlNode node in root.SelectNodes("OracleItem"))
                {
                    foreach (XmlNode n in node.SelectNodes("tag"))
                    {
                        DataItem Onetag = new DataItem();
                        Onetag.TagName = XmlKit.GetByXml("tagname", n);
                        Onetag.TagId = int.Parse(XmlKit.GetByXml("id", n));
                        Onetag.Value = "-";
                        logger.Info("tag--parse xml file-oracle--------tagname[{}]TagId[{}]", Onetag.TagName, Onetag.TagId);
                        OpcAddIntoOracleList(Onetag);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Debug("error[{}]", e.ToString());
            }
            xDoc.Save(XmlPath);
            return;
        }
    }
}
