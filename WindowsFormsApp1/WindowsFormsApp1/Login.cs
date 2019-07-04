﻿using Accord.Imaging;
using Accord.Imaging.Filters;
using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace GameDesire
{
    public partial class Login : Form
    {
        public static Thread LoginThread;
        public static ScreenCapture sc = new ScreenCapture();
        public static Bitmap LoginHeader = TemplateMatching.generateFormattedBitmap(new Bitmap("LoginHeader.png"));
        public static Bitmap Players = TemplateMatching.generateFormattedBitmap(new Bitmap("Players.png"));
        public static Bitmap SelectRoomHeader = TemplateMatching.generateFormattedBitmap(new Bitmap("SelectRoomHeader.png"));
        public static Bitmap Dark = TemplateMatching.generateFormattedBitmap(new Bitmap("DarkMainlobby.png"));
        public static Bitmap Light = TemplateMatching.generateFormattedBitmap(new Bitmap("LightMainLobby.png"));

        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;


        public Login()
        {
            InitializeComponent();
            InitializeElements();
        }

        private void InitializeElements()
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                this.textBox1.TextChanged -= new System.EventHandler(this.TextBox1_TextChanged);
                this.textBox2.TextChanged -= new System.EventHandler(this.TextBox2_TextChanged);
                this.textBox3.TextChanged -= new System.EventHandler(this.TextBox3_TextChanged);

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Authentication";
                    SQLiteDataReader r = cmd.ExecuteReader();

                    while (r.Read())
                    {
                        textBox1.Text = r.GetString(0);
                        textBox2.Text = r.GetString(1);
                        textBox3.Text = r.GetString(3);
                    }
                }
                con.Close();
            }

            this.textBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
            this.textBox2.TextChanged += new System.EventHandler(this.TextBox2_TextChanged);
            this.textBox3.TextChanged += new System.EventHandler(this.TextBox3_TextChanged);
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

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    string sql = "UPDATE Authentication SET Login=@Logi";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SQLiteParameter("@Logi", textBox1.Text));
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    string sql = "UPDATE Authentication SET Password=@Logi";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SQLiteParameter("@Logi", textBox2.Text));
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    string sql = "UPDATE Authentication SET PokerLauncherPath=@Logi";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SQLiteParameter("@Logi", textBox3.Text));
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Title = "Browse for poker launcher",
                Filter = "Executable files (*.exe)|*.exe"
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = openFileDialog1.FileName;
            }
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

        public bool isLoggedIn(int wait=0)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            while (true)
            {
                string[] titles = OpenWindows.GetDesktopWindowsCaptions();

                foreach (string title in titles)
                {
                    if (title.Contains("Main Lobby") && title.Contains("Poker Texas Hold'em"))
                    {
                        return true;
                    }
                }

                if(stopWatch.Elapsed.TotalSeconds > wait)
                {
                    return false;
                }
            }
        }

        public bool waitForWindowToAppear(string windowText, int seconds)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            while (true)
            {
                if( (stopWatch.Elapsed.TotalSeconds > seconds )  )
                {
                    return false;
                }

                string[] titles = OpenWindows.GetDesktopWindowsCaptions();

                foreach (string title in titles)
                {
                    if (title == windowText)
                    {
                        return true;
                    }
                }
            }
        }

        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public IntPtr GetHandleWindow(string title)
        {
            return FindWindow(null, title);
        }

        [DllImport("user32.dll")]
        static extern byte VkKeyScan(char ch);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool PostMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        public void sendKeys(IntPtr H, string keys)
        {
            const Int32 WM_CHAR = 0x0102;

            foreach (char c in keys)
            {
                PostMessage(H, WM_CHAR, (IntPtr)c, IntPtr.Zero);
            }
        }

        public void sendTab(IntPtr H)
        {
            const uint WM_KEYDOWN = 0x0100;
            const uint WM_KEYUP = 0x0101;
            const int VK_TAB = 0x09;

            PostMessage(H, (int)WM_KEYDOWN, (IntPtr)VK_TAB, IntPtr.Zero);
            PostMessage(H, (int)WM_KEYUP, (IntPtr)VK_TAB, IntPtr.Zero);
        }

        public void sendEnter(IntPtr H)
        {
            const uint WM_KEYDOWN = 0x0100;
            const uint WM_KEYUP = 0x0101;
            const int VK_TAB = 0x0D;

            PostMessage(H, (int)WM_KEYDOWN, (IntPtr)VK_TAB, IntPtr.Zero);
            PostMessage(H, (int)WM_KEYUP, (IntPtr)VK_TAB, IntPtr.Zero);
        }


        public int MakeLParam(int LoWord, int HiWord)
        {
            return (int)((HiWord << 16) | (LoWord & 0xFFFF));
        }


        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        public void leftClick(IntPtr Handle, ClickableCoordinate cc)
        {
            PostMessage(Handle, WM_LBUTTONDOWN, 1, MakeLParam(cc.X, cc.Y));
            PostMessage(Handle, WM_LBUTTONUP, 0, MakeLParam(cc.X, cc.Y));
        }

        public void StartPoker()
        {
            int w = 5;

            while (true)
            {
                Process p = Process.Start(textBox3.Text);

                bool wait = waitForWindowToAppear("Poker", w);

                if (wait)
                {
                    return;
                }
                else
                {
                    try
                    {
                        p.Kill();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception #1 " + ex.StackTrace);
                    }

                    w = w + 5;
                }
            }
        }

        // Keeps the main lobby open
        public void LoginToPoker()
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string path = textBox3.Text;

            while (true)
            {
                if (!isLoggedIn())
                {
                    Stopwatch s = new Stopwatch();
                    s.Start();

                    StartPoker();

                    IntPtr hWnd = GetHandleWindow("Poker");

                    TemplateMatching.WaitForElement(hWnd, LoginHeader, 60, new Rectangle(new Point(133-5, 192-5), new Size(294+6, 38+6)));

                    sendKeys(hWnd, username);
                    sendTab(hWnd);
                    sendKeys(hWnd, password);
                    sendEnter(hWnd);

                    TemplateMatching.WaitForElement(hWnd, SelectRoomHeader, 60, new Rectangle(new Point(16 - 5, 129 - 5), new Size(101 + 6, 26 + 6)));
                    TemplateMatching.WaitForElement(hWnd, Players, 60, new Rectangle(new Point(469 - 5, 149 - 5), new Size(50 + 6, 27 + 6)));

                    ClickableCoordinate login2 = TemplateMatching.getClickableCoordinate(hWnd, Players, 60, 0, 0, new Rectangle(new Point(469 - 5, 149 - 5), new Size(50 + 6, 27 + 6)));
                    leftClick(hWnd, login2);

                    login2 = TemplateMatching.getClickableCoordinate(hWnd, Players, 60, 0, 0, new Rectangle(new Point(469 - 5, 149 - 5), new Size(50 + 6, 27 + 6)));
                    leftClick(hWnd, login2);

                    ClickableCoordinate dark = TemplateMatching.getClickableCoordinate(hWnd, Dark, 0, 0, 0, new Rectangle(new Point(16 - 5, 129 - 5), new Size(178 + 6 + 60, 17 + 6 + 200)));

                    if (dark != null)
                    {
                        leftClick(hWnd, dark);
                    }
                    else
                    {
                        ClickableCoordinate light = TemplateMatching.getClickableCoordinate(hWnd, Light, 0, 0, 0, new Rectangle(new Point(16 - 5, 129 - 5), new Size(178 + 6 + 60, 17 + 6 + 200)));

                        if (light != null)
                        {
                            leftClick(hWnd, light);
                        }
                    }

                    sendEnter(hWnd);

                    bool b = isLoggedIn(30);

                }
            }
        }

        private void CheckBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                LoginThread = new Thread(LoginToPoker);
                LoginThread.Start();
            }
            else
            {
                LoginThread.Abort();
            }
        }
    }
}