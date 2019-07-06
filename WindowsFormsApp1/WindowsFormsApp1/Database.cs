﻿
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
                        string sql = "CREATE TABLE Hands (ID TEXT, Player TEXT, Stake TEXT, TableName TEXT, DateTime TEXT, Card1 TEXT, Card2 TEXT, FlopCard1 TEXT, FlopCard2 TEXT, FlopCard3 TEXT, TurnCard TEXT, RiverCard TEXT, Result TEXT)";
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        string sql2 = "CREATE TABLE Authentication (Login TEXT, Password TEXT, Install TEXT, PokerLauncherPath TEXT)";
                        cmd.CommandText = sql2;
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "INSERT INTO Authentication (Login, Password, Install, PokerLauncherPath) values (@Login, @Password, @Install, @PokerLauncherPath)";
                        cmd.Parameters.Add(new SQLiteParameter("@Login", ""));
                        cmd.Parameters.Add(new SQLiteParameter("@Password", ""));
                        cmd.Parameters.Add(new SQLiteParameter("@Install", "1"));
                        cmd.Parameters.Add(new SQLiteParameter("@PokerLauncherPath", ""));
                        cmd.ExecuteNonQuery();

                        string sql3 = "CREATE TABLE Bankroll (Stake TEXT, BuyInBB TEXT, Above TEXT, Below TEXT, IR TEXT)";
                        cmd.CommandText = sql3;
                        cmd.ExecuteNonQuery();

                        string sql4 = "CREATE TABLE OpenTables (Stake TEXT, Players TEXT, Eligible TEXT)";
                        cmd.CommandText = sql4;
                        cmd.ExecuteNonQuery();

                        string[] stakes = { "250K/500K", "100K/200K", "50K/100K", "25K/50K", "10K/20K", "5K/10K", "2.5K/5K", "1K/2K", "500/1K", "250/500", "100/200", "50/100", "25/50", "10/20", "5/10" };

                        foreach (string stake in stakes)
                        {
                            cmd.CommandText = "INSERT INTO Bankroll (Stake, BuyInBB, Above, Below, IR) values (@Stake, @BuyInBB, @Above, @Below, @IR)";
                            cmd.Parameters.Add(new SQLiteParameter("@Stake", stake));
                            cmd.Parameters.Add(new SQLiteParameter("@BuyInBB", 100));
                            cmd.Parameters.Add(new SQLiteParameter("@Above", 20));
                            cmd.Parameters.Add(new SQLiteParameter("@Below", 17));
                            cmd.Parameters.Add(new SQLiteParameter("@IR", stake == "5/10" ? "1" : "0" ));
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "INSERT INTO OpenTables (Stake, Players, Eligible) values (@Stake, @Players, @Eligible)";
                            cmd.Parameters.Add(new SQLiteParameter("@Stake", stake));
                            cmd.Parameters.Add(new SQLiteParameter("@Players", "5"));
                            cmd.Parameters.Add(new SQLiteParameter("@Eligible", "0"));
                            cmd.ExecuteNonQuery();
                        }
                    }
                    con.Close();
                }
            }
        }
    }
}