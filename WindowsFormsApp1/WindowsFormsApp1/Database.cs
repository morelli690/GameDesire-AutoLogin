using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Database
    {
        public static void Create()
        {
            if (!File.Exists("Database.sqlite"))
            {
                SQLiteConnection.CreateFile("Database.sqlite");
            }
        }
    }
}
