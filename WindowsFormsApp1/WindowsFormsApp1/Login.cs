using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace GameDesire
{
    public partial class Login : Form
    {
        public static Thread LoginThread;
        public static ScreenCapture sc = new ScreenCapture();
        public static Bitmap LoginHeader = TemplateMatching.generateFormattedBitmap(new Bitmap(GameDesire.Properties.Resources.LoginHeader));
        public static Bitmap Players = TemplateMatching.generateFormattedBitmap(new Bitmap(GameDesire.Properties.Resources.Players));
        public static Bitmap SelectRoomHeader = TemplateMatching.generateFormattedBitmap(new Bitmap(GameDesire.Properties.Resources.SelectRoomHeader));
        public static Bitmap Dark = TemplateMatching.generateFormattedBitmap(new Bitmap(GameDesire.Properties.Resources.DarkMainLobby));
        public static Bitmap Light = TemplateMatching.generateFormattedBitmap(new Bitmap(GameDesire.Properties.Resources.LightMainLobby));

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
                    using (SQLiteDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            textBox1.Text = r.GetString(0);
                            textBox2.Text = r.GetString(1);
                            textBox3.Text = r.GetString(2);
                        }
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

        public bool isLoggedIn(int wait = 0)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            while (true)
            {
                OpenWindows so = new OpenWindows();
                string[] titles = so.GetDesktopWindowsCaptions();

                foreach (string title in titles)
                {
                    if (title.Contains("Main Lobby") && title.Contains("Poker Texas Hold'em"))
                    {
                        return true;
                    }
                }

                if (stopWatch.Elapsed.TotalSeconds > wait)
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
                if ((stopWatch.Elapsed.TotalSeconds > seconds))
                {
                    return false;
                }

                OpenWindows so = new OpenWindows();
                string[] titles = so.GetDesktopWindowsCaptions();

                foreach (string title in titles)
                {
                    if (title == windowText)
                    {
                        return true;
                    }
                }
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public IntPtr GetHandleWindow(string title)
        {
            return FindWindow(null, title);
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

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        public void copyXML()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            path = path + "\\GanymedeNet\\Pokers";
            Directory.CreateDirectory(path);
            File.WriteAllText(path + "\\config.xml", Properties.Resources.config);
        }

        public void killPrevious()
        {
            while (true)
            {
                IntPtr hWnd = GetHandleWindow("Poker");

                if (hWnd == IntPtr.Zero)
                {
                    return;
                }
                else
                {
                    Keyboard.SendClose(hWnd);
                    Thread.Sleep(1000);
                }
            }
        }

        public void LoginToPoker()
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            copyXML();
            killPrevious();

            while (true)
            {
                if (!isLoggedIn())
                {
                    Stopwatch s = new Stopwatch();
                    s.Start();

                    StartPoker();

                    IntPtr hWnd = GetHandleWindow("Poker");
                    MoveWindow(hWnd, 0, 0, 0, 0, true);

                    bool l = TemplateMatching.WaitForElement(hWnd, LoginHeader, 15, new Rectangle(new Point(133 - 20, 192 - 20), new Size(294 + 35, 38 + 35)));

                    if (!l)
                    {
                        TemplateMatching.WaitForElement(hWnd, LoginHeader, 60);
                    }

                    Keyboard.sendKeys(hWnd, username);
                    Keyboard.sendTab(hWnd);
                    Keyboard.sendKeys(hWnd, password);
                    Keyboard.sendEnter(hWnd);

                    bool srh = TemplateMatching.WaitForElement(hWnd, SelectRoomHeader, 15, new Rectangle(new Point(16 - 10, 129 - 20), new Size(101 + 35, 26 + 35)));

                    if (!srh)
                    {
                        TemplateMatching.WaitForElement(hWnd, SelectRoomHeader, 60);
                    }

                    ClickableCoordinate ko2 = TemplateMatching.getClickableCoordinate(hWnd, Players, 15, 0, 0, new Rectangle(new Point(469 - 10, 149 - 20), new Size(50 + 12, 27 + 23)));

                    if (ko2 == null)
                    {
                        ClickableCoordinate login2 = TemplateMatching.getClickableCoordinate(hWnd, Players, 60, 0, 0);
                        Mouse.leftClick(hWnd, login2);
                    }
                    else
                    {
                        Mouse.leftClick(hWnd, ko2);
                    }

                    ClickableCoordinate ko3 = TemplateMatching.getClickableCoordinate(hWnd, Players, 15, 0, 0, new Rectangle(new Point(469 - 10, 149 - 20), new Size(50 + 12, 27 + 23)));

                    if (ko3 == null)
                    {
                        ClickableCoordinate login3 = TemplateMatching.getClickableCoordinate(hWnd, Players, 60, 0, 0);
                        Mouse.leftClick(hWnd, login3);
                    }
                    else
                    {
                        Mouse.leftClick(hWnd, ko3);
                    }

                    ClickableCoordinate dark2 = TemplateMatching.getClickableCoordinate(hWnd, Dark, 10, 0, 0, new Rectangle(new Point(16 - 12, 129 - 20), new Size(600 + 12, 360 + 23)));

                    if (dark2 != null)
                    {
                        Mouse.leftClick(hWnd, dark2);
                    }
                    else
                    {
                        ClickableCoordinate dark = TemplateMatching.getClickableCoordinate(hWnd, Dark, 0, 0, 0);

                        if (dark != null)
                        {
                            Mouse.leftClick(hWnd, dark);
                        }
                        else
                        {
                            ClickableCoordinate light2 = TemplateMatching.getClickableCoordinate(hWnd, Light, 10, 0, 0, new Rectangle(new Point(16 - 12, 129 - 20), new Size(600 + 12, 360 + 23)));

                            if (light2 != null)
                            {
                                Mouse.leftClick(hWnd, light2);
                            }
                            else
                            {
                                ClickableCoordinate light = TemplateMatching.getClickableCoordinate(hWnd, Light, 0, 0, 0);

                                if (light != null)
                                {
                                    Mouse.leftClick(hWnd, light);
                                }
                            }
                        }
                    }

                    Keyboard.sendEnter(hWnd);

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