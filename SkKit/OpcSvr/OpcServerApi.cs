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
        private readonly string XmlPath = "config\\ServerConfig.xml";
        public string VendorInfo = "ql.co.opc-server.v1.0.0";
        public string Descr = " it in runing is ql opc server app";
        private string GUID = "{534793BC-B818-42B4-9672-42A9F80FE708}";
        public delegate void TextBoxEventHandler(int id, string str);
        public TextBoxEventHandler RWTextBoxFunc;
        public string AppName { get; set; }
        public uint ServerRate { get; set; }

        private Map<string, uint> TagList;
        private Dictionary<int, uint> TagIDList;


        void printfbyte(byte[] data)
        {
            ParseFrameDate(data);
        }
        public OpcServerLib(string auth)
        {
            this.TagList = new Map<string, uint>();
            this.TagIDList = new Dictionary<int, uint>();

            bool flag = WtOPCsvr_DLL.Deactivate30MinTimer(auth);
            logger.Debug("OpcServerLib is demo {}", flag);

            logger.Info("**********************************parse xml***************************sss*******************");
            string path1 = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string xmlpath = path1 + XmlPath;
            logger.Info("-----Start-xmlpath[{}]-", xmlpath);
            ServerConfig = new BaseOpcServerConfig();
            this.AppName = ServerConfig.server_name;
            this.ServerRate = (uint)ServerConfig.serverrate;
            logger.Info("AppName[{}]ServerRate[{}]", this.AppName, this.ServerRate);
            logger.Info("**********************************parse xml***********************eee***********************");

        }
        public bool ServerRegister(string exepath)
        {
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
            WtOPCsvr_DLL.SetVendorInfo("QILang.CO");
            bool ret = WtOPCsvr_DLL.InitWTOPCsvr(GUID, 1000 * ServerRate);
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
                        TagList.Add(s.TagName, TagHandle);
                        TagIDList.Add(s.TagId, TagHandle);

                    }
                }
            }
            logger.Info("NumbrClientConnections[{}]", WtOPCsvr_DLL.NumbrClientConnections());

        }
        public void UpdateTagItem(string host, DataItem[] tags)    //提出来给数据提供这使用，用来更新数据
        {
            if (tags.Length == 0)
            {
                return;
            }
            WtOPCsvr_DLL.StartUpdateTags();

            for (int i = 0; i < tags.Length; i++)
            {
                DataItem tag = tags[i];
                /*
                if (!TagList.ContainsKey(tag.TagName))
                {
                    uint TagHandle = WtOPCsvr_DLL.CreateTag(tag.TagName, tag.Value, tag.Quality, false);
                    logger.Debug("opc add itme name{} handle{}", tag.TagName, TagHandle);
                    if (TagHandle > 0)
                    {
                        TagList.Add(tag.TagName, TagHandle);
                    }
                    else
                    {
                        continue;
                    }
                }
                */
                logger.Error("opc update data TagHandle{} value{} quality{}", tag.TagHandle, tag.Value, tag.Quality);
                WtOPCsvr_DLL.UpdateTagToList(tag.TagHandle, tag.Value, tag.Quality);
            }
            WtOPCsvr_DLL.EndUpdateTags();

            tags = null;
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
                UpdateTextBoxUI(Tagid, Value);
            }
            UpdateTagItem("opcserver", InputItemDatas);
        }
        public void testgettagvalue()
        {
            foreach (KeyValuePair<string, uint> s in TagList)
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
            UpdateTagItem("opcserver", InputItemDatas);
        }

        public int i = 1;
        public void testUpdateTextBox()
        {
            if (RWTextBoxFunc == null)
                return;

            RWTextBoxFunc(i, "zhang");
            i++;

        }
        public void UpdateTextBoxUI(int boxid,float value)
        {
            if (RWTextBoxFunc == null)
                return;

            RWTextBoxFunc(boxid, value.ToString());
            i++;
        }

    }
}
