using System;
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

        public DataItem()
        {
            this.TagName = "unKown name";
            this.DataType = 1;
            this.Quality = 192;
            this.Active = true;
        }

        public DataItem(string name,int id,byte type,object value,string time)
        {
            this.TagName = name;
            this.TagId = id;
            this.DataType = type;
            this.Value = value;
            this.DataTime = time;// DateTime.Now.ToString();
            this.Quality = 192;
            this.Active = true;
        }
    }
    public enum DATATYPE
    { 
        FLOAT = 1,
        LONG  = 2
    }
}
