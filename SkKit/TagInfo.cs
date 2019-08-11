﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkKit
{
    public class DevInfo
    {
        public ConcurrentDictionary<int, TagInfo> TagList;
        public ConcurrentDictionary<string, int> TagListWithName;
        public ConcurrentDictionary<int, string> TagListWithID;

        public string CuTime;
        public object SkClientHandle;
        public DevInfo()
        {
            //TagListWithName = new Dictionary<string, TagInfo>();
            TagList = new ConcurrentDictionary<int, TagInfo>();
            CuTime = DateTime.Now.ToString();
        }
        public DevInfo(object inhandle)
        {
            TagListWithID = new ConcurrentDictionary<int, string>();
            TagListWithName = new ConcurrentDictionary<string, int>();

            this.TagList = new ConcurrentDictionary<int, TagInfo>();
            this.CuTime = DateTime.Now.ToString();
            this.SkClientHandle = inhandle;
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
            myname = "invalid";
            mytime = DateTime.Now.ToString();
        }
    }
}
