
using Base.kit;
using Common.log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace OpcSvr
{
    public class BaseOpcServerConfig
    {
        private static readonly NLOG Logger = new NLOG("BaseOpcServerConfig");
        public string server_name;
        public int serverrate;
        public Dictionary<string , OpcServerGroupInfo> OpcServerCFGs = new Dictionary<string, OpcServerGroupInfo>();

    }
    public class OpcServerGroupInfo
    {
        public string name;
        public int rate;
        public Dictionary<int, DataItem> tags = new Dictionary<int, DataItem>();
    }
}
