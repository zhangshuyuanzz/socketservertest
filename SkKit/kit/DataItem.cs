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
        public long DataTime { set; get; }
        public ushort Quality { set; get; }
        public bool Active { set; get; }
        public UInt32 TagHandle { set; get; }

       /* public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            return this.TagName == ((DataItem)obj).TagName;
        }*/
    }
}
