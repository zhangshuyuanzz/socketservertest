﻿using Common.log;
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
        public string OpcIP = null;
        public string OpcName = null;

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
            if (this.DevListAll.ContainsKey(NewDev.taginfo.TagName) == false)
            {
                this.DevListAll.TryAdd(NewDev.taginfo.TagName, NewDev);
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
            if (this.DevBannerList.ContainsKey(NewDev.taginfo.TagName) == false)
            {
                this.DevBannerList.TryAdd(NewDev.taginfo.TagName, NewDev);
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
                    //string Opcstr = "opcda://{0}/{1}";
                    OpcIP = XmlKit.GetByXml("tip", onenode);
                    OpcName = XmlKit.GetByXml("name", onenode);

                    logger.Debug("OpcIp[{}]OpcName[{}]", OpcIP, OpcName);
                }

                XmlNode devsNode = root.SelectSingleNode("devs");
                foreach (XmlNode node in devsNode.SelectNodes("tag"))
                {
                    OpcAddIntoTagList(ParseDevNode(node));
                }

                XmlNode OradevsNode = root.SelectSingleNode("OracleDevs");
                foreach (XmlNode node in OradevsNode.SelectNodes("tag"))
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
            NMDev OraNewDev = new NMDev() {
                taginfo = new DataItem() {
                    TagName = XmlKit.GetByXml("tagname", oNode),
                    TagId = int.Parse(XmlKit.GetByXml("id", oNode, "0")),
                } ,
                devuint = XmlKit.GetByXml("unit", oNode),
                devdescription = XmlKit.GetByXml("des", oNode),
                devfac = XmlKit.GetByXml("tagfac", oNode),
                devtype = int.Parse(XmlKit.GetByXml("type", oNode, "1")),
                devprefix = XmlKit.GetByXml("Prefix", oNode) == "yes"?true:false,
        };
            OraNewDev.setTagLable();
            logger.Info("-parse dev node--devid[{}]", OraNewDev.taginfo.TagId);
            logger.Info("-parse dev node--devname[{}]", OraNewDev.taginfo.TagName);
            logger.Info("-parse dev node--devuint[{}]", OraNewDev.devuint);
            logger.Info("-parse dev node--devdescription[{}]", OraNewDev.devdescription);
            logger.Info("-parse dev node--devfac[{}]", OraNewDev.devfac);
            logger.Info("-parse dev node--devtype[{}]", OraNewDev.devtype);
            logger.Info("-parse dev node--devprefix[{}]", OraNewDev.devprefix);

            return OraNewDev ;
        }
    }
}
