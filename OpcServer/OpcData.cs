using Base.kit;
using Common.log;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading;

namespace OpcServer
{
    class OpcData
    {
        private static readonly NLOG Logger = new NLOG("OpcData");
        private SQLiteHelper _mgr;
        private string DbPath =  @".\DB\OpcServer.db";

        public delegate void OpcServerEventHandler(List<DataItem> taglist);
        public OpcServerEventHandler Opc_tag_data_handle;
        static Timer timer;
        public OpcData()
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
        public bool OpcDataGet()
        {
            List<object> list = this._mgr.ExecuteRow("select * from opc_tag", null, null);
            Logger.Debug("OpcDataGet--Count[{}]", list.Count);

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
                Logger.Debug("-----------------------------------------------eeeeeeeeeee---------------------------------------");
            }
            Opc_tag_data_handle?.Invoke(alltag);
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
        public bool OpcDateRegisterCB(int interval)
        {
            Logger.Info("startTimer---DataUpdateRate[{}]", interval);
            timer = new Timer(new TimerCallback(readItemSync), this, 1000, interval * 1000);
            return true;
        }
        public void readItemSync(object state)
        {
            Logger.Info("startTimer--get db tag data!!");
            OpcDataGet();
        }
    }
}
