
using System.Data.SQLite;
using System.IO;

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
                        string sql2 = "CREATE TABLE Authentication (Login TEXT, Password TEXT, PokerLauncherPath TEXT)";
                        cmd.CommandText = sql2;
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "INSERT INTO Authentication (Login, Password, PokerLauncherPath) values (@Login, @Password, @PokerLauncherPath)";
                        cmd.Parameters.Add(new SQLiteParameter("@Login", ""));
                        cmd.Parameters.Add(new SQLiteParameter("@Password", ""));
                        cmd.Parameters.Add(new SQLiteParameter("@PokerLauncherPath", ""));
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
        }
    }
}