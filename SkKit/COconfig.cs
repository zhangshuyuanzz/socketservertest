using Common.log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkKit
{
    public class COconfig
    {
        private NLOG logger = new NLOG("COconfig");
        public string CoGetFileStr(string path)
        {
            logger.Debug("path[{}]",path);
            string Filestr = System.IO.File.ReadAllText(path);
            return Filestr;
        }
    }
}
