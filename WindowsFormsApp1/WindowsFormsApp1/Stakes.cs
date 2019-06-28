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
            public string Eligible { get; set; }


            public View(string s, string sl, string h, string r, string el)
            {
                Stake = s;
                StakeLabel = sl;
                Hands = h;
                Result = r;
                Eligible = el;
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

        private Int64 getBigBlind(string stake)
        {
            if (stake == "250K_500K")
            {
                return 500000;
            }

            else if (stake == "100K_200K")
            {
                return 200000;
            }
            else if (stake == "50K_100K")
            {
                return 100000;
            }
            else if (stake == "25K_50K")
            {
                return 50000;
            }
            else if (stake == "10K_20K")
            {
                return 20000;
            }
            else if (stake == "5K_10K")
            {
                return 10000;
            }
            else if (stake == "2.5K_5K")
            {
                return 5000;
            }
            else if (stake == "1K_2K")
            {
                return 2000;
            }
            else if (stake == "500_1K")
            {
                return 1000;
            }
            else if (stake == "250_500")
            {
                return 500;
            }
            else if (stake == "100_200")
            {
                return 200;
            }
            else if (stake == "50_100")
            {
                return 100;
            }
            else if (stake == "25_50")
            {
                return 50;
            }
            else if (stake == "10_20")
            {
                return 20;
            }
            else if (stake == "5_10")
            {
                return 10;
            }
            return -1;
        }

        private string ConvertStake(string stake)
        {
            if (stake == "250K_500K")
            {
                return "250K/500K";
            }
            else if (stake == "100K_200K")
            {
                return "100K/200K";
            }
            else if (stake == "50K_100K")
            {
                return "50K/100K";
            }
            else if (stake == "25K_50K")
            {
                return "25K/50K";
            }
            else if (stake == "10K_20K")
            {
                return "10K/20K";
            }
            else if (stake == "5K_10K")
            {
                return "5K/10K";
            }
            else if (stake == "2.5K_5K")
            {
                return "2.5K/5K";
            }
            else if (stake == "1K_2K")
            {
                return "1K/2K";
            }
            else if (stake == "500_1K")
            {
                return "500/1K";
            }
            else if (stake == "250_500")
            {
                return "250/500";
            }
            else if (stake == "100_200")
            {
                return "100/200";
            }
            else if (stake == "50_100")
            {
                return "50/100";
            }
            else if (stake == "25_50")
            {
                return "25/50";
            }
            else if (stake == "10_20")
            {
                return "10/20";
            }
            else if (stake == "5_10")
            {
                return "5/10";
            }
            return "";
        }

        public string getEligible(string stake, Int64 total)
        {
            Int64 BB = Convert.ToInt64(getBigBlind(stake).ToString());
            Int64 buyinbb = 0;
            Int64 above = 0;
            string stake2 = ConvertStake(stake);
            Int64 offset = 0;

            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT BuyInBB, Above FROM Bankroll WHERE Stake = @Stake";
                    cmd.Parameters.Add(new SQLiteParameter("@Stake", stake2));

                    SQLiteDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        buyinbb = Convert.ToInt64(r.GetString(0));
                        above = Convert.ToInt64(r.GetString(1));
                    }
                }
                con.Close();
            }

            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT Type, Offset FROM ResultOffset";

                    SQLiteDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        string type = r.GetString(0);
                        string offs = r.GetString(1);

                        if(offs == "")
                        {
                            offset = 0;
                        }
                        else if(type == "-")
                        {
                            offset = offset - Convert.ToInt64(offs);
                        }
                        else
                        {
                            offset = offset + Convert.ToInt64(offs);
                        }
                    }
                }
                con.Close();
            }

            Int64 RequiredForThisStake = BB * buyinbb * above;

            if((total+offset) >= RequiredForThisStake)
            {
                return "✓";
            }
            else
            {
                return "✖ (" + (RequiredForThisStake-(total+offset)).ToString("N0") + ")";
            }
        }

        public async void Refresh()
        {
            try
            {
                while (true)
                {
                    ArrayList Views = new ArrayList();
                    Views.Add(new View("5_10", "label20", "label49", "label50", "label68"));
                    Views.Add(new View("10_20", "label19", "label47", "label48", "label67"));
                    Views.Add(new View("25_50", "label18", "label45", "label46", "label66"));
                    Views.Add(new View("50_100", "label16", "label43", "label44", "label65"));
                    Views.Add(new View("100_200", "label17", "label41", "label42", "label64"));
                    Views.Add(new View("250_500", "label15", "label39", "label40", "label63"));
                    Views.Add(new View("500_1K", "label14", "label37", "label38", "label62"));
                    Views.Add(new View("1K_2K", "label13", "label35", "label36", "label61"));
                    Views.Add(new View("2.5K_5K", "label12", "label33", "label34", "label60"));
                    Views.Add(new View("5K_10K", "label11", "label31", "label32", "label59"));
                    Views.Add(new View("10K_20K", "label10", "label29", "label30", "label58"));
                    Views.Add(new View("25K_50K", "label9", "label27", "label28", "label57"));
                    Views.Add(new View("50K_100K", "label8", "label25", "label26", "label56"));
                    Views.Add(new View("100K_200K", "label7", "label23", "label24", "label55"));
                    Views.Add(new View("250K_500K", "label6", "label21", "label22", "label54"));
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
                    }


                    label52.Text = total > 0 ? ("+" + total.ToString("N0")) : total.ToString("N0");
                    label51.Text = total2.ToString("N0");
                    Label Stakes2 = this.Controls.Find("label51", true).FirstOrDefault() as Label;
                    Label Hands2 = this.Controls.Find("label52", true).FirstOrDefault() as Label;

                    foreach (View v in Views)
                    {
                        Label Eligible = this.Controls.Find(v.Eligible, true).FirstOrDefault() as Label;
                        Eligible.Text = getEligible(v.Stake, total);

                    }



                    Thread.Sleep(2000);


                }
            }
            catch(Exception ex)
            {

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

            var formToShow = Application.OpenForms.Cast<Form>().FirstOrDefault(c => c is GameDesire.Bankroll);

            if (formToShow != null)
            {
                formToShow.Left = this.Location.X;
                formToShow.Top = this.Location.Y + 100;
                formToShow.BringToFront();
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

        private void PictureBox1_Click(object sender, EventArgs e)
        {
        }

        public void PictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;
        }

        public void PictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
        }
        private void onFormClosed(object sender, EventArgs e)
        {
            if (Application.OpenForms.Count == 0)
            {
                Application.Exit();
            }
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var formToShow = Application.OpenForms.Cast<Form>().FirstOrDefault(c => c is GameDesire.Cards);

            if (formToShow != null)
            {
                formToShow.FormClosed += onFormClosed;
                formToShow.Left = this.Location.X;
                formToShow.Top = this.Location.Y;
                formToShow.Show();
            }
            else
            {
                new GameDesire.Cards(this.Location.X, this.Location.Y).Show();
            }

            var formToShow2 = Application.OpenForms.Cast<Form>().FirstOrDefault(c => c is GameDesire.Bankroll);

            if (formToShow2 != null)
            {
                formToShow2.Hide();
            }
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            var formToShow = Application.OpenForms.Cast<Form>().FirstOrDefault(c => c is GameDesire.Bankroll);

            if (formToShow != null)
            {
                formToShow.Left = this.Location.X;
                formToShow.Top = this.Location.Y+100;
                formToShow.Show();
                formToShow.BringToFront();
            }
            else
            {
                new GameDesire.Bankroll(this.Location.X, this.Location.Y+100).Show();
                formToShow = Application.OpenForms.Cast<Form>().FirstOrDefault(c => c is GameDesire.Bankroll);

                if (formToShow != null)
                {
                    formToShow.Left = this.Location.X;
                    formToShow.Top = this.Location.Y + 100;
                    formToShow.Show();
                    formToShow.BringToFront();
                }
            }
        }
    }
}
