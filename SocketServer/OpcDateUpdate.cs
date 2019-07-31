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
        public bool OpcDataUpdateTag(List<DataItem> taglist)
        {
            if (taglist == null) {
                return false;
            }
            logger.Debug("OpcDateWrite");
            int c = this.OpcServerDBHandle.Update("opc_tag", taglist, "username=@username");
            logger.Debug("c[{}]",c);
            return true;
        }
    }
}
