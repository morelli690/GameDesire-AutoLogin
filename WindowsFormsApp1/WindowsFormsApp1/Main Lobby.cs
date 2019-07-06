using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameDesire
{
    public partial class Main_Lobby : Form
    {
        public static Thread OpenTableThread;
        public static IntPtr MainLobbyHandle;
        public static Bitmap HorizontalScrollbar = TemplateMatching.generateFormattedBitmap(new Bitmap("ScrollbarHorizontal.png"));

        public Main_Lobby()
        {
            InitializeComponent();
            InitializeElements();
        }

        public void InitializeElements()
        {
            this.comboBox1.SelectedIndexChanged -= new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            this.comboBox2.SelectedIndexChanged -= new System.EventHandler(this.ComboBox2_SelectedIndexChanged);
            this.comboBox3.SelectedIndexChanged -= new System.EventHandler(this.ComboBox3_SelectedIndexChanged);
            this.comboBox4.SelectedIndexChanged -= new System.EventHandler(this.ComboBox4_SelectedIndexChanged);
            this.comboBox5.SelectedIndexChanged -= new System.EventHandler(this.ComboBox5_SelectedIndexChanged);
            this.comboBox6.SelectedIndexChanged -= new System.EventHandler(this.ComboBox6_SelectedIndexChanged);
            this.comboBox7.SelectedIndexChanged -= new System.EventHandler(this.ComboBox7_SelectedIndexChanged);
            this.comboBox8.SelectedIndexChanged -= new System.EventHandler(this.ComboBox8_SelectedIndexChanged);
            this.comboBox9.SelectedIndexChanged -= new System.EventHandler(this.ComboBox9_SelectedIndexChanged);
            this.comboBox10.SelectedIndexChanged -= new System.EventHandler(this.ComboBox10_SelectedIndexChanged);
            this.comboBox11.SelectedIndexChanged -= new System.EventHandler(this.ComboBox11_SelectedIndexChanged);
            this.comboBox12.SelectedIndexChanged -= new System.EventHandler(this.ComboBox12_SelectedIndexChanged);
            this.comboBox13.SelectedIndexChanged -= new System.EventHandler(this.ComboBox13_SelectedIndexChanged);
            this.comboBox14.SelectedIndexChanged -= new System.EventHandler(this.ComboBox14_SelectedIndexChanged);
            this.comboBox15.SelectedIndexChanged -= new System.EventHandler(this.ComboBox15_SelectedIndexChanged);



            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM OpenTables";

                    using (SQLiteDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            string stake = r.GetString(0);

                            if (stake == "250K/500K")
                            {
                                comboBox1.Text = r.GetString(1);
                            }
                            else if (stake == "100K/200K")
                            {
                                comboBox2.Text = r.GetString(1);
                            }
                            else if (stake == "50K/100K")
                            {
                                comboBox3.Text = r.GetString(1);
                            }
                            else if (stake == "25K/50K")
                            {
                                comboBox4.Text = r.GetString(1);
                            }
                            else if (stake == "10K/20K")
                            {
                                comboBox5.Text = r.GetString(1);
                            }
                            else if (stake == "5K/10K")
                            {
                                comboBox6.Text = r.GetString(1);
                            }
                            else if (stake == "2.5K/5K")
                            {
                                comboBox7.Text = r.GetString(1);
                            }
                            else if (stake == "1K/2K")
                            {
                                comboBox8.Text = r.GetString(1);
                            }
                            else if (stake == "500/1K")
                            {
                                comboBox9.Text = r.GetString(1);
                            }
                            else if (stake == "250/500")
                            {
                                comboBox10.Text = r.GetString(1);
                            }
                            else if (stake == "100/200")
                            {
                                comboBox11.Text = r.GetString(1);
                            }
                            else if (stake == "50/100")
                            {
                                comboBox12.Text = r.GetString(1);
                            }
                            else if (stake == "25/50")
                            {
                                comboBox13.Text = r.GetString(1);
                            }
                            else if (stake == "10/20")
                            {
                                comboBox14.Text = r.GetString(1);
                            }
                            else if (stake == "5/10")
                            {
                                comboBox15.Text = r.GetString(1);
                            }
                        }
                    }
                }
                con.Close();
            }

            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.ComboBox2_SelectedIndexChanged);
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.ComboBox3_SelectedIndexChanged);
            this.comboBox4.SelectedIndexChanged += new System.EventHandler(this.ComboBox4_SelectedIndexChanged);
            this.comboBox5.SelectedIndexChanged += new System.EventHandler(this.ComboBox5_SelectedIndexChanged);
            this.comboBox6.SelectedIndexChanged += new System.EventHandler(this.ComboBox6_SelectedIndexChanged);
            this.comboBox7.SelectedIndexChanged += new System.EventHandler(this.ComboBox7_SelectedIndexChanged);
            this.comboBox8.SelectedIndexChanged += new System.EventHandler(this.ComboBox8_SelectedIndexChanged);
            this.comboBox9.SelectedIndexChanged += new System.EventHandler(this.ComboBox9_SelectedIndexChanged);
            this.comboBox10.SelectedIndexChanged += new System.EventHandler(this.ComboBox10_SelectedIndexChanged);
            this.comboBox11.SelectedIndexChanged += new System.EventHandler(this.ComboBox11_SelectedIndexChanged);
            this.comboBox12.SelectedIndexChanged += new System.EventHandler(this.ComboBox12_SelectedIndexChanged);
            this.comboBox13.SelectedIndexChanged += new System.EventHandler(this.ComboBox13_SelectedIndexChanged);
            this.comboBox14.SelectedIndexChanged += new System.EventHandler(this.ComboBox14_SelectedIndexChanged);
            this.comboBox15.SelectedIndexChanged += new System.EventHandler(this.ComboBox15_SelectedIndexChanged);
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

        public int getOpenTableCount()
        {
            //Poker Texas Hold'em, Room 3Miasto, Table no.14 GameChips Game. Fees 2,500/5K NL
            OpenWindows so = new OpenWindows();
            string[] windows = so.GetDesktopWindowsCaptions();

            int count = 0;

            foreach (string s in windows)
            {
                if(s.Contains("Poker Texas Hold'em,") && s.Contains("Table"))
                {
                    count = count + 1;
                }
            }

            return count;
        }

        public List<string> getEligible()
        {
            List<string> eligible = new List<string>();
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM OpenTables WHERE Eligible =\"1\"";

                    using (SQLiteDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            eligible.Add(r.GetString(0));
                        }
                    }
                }
                con.Close();
            }

            return eligible;
        }


        public string getType(string stake)
        {
            if (stake == "250K/500K" || stake == "100K/200K" || stake == "50K/100K" || stake == "25K/50K" || stake == "10K/20K" || stake == "5K/10K" || stake == "2.5K/5K")
            {
                return "High";
            }
            else if (stake == "1K/2K" || stake == "500/1K" || stake == "250/500")
            {
                return "Medium";
            }
            else if (stake == "100/200" || stake == "50/100" || stake == "25/50")
            {
                return "Low";
            }
            else if (stake == "10/20" || stake == "5/10")
            {
                return "Micro";
            }

            return null;
        }

        public void changeType(string type)
        {
            if(type == "High")
            {
                Mouse.leftClick(MainLobbyHandle, new ClickableCoordinate(101, 121));
            }
            else if(type == "Medium")
            {
                Mouse.leftClick(MainLobbyHandle, new ClickableCoordinate(150, 121));
            }
            else if(type == "Low")
            {
                Mouse.leftClick(MainLobbyHandle, new ClickableCoordinate(216, 121));
            }
            else if(type == "Micro")
            {
                Mouse.leftClick(MainLobbyHandle, new ClickableCoordinate(266, 121));
            }

            Thread.Sleep(1000);
        }

        public void clickArrowDown()
        {

        }


        public void moveHorizontalScroolbarToBottom()
        {
            ClickableCoordinate k = TemplateMatching.getClickableCoordinate(MainLobbyHandle, HorizontalScrollbar, 0, 0, 0);
            Mouse.dragAndDrop(MainLobbyHandle, k.X, k.Y, 295, 511);
        }

        public void goToTop()
        {
            moveHorizontalScroolbarToBottom();

        }

        public bool open(string Stake)
        {
            changeType(getType(Stake));
            goToTop();


            // if we opened return true, else return false...
            return false;
        }


        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        public IntPtr GetHandleWindow(string title)
        {
            return FindWindow(null, title);
        }

        public IntPtr getMainLobbyHandle()
        {
            OpenWindows so = new OpenWindows();
            string[] windows = so.GetDesktopWindowsCaptions();

            foreach (string s in windows)
            {
                if (s.Contains("Poker Texas Hold'em,") && s.Contains("Main Lobby"))
                {
                    return GetHandleWindow(s);
                }
            }

            return IntPtr.Zero;
        }

        public void OpenTable()
        {
            while (true)
            {
                MainLobbyHandle = getMainLobbyHandle();

                if (MainLobbyHandle != IntPtr.Zero && getOpenTableCount() == 0)
                {
                    List<string> eligible = getEligible();

                    foreach(string s in eligible)
                    {
                        bool opened = open(s);

                        if(opened == true)
                        {
                            break;
                        }
                    }

                    Thread.Sleep(3000);
                }
                else
                {
                    Thread.Sleep(3000);
                }
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                OpenTableThread = new Thread(OpenTable);
                OpenTableThread.Start();
            }
            else
            {
                OpenTableThread.Abort();
            }
        }

        public void change(string Stake, string newValue)
        {
            this.comboBox1.SelectedIndexChanged -= new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            this.comboBox2.SelectedIndexChanged -= new System.EventHandler(this.ComboBox2_SelectedIndexChanged);
            this.comboBox3.SelectedIndexChanged -= new System.EventHandler(this.ComboBox3_SelectedIndexChanged);
            this.comboBox4.SelectedIndexChanged -= new System.EventHandler(this.ComboBox4_SelectedIndexChanged);
            this.comboBox5.SelectedIndexChanged -= new System.EventHandler(this.ComboBox5_SelectedIndexChanged);
            this.comboBox6.SelectedIndexChanged -= new System.EventHandler(this.ComboBox6_SelectedIndexChanged);
            this.comboBox7.SelectedIndexChanged -= new System.EventHandler(this.ComboBox7_SelectedIndexChanged);
            this.comboBox8.SelectedIndexChanged -= new System.EventHandler(this.ComboBox8_SelectedIndexChanged);
            this.comboBox9.SelectedIndexChanged -= new System.EventHandler(this.ComboBox9_SelectedIndexChanged);
            this.comboBox10.SelectedIndexChanged -= new System.EventHandler(this.ComboBox10_SelectedIndexChanged);
            this.comboBox11.SelectedIndexChanged -= new System.EventHandler(this.ComboBox11_SelectedIndexChanged);
            this.comboBox12.SelectedIndexChanged -= new System.EventHandler(this.ComboBox12_SelectedIndexChanged);
            this.comboBox13.SelectedIndexChanged -= new System.EventHandler(this.ComboBox13_SelectedIndexChanged);
            this.comboBox14.SelectedIndexChanged -= new System.EventHandler(this.ComboBox14_SelectedIndexChanged);
            this.comboBox15.SelectedIndexChanged -= new System.EventHandler(this.ComboBox15_SelectedIndexChanged);

            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    string sql = "UPDATE OpenTables SET Players=@Player WHERE Stake=@Stak";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SQLiteParameter("@Player", newValue));
                    cmd.Parameters.Add(new SQLiteParameter("@Stak", Stake));

                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }

            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.ComboBox2_SelectedIndexChanged);
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.ComboBox3_SelectedIndexChanged);
            this.comboBox4.SelectedIndexChanged += new System.EventHandler(this.ComboBox4_SelectedIndexChanged);
            this.comboBox5.SelectedIndexChanged += new System.EventHandler(this.ComboBox5_SelectedIndexChanged);
            this.comboBox6.SelectedIndexChanged += new System.EventHandler(this.ComboBox6_SelectedIndexChanged);
            this.comboBox7.SelectedIndexChanged += new System.EventHandler(this.ComboBox7_SelectedIndexChanged);
            this.comboBox8.SelectedIndexChanged += new System.EventHandler(this.ComboBox8_SelectedIndexChanged);
            this.comboBox9.SelectedIndexChanged += new System.EventHandler(this.ComboBox9_SelectedIndexChanged);
            this.comboBox10.SelectedIndexChanged += new System.EventHandler(this.ComboBox10_SelectedIndexChanged);
            this.comboBox11.SelectedIndexChanged += new System.EventHandler(this.ComboBox11_SelectedIndexChanged);
            this.comboBox12.SelectedIndexChanged += new System.EventHandler(this.ComboBox12_SelectedIndexChanged);
            this.comboBox13.SelectedIndexChanged += new System.EventHandler(this.ComboBox13_SelectedIndexChanged);
            this.comboBox14.SelectedIndexChanged += new System.EventHandler(this.ComboBox14_SelectedIndexChanged);
            this.comboBox15.SelectedIndexChanged += new System.EventHandler(this.ComboBox15_SelectedIndexChanged);
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            change( ((ComboBox)sender).Tag.ToString(), ((ComboBox)sender).SelectedItem.ToString());
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            change(((ComboBox)sender).Tag.ToString(), ((ComboBox)sender).SelectedItem.ToString());
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            change(((ComboBox)sender).Tag.ToString(), ((ComboBox)sender).SelectedItem.ToString());
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            change(((ComboBox)sender).Tag.ToString(), ((ComboBox)sender).SelectedItem.ToString());
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            change(((ComboBox)sender).Tag.ToString(), ((ComboBox)sender).SelectedItem.ToString());
        }

        private void ComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            change(((ComboBox)sender).Tag.ToString(), ((ComboBox)sender).SelectedItem.ToString());
        }

        private void ComboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            change(((ComboBox)sender).Tag.ToString(), ((ComboBox)sender).SelectedItem.ToString());
        }

        private void ComboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            change(((ComboBox)sender).Tag.ToString(), ((ComboBox)sender).SelectedItem.ToString());
        }

        private void ComboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            change(((ComboBox)sender).Tag.ToString(), ((ComboBox)sender).SelectedItem.ToString());
        }

        private void ComboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            change(((ComboBox)sender).Tag.ToString(), ((ComboBox)sender).SelectedItem.ToString());
        }

        private void ComboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            change(((ComboBox)sender).Tag.ToString(), ((ComboBox)sender).SelectedItem.ToString());
        }

        private void ComboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            change(((ComboBox)sender).Tag.ToString(), ((ComboBox)sender).SelectedItem.ToString());
        }

        private void ComboBox13_SelectedIndexChanged(object sender, EventArgs e)
        {
            change(((ComboBox)sender).Tag.ToString(), ((ComboBox)sender).SelectedItem.ToString());
        }

        private void ComboBox14_SelectedIndexChanged(object sender, EventArgs e)
        {
            change(((ComboBox)sender).Tag.ToString(), ((ComboBox)sender).SelectedItem.ToString());
        }

        private void ComboBox15_SelectedIndexChanged(object sender, EventArgs e)
        {
            change(((ComboBox)sender).Tag.ToString(), ((ComboBox)sender).SelectedItem.ToString());
        }
    }
}
