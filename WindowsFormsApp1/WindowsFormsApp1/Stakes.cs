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
    public partial class Stakes : Form
    {
        public Stakes()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            Task.Factory.StartNew(this.Refresh);
        }

        public class View
        {
            public string Stake { get; set; }
            public string Hands { get; set; }
            public string Result { get; set; }

            public View(string s, string h, string r)
            {
                Stake = s;
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
                        return count.ToString("N0");
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
                Views.Add(new View("5_10", "label49", "label50"));
                Views.Add(new View("10_20", "label47", "label48"));
                Views.Add(new View("25_50", "label45", "label46"));
                Views.Add(new View("50_100", "label43", "label44"));
                Views.Add(new View("100_200", "label41", "label42"));
                Views.Add(new View("250_500", "label39", "label40"));
                Views.Add(new View("500_1K", "label37", "label38"));
                Views.Add(new View("1K_2K", "label35", "label36"));
                Views.Add(new View("2.5K_5K", "label33", "label34"));
                Views.Add(new View("5K_10K", "label31", "label32"));
                Views.Add(new View("10K_20K", "label29", "label30"));
                Views.Add(new View("25K_50K", "label27", "label28"));
                Views.Add(new View("50K_100K", "label25", "label26"));
                Views.Add(new View("100K_200K", "label23", "label24"));
                Views.Add(new View("250K_500K", "label21", "label22"));

                foreach (View v in Views)
                {
                    Label Hands = this.Controls.Find(v.Hands, true).FirstOrDefault() as Label;
                    Hands.Text = getHands(v.Stake);

                    Label Result = this.Controls.Find(v.Result, true).FirstOrDefault() as Label;
                    Result.Text = getResult(v.Stake);
                }

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
            label1.BackColor = Color.FromArgb(82, 138, 140);
            label1.ForeColor = Color.FromArgb(175, 191, 191);

        }

        private void Label21_Click(object sender, EventArgs e)
        {

        }

        private void Label23_Click(object sender, EventArgs e)
        {

        }

        private void Label25_Click(object sender, EventArgs e)
        {

        }

        private void Label27_Click(object sender, EventArgs e)
        {

        }

        private void Label29_Click(object sender, EventArgs e)
        {

        }

        private void Label31_Click(object sender, EventArgs e)
        {

        }

        private void Label33_Click(object sender, EventArgs e)
        {

        }

        private void Label35_Click(object sender, EventArgs e)
        {

        }

        private void Label37_Click(object sender, EventArgs e)
        {

        }

        private void Label39_Click(object sender, EventArgs e)
        {

        }

        private void Label41_Click(object sender, EventArgs e)
        {

        }

        private void Label43_Click(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void Label47_Click(object sender, EventArgs e)
        {

        }

        private void Label19_Click(object sender, EventArgs e)
        {

        }

        private void Label18_Click(object sender, EventArgs e)
        {

        }

        private void Label16_Click(object sender, EventArgs e)
        {

        }

        private void Label17_Click(object sender, EventArgs e)
        {

        }

        private void Label14_Click(object sender, EventArgs e)
        {

        }

        private void Label15_Click(object sender, EventArgs e)
        {

        }

        private void Label20_Click(object sender, EventArgs e)
        {

        }
    }
}
