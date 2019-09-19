using Common.log;
using SkKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpcClientForMetering
{
    class OpcSetSocketSver
    {
        readonly NLOG logger = new NLOG("OpcSetSocketSver");
        public SkServer OpcSetServerHandle;
        private Dictionary<string, DevInfo> DevConnList = new Dictionary<string, DevInfo>();
        public OpcSetSocketSver(int port)
        {
            logger.Debug("port[{}]", port);
            OpcSetServerHandle = new SkServer(port);
            OpcSetServerHandle.SkStartListen();
            OpcSetServerHandle.Server_get_handle += new SkServer.ServerDataEventHandler(OpcSetGetData);
            OpcSetServerHandle.Server_conn_handle += new SkServer.ServerConnEventHandler(ConnedNotification);
            OpcSetServerHandle.Server_disconn_handle += new SkServer.ServerDisconnEventHandler(DisconnedNotification);
        }
        public void OpcSetGetData(List<DevTagIndo> tagslist, string UDip)
        {
            logger.Debug("updateDevInfo---ip[{}]", UDip);
            try
            {
            }
            catch (Exception ex)
            {
                logger.Error("error[{}]", ex.ToString());
            }
            logger.Debug("end !!OpcSetGetData  UDip[{ }]", UDip);

        }
        public void ConnedNotification(object handle, string ip)
        {
            logger.Debug("ConnedNotification--ip[{}]", ip);
            DevInfo onedev;

            if (DevConnList.ContainsKey(ip) == false)
            {
                onedev = new DevInfo(handle);
                DevConnList.Add(ip, onedev);
            }
            else
            {
                logger.Debug("has this ip");
                onedev = DevConnList[ip];
            }
            logger.Debug("ConnedNotification end");
        }
        public void DisconnedNotification(object handle, string ip)
        {
            logger.Debug("DisconnedNotification--ip[{}]", ip);

            if (DevConnList.ContainsKey(ip) == true)
            {
                DevInfo buffer = DevConnList[ip];
                buffer.TagList.Clear();
                DevConnList.Remove(ip);
                foreach (string s in DevConnList.Keys)
                {
                    logger.Debug("sssss[{}]", s);
                }
            }
        }
    }
}
