using Common.log;
using SQLite;
using System;
using System.Collections.Generic;

namespace OpcServer
{
    class OpcData
    {
        private static readonly NLOG Logger = new NLOG("OpcData");
        private SQLiteHelper _mgr;
        private string DbPath =  @".\DB\OpcServer.db";
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
            Logger.Debug("OpcDataGet");
            List<object> list = this._mgr.ExecuteRow("select * from opc_tag", null, null);
            foreach (object o in list)
            {
                Dictionary<string, object> d = (Dictionary<string, object>)o;
                foreach (KeyValuePair<string, object> s in d)
                {
                    Logger.Debug("key[{}]Value[{}]", s.Key,s.Value);
                }
            }
            return true;
        }
    }
}
