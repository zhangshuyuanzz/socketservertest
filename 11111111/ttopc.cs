using Common.log1;
using Opc;
using Opc.Da;
using System;
using System.Collections.Generic;
using System.Threading;

namespace OpcDa1tt
{
    public class OpcDa1
    {
        private static readonly NLOG1 Logger = new NLOG1("OpcDa1");

        private Opc.IDiscovery m_discovery = new OpcCom.ServerEnumerator();
        private Opc.Da.Server Server;
        private Opc.Da.Server ConnServer;

        private Opc.Da.Subscription subscription = null;
        private System.Threading.Timer ReadTimer;
        private int DataUpdateRate = 30000;
        public void SetOpcDaServer(Opc.Da.Server server)
        {
            this.Server = server;
            this.ConnServer = server;

        }

        public OpcDa1()
        {
        }

        public bool JudgeOpcServerConnectability(string ip)
        {
            Logger.Info("-- JudgeOpcServerConnectability-ip[{}]", ip);
            bool RetJudge = false;
            try
            {
                Opc.Server[] servers = m_discovery.GetAvailableServers(Specification.COM_DA_20, ip, null);
                Logger.Info("-- ----");

                if (servers != null)
                {
                    foreach (Opc.Da.Server server in servers)
                    {
                        Logger.Info("server-name--[{}]--Url+[{}]--Locale[{}]--IsConnected[{}]", server.Name, server.Url, server.Locale, server.IsConnected);

                    }
                }
                else
                {
                    Logger.Info("no servers--");
                }
            }
            catch (Exception ex)
            {
                RetJudge = false;
                Logger.Fatal("-- GetAvailableServers--error[{}]", ex.ToString());
            }

            return RetJudge;
        }

        public void startTimer()
        {
            Logger.Info("startTimer---DataUpdateRate[{}]", DataUpdateRate);

            ReadTimer = new System.Threading.Timer(new TimerCallback(readItemSync), this, 1000, DataUpdateRate);
        }
        public void readItemSync(object state)
        {
            Logger.Fatal("___readItemSync---connstats[{}]--[{}]", this.Server.IsConnected, subscription);
            try
            {
                Logger.Fatal("___readItemSync---ServerState[{}]-StatusInfo-[{}]", this.Server.GetStatus().ServerState, this.Server.GetStatus().StatusInfo);
            }
            catch (Exception ex)
            {
                Logger.Fatal("_.GetStatus().ServerState --error--[{}]", ex.ToString());
                return;
            }
            try
            {
                ItemValueResult[] Values = subscription.Read(subscription.Items);
            }
            catch (Exception ex)
            {
                Logger.Fatal("_readItemSync --error--[{}]", ex.ToString());
                ReadTimer.Dispose();

                this.Server.Disconnect();
                Thread.Sleep(5000);
                if (this.Server != null)
                {
                    Logger.Info("disconnect--------zz-----------now reconn");
                    this.Server = this.ConnServer;
                    this.Server.Connect();
                }
            }

        }

    }

    public class ResultValue
    {
        public int ResultID { get; set; }
        public float Value { get; set; }
        public int Quality { get; set; }
        public string Timestamp { get; set; }
        public long TimesInt { get; set; }

        public string ItemName { get; set; }
    }
}
