using System;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
            InitializeOffset();
            initializeOther();
        }

        private string getOffset()
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT Offset FROM ResultOffset";

                    SQLiteDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        return r.GetString(0);
                    }
                }
                con.Close();
            }

            return "";
        }

        private string getType()
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT Type FROM ResultOffset";

                    SQLiteDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        return r.GetString(0);
                    }
                }
                con.Close();
            }
            return "";
        }

        private string getActivePlayersSit()
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT ActivePlayer1 FROM Bankroll WHERE Stake=@Stake";
                    cmd.Parameters.Add(new SQLiteParameter("@Stake", activeStake));

                    SQLiteDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        return r.GetString(0);
                    }
                }
                con.Close();
            }
            return "";
        }

        private bool isChecked()
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT Rebuy FROM Bankroll WHERE Stake=@Stake";
                    cmd.Parameters.Add(new SQLiteParameter("@Stake", activeStake));

                    SQLiteDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        return (r.GetString(0) == "1") ? true : false;
                    }
                }
                con.Close();
            }
            return false;
        }

        public string getActiveLeave()
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT ActivePlayersLeave FROM Bankroll WHERE Stake=@Stake";
                    cmd.Parameters.Add(new SQLiteParameter("@Stake", activeStake));

                    SQLiteDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        return r.GetString(0);
                    }
                }
                con.Close();
            }
            return "";
        }

        public string getRebuyBelow()
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT MyStackBelowRebuy FROM Bankroll WHERE Stake=@Stake";
                    cmd.Parameters.Add(new SQLiteParameter("@Stake", activeStake));

                    SQLiteDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        return r.GetString(0);
                    }
                }
                con.Close();
            }
            return "";
        }

        public string getRebuyUpto()
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT RebuyUpto FROM Bankroll WHERE Stake=@Stake";
                    cmd.Parameters.Add(new SQLiteParameter("@Stake", activeStake));

                    SQLiteDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        return r.GetString(0);
                    }
                }
                con.Close();
            }
            return "";
        }

        public int getBuyInBB()
        {
            int t = Convert.ToInt32(comboBox1.Text.Split(' ')[0]);
            return 84;
        }

        private void initializeOther()
        {
            this.comboBox6.SelectedIndexChanged -= new System.EventHandler(this.ComboBox6_SelectedIndexChanged);
            this.checkBox1.CheckedChanged -= new System.EventHandler(this.CheckBox1_CheckedChanged);
            this.comboBox7.SelectedIndexChanged -= new System.EventHandler(this.ComboBox7_SelectedIndexChanged);
            this.comboBox10.SelectedIndexChanged -= new System.EventHandler(this.ComboBox10_SelectedIndexChanged);
            this.comboBox11.SelectedIndexChanged -= new System.EventHandler(this.ComboBox11_SelectedIndexChanged);

            comboBox6.SelectedItem = getActivePlayersSit();
            checkBox1.Checked = isChecked();
            comboBox7.SelectedItem = getActiveLeave();

            comboBox10.Items.Clear();
            int bb = (int)Convert.ToInt64(comboBox1.SelectedItem.ToString().Split(' ')[0]);
            for(int i = 200; i>=1; i--)
            {
                comboBox10.Items.Add(i + " BB (" + (i * getBigBlind(activeStake)).ToString("N0") + ") ".ToString());
            }

            int getRebuyB = (int)Convert.ToInt16(getRebuyBelow());
            comboBox10.SelectedItem = getRebuyB + " BB (" + (getRebuyB * getBigBlind(activeStake)).ToString("N0") + ") ".ToString();

            comboBox11.Items.Clear();
            for (int i = 200;  i >= getRebuyB; i--)
            {
                comboBox11.Items.Add(i + " BB (" + (i * getBigBlind(activeStake)).ToString("N0") + ") ".ToString());
            }

            int g = (int)Convert.ToInt16(getRebuyUpto());
            comboBox11.SelectedItem = g + " BB (" + (g * getBigBlind(activeStake)).ToString("N0") + ") ".ToString();

            if (g <= getRebuyB)
            {
                comboBox11.SelectedItem = getRebuyB + " BB (" + (getRebuyB * getBigBlind(activeStake)).ToString("N0") + ") ".ToString();
            }

            this.comboBox7.SelectedIndexChanged += new System.EventHandler(this.ComboBox7_SelectedIndexChanged);
            this.comboBox10.SelectedIndexChanged += new System.EventHandler(this.ComboBox10_SelectedIndexChanged);
            this.comboBox11.SelectedIndexChanged += new System.EventHandler(this.ComboBox11_SelectedIndexChanged);
            this.comboBox6.SelectedIndexChanged += new System.EventHandler(this.ComboBox6_SelectedIndexChanged);
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
        }

        public void InitializeOffset()
        {
            this.comboBox5.SelectedIndexChanged -= new System.EventHandler(this.ComboBox5_SelectedIndexChanged);
            this.textBox1.TextChanged -= new System.EventHandler(this.TextBox1_TextChanged);
            this.comboBox6.SelectedIndexChanged -= new System.EventHandler(this.ComboBox6_SelectedIndexChanged);

            textBox1.Text = getOffset();
            comboBox5.SelectedItem = getType();

            this.comboBox6.SelectedIndexChanged += new System.EventHandler(this.ComboBox6_SelectedIndexChanged);
            this.comboBox5.SelectedIndexChanged += new System.EventHandler(this.ComboBox5_SelectedIndexChanged);
            this.textBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
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
            initializeOther();
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
            initializeOther();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    string sql = "UPDATE ResultOffset SET Offset=@Logi";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SQLiteParameter("@Logi", textBox1.Text));
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    string sql = "UPDATE ResultOffset SET Type=@Logi";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SQLiteParameter("@Logi", comboBox5.SelectedItem.ToString()));
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        private void ComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    string sql = "UPDATE Bankroll SET ActivePlayer1=@Logi WHERE Stake = @sta";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SQLiteParameter("@Logi", comboBox6.SelectedItem.ToString()));
                    cmd.Parameters.Add(new SQLiteParameter("@sta", activeStake));
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        private void ComboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    string sql = "UPDATE Bankroll SET ActivePlayersLeave=@Logi WHERE Stake = @sta";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SQLiteParameter("@Logi", comboBox7.SelectedItem.ToString()));
                    cmd.Parameters.Add(new SQLiteParameter("@sta", activeStake));
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    string sql = "UPDATE Bankroll SET Rebuy=@Logi WHERE Stake = @sta";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SQLiteParameter("@Logi", (checkBox1.Checked ? "1" : "0")));
                    cmd.Parameters.Add(new SQLiteParameter("@sta", activeStake));
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        private void ComboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    string sql = "UPDATE Bankroll SET MyStackBelowRebuy=@Logi WHERE Stake = @sta";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SQLiteParameter("@Logi", comboBox10.SelectedItem.ToString().Split(' ')[0] ));
                    cmd.Parameters.Add(new SQLiteParameter("@sta", activeStake));
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            initializeOther();
        }

        private void ComboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    string sql = "UPDATE Bankroll SET Rebuyupto=@Logi WHERE Stake = @sta";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SQLiteParameter("@Logi", comboBox11.SelectedItem.ToString().Split(' ')[0]));
                    cmd.Parameters.Add(new SQLiteParameter("@sta", activeStake));
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            initializeOther();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            comboBox10.SelectedIndex = 0;
        }

        private void Label5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Label5_MouseEnter(object sender, EventArgs e)
        {
            label5.BackColor = Color.Red;
            label5.ForeColor = Color.White;
        }

        public void Label5_MouseLeave(object sender, EventArgs e)
        {
            label5.BackColor = Color.FromArgb(35, 84, 84);
            label5.ForeColor = Color.FromArgb(175, 191, 191);
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void label6_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
