using Base.kit;
using Common.log;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpcClientForMetering
{
    class OpcSetSqlite
    {
        readonly NLOG logger = new NLOG("OpcSetSqlite");
        private SQLiteHelper OpcSetDBHandle = null;
        private string OpcSetDBPath = @".\DB\OpcSetClient.db";
        public OpcSetSqlite()
        {
            if (System.IO.File.Exists(OpcSetDBPath))
            {
                logger.Debug("file exist!!");
            }
            else
            {
                logger.Debug("file is not exist!!");
                return;
            }
            this.OpcSetDBHandle = new SQLiteHelper(OpcSetDBPath);
            if (this.OpcSetDBHandle.Open() == false)
            {
                logger.Debug("open error !!!");
                return;
            }
        }
        public bool OpcSetWriteTag(List<DataItem> taglist, bool fff)
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
                    c = this.OpcSetDBHandle.Replace("opc_tag", taglist);
                }
                else
                {
                    logger.Debug("DB  UpdateUpdateUpdateUpdateUpdate");
                    c = this.OpcSetDBHandle.Update("opc_tag", taglist);
                }
                logger.Debug("c[{}]", c);
            }
            return true;
        }
    }
}
