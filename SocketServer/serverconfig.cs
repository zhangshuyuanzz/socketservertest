using Base.kit;
using Common.log;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
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
        }
        public bool isxml()
        {
            string xmlpath = XmlPath;
            StreamReader sr = new StreamReader(xmlpath);
            string strXml = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(strXml);//判断是否加载成功
                return true;//是xml文件，返回
            }
            catch
            {
                return false;//不是xml文件，返回
            }
        }
        private void addintotagall(ConcurrentDictionary<int, string> wholet,int a,string b)
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
        public void ServerConfigParseXml(ref ConcurrentDictionary<int, string> allll, ref ConcurrentDictionary<string, int> allnames)
        {
            try
            {
            xDoc.Load(XmlPath);
            logger.Info("ConfigParseXml  strart");
            XmlNode root = xDoc.SelectSingleNode("collector");

            /*清空两个字典*/
            if (allll.Count != 0) {
                logger.Info("clear TagListWithID strart");
                allll.Clear();
            }
            if (allnames.Count != 0)
            {
                logger.Info("clear TagListWithName strart");
                allnames.Clear();
            }

            foreach (XmlNode node in root.SelectNodes("device"))
            {
                foreach (XmlNode n in node.SelectNodes("tag"))
                {
                    int tagid;
                    string tagname;
                    tagid = int.Parse(XmlKit.GetByXml("id", n));
                    tagname = XmlKit.GetByXml("name", n);
                    addintotagall(allll, tagid, tagname);
                    addintotagname(allnames, tagid, tagname);
                    logger.Info("tag--parse xml file-------------tagid[{}]tagname[{}]", tagid, tagname);
                }
                foreach (XmlNode n in node.SelectNodes("command"))
                {
                    int tagid;
                    string tagname;
                    tagid = int.Parse(XmlKit.GetByXml("id", n));
                    tagname = XmlKit.GetByXml("name", n);
                    addintotagall(allll, tagid, tagname);
                    addintotagname(allnames, tagid, tagname);
                    logger.Info("cmd--parse xml file-------------tagid[{}]tagname[{}]", tagid, tagname);
                }
            }
            }
            catch (Exception e)
            {
                logger.Debug("error[{}]",e.ToString());
            }
            xDoc.Save(XmlPath);
            return ;
        }
    }
}
