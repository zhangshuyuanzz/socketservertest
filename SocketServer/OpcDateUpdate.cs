﻿using Base.kit;
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
            if (taglist == null) {
                return false;
            }
            logger.Debug("OpcDateWrite");
            int c;
            if (fff == true)
            {
                logger.Debug("ReplaceReplaceReplaceReplaceReplace");
                c = this.OpcServerDBHandle.Replace("opc_tag", taglist);
            }
            else
            {
                logger.Debug("UpdateUpdateUpdateUpdateUpdate");
                c = this.OpcServerDBHandle.Update("opc_tag", taglist);
            }
            logger.Debug("c[{}]",c);
            return true;
        }
    }
}