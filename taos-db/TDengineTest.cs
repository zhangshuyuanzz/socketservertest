/*
 * Copyright (c) 2019 TAOS Data, Inc. <jhtao@taosdata.com>
 *
 * This program is free software: you can use, redistribute, and/or modify
 * it under the terms of the GNU Affero General Public License, version 3
 * or later ("AGPL"), as published by the Free Software Foundation.
 *
 * This program is distributed in the hope that it will be useful, but WITHOUT
 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE.
 *
 * You should have received a copy of the GNU Affero General Public License
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Collections;
using Common.log;

namespace TDengineDriver
{
    class TDengineTest
    {
        private NLOG logger = new NLOG("TDengineTest");

        //connect parameters
        private string host;
        private string configDir;
        private string user;
        private string password;
        private int port = 6020;
        private int keeplive = 100;

        //sql parameters
        private string dbName;
        private string stableName;
        private string tablePrefix;

        private bool isInsertData;
        private bool isQueryData;

        private long tableCount;
        private long totalRows;
        private long batchRows;
        private long beginTimestamp = 1551369600000L;

        private long conn = 0;
        private long rowsInserted = 0;
        public long GetArgumentAsLong(String[] argv, String argName, int minVal, int maxVal, int defaultValue)
        {
            int argc = argv.Length;
            for (int i = 0; i < argc; ++i)
            {
                if (argName != argv[i])
                {
                    continue;
                }
                if (i < argc - 1)
                {
                    String tmp = argv[i + 1];
                    if (tmp[0] == '-')
                    {
                        logger.Debug("option {0:G} requires an argument", tmp);
                        ExitProgram();
                    }

                    long tmpVal = Convert.ToInt64(tmp);
                    if (tmpVal < minVal || tmpVal > maxVal)
                    {
                        logger.Debug("option {0:G} should in range [{1:G}, {2:G}]", argName, minVal, maxVal);
                        ExitProgram();
                    }

                    return tmpVal;
                }
            }

            return defaultValue;
        }

        public String GetArgumentAsString(String[] argv, String argName, String defaultValue)
        {
            int argc = argv.Length;
            for (int i = 0; i < argc; ++i)
            {
                if (argName != argv[i])
                {
                    continue;
                }
                if (i < argc - 1)
                {
                    String tmp = argv[i + 1];
                    if (tmp[0] == '-')
                    {
                        logger.Debug("option {0:G} requires an argument", tmp);
                        ExitProgram();
                    }
                    return tmp;
                }
            }

            return defaultValue;
        }

        public void PrintHelp( )
        {

            String indent = "    ";
            logger.Debug("taosTest is simple example to operate TDengine use C# Language.\n");
            logger.Debug("{0:G}{1:G}", indent, "-h");
            logger.Debug("{0:G}{1:G}{2:G}", indent, indent, "TDEngine server IP address to connect");
            logger.Debug("{0:G}{1:G}", indent, "-u");
            logger.Debug("{0:G}{1:G}{2:G}", indent, indent, "The TDEngine user name to use when connecting to the server, default is root");
            logger.Debug("{0:G}{1:G}", indent, "-p");
            logger.Debug("{0:G}{1:G}{2:G}", indent, indent, "The TDEngine user name to use when connecting to the server, default is taosdata");
            logger.Debug("{0:G}{1:G}", indent, "-d");
            logger.Debug("{0:G}{1:G}{2:G}", indent, indent, "Database used to create table or import data, default is db");
            logger.Debug("{0:G}{1:G}", indent, "-s");
            logger.Debug("{0:G}{1:G}{2:G}", indent, indent, "Super Tables used to create table, default is mt");
            logger.Debug("{0:G}{1:G}", indent, "-t");
            logger.Debug("{0:G}{1:G}{2:G}", indent, indent, "Table prefixs, default is t");
            logger.Debug("{0:G}{1:G}", indent, "-w");
            logger.Debug("{0:G}{1:G}{2:G}", indent, indent, "Whether to insert data");
            logger.Debug("{0:G}{1:G}", indent, "-r");
            logger.Debug("{0:G}{1:G}{2:G}", indent, indent, "Whether to query data");
            logger.Debug("{0:G}{1:G}", indent, "-n");
            logger.Debug("{0:G}{1:G}{2:G}", indent, indent, "How many Tables to create, default is 10");
            logger.Debug("{0:G}{1:G}", indent, "-b");
            logger.Debug("{0:G}{1:G}{2:G}", indent, indent, "How many rows per insert batch, default is 10");
            logger.Debug("{0:G}{1:G}", indent, "-i");
            logger.Debug("{0:G}{1:G}{2:G}", indent, indent, "How many rows to insert, default is 100");
            logger.Debug("{0:G}{1:G}", indent, "-c");
            logger.Debug("{0:G}{1:G}{2:G}", indent, indent, "Configuration directory");
        }

        public void ReadArgument()
        {
            PrintHelp();
            host = "192.168.138.134";
            user = "root";
            password = "taosdata";
            dbName = "TD_QL_DB";
            stableName = "co_st"; //采集机超级表名
            tablePrefix = "t";
            isInsertData = true;
            isQueryData =true;
            tableCount = 100;
            batchRows = 500;
            totalRows = 10000;
            configDir = "./config";
            logger.Debug("---host[{}], user[{}], password[{}], dbName[{}]----", host, user, password, dbName);
            logger.Debug("---stableName[{}], tablePrefix[{}], isInsertData[{}]----", stableName, tablePrefix, isInsertData);
            logger.Debug("---isQueryData[{}], tableCount[{}], configDir[{}]----", isQueryData, tableCount, configDir);
        }

        public void InitTDengine()
        {
            TDengine.Options((int)TDengineInitOption.TDDB_OPTION_CONFIGDIR, this.configDir);
            TDengine.Options((int)TDengineInitOption.TDDB_OPTION_SHELL_ACTIVITY_TIMER, "60");
            TDengine.Init();
            logger.Debug("TDengine Initialization finished");
        }

        public void ConnectTDengine()
        {
            string db = "";
            logger.Debug("---host[{}], user[{}], password[{}], port[{}]--", host, user, password, port);
            this.conn = TDengine.Connect(this.host, this.user, this.password, db, this.port);
            if (this.conn == 0)
            {
                logger.Debug("Connect to TDengine failed");
                ExitProgram();
            }
            else
            {
                logger.Debug("Connect to TDengine success");
            }
        }

        public void CreateDbAndTable()
        {
            if (!this.isInsertData)
            {
                return;
            }

            StringBuilder sql = new StringBuilder();
            sql.Append("create database if not exists ").Append(this.dbName).Append(" days ").Append(this.keeplive);
            logger.Debug("database sql [{}]--", sql.ToString());
            int code = TDengine.Query(this.conn, sql.ToString());
            if (code == TDengine.TSDB_CODE_SUCCESS)
            {
                logger.Debug(sql.ToString() + " success");
            }
            else
            {
                logger.Debug(sql.ToString() + " failure, reason: " + TDengine.Error(conn));
                ExitProgram();
            }

            sql.Clear();
            sql.Append("use ").Append(this.dbName);
            code = TDengine.Query(this.conn, sql.ToString());
            if (code == TDengine.TSDB_CODE_SUCCESS)
            {
                logger.Debug(sql.ToString() + " success");
            }
            else
            {
                logger.Debug(sql.ToString() + " failure, reason: " + TDengine.Error(this.conn));
                ExitProgram();
            }

            sql.Clear();
            sql.Append("create table if not exists ").Append(this.stableName).Append("(ts timestamp, v1 int) tags(t1 int)");
            code = TDengine.Query(this.conn, sql.ToString());
            if (code == TDengine.TSDB_CODE_SUCCESS)
            {
                logger.Debug(sql.ToString() + " success");
            }
            else
            {
                logger.Debug(sql.ToString() + " failure, reason: " + TDengine.Error(this.conn));
                ExitProgram();
            }

            for (int i = 0; i < this.tableCount; i++)
            {
                sql.Clear();
                sql = sql.Append("create table if not exists ").Append(this.tablePrefix).Append(i)
                  .Append(" using ").Append(this.stableName).Append(" tags(").Append(i).Append(")");
                code = TDengine.Query(this.conn, sql.ToString());
                if (code == TDengine.TSDB_CODE_SUCCESS)
                {
                    logger.Debug(sql.ToString() + " success");
                }
                else
                {
                    logger.Debug(sql.ToString() + " failure, reason: " + TDengine.Error(this.conn));
                    ExitProgram();
                }
            }

            logger.Debug("create db and table success");
        }

        public void ExecuteInsert()
        {
            if (!this.isInsertData)
            {
                return;
            }

            System.DateTime start = new System.DateTime();
            long loopCount = this.totalRows / this.batchRows;

            for (int table = 0; table < this.tableCount; ++table)
            {
                for (long loop = 0; loop < loopCount; loop++)
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("insert into ").Append(this.tablePrefix).Append(table).Append(" values");
                    for (int batch = 0; batch < this.batchRows; ++batch)
                    {
                        long rows = loop * this.batchRows + batch;
                        sql.Append("(").Append(this.beginTimestamp + rows).Append(",").Append(rows).Append(")");
                    }
                    logger.Debug("insert sql[{}]", sql.ToString();
                    int code = TDengine.Query(conn, sql.ToString());
                    if (code != TDengine.TSDB_CODE_SUCCESS)
                    {
                        logger.Debug(sql.ToString() + " failure, reason: " + TDengine.Error(conn));
                    }

                    int affectRows = TDengine.AffectRows(conn);
                    this.rowsInserted += affectRows;
                }
            }

            System.DateTime end = new System.DateTime();
            TimeSpan ts = end - start;

            logger.Debug("Total {0:G} rows inserted, {1:G} rows failed, time spend {2:G} seconds.\n"
              , this.rowsInserted, this.totalRows * this.tableCount - this.rowsInserted, ts.TotalSeconds);
        }

        public void ExecuteQuery()
        {
            if (!this.isQueryData)
            {
                return;
            }

            System.DateTime start = new System.DateTime();
            long queryRows = 0;

            for (int i = 0; i < this.tableCount; ++i)
            {
                String sql = "select * from " + this.dbName + "." + tablePrefix + i;
                logger.Debug("select sql---[{}]",sql);

                int code = TDengine.Query(conn, sql);
                if (code != TDengine.TSDB_CODE_SUCCESS)
                {
                    logger.Debug(sql + " failure, reason: " + TDengine.Error(conn));
                    ExitProgram();
                }

                int fieldCount = TDengine.FieldCount(conn);
                logger.Debug("field count: " + fieldCount);

                List<TDengineMeta> metas = TDengine.FetchFields(conn);
                for (int j = 0; j < metas.Count; j++)
                {
                    TDengineMeta meta = (TDengineMeta)metas[j];
                    logger.Debug("index:" + j + ", type:" + meta.type + ", typename:" + meta.TypeName() + ", name:" + meta.name + ", size:" + meta.size);
                }

                long result = TDengine.UseResult(conn);
                if (result == 0)
                {
                    logger.Debug(sql + " result set is null");
                    return;
                }

                IntPtr rowdata;
                while ((rowdata = TDengine.FetchRows(result)) != IntPtr.Zero)
                {
                    queryRows++;
                    for (int fields = 0; fields < fieldCount; ++fields)
                    {
                        TDengineMeta meta = metas[fields];
                        int offset = 8 * fields;
                        IntPtr data = Marshal.ReadIntPtr(rowdata, offset);

                        if (data == IntPtr.Zero)
                        {
                            continue;
                        }

                        switch ((TDengineDataType)meta.type)
                        {
                            case TDengineDataType.TSDB_DATA_TYPE_BOOL:
                                bool v1 = Marshal.ReadByte(data) == 0 ? false : true;
                                logger.Debug(v1);
                                break;
                            case TDengineDataType.TSDB_DATA_TYPE_TINYINT:
                                byte v2 = Marshal.ReadByte(data);
                                //logger.Debug(v2);
                                break;
                            case TDengineDataType.TSDB_DATA_TYPE_SMALLINT:
                                short v3 = Marshal.ReadInt16(data);
                                //logger.Debug(v3);
                                break;
                            case TDengineDataType.TSDB_DATA_TYPE_INT:
                                int v4 = Marshal.ReadInt32(data);
                                //logger.Debug(v4);
                                break;
                            case TDengineDataType.TSDB_DATA_TYPE_BIGINT:
                                long v5 = Marshal.ReadInt64(data);
                                //logger.Debug(v5);
                                break;
                            case TDengineDataType.TSDB_DATA_TYPE_FLOAT:
                                float v6 = (float)Marshal.PtrToStructure(data, typeof(float));
                                //logger.Debug(v6);
                                break;
                            case TDengineDataType.TSDB_DATA_TYPE_DOUBLE:
                                double v7 = (double)Marshal.PtrToStructure(data, typeof(double));
                                //logger.Debug(v7);
                                break;
                            case TDengineDataType.TSDB_DATA_TYPE_BINARY:
                                string v8 = Marshal.PtrToStringAnsi(data);
                                //logger.Debug(v8);
                                break;
                            case TDengineDataType.TSDB_DATA_TYPE_TIMESTAMP:
                                long v9 = Marshal.ReadInt64(data);
                                //logger.Debug(v9);
                                break;
                            case TDengineDataType.TSDB_DATA_TYPE_NCHAR:
                                string v10 = Marshal.PtrToStringAnsi(data);
                                //logger.Debug(v10);
                                break;
                        }
                    }
                    //logger.Debug("---");
                }

                if (TDengine.ErrorNo(conn) != 0)
                {
                    logger.Debug("Query is not complete， Error {0:G}", TDengine.ErrorNo(conn), TDengine.Error(conn));
                }

                TDengine.FreeResult(result);
            }

            System.DateTime end = new System.DateTime();
            TimeSpan ts = end - start;

            logger.Debug("Total {0:G} rows inserted, {1:G} rows query, time spend {2:G} seconds.\n"
             , this.rowsInserted, queryRows, ts.TotalSeconds);
        }

        public void CloseConnection()
        {
            if (conn != 0)
            {
                TDengine.Close(conn);
            }
        }

        static void ExitProgram()
        {
            System.Environment.Exit(0);
        }
    }
}
