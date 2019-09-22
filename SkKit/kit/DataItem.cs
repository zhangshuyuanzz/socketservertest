﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Base.kit
{
    public class DataItem
    {
        public string TagName { get; set; }
        public int TagId { get; set; }
        public byte DataType { get; set; }
        public object Value { get; set; }
        public string DataTime { set; get; }
        public ushort Quality { set; get; }
        public bool Active { set; get; }
        public UInt32 TagHandle { set; get; }
        public string Tagstr { set; get; }
        public string TagIP { set; get; }

        public DataItem()
        {
            this.TagName = "unKown name";
            this.DataType = 1;
            this.Quality = 192;
            this.Active = false;
            this.Value = 0;
            this.DataTime = DateTime.Now.ToString(); 
        }

        public DataItem(string name,int id,byte type,object value,string time,string inip)
        {
            this.TagName = name;
            this.TagId = id;
            this.DataType = type;
            this.Value = value;
            this.DataTime = time;// DateTime.Now.ToString();
            this.Quality = 192;
            this.Active = true;
            this.TagIP = inip;
        }
    }
    public enum DATATYPE
    { 
        FLOAT = 1,
        LONG  = 2
    }
}
