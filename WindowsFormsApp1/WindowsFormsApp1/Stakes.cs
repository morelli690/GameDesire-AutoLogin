using System;
using System.Collections;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Analyzer : Form
    {
        public Analyzer()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            Task.Factory.StartNew(this.Refresh);
        }

        public class View
        {
            public string Stake { get; set; }
            public string StakeLabel { get; set; }

            public string Hands { get; set; }
            public string Result { get; set; }


            public View(string s, string sl, string h, string r)
            {
                Stake = s;
                StakeLabel = sl;
                Hands = h;
                Result = r;
            }
        }

        public static string getResult(string Stake)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                        cmd.CommandText = "SELECT SUM(Result) FROM Hands WHERE Stake = @Stake";
                        cmd.Parameters.Add(new SQLiteParameter("@Stake", Stake));

                    try
                    {
                        Int64 count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count > 0)
                        {
                            return ("+") + count.ToString("N0");
                        } else
                        {
                            return count.ToString("N0");

                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                con.Close();
            }

            return "0";
        }

        public static string getHands(string Stake)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM Hands WHERE Stake = @Stake";
                    cmd.Parameters.Add(new SQLiteParameter("@Stake", Stake));
                    Int64 count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count.ToString("N0");

                }
                con.Close();
            }

            return "0";
        }

        public async void Refresh()
        {
            while (true)
            {
                ArrayList Views = new ArrayList();
                Views.Add(new View("5_10", "label20", "label49", "label50"));
                Views.Add(new View("10_20", "label19", "label47", "label48"));
                Views.Add(new View("25_50", "label18", "label45", "label46"));
                Views.Add(new View("50_100", "label16", "label43", "label44"));
                Views.Add(new View("100_200", "label17", "label41", "label42"));
                Views.Add(new View("250_500", "label15", "label39", "label40"));
                Views.Add(new View("500_1K", "label14", "label37", "label38"));
                Views.Add(new View("1K_2K", "label13", "label35", "label36"));
                Views.Add(new View("2.5K_5K", "label12", "label33", "label34"));
                Views.Add(new View("5K_10K", "label11", "label31", "label32"));
                Views.Add(new View("10K_20K", "label10", "label29", "label30"));
                Views.Add(new View("25K_50K","label9", "label27", "label28"));
                Views.Add(new View("50K_100K", "label8", "label25", "label26"));
                Views.Add(new View("100K_200K", "label7", "label23", "label24"));
                Views.Add(new View("250K_500K", "label6", "label21", "label22"));
                Int64 total = 0;
                Int64 total2 = 0;
                foreach (View v in Views)
                {
                    Label Stake = this.Controls.Find(v.StakeLabel, true).FirstOrDefault() as Label;

                    Label Hands = this.Controls.Find(v.Hands, true).FirstOrDefault() as Label;
                    Hands.Text = getHands(v.Stake);
                    total2 = total2 + Convert.ToInt64(Hands.Text.Replace(",", ""));

                    Label Result = this.Controls.Find(v.Result, true).FirstOrDefault() as Label;
                    Result.Text = getResult(v.Stake);

                    Int64 res = Convert.ToInt64(Result.Text.Replace(",", "").Replace("+", ""));
                    total = total + res;
                    if(res > 0)
                    {
                        Stake.BackColor = Color.Green;
                        Hands.BackColor = Color.DarkGreen;
                        Result.BackColor = Color.DarkGreen;

                    }
                    else if(res < 0)
                    {
                        Stake.BackColor = Color.Maroon;
                        Hands.BackColor = Color.DarkRed;
                        Result.BackColor = Color.DarkRed;
                    }
                }

                label52.Text = total > 0 ? ("+" + total.ToString("N0")) : total.ToString("N0");
                label51.Text = total2.ToString("N0");
                Thread.Sleep(2000);
            }
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Label1_Click_2(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void Label1_MouseEnter(object sender, EventArgs e)
        {
            label1.BackColor = Color.Red;
            label1.ForeColor = Color.White;
        }

        public void Label1_MouseLeave(object sender, EventArgs e)
        {
            label1.BackColor = Color.FromArgb(35, 84, 84);
            label1.ForeColor = Color.FromArgb(175, 191, 191);
        }
    }
}
