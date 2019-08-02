using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkKit
{
    public class DevInfo
    {
        public Dictionary<int, TagInfo> TagList;
        public string CuTime;
        public DevInfo()
        {
            TagList = new Dictionary<int, TagInfo>();
            CuTime = DateTime.Now.ToString();
        }
    }
    public class TagInfo
    {
        public float myvalue;
        public string myname;
        public TagInfo()
        {
            myvalue = 0;
            myname = "no name!";
        }
    }
}
