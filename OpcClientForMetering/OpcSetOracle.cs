using Common.log;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;
using Base.kit;

namespace OpcClientForMetering
{
    class OpcSetOracle
    {
        readonly NLOG logger = new NLOG("OpcSetOracle");
        OracleConnection oledbConnection;
        string OracleConnStr = "Data Source=(DESCRIPTION =" +
                                            "(ADDRESS_LIST=" +
                                            "(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT = 1521)))" +
                                            " (CONNECT_DATA =(SERVICE_NAME={1}))" +
                                            ");User Id = {2}; Password={3};";
        string OpcSetTableName;
        public OpcSetOracle(string ip,string srcname, string UsrId, string UsrPw,string tablename)
        {
            OpcSetTableName = tablename;    
            logger.Debug("OpcSetOracle--ip[{}]--[{}][{}][{}]---", ip, srcname, UsrId, UsrPw);
            string dash = string.Format(OracleConnStr, "192.168.0.240", "orcl", "ZHANGYONGQIANG", "ZHANGYONGQIANG");
            logger.Debug("dash[{}]", dash);

            oledbConnection = new OracleConnection(dash);
            oledbConnection.Open();
            logger.Debug("oracle db conn---[{}]",oledbConnection.State.ToString());
        }
        void isconned()
        {
            if (oledbConnection.State == System.Data.ConnectionState.Closed) {
                oledbConnection.Open();
            }
        }
        public int OpcSetOracleInsertData(DataItem indata)
        {
            string OpcSetSQLString = "inset into " + OpcSetTableName + "(NAME,VALUE,TIME) values ('{0}','{1}','{2}')" ;
            OpcSetSQLString = string.Format(OpcSetSQLString, indata.TagName, indata.Value, indata.DataTime);
            logger.Debug("OpcSetSQLString[{}]", OpcSetSQLString);
            using (OracleCommand cmd = new OracleCommand(OpcSetSQLString, this.oledbConnection))
            {
                int rows = 0;
                try
                {
                    isconned();
                    rows = cmd.ExecuteNonQuery();
                    oledbConnection.Close();
                }
                catch (OracleException E)
                {
                    logger.Debug("error[{}]", E.ToString());
                }
                return rows;
            }
        }
    }
}
