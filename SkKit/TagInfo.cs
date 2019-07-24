using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkKit
{
    public class TagInfo
    {
        public Dictionary<int, float> TagList;
        public string CuTime;
        public TagInfo()
        {
            TagList = new Dictionary<int, float>();
            CuTime = DateTime.Now.ToString();
        }
    }
}
