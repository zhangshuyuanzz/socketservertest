using System;
using Common.log;
using System.Collections.Generic;
using System.Threading;
using Base.kit;

namespace OpcSvr
{
    public class OpcServerLib
    {
        private static readonly NLOG logger = new NLOG("OpcServerLib");
        BaseOpcServerConfig ServerConfig;

        private readonly string XmlPath = @"config\opcserver.xml";
        public string VendorInfo = "ql.co.opc-server.v1.0.0";
        public string Descr = "provide opc service ,please not forbidden it!!";
        private string GUID = "{96B01D0F-D4E0-45A9-BFBD-B2E0A052964F}";

        public string AppName { get; set; }
        public int ServerRate { get; set; }
        public string Auth { get; set; }

        private Dictionary<int, uint> TagIDList = new Dictionary<int, uint>();

        void printfbyte(byte[] data)
        {
            ParseFrameDate(data);
        }
        public OpcServerLib(string auth)
        {
            Auth = auth;
            logger.Info("**********************************parse xml***************************sss*******************");
            string path1 = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string xmlpath = path1 + XmlPath;
            logger.Info("-----Start-xmlpath[{}]-", xmlpath);
            ServerConfig = new BaseOpcServerConfig(xmlpath);
            this.AppName = ServerConfig.server_name;
            this.ServerRate = ServerConfig.serverrate;
            logger.Info("AppName[{}]ServerRate[{}]", this.AppName, this.ServerRate);
            logger.Info("**********************************parse xml***********************eee***********************");
        }
        public bool ServerRegister(string exepath)
        {
            bool flag = WtOPCsvr_DLL.Deactivate30MinTimer(Auth);
            logger.Debug("OpcServerLib is demo {}", flag);

            logger.Info("VendorInfo[{}]ServerRate[{}]GUID[{}]AppName[{}]", VendorInfo, ServerRate, GUID, AppName);

            bool ret = WtOPCsvr_DLL.UpdateRegistry(GUID,
                                        AppName,
                                        Descr,
                                        exepath);
            logger.Info("exepath[{}]ret[{}]", exepath, ret);
            if (!ret)
            {
                logger.Debug("UpdateRegistry is false!!!");
            }
            return ret;
        }
        public bool ServerInit()
        {
            WtOPCsvr_DLL.SetVendorInfo("DL,QILang,CO");
            bool ret = WtOPCsvr_DLL.InitWTOPCsvr(GUID, 1000 * (uint)ServerRate);
            if (!ret)
            {
                logger.Debug("InitWTOPCsvr is false!!!");
            }
            return ret;
        }

        private void UnknownItemNotification1(string p, string i)
        {
            try
            {
                logger.Debug("UnknownItemNotification1 {}/{}", p, i);
                //  uint TagHandle = WtOPCsvr_DLL.CreateTag(i, null, 0, false);
                //   TagList.Add(i, TagHandle);
            }
            catch (Exception e)
            {
                logger.Error("添加位号", e);
            }
        }
        private void RemoveTagItem(UInt32 hItem, String PathName, String ItemName)
        {
            try
            {
                logger.Debug("RemoveTagItem {}", ItemName);
                //  uint TagHandle = TagList.Remove0(ItemName);
                // WtOPCsvr_DLL.RemoveTag(TagHandle);
            }
            catch (Exception e)
            {
                logger.Error("删除位号", e);
            }
        }

        public void UnregisterQLServer()
        {
            logger.Info("stop UnregisterQLServer--GUID[{}]---AppName[{}]", this.GUID, this.AppName);
            WtOPCsvr_DLL.RequestDisconnect();
            WtOPCsvr_DLL.UnregisterServer(this.GUID,
                    this.AppName);
        }
        public void CreateTag()
        {
            logger.Info("CreateTagr--GUID[{}]---AppName[{}]", this.GUID, this.AppName);

            foreach (OpcServerGroupInfo group in ServerConfig.OpcServerCFGs.Values)
            {
                foreach (DataItem s in group.tags.Values)
                {
                    uint TagHandle = WtOPCsvr_DLL.CreateTag(s.TagName, s.Value, s.Quality, false);
                    logger.Debug("opc add itme name{} TagId[{}] handle{}", s.TagName, s.TagId, TagHandle);
                    if (TagHandle > 0)
                    {
                        TagIDList.Add(s.TagId, TagHandle);
                    }
                }
            }
            logger.Info("NumbrClientConnections[{}]", WtOPCsvr_DLL.NumbrClientConnections());

        }
        public void UpdateTagItem(string host, List<DataItem> tags)    //提出来给数据提供这使用，用来更新数据
        {
            if (tags.Count == 0)
            {
                return;
            }
            WtOPCsvr_DLL.StartUpdateTags();

            foreach(DataItem tag in tags)
            {
                if (!TagIDList.ContainsKey(tag.TagId))
                {
                    uint TagHandle = WtOPCsvr_DLL.CreateTag(tag.TagName, tag.Value, tag.Quality, false);
                    logger.Debug("add new tag");
                    if (TagHandle > 0)
                    {
                        TagIDList.Add(tag.TagId, TagHandle);
                    }
                }
                else
                {
                    WtOPCsvr_DLL.UpdateTagToList(TagIDList[tag.TagId], tag.Value, tag.Quality);
                }
                logger.Error("opc update data TagId-[{}] value{} quality{}", tag.TagId, tag.Value, tag.Quality);
            }
            WtOPCsvr_DLL.EndUpdateTags();
        }
        public void ParseFrameDate(byte[] datas)
        {
            if (datas.Length <= 4)
            {
                logger.Info("11 this frame is error");
                return;
            }

            byte[] FrameBuffer = new byte[datas.Length];
            datas.CopyTo(FrameBuffer, 0);
            logger.Info("------------------ParseFrameDate---------------Bufferlenth[{}]---------", FrameBuffer[2]);
            if (FrameBuffer[2] + 4 > datas.Length)
            {
                logger.Info("22 this frame is error");
                return;
            }
            foreach (byte s in FrameBuffer)
            {
                logger.Info("--------------------[{:X2}]", s);
            }
            if (FrameBuffer[0] != 0xfe || FrameBuffer[1] != 0xfe)
            {
                logger.Info("33 this frame is error");
                return;
            }
            byte Bufferlenth = FrameBuffer[2];
            byte[] AgentBuffer = new byte[8];
            byte shaobing = 3;
            float Value = 0;
            int Tagid = 0;
            DataItem[] InputItemDatas = new DataItem[FrameBuffer[2] / 8];
            while ((shaobing + 1) < Bufferlenth + 3)    // becus: last parity byte
            {
                Buffer.BlockCopy(FrameBuffer, shaobing, AgentBuffer, 0, 8);
                Value = BitConverter.ToSingle(AgentBuffer, 0);
                Tagid = BitConverter.ToInt32(AgentBuffer, 4);
                logger.Info("-----------------index[{}]----------------------", (shaobing - 3) / 8);
                logger.Info("----get----ff--------Value[{}]", Value);
                logger.Info("----get------------Tagid[{}]", Tagid);

                InputItemDatas[(shaobing - 3) / 8] = new DataItem();
                InputItemDatas[(shaobing - 3) / 8].TagHandle = TagIDList[Tagid];
                InputItemDatas[(shaobing - 3) / 8].Value = Value;
                InputItemDatas[(shaobing - 3) / 8].Quality = 192;

                shaobing += 8;
            }
           //pdateTagItem("opcserver", InputItemDatas);
        }
        public void testgettagvalue()
        {
            foreach (KeyValuePair<int, uint> s in TagIDList)
            {
                logger.Info("-testgettagvalue--key[{}]---Value[{}]------", s.Key, s.Value);
                Object vv = new Object(); ;
                WtOPCsvr_DLL.ReadTag(s.Value, ref vv);
                logger.Info("ReadTagReadTag---[{}]", vv);
            }
        }
        public void testUpdateTag(List<float> data)
        {
            DataItem[] InputItemDatas = new DataItem[data.Count];
            List<int> test = new List<int>(TagIDList.Keys);
            for (int i = 0; i < 4; i++)
            {
                InputItemDatas[i] = new DataItem();
                InputItemDatas[i].Quality = 192;
                InputItemDatas[i].Value = data[i];
                InputItemDatas[i].TagHandle = TagIDList[test[i]];
            }
            //UpdateTagItem("opcserver", InputItemDatas);
        }
        public void OpcServerUpdateTag(List<DataItem> taglist)
        {
            logger.Debug("OpcServerUpdateTag");
            UpdateTagItem("opcserver", taglist);
        }
    }
}
