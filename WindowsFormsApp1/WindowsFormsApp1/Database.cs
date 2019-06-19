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
                using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
                {
                    con.Open();

                    using (SQLiteCommand cmd = con.CreateCommand())
                    {
                        string sql = "CREATE TABLE Hands (ID TEXT, Player TEXT, Stake TEXT, TableName TEXT, DateTime TEXT, Card1 TEXT, Card2 TEXT, FlopCard1 TEXT, FlopCard2 TEXT, FlopCard3 TEXT, TurnCard TEXT, RiverCard TEXT, Result TEXT)";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        string sql2 = "CREATE TABLE Authentication (Login TEXT, Password TEXT, Install TEXT, PokerLauncherPath TEXT)";
                        cmd.CommandText = sql2;
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "INSERT INTO Authentication (Login, Password, Install, PokerLauncherPath) values (@Login, @Password, @Install, @PokerLauncherPath)";
                        cmd.Parameters.Add(new SQLiteParameter("@Login", ""));
                        cmd.Parameters.Add(new SQLiteParameter("@Password", ""));
                        cmd.Parameters.Add(new SQLiteParameter("@Install", ""));
                        cmd.Parameters.Add(new SQLiteParameter("@PokerLauncherPath", ""));
                        cmd.ExecuteNonQuery();

                    }
                    con.Close();
                }
            }
        }
    }
}
