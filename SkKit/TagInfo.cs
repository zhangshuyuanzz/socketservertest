using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkKit
{
    public class DevInfo
    {
        public Dictionary<int, TagInfo> TagList;
        //public Dictionary<string, TagInfo> TagListWithName;
        public string CuTime;
        public DevInfo()
        {
            //TagListWithName = new Dictionary<string, TagInfo>();
            TagList = new Dictionary<int, TagInfo>();
            CuTime = DateTime.Now.ToString();
        }
    }
    public class TagInfo
    {
        public float myvalue;
        public string myname;
        public string mytime;

        public TagInfo()
        {
            myvalue = 0;
            myname = "no name!";
            mytime = DateTime.Now.ToString();
        }
    }
}
