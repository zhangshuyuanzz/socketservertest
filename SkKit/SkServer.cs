using Common.log;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SkKit
{
    public class SkServer
    {
        private string ip;
        private int port;
        private Socket _socket = null;

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
                //int lenth = 
            }
        }
        private void ReceiveMessage(object SkHadle)
        {
            logger.Debug("ReceiveMessage");
            Socket clientSk = (Socket)SkHadle;
            byte[] getbufer = new byte[1024 * 10];
            while (true) {
                int lenth = clientSk.Receive(getbufer);
                logger.Debug("get buffer lenth [{}]client[{}]",lenth, clientSk.RemoteEndPoint.ToString());
                logger.Debug("msg[{}]", Encoding.UTF8.GetString(getbufer, 0, lenth));
            }
        }
    }
}
