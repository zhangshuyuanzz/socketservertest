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
        public List<DataItem> TagListAll = new List<DataItem>();
        public string OpcHandle = null;    //Uri("opcda://127.0.0.1/Matrikon.OPC.Simulation.1")
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
        private void addintotagall(ConcurrentDictionary<int, string> wholet, int a, string b)
        {
            if (wholet.ContainsKey(a) == false)
            {
                wholet.TryAdd(a, b);
            }
            else
            {
                wholet[a] = b;
                logger.Debug("this dic had this tag info ,so,back check you config file!!");
            }
        }
        private void addintotagname(ConcurrentDictionary<string, int> wholet, int a, string b)
        {
            if (wholet.ContainsKey(b) == false)
            {
                wholet.TryAdd(b, a);
            }
            else
            {
                wholet[b] = a;
                logger.Debug("name--this dic had this tag info ,so,bach check you config file!!");
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

                /*清空两个字典*/
                if (this.TagListAll.Count != 0)
                {
                    logger.Info("clear TagListAll strart");
                    this.TagListAll.Clear();
                }

           //     XmlNode opctroot = root.SelectSingleNode("Opc");
         //       string OpcIpw = XmlKit.GetByXml("tip", opctroot);
         //       string OpcNamew = XmlKit.GetByXml("name", opctroot);
           //     logger.Debug("11111OpcIp[{}]OpcName[{}]", OpcIp, OpcNamew);

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
                        this.TagListAll.Add(Onetag);
                        logger.Info("tag--parse xml file---------tagname[{}]", Onetag.TagName);
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
