using Common.log;
using SQLite;

namespace OpcServer
{
    class OpcData
    {
        private static readonly NLOG Logger = new NLOG("OpcData");
        private SQLiteHelper _mgr;
        public OpcData()
        {
            this._mgr = new SQLiteHelper("sqlite.db");
            this._mgr.Open();
        }
        public void TestTableExists()
        {
            Console.WriteLine("表test是否存在: " + this._mgr.TableExists("test"));
        }
    }
}
