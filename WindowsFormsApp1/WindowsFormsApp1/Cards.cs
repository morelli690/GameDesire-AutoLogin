using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameDesire
{
    public partial class Cards : Form
    {
        public static List<Range> Ranges = new List<Range>();
        public static List<Page> Pages = new List<Page>();
        public static int ActivePage = 1;
        public static bool ShouldRefresh = false;

        public class Page
        {
            public int PageID { get; set; }
            public List<Range> Ranges = new List<Range>();

            public Page(int pid)
            {
                PageID = pid;
            }
        }

        public class Range
        {
            public string ID { get; set; }
            public Int64 Result { get; set; }
            public Int64 Hands { get; set; }
            public Range(string id)
            {
                ID = id;
                Result = 0;
                Hands = 0;
            }
        }

        public Cards()
        {
            InitializeComponent();
            Task.Factory.StartNew(this.Refresh);
        }

        public string ConvertCard(string card)
        {
            if (card.Contains("A"))
            {
                return "14";
            }
            else if (card.Contains("K"))
            {
                return "13";
            }
            else if (card.Contains("Q"))
            {
                return "12";
            }
            else if (card.Contains("J"))
            {
                return "11";
            }
            else if (card.Contains("T"))
            {
                return "10";
            }
            else if (card.Contains("9"))
            {
                return "9";
            }
            else if (card.Contains("8"))
            {
                return "8";
            }
            else if (card.Contains("7"))
            {
                return "7";
            }
            else if (card.Contains("6"))
            {
                return "6";
            }
            else if (card.Contains("5"))
            {
                return "5";
            }
            else if (card.Contains("4"))
            {
                return "4";
            }
            else if (card.Contains("3"))
            {
                return "3";
            }
            else if (card.Contains("2"))
            {
                return "2";
            }
            return "";
        }

        public void InitializeRanges()
        {
            Ranges.Add(new Range("AA"));
            Ranges.Add(new Range("AKs"));
            Ranges.Add(new Range("AQs"));
            Ranges.Add(new Range("AJs"));
            Ranges.Add(new Range("A10s"));
            Ranges.Add(new Range("A9s"));
            Ranges.Add(new Range("A8s"));
            Ranges.Add(new Range("A7s"));
            Ranges.Add(new Range("A6s"));
            Ranges.Add(new Range("A5s"));
            Ranges.Add(new Range("A4s"));
            Ranges.Add(new Range("A3s"));
            Ranges.Add(new Range("A2s"));

            Ranges.Add(new Range("AKo"));
            Ranges.Add(new Range("KK"));
            Ranges.Add(new Range("KQs"));
            Ranges.Add(new Range("KJs"));
            Ranges.Add(new Range("K10s"));
            Ranges.Add(new Range("K9s"));
            Ranges.Add(new Range("K8s"));
            Ranges.Add(new Range("K7s"));
            Ranges.Add(new Range("K6s"));
            Ranges.Add(new Range("K5s"));
            Ranges.Add(new Range("K4s"));
            Ranges.Add(new Range("K3s"));
            Ranges.Add(new Range("K2s"));

            Ranges.Add(new Range("AQo"));
            Ranges.Add(new Range("KQo"));
            Ranges.Add(new Range("QQ"));
            Ranges.Add(new Range("QJs"));
            Ranges.Add(new Range("Q10s"));
            Ranges.Add(new Range("Q9s"));
            Ranges.Add(new Range("Q8s"));
            Ranges.Add(new Range("Q7s"));
            Ranges.Add(new Range("Q6s"));
            Ranges.Add(new Range("Q5s"));
            Ranges.Add(new Range("Q4s"));
            Ranges.Add(new Range("Q3s"));
            Ranges.Add(new Range("Q2s"));

            Ranges.Add(new Range("AJo"));
            Ranges.Add(new Range("KJo"));
            Ranges.Add(new Range("QJo"));
            Ranges.Add(new Range("JJ"));
            Ranges.Add(new Range("J10s"));
            Ranges.Add(new Range("J9s"));
            Ranges.Add(new Range("J8s"));
            Ranges.Add(new Range("J7s"));
            Ranges.Add(new Range("J6s"));
            Ranges.Add(new Range("J5s"));
            Ranges.Add(new Range("J4s"));
            Ranges.Add(new Range("J3s"));
            Ranges.Add(new Range("J2s"));

            Ranges.Add(new Range("A10o"));
            Ranges.Add(new Range("K10o"));
            Ranges.Add(new Range("Q10o"));
            Ranges.Add(new Range("J10o"));
            Ranges.Add(new Range("1010"));
            Ranges.Add(new Range("109s"));
            Ranges.Add(new Range("108s"));
            Ranges.Add(new Range("107s"));
            Ranges.Add(new Range("106s"));
            Ranges.Add(new Range("105s"));
            Ranges.Add(new Range("104s"));
            Ranges.Add(new Range("103s"));
            Ranges.Add(new Range("102s"));

            Ranges.Add(new Range("A9o"));
            Ranges.Add(new Range("K9o"));
            Ranges.Add(new Range("Q9o"));
            Ranges.Add(new Range("J9o"));
            Ranges.Add(new Range("109o"));
            Ranges.Add(new Range("99"));
            Ranges.Add(new Range("98s"));
            Ranges.Add(new Range("97s"));
            Ranges.Add(new Range("96s"));
            Ranges.Add(new Range("95s"));
            Ranges.Add(new Range("94s"));
            Ranges.Add(new Range("93s"));
            Ranges.Add(new Range("92s"));

            Ranges.Add(new Range("A8o"));
            Ranges.Add(new Range("K8o"));
            Ranges.Add(new Range("Q8o"));
            Ranges.Add(new Range("J8o"));
            Ranges.Add(new Range("108o"));
            Ranges.Add(new Range("98o"));
            Ranges.Add(new Range("88"));
            Ranges.Add(new Range("87s"));
            Ranges.Add(new Range("86s"));
            Ranges.Add(new Range("85s"));
            Ranges.Add(new Range("84s"));
            Ranges.Add(new Range("83s"));
            Ranges.Add(new Range("82s"));

            Ranges.Add(new Range("A7o"));
            Ranges.Add(new Range("K7o"));
            Ranges.Add(new Range("Q7o"));
            Ranges.Add(new Range("J7o"));
            Ranges.Add(new Range("107o"));
            Ranges.Add(new Range("97o"));
            Ranges.Add(new Range("87o"));
            Ranges.Add(new Range("77"));
            Ranges.Add(new Range("76s"));
            Ranges.Add(new Range("75s"));
            Ranges.Add(new Range("74s"));
            Ranges.Add(new Range("73s"));
            Ranges.Add(new Range("72s"));

            Ranges.Add(new Range("A6o"));
            Ranges.Add(new Range("K6o"));
            Ranges.Add(new Range("Q6o"));
            Ranges.Add(new Range("J6o"));
            Ranges.Add(new Range("106o"));
            Ranges.Add(new Range("96o"));
            Ranges.Add(new Range("86o"));
            Ranges.Add(new Range("76o"));
            Ranges.Add(new Range("66"));
            Ranges.Add(new Range("65s"));
            Ranges.Add(new Range("64s"));
            Ranges.Add(new Range("63s"));
            Ranges.Add(new Range("62s"));

            Ranges.Add(new Range("A5o"));
            Ranges.Add(new Range("K5o"));
            Ranges.Add(new Range("Q5o"));
            Ranges.Add(new Range("J5o"));
            Ranges.Add(new Range("105o"));
            Ranges.Add(new Range("95o"));
            Ranges.Add(new Range("85o"));
            Ranges.Add(new Range("75o"));
            Ranges.Add(new Range("65o"));
            Ranges.Add(new Range("55"));
            Ranges.Add(new Range("54s"));
            Ranges.Add(new Range("53s"));
            Ranges.Add(new Range("52s"));

            Ranges.Add(new Range("A4o"));
            Ranges.Add(new Range("K4o"));
            Ranges.Add(new Range("Q4o"));
            Ranges.Add(new Range("J4o"));
            Ranges.Add(new Range("104o"));
            Ranges.Add(new Range("94o"));
            Ranges.Add(new Range("84o"));
            Ranges.Add(new Range("74o"));
            Ranges.Add(new Range("64o"));
            Ranges.Add(new Range("54o"));
            Ranges.Add(new Range("44"));
            Ranges.Add(new Range("43s"));
            Ranges.Add(new Range("42s"));

            Ranges.Add(new Range("A3o"));
            Ranges.Add(new Range("K3o"));
            Ranges.Add(new Range("Q3o"));
            Ranges.Add(new Range("J3o"));
            Ranges.Add(new Range("103o"));
            Ranges.Add(new Range("93o"));
            Ranges.Add(new Range("83o"));
            Ranges.Add(new Range("73o"));
            Ranges.Add(new Range("63o"));
            Ranges.Add(new Range("53o"));
            Ranges.Add(new Range("43o"));
            Ranges.Add(new Range("33"));
            Ranges.Add(new Range("32s"));

            Ranges.Add(new Range("A2o"));
            Ranges.Add(new Range("K2o"));
            Ranges.Add(new Range("Q2o"));
            Ranges.Add(new Range("J2o"));
            Ranges.Add(new Range("102o"));
            Ranges.Add(new Range("92o"));
            Ranges.Add(new Range("82o"));
            Ranges.Add(new Range("72o"));
            Ranges.Add(new Range("62o"));
            Ranges.Add(new Range("52o"));
            Ranges.Add(new Range("42o"));
            Ranges.Add(new Range("32o"));
            Ranges.Add(new Range("22"));


        }

        public void InitilaizePages()
        {
            List<IEnumerable<Range>> listOfLists = new List<IEnumerable<Range>>();

            for (int i = 0; i < Ranges.Count(); i += 13)
            {
                listOfLists.Add(Ranges.Skip(i).Take(13));
            }

            for (int i = 0; i < listOfLists.Count(); i++)
            {
                Page p = new Page(i + 1);

                foreach(Range r in listOfLists[i])
                {
                    p.Ranges.Add(r);
                }

                Pages.Add(p);
            }
        }

        public async void Refresh()
        {
            InitializeRanges();
            comboBox1.SelectedItem = "1";
            InitilaizePages();

            while (true)
            {
                ShouldRefresh = false;

                using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
                {
                    con.Open();

                    using (SQLiteCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "SELECT Card1, Card2, Result FROM Hands";

                        SQLiteDataReader r = cmd.ExecuteReader();

                        while (r.Read())
                        {
                            string card1 = r.GetString(0);
                            string card2 = r.GetString(1);

                            Int64 Result = Convert.ToInt64(r.GetString(2));
                            bool isSuited = false;

                            if ((card1 + card2).Count(ch => ch == 's') == 2)
                            {
                                isSuited = true;
                            }
                            else if ((card1 + card2).Count(ch => ch == 'h') == 2)
                            {
                                isSuited = true;
                            }
                            else if ((card1 + card2).Count(ch => ch == 'd') == 2)
                            {
                                isSuited = true;
                            }
                            else if ((card1 + card2).Count(ch => ch == 'c') == 2)
                            {
                                isSuited = true;
                            }

                            string c1 = ConvertCard(card1);
                            string c2 = ConvertCard(card2);

                            string range = "";

                            if (Convert.ToInt64(c1) >= Convert.ToInt64(c2))
                            {
                                range = card1 + card2;
                            }
                            else
                            {
                                range = card2 + card1;
                            }

                            range = range.Replace("s", "");
                            range = range.Replace("c", "");
                            range = range.Replace("h", "");
                            range = range.Replace("d", "");
                            range = range.Replace("T", "10");

                            range = isSuited ? (range + "s") : (range + "o");

                            if (c1 == c2)
                            {
                                range = range.Replace("o", "");
                            }


                            Range result = Ranges.First(s => s.ID == range);
                            result.Result = result.Result + Result;
                            result.Hands = result.Hands + 1;
                        }
                    }
                    con.Close();
                }

                activatePage(ActivePage);

                foreach (Range r in Ranges)
                {
                    r.Result = 0;
                    r.Hands = 0;
                }

                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                bool wait = true;

                while (wait) {
                    if(stopWatch.Elapsed.TotalSeconds > 5 || ShouldRefresh)
                    {
                        wait = false;
                    }
                }
            }
        }

        private void InitializeRangeElements(int rangeLine, Range r)
        {
            Label Cart = new Label();
            Label Hands = new Label();
            Label Result = new Label();

            if (rangeLine == 1)
            {
                Cart = this.Controls.Find("label1", true).FirstOrDefault() as Label;
                Hands = this.Controls.Find("label21", true).FirstOrDefault() as Label;
                Result = this.Controls.Find("label22", true).FirstOrDefault() as Label;
            }
            else if(rangeLine == 2)
            {
                Cart = this.Controls.Find("label7", true).FirstOrDefault() as Label;
                Hands = this.Controls.Find("label9", true).FirstOrDefault() as Label;
                Result = this.Controls.Find("label20", true).FirstOrDefault() as Label;
            }
            else if (rangeLine == 3)
            {
                Cart = this.Controls.Find("label17", true).FirstOrDefault() as Label;
                Hands = this.Controls.Find("label23", true).FirstOrDefault() as Label;
                Result = this.Controls.Find("label24", true).FirstOrDefault() as Label;

            }
            else if (rangeLine == 4)
            {
                Cart = this.Controls.Find("label18", true).FirstOrDefault() as Label;
                Hands = this.Controls.Find("label25", true).FirstOrDefault() as Label;
                Result = this.Controls.Find("label26", true).FirstOrDefault() as Label;

            }
            else if (rangeLine == 5)
            {
                Cart = this.Controls.Find("label16", true).FirstOrDefault() as Label;
                Hands = this.Controls.Find("label27", true).FirstOrDefault() as Label;
                Result = this.Controls.Find("label28", true).FirstOrDefault() as Label;

            }
            else if (rangeLine == 6)
            {
                Cart = this.Controls.Find("label15", true).FirstOrDefault() as Label;
                Hands = this.Controls.Find("label29", true).FirstOrDefault() as Label;
                Result = this.Controls.Find("label30", true).FirstOrDefault() as Label;

            }
            else if (rangeLine == 7)
            {
                Cart = this.Controls.Find("label14", true).FirstOrDefault() as Label;
                Hands = this.Controls.Find("label31", true).FirstOrDefault() as Label;
                Result = this.Controls.Find("label32", true).FirstOrDefault() as Label;

            }
            else if (rangeLine == 8)
            {
                Cart = this.Controls.Find("label13", true).FirstOrDefault() as Label;
                Hands = this.Controls.Find("label33", true).FirstOrDefault() as Label;
                Result = this.Controls.Find("label34", true).FirstOrDefault() as Label;
            }
            else if (rangeLine == 9)
            {
                Cart = this.Controls.Find("label12", true).FirstOrDefault() as Label;
                Hands = this.Controls.Find("label35", true).FirstOrDefault() as Label;
                Result = this.Controls.Find("label36", true).FirstOrDefault() as Label;
            }
            else if (rangeLine == 10)
            {
                Cart = this.Controls.Find("label11", true).FirstOrDefault() as Label;
                Hands = this.Controls.Find("label37", true).FirstOrDefault() as Label;
                Result = this.Controls.Find("label38", true).FirstOrDefault() as Label;
            }
            else if (rangeLine == 11)
            {
                Cart = this.Controls.Find("label10", true).FirstOrDefault() as Label;
                Hands = this.Controls.Find("label39", true).FirstOrDefault() as Label;
                Result = this.Controls.Find("label40", true).FirstOrDefault() as Label;
            }
            else if (rangeLine == 12)
            {
                Cart = this.Controls.Find("label8", true).FirstOrDefault() as Label;
                Hands = this.Controls.Find("label41", true).FirstOrDefault() as Label;
                Result = this.Controls.Find("label42", true).FirstOrDefault() as Label;
            }
            else if (rangeLine == 13)
            {
                Cart = this.Controls.Find("label9", true).FirstOrDefault() as Label;
                Hands = this.Controls.Find("label43", true).FirstOrDefault() as Label;
                Result = this.Controls.Find("label44", true).FirstOrDefault() as Label;
            }

            try
            {
                Cart.Text = r.ID;
                Hands.Text = r.Hands.ToString("N0");
                Result.Text = r.Result.ToString("N0");
            }
            catch(Exception ex)
            {

            }
        }

        private void activatePage(int PageID)
        {
            foreach(Page p in Pages)
            {
                if(p.PageID == PageID) {
                    int Counter = 1;

                    foreach(Range r in p.Ranges)
                    {
                        foreach(Range rk in Ranges)
                        {
                            if(rk.ID == r.ID)
                            {
                                InitializeRangeElements(Counter, rk);
                                Counter = Counter + 1;
                                break;
                            }
                        }
                    }
                    return;
                }
            }
        }

        private void Label5_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActivePage = Convert.ToInt32(comboBox1.SelectedItem.ToString());
            ShouldRefresh = true;
        }

        private void Label46_Click(object sender, EventArgs e)
        {
            if(ActivePage != 1)
            {
                ActivePage = ActivePage - 1;
                comboBox1.SelectedItem = ActivePage.ToString();
            }
        }

        private void Label50_Click(object sender, EventArgs e)
        {
            if (ActivePage != 13)
            {
                ActivePage = ActivePage + 1;
                comboBox1.SelectedItem = ActivePage.ToString();
            }
        }
    }
}
