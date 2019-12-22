using Common.log;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Timers;

namespace SkKit
{
    public class DevTagIndo
    {
        public int ID;
        public object value;   //no use
        public string TagStr;
        public byte[] TagMetadata ;
        public DevTagIndo()
        {
            this.ID = 0;
            this.value = 0.0F;
            this.TagStr = "unkown!!";
            TagMetadata = new byte[4];
        }
        public DevTagIndo(int id,float val)
        {
            this.ID = id;
            this.value = val;
            this.TagStr = "unkown!!";
            TagMetadata = new byte[4];
        }
    }
    public class SkServer
    {
        private string ip;
        private int port;
        private Socket _socket = null;
        private System.Timers.Timer SkServerDisCTimer = new System.Timers.Timer(2*1000);

        private Dictionary<string, Socket> DevSkList = new Dictionary<string, Socket>();
        public delegate void ServerDataEventHandler(List<DevTagIndo> tags, string ip);
        public ServerDataEventHandler Server_get_handle;

        public delegate void ServerConnEventHandler(object handle, string ip);
        public ServerConnEventHandler Server_conn_handle;

        public delegate void ServerDisconnEventHandler(object handle, string ip);
        public ServerDisconnEventHandler Server_disconn_handle;

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
            _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

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
                logger.Debug("error:[{}]", e.ToString());
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
                //                clientsocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 500);

                string ip = (clientsocket.RemoteEndPoint as IPEndPoint).Address.ToString();
                logger.Debug("conn ip [{}]", ip);
                if (DevSkList.ContainsKey(ip))
                {
                    DevSkList[ip] = clientsocket;
                }
                else
                {
                    DevSkList.Add(ip, clientsocket);
                }
                logger.Debug("there is a client!!");
                Server_conn_handle?.Invoke(this, ip);
                Thread subfunc = new Thread(ReceiveMessage);
                subfunc.Start(clientsocket);
            }
        }
        private void ReceiveMessage(object SkHadle)
        {
            logger.Debug("Receive socket Message");
            Socket clientSk = (Socket)SkHadle;
            string reip = (clientSk.RemoteEndPoint as IPEndPoint).Address.ToString();
            logger.Debug("ip[{}]", reip);

            try
            {
                byte[] getbufer = new byte[1024 * 10];
                while (true)
                {
                    List<DevTagIndo> TagsL = new List<DevTagIndo>();
                    if (clientSk == null) {
                        logger.Debug("clientSk == null--reip[{}]", reip);
                        return;
                    }

                    int lenth = clientSk.Receive(getbufer);
                    if (lenth == 0) {
                        logger.Debug("receive data is zero");
                        if (SocketJudgeIsConn(ref clientSk, reip) == false)
                        {
                            break;
                        }
                        else {
                            continue;
                        }
                    }
                    logger.Debug("ReceiveMessage  get data lenth[{}]", lenth);
                    byte[] GetData = new byte[lenth];

                    Array.Copy(getbufer, GetData, lenth);
                    logger.Debug("get buffer client[{}]", clientSk.RemoteEndPoint.ToString());
                    logger.Debug("get socket msg[{}]", BitConverter.ToString(GetData));

                    if (GetData[2] == 0xff && GetData[3] == 0xff && lenth == 4)
                    {
                        logger.Error("get reboot back data---reip[{}]", reip);
                        SocketJudgeIsConn(ref clientSk, reip,false);
                        return;
                    }
                    else {
                        if (SkParseFrame(TagsL, GetData) == true)            // FE FE 01 01 00 00 00 00 00 FE
                        {
                            Server_get_handle?.Invoke(TagsL, reip);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SocketJudgeIsConn(ref clientSk, reip);
                logger.Error("error [{}]", e.ToString());
            }
            logger.Debug("ReceiveMessage--end");
        }
        public bool SkParseFrame(List<DevTagIndo> TagsL, byte[] InData)
        {
            bool RetParse = true;
            if (InData.Length == 0) {
                logger.Error("InData.Length == 0");
                return false;
            }
            if (InData[0] != 0xFE || InData[1] != 0xFE) {
                logger.Error("error get frame [{}] [{}]", InData[0], InData[1]);
                return false;
            }
            logger.Debug("get sk msg！ Dev tag Count [{}] ", InData[2]);
            byte DevCount = InData[2];
            if ((DevCount * 6 + 4) > InData.Length) {
                logger.Error("error src data lenth [{}] ", InData.Length);
                return false;
            }
            byte num;
            byte[] OneTagdata = new byte[6];
            for (num = 0; num < DevCount; num++) {
                DevTagIndo OneIDInfo = new DevTagIndo();
                Array.Copy(InData, 3 + num * 6, OneTagdata, 0, 6);
                logger.Debug("-----------sk msg !!OneTagdata[{}]", BitConverter.ToString(OneTagdata));

                int TagID = (int)BitConverter.ToUInt16(OneTagdata, 0);

                Array.Copy(OneTagdata, 2, OneIDInfo.TagMetadata, 0, 4);
                // OneIDInfo.value = BitConverter.ToSingle(OneTagdata,2);
                OneIDInfo.ID = TagID;
                logger.Debug("parse !!!---tag info ID[{}]-", OneIDInfo.ID);
                TagsL.Add(OneIDInfo);
            }
            return RetParse;
        }
        public bool SocketJudgeIsConn(ref Socket ClientFd, string reip,bool inret = true)
        {
            logger.Debug("SocketJudgeIsConn");
            if (ClientFd == null) {
                return true;
            }
            bool ret = inret;
            byte[] buffer = new byte[100];
            try
            {
                lock (this)
                {
                    //string teststr = "test-alive";
                    //byte[] SendData = System.Text.Encoding.ASCII.GetBytes(teststr);
                    //ClientFd.Send(SendData);
                    if (ClientFd.Poll(0, SelectMode.SelectRead))
                    {
                        int nRead = ClientFd.Receive(buffer);
                        if (nRead == 0)
                        {
                            logger.Debug("socket连接已断开");
                            ret = false;
                        }
                    }
                }
            }
            catch (SocketException ex)
            {
                logger.Debug("error[{}]", ex.ToString());
                logger.Debug("socket连接已断开");
                ret = false;
            }
            if (ret == false) {
                Server_disconn_handle?.Invoke(this, reip);
                if (DevSkList.ContainsKey(reip) == true)
                {
                    DevSkList.Remove(reip);
                }
                SocketClose(ref ClientFd);
            }
            return ret;
        }

        public bool SocketClose(ref Socket ClientFd)
        {
            bool CloseRet = true;
            if (ClientFd != null) {
                ClientFd.Shutdown(SocketShutdown.Both);
                ClientFd.Close();
                ClientFd = null;
            }
            return CloseRet;
        }
        public void CoSendFile(string ip, string path)
        {
            try
            {
                logger.Debug("CoSendFile--ip[{}]path[{}]", ip, path);
                if (DevSkList.ContainsKey(ip))
                {
                    DevSkList[ip].SendFile(path);
                }
                else
                {
                    logger.Debug("no this device");
                }
                logger.Debug("CoSendFile end");
            }
            catch (Exception ex)
            {
                logger.Debug("send file error:[{}]",ex.ToString());
            }
        }
        public void CoSendString(string ip, string data)
        {
            logger.Debug("CoSendString--ip[{}]data[{}]", ip, string.Join(",", data));
            byte[] SendData = System.Text.Encoding.ASCII.GetBytes(data);
            logger.Debug("SendData----SendData[{}]", BitConverter.ToString(SendData));

            try
            {
                if (DevSkList.ContainsKey(ip))
                {
                    DevSkList[ip].Send(SendData);
                }
                else
                {
                    logger.Debug("no this device");
                }
                logger.Debug("CoSendString end");
            }
            catch (Exception ex)
            {
                logger.Debug("send string error:[{}]",ex.ToString());
            }
        }
        public void CoSendByte(string ip, byte[] medata)
        {
            logger.Debug("CoSendByte--ip[{}]data[{}]", ip, string.Join(",", medata));
            try
            {
                if (DevSkList.ContainsKey(ip))
                {
                    DevSkList[ip].Send(medata);
                }
                else
                {
                    logger.Debug("no this device");
                }
                logger.Debug("CoSendByte end");
            }
            catch (Exception ex)
            {
                logger.Debug("send string error:[{}]", ex.ToString());
            }
        }
        private void init_timer()
        {
            SkServerDisCTimer.AutoReset = true;
            SkServerDisCTimer.Enabled = true;
            SkServerDisCTimer.Elapsed += new ElapsedEventHandler(SkServerTimerEvent);
        }
        public  void SkServerTimerEvent(object sender, ElapsedEventArgs e)
        {
            logger.Debug("SkServerTimerEvent!!");
            foreach (KeyValuePair<string, Socket> s in DevSkList) {
                logger.Debug("key[{}]value[{}]",s.Key,s.Value);
                //SocketJudgeIsConn(ref s.Value, s.Key);
            }
        }
    }
}
