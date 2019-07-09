using Common.log;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SkKit
{
    public class DevTagIndo
    {
        public int ID;
        public float value;
        public string TagStr;
        public DevTagIndo()
        {
            this.ID = 0;
            this.value = 0.0F;
        }
    }
    public class SkServer
    {
        private string ip;
        private int port;
        private Socket _socket = null;

        public delegate void ServerDataEventHandler(List<DevTagIndo> tags);
        public ServerDataEventHandler Server_get_handle;

        private NLOG logger = new NLOG("SkServer");
        public SkServer(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }
        public SkServer(int port)
        {
            logger.Debug("SkServer[{}]", port);
            this.ip = "0.0.0.0";
            this.port = port;
        }

        public bool SkStartListen()
        {
            logger.Debug("SkStartListen");
            bool ret = false;

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            logger.Debug("111111");
            logger.Debug("IP[{}port[{}]", ip, port);
            try
            {
                IPAddress address = IPAddress.Parse(ip);
                logger.Debug("222");
                IPEndPoint endPoint = new IPEndPoint(address, port);
                _socket.Bind(endPoint);
            }
            catch (Exception e)
            {
                logger.Debug("error:[{}]",e.ToString());
            }

            logger.Debug("IP[{}port[{}]", ip, port);

            logger.Debug("开始监听");
            _socket.Listen(int.MaxValue);
            logger.Debug("监听{0}消息成功", _socket.LocalEndPoint.ToString());

            Thread thread = new Thread(ListenClientConnect);
            thread.Start();
            return ret;
        }
        private void ListenClientConnect()
        {
            while (true) {
                logger.Debug("now listen!!");
                Socket clientsocket = _socket.Accept();
                logger.Debug("there is a listen!!");
                clientsocket.Send(Encoding.UTF8.GetBytes("zheshige fuwuduan!!!!!!!!!!"));
                Thread subfunc = new Thread(ReceiveMessage);
                subfunc.Start(clientsocket);
            }
        }
        private void ReceiveMessage(object SkHadle)
        {
            try
            {
                logger.Debug("ReceiveMessage");
                Socket clientSk = (Socket)SkHadle;
                byte[] getbufer = new byte[1024 * 10];
                while (true)
                {
                    List<DevTagIndo> TagsL = new List<DevTagIndo>();
                    int lenth = clientSk.Receive(getbufer);
                    logger.Debug("Connected[{}]", clientSk.Connected);
                    byte[] GetData = new byte[lenth];

                    Array.Copy(getbufer, GetData, lenth);
                    logger.Debug("get buffer lenth [{}]client[{}]", lenth, clientSk.RemoteEndPoint.ToString());
                    logger.Debug("set msg[{}]", BitConverter.ToString(GetData));

                    SkParseFrame(TagsL, GetData);
                    if (Server_get_handle != null)
                    {
                        Server_get_handle(TagsL);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("error [{}]", e.ToString());
            }
            logger.Debug("socket conn disconned");
        }
        public bool SkParseFrame(List<DevTagIndo> TagsL, byte[] InData)
        {
            bool RetParse = true;
            if (InData[0] != 0xFE || InData[1] != 0xFE) {
                logger.Error("error get frame [{}] [{}]", InData[0], InData[1]);
                return false;
            }
            logger.Debug("DevCount [{}] ", InData[2]);
            byte DevCount = InData[2];
            if ((DevCount * 6 + 4) > InData.Length) {
                logger.Error("error src data lenth [{}] ", InData.Length);
                return false;
            }
            byte num;
            byte []OneTagdata = new byte[6];
            for (num = 0; num < DevCount; num++) {
                DevTagIndo OneIDInfo = new DevTagIndo();
                Array.Copy(InData,3+num*6, OneTagdata,0,6);
                logger.Debug("OneTagdata[{}]", BitConverter.ToString(OneTagdata));

                int TagID = (int)BitConverter.ToUInt16(OneTagdata,0);
                OneIDInfo.value = BitConverter.ToSingle(OneTagdata,2);
                OneIDInfo.ID = num + 1;
                OneIDInfo.TagStr = TagID.ToString() + ":" + OneIDInfo.value.ToString();
                logger.Debug("tag info ID[{}]value[{}]", OneIDInfo.ID, OneIDInfo.value);
                logger.Debug("TagStr[{}]", OneIDInfo.TagStr);
                TagsL.Add(OneIDInfo);
            }
            return RetParse; 
        }
    }
}
