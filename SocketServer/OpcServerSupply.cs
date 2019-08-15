using Base.kit;
using Common.log;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SocketServer
{
    class OpcDataSu
    {
        private static readonly NLOG Logger = new NLOG("OpcData");
        private SQLiteHelper _mgr;
        private string DbPath =  @".\DB\OpcServer.db";

        public delegate void OpcServerEventHandler(List<DataItem> taglist);
        public OpcServerEventHandler Opc_tag_data_handle;
        static Timer OpcServerTimer;
        public OpcDataSu()
        {
            this._mgr = new SQLiteHelper(DbPath);
            Logger.Debug("DbPath[{}]", DbPath);
            if (System.IO.File.Exists(DbPath))
            {
                Logger.Debug("file exist!!");
            }
            else {
                Logger.Debug("file is not exist!!");
                return;
            }
            this._mgr.Open();
            Logger.Debug("Open end!!");
        }
        public void TestTableExists()
        {
            Logger.Debug("表test是否存在: " + this._mgr.TableExists("test"));
        }

        public bool OpcDataGet(List<string> ips)
        {
            string insql = "select * from opc_tag where devip in ( {0} )";
            List<object> list = this._mgr.ExecuteRow(insql, ips);
            Logger.Debug("OpcServer Get--Count[{}]", list.Count);

            List<DataItem> alltag = new List<DataItem>() ;
            foreach (object o in list)
            {
                Dictionary<string, object> d = (Dictionary<string, object>)o;
                Logger.Debug("-----------------------------------------------sssssssssssss---------------------------------------");
                DataItem onetag = new DataItem();
                foreach (KeyValuePair<string, object> s in d)
                {
                    Logger.Debug("key[{}]Value[{}]", s.Key,s.Value);
                    switch (s.Key)
                    {
                        case "tagid":
                            onetag.TagId = Convert.ToInt32(s.Value);
                            break;
                        case "name":
                            onetag.TagName = Convert.ToString(s.Value);
                            break;
                        case "value":
                            onetag.Value = s.Value;
                            break;
                        case "time":
                            onetag.DataTime = Convert.ToString(s.Value);
                            break;
                        case "type":
                            onetag.DataType = Convert.ToByte(s.Value);
                            break;
                        default:
                            break;
                    }
                }
                alltag.Add(onetag);
            }
            Opc_tag_data_handle?.Invoke(alltag);
            Logger.Debug("-----------------------------------------------eeeeeeeeeee---------------------------------------");
            return true;
        }
        public List<string> OpcDateIPList()
        {
            List<string> iplist = new List<string>();
            List<object> Reslist = this._mgr.ExecuteRow("select ip from all_slaves_dev", null, null);
            foreach (object o in Reslist)
            {
                Dictionary<string, object> d = (Dictionary<string, object>)o;
                Logger.Debug("this row");
                foreach (KeyValuePair<string, object> s in d)
                {
                    Logger.Debug("this Column");
                    Logger.Debug("key[{}]Value[{}]", s.Key, s.Value);
                    iplist.Add(s.Value.ToString());
                }
            }

            return iplist;
        }

        private List<string> OpcIPs;
        private int UpdateInterval;
        public bool OpcDateRegisterCB(int interval)
        {
            UpdateInterval = interval;
            Logger.Info("startTimer---DataUpdateRate[{}]", interval);
            OpcServerTimer = new Timer(new TimerCallback(readItemSync), this, System.Threading.Timeout.Infinite, interval * 1000);


            List<string> OpcIPs = new List<string>();
            OpcIPs.Add("192.168.0.88");

            OpcDataGet(OpcIPs);

            return true;
        }
        public void readItemSync(object state)
        {
            Logger.Info("readItemSync--get db tag data!!for opc server!!");
            foreach (string s in OpcIPs)
            {
                Logger.Debug("OpcServerStartUpdate--[{}]", s);
            }
            OpcDataGet(OpcIPs);
        }
        public void OpcServerStartUpdate(List<string> IPlists)
        {
            OpcIPs = IPlists;
            OpcServerTimer.Change(1000, UpdateInterval * 1000);
        }
    }
}
