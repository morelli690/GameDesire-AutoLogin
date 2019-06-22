using System;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;

namespace GameDesire
{
    public partial class Bankroll : Form
    {
        public static string activeStake = "250K/500K";
        public Bankroll()
        {
            InitializeComponent();
            InitializeElements();
            comboBox4.SelectedItem = "250K/500K";
        }

        public void Deactivate()
        {
            this.comboBox1.SelectedIndexChanged -= new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            this.comboBox2.SelectedIndexChanged -= new System.EventHandler(this.ComboBox2_SelectedIndexChanged);
            this.comboBox3.SelectedIndexChanged -= new System.EventHandler(this.ComboBox3_SelectedIndexChanged);
        }

        public void Activate()
        {
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.ComboBox2_SelectedIndexChanged);
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.ComboBox3_SelectedIndexChanged);
        }


        private Int64 getBB(string stake)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT BuyInBB FROM Bankroll WHERE Stake = @Stake";
                    cmd.Parameters.Add(new SQLiteParameter("@Stake", stake));

                    SQLiteDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        return Convert.ToInt64(r.GetString(0));
                    }
                }
                con.Close();
            }


            return 0;
        }

        private Int64 getAbove(string stake)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT Above FROM Bankroll WHERE Stake = @Stake";
                    cmd.Parameters.Add(new SQLiteParameter("@Stake", stake));

                    SQLiteDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        return Convert.ToInt64(r.GetString(0));
                    }
                }
                con.Close();
            }


            return 0;
        }

        private Int64 getBelow(string stake)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT Below FROM Bankroll WHERE Stake = @Stake";
                    cmd.Parameters.Add(new SQLiteParameter("@Stake", stake));

                    SQLiteDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        return Convert.ToInt64(r.GetString(0));
                    }
                }
                con.Close();
            }


            return 0;
        }

        private Int64 getBigBlind(string stake)
        {
            if(stake == "250K/500K")
            {
                return 500000;
            }

            else if (stake == "100K/200K")
            {
                return 200000;
            }
            else if (stake == "50K/100K")
            {
                return 100000;
            }
            else if (stake == "25K/50K")
            {
                return 50000;
            }
            else if(stake == "10K/20K")
            {
                return 20000;
            }
            else if (stake == "5K/10K")
            {
                return 10000;
            }
            else if (stake == "2.5K/5K")
            {
                return 5000;
            }
            else if (stake == "1K/2K")
            {
                return 2000;
            }
            else if (stake == "500/1K")
            {
                return 1000;
            }
            else if (stake == "250/500")
            {
                return 500;
            }
            else if (stake == "100/200")
            {
                return 200;
            }
            else if (stake == "50/100")
            {
                return 100;
            }
            else if (stake == "25/50")
            {
                return 50;
            }
            else if (stake == "10/20")
            {
                return 20;

            }
            else if (stake == "5/10")
            {
                return 10;
            }

            return -1;
        }

        private void InitializeElements()
        {
            Deactivate();

            ComboBox BuyIn = this.Controls.Find("comboBox1", true).FirstOrDefault() as ComboBox;
            BuyIn.Items.Clear();

            Int64 bb = getBB(activeStake);
            Int64 BigBlind = getBigBlind(activeStake);
            Int64 Above2 = getAbove(activeStake);
            Int64 Below2 = getBelow(activeStake);

            for (int i = 200; i >= 10; i--)
            {
                BuyIn.Items.Add(i + " BB (" + (i * BigBlind).ToString("N0") + ") ");

                if (i == bb)
                {
                    BuyIn.SelectedItem = i + " BB (" + (i * BigBlind).ToString("N0") + ") ".ToString();
                }
            }

            ComboBox Above = this.Controls.Find("comboBox2", true).FirstOrDefault() as ComboBox;
            Above.Items.Clear();

            for (int i = 200; i >= 1; i--)
            {
                Above.Items.Add(i + " BI (" + (i * BigBlind * bb).ToString("N0") + ") ");

                if (i == Above2)
                {
                    Above.SelectedItem = i + " BI (" + (i * BigBlind * bb).ToString("N0") + ") ".ToString();
                }
            }

            ComboBox Below = this.Controls.Find("comboBox3", true).FirstOrDefault() as ComboBox;
            Below.Items.Clear();

            Int64 toDo = Above2 - 1; 
            for (int i = (int)toDo; i >= 0; i--)
            {
                Below.Items.Add(i + " BI (" + (i * BigBlind * bb).ToString("N0") + ") ");

                if (i == Below2)
                {
                    Below.SelectedItem = i + " BI (" + (i * BigBlind * bb).ToString("N0") + ") ".ToString();
                }
            }

            Activate();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    string sql = "UPDATE Bankroll SET BuyInBB=@Logi WHERE Stake = @sta";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SQLiteParameter("@Logi", comboBox1.SelectedItem.ToString().Split(' ')[0]));
                    cmd.Parameters.Add(new SQLiteParameter("@sta", activeStake));
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }

            InitializeElements();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    string sql = "UPDATE Bankroll SET Above=@Logi WHERE Stake = @sta";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SQLiteParameter("@Logi", comboBox2.SelectedItem.ToString().Split(' ')[0]));
                    cmd.Parameters.Add(new SQLiteParameter("@sta", activeStake));
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }

            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT Above, Below FROM Bankroll WHERE Stake = @stak";
                    cmd.Parameters.Add(new SQLiteParameter("@stak", activeStake));

                    SQLiteDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        Int64 above = Convert.ToInt64(r.GetString(0));
                        Int64 below = Convert.ToInt64(r.GetString(1));

                        if(below >= above)
                        {
                            below = above - 1;

                            using (SQLiteConnection con2 = new SQLiteConnection("Data Source=Database.sqlite"))
                            {
                                con2.Open();

                                using (SQLiteCommand cmd2 = con.CreateCommand())
                                {
                                    string sql = "UPDATE Bankroll SET Below=@Logi WHERE Stake = @sta";
                                    cmd2.CommandText = sql;
                                    cmd2.Parameters.Add(new SQLiteParameter("@Logi", below.ToString()));
                                    cmd2.Parameters.Add(new SQLiteParameter("@sta", activeStake));
                                    cmd2.ExecuteNonQuery();
                                }
                                con2.Close();
                            }
                        }
                    }
                }
                con.Close();
            }
            InitializeElements();
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    string sql = "UPDATE Bankroll SET Below=@Logi WHERE Stake = @sta";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SQLiteParameter("@Logi", comboBox3.SelectedItem.ToString().Split(' ')[0]));
                    cmd.Parameters.Add(new SQLiteParameter("@sta", activeStake));
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            InitializeElements();
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            activeStake = comboBox4.SelectedItem.ToString();
            InitializeElements();
        }
    }
}
