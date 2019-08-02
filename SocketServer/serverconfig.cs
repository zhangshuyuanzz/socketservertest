using Base.kit;
using Common.log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SocketServer
{
    class Serverconfig
    {
        readonly NLOG logger = new NLOG("serverconfig");
        XmlDocument xDoc;
        string XmlPath;
        public Serverconfig(string ip)
        {
            string path1 = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            XmlPath = path1 + @"config\" + ip + ".xml";
            logger.Debug("xmlpath[{}]", XmlPath);
            if (System.IO.File.Exists(XmlPath) == false)
            {
                logger.Debug("this file is not exist!!");
                return;
            }
            xDoc = new XmlDocument();
            xDoc.Load(XmlPath);
        }
        private void addintotagall(Dictionary<int, string> wholet,int a,string b)
        {
            if (wholet.ContainsKey(a) == false)
            {
                wholet.Add(a, b);
            }
            else
            {
                logger.Debug("this dic had this tag info ,so,bach check you config file!!");
            }
        }
        public Dictionary<int, string> ServerConfigParseXml()
        {
            Dictionary<int, string> allll = new Dictionary<int, string>();
            try
            {
            logger.Info("ConfigParseXml  strart");
            XmlNode root = xDoc.SelectSingleNode("collector");

            foreach (XmlNode node in root.SelectNodes("device"))
            {
                foreach (XmlNode n in node.SelectNodes("tag"))
                {
                    int tagid;
                    string tagname;
                    tagid = int.Parse(XmlKit.GetByXml("id", n));
                    tagname = XmlKit.GetByXml("name", n);
                    addintotagall(allll, tagid, tagname);
                    logger.Info("--pp-------------tagid[{}]tagname[{}]", tagid, tagname);
                }
                foreach (XmlNode n in node.SelectNodes("command"))
                {
                    int tagid;
                    string tagname;
                    tagid = int.Parse(XmlKit.GetByXml("id", n));
                    tagname = XmlKit.GetByXml("name", n);
                    addintotagall(allll, tagid, tagname);
                    logger.Info("--pp-------------tagid[{}]tagname[{}]", tagid, tagname);
                }
            }
            }
            catch (Exception e)
            {
                logger.Debug("error[{}]",e.ToString());
            }
            xDoc.Save(XmlPath);
            return allll;
        }
    }
}
