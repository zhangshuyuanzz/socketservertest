using Base.kit;
using Common.log;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketServer
{
    class OpcDateUpdate
    {
        readonly NLOG logger = new NLOG("OpcDateUpdate");
        private SQLiteHelper OpcServerDBHandle = null;
        private string DbUpPath = @".\DB\OpcServer.db";
        public OpcDateUpdate()
        {
            this.OpcServerDBHandle = new SQLiteHelper(DbUpPath);
            if (System.IO.File.Exists(DbUpPath))
            {
                logger.Debug("file exist!!");
            }
            else
            {
                logger.Debug("file is not exist!!");
            }
            if (this.OpcServerDBHandle.Open() == false) {
                OpcServerDBHandle = null;
                logger.Debug("open error !!!");
                return;
            }
        }
        public bool OpcDataWriteTag(List<DataItem> taglist,bool fff)
        {
            lock (this)
            {
                if (taglist == null)
                {
                    return false;
                }
                logger.Debug("Local Dbsqlite DateWrite");
                int c;
                if (fff == true)
                {
                    logger.Debug("DB  ReplaceReplaceReplaceReplaceReplace");
                    c = this.OpcServerDBHandle.Replace("opc_tag", taglist);
                }
                else
                {
                    logger.Debug("DB  UpdateUpdateUpdateUpdateUpdate");
                    c = this.OpcServerDBHandle.Update("opc_tag", taglist);
                }
                logger.Debug("c[{}]", c);
            }
            return true;
        }
        public bool OpcDataDelTagWithIp(string delip)
        {
            this.OpcServerDBHandle.DeleteAll("opc_tag",delip);
            return true;
        }
    }
}
