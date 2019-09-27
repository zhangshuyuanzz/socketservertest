using Common.log;
using System;
using Base.kit;
using SkKit;
using System.Collections.Concurrent;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using SkKit.kit;

namespace OpcClientForMetering
{
    class OpcSetConfig
    {
        readonly NLOG logger = new NLOG("OpcSetConfig");
        XmlDocument xDoc = null;
        string XmlPath = null;

        public ConcurrentDictionary<string, NMDev> DevListAll = new ConcurrentDictionary<string, NMDev>();
        public string OpcHandle = null;    
        public int SocketPort;
        public int SocketPeriod;
        public ConcurrentDictionary<string, NMDev> DevBannerList = new ConcurrentDictionary<string, NMDev>();
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
        public bool OpcAddIntoTagList(NMDev NewDev)
        {
            bool RetOp = false;
            if (this.DevListAll.ContainsKey(NewDev.devname) == false)
            {
                this.DevListAll.TryAdd(NewDev.devname, NewDev);
                RetOp = true;
            }
            else
            {
                logger.Debug("this dic had this tag info ,so,back check you config file!!");
            }
            return RetOp;
        }
        private void OpcAddIntoOracleList(NMDev NewDev)
        {
            if (this.DevBannerList.ContainsKey(NewDev.devname) == false)
            {
                this.DevBannerList.TryAdd(NewDev.devname, NewDev);
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
                if (this.DevListAll.Count != 0)
                {
                    logger.Info("clear TagListAll strart");
                    this.DevListAll.Clear();
                }
                if (this.DevBannerList.Count != 0)
                {
                    logger.Info("clear TagBannerList strart");
                    this.DevBannerList.Clear();
                }

                XmlNode SkNode = root.SelectSingleNode("Socket");
                this.SocketPort = int.Parse(XmlKit.GetByXml("port", SkNode,"0"));
                this.SocketPeriod = int.Parse(XmlKit.GetByXml("period", SkNode,"0"));
                logger.Info("SocketPort[{}]SocketPeriod[{}]", SocketPort, SocketPeriod);

                foreach (XmlNode onenode in root.SelectNodes("Opc"))
                {
                    string Opcstr = "opcda://{0}/{1}";
                    string OpcIp = XmlKit.GetByXml("tip", onenode);
                    string OpcName = XmlKit.GetByXml("name", onenode);
                    this.OpcHandle = string.Format(Opcstr, OpcIp, OpcName);
                    logger.Debug("OpcIp[{}]OpcName[{}]OpcHandle[{}]", OpcIp, OpcName, this.OpcHandle);
                }

                XmlNode devsNode = root.SelectSingleNode("devs");
                foreach (XmlNode node in devsNode.SelectNodes("dev"))
                {
                    OpcAddIntoTagList(ParseDevNode(node));
                }

                XmlNode OradevsNode = root.SelectSingleNode("OracleDevs");
                foreach (XmlNode node in OradevsNode.SelectNodes("dev"))
                {
                    OpcAddIntoOracleList(ParseDevNode(node));
                }
            }
            catch (Exception e)
            {
                logger.Debug("error[{}]", e.ToString());
            }
            xDoc.Save(XmlPath);
            return;
        }
        NMDev ParseDevNode(XmlNode oNode)
        {
            string devname = XmlKit.GetByXml("devname", oNode);
            string devdesc = XmlKit.GetByXml("des", oNode);
            string devprefix = XmlKit.GetByXml("Prefix", oNode);
            int devid = int.Parse(XmlKit.GetByXml("id", oNode, "0"));
            logger.Info("-parse dev node---devname[{}]devprefix[{}]devdesc[{}]devid[{}]", devname, devprefix, devdesc, devid);

            NMDev OraNewDev = new NMDev(devid, devname, devprefix, devdesc);
            foreach (XmlNode n in oNode.SelectNodes("tag"))
            {
                DataItem Onetag = new DataItem();
                Onetag.TagName = XmlKit.GetByXml("tagname", n);
                Onetag.Tagstr = XmlKit.GetByXml("unit", n);
                Onetag.Value = "-";
                logger.Info("tag--parse xml file-oracle--------tagname[{}]UNIT[{}]", Onetag.TagName, Onetag.Tagstr);
                OraNewDev.AddTagList(Onetag);
            }
            return OraNewDev ;
        }
    }
}
