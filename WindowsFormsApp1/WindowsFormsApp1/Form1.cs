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
    public partial class Form1 : Form
    {
        public static List<Range> Ranges = new List<Range>();

        public class Range
        {
            public string ID { get; set; }
            public Int64 Result { get; set; }
            public Int64 Hands { get; set; }
            public string HandsLabel { get; set; }
            public string ResultLabel { get; set; }

            public Range(string id, string h, string r)
            {
                ID = id;
                Result = 0;
                Hands = 0;
                HandsLabel = h;
                ResultLabel = r;
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
            Ranges.Add(new Range("AA", "label4", "label7"));
            Ranges.Add(new Range("AKs", "label10", "label9"));
            Ranges.Add(new Range("AQs", "label15", "label14"));
            Ranges.Add(new Range("AJs", "label20", "label19"));
            Ranges.Add(new Range("A10s", "label25", "label24"));
            Ranges.Add(new Range("A9s", "label30", "label29"));
            Ranges.Add(new Range("A8s", "label35", "label34"));
            Ranges.Add(new Range("A7s", "label45", "label44"));
            Ranges.Add(new Range("A6s", "label40", "label39"));
            Ranges.Add(new Range("A5s", "label50", "label49"));
            Ranges.Add(new Range("A4s", "label55", "label54"));
            Ranges.Add(new Range("A3s", "label60", "label59"));
            Ranges.Add(new Range("A2s", "label65", "label64"));

            Ranges.Add(new Range("AKo", "label70", "label69"));
            Ranges.Add(new Range("KK", "label75", "label74"));
            Ranges.Add(new Range("KQs", "label80", "label79"));
            Ranges.Add(new Range("KJs", "label85", "label84"));
            Ranges.Add(new Range("K10s", "label90", "label89"));
            Ranges.Add(new Range("K9s", "label95", "label94"));
            Ranges.Add(new Range("K8s", "label100", "label99"));
            Ranges.Add(new Range("K7s", "label105", "label104"));
            Ranges.Add(new Range("K6s", "label110", "label109"));
            Ranges.Add(new Range("K5s", "label115", "label114"));
            Ranges.Add(new Range("K4s", "label120", "label119"));
            Ranges.Add(new Range("K3s", "label125", "label124"));
            Ranges.Add(new Range("K2s", "label130", "label129"));

            Ranges.Add(new Range("AQo", "label145", "label144"));
            Ranges.Add(new Range("KQo", "label150", "label149"));
            Ranges.Add(new Range("QQ", "label155", "label154"));
            Ranges.Add(new Range("QJs", "label160", "label159"));
            Ranges.Add(new Range("Q10s", "label165", "label164"));
            Ranges.Add(new Range("Q9s", "label170", "label169"));
            Ranges.Add(new Range("Q8s", "label175", "label174"));
            Ranges.Add(new Range("Q7s", "label180", "label179"));
            Ranges.Add(new Range("Q6s", "label185", "label184"));
            Ranges.Add(new Range("Q5s", "label190", "label189"));
            Ranges.Add(new Range("Q4s", "label195", "label194"));
            Ranges.Add(new Range("Q3s", "label200", "label199"));
            Ranges.Add(new Range("Q2s", "label205", "label204"));

            Ranges.Add(new Range("AJo", "label210", "label209"));
            Ranges.Add(new Range("KJo", "label215", "label214"));
            Ranges.Add(new Range("QJo", "label220", "label219"));
            Ranges.Add(new Range("JJ", "label225", "label224"));
            Ranges.Add(new Range("J10s", "label230", "label229"));
            Ranges.Add(new Range("J9s", "label235", "label234"));
            Ranges.Add(new Range("J8s", "label240", "label239"));
            Ranges.Add(new Range("J7s", "label245", "label244"));
            Ranges.Add(new Range("J6s", "label250", "label249"));
            Ranges.Add(new Range("J5s", "label255", "label254"));
            Ranges.Add(new Range("J4s", "label260", "label259"));
            Ranges.Add(new Range("J3s", "label265", "label264"));
            Ranges.Add(new Range("J2s", "label270", "label269"));

            Ranges.Add(new Range("A10o", "label275", "label274"));
            Ranges.Add(new Range("K10o", "label280", "label279"));
            Ranges.Add(new Range("QQo", "label285", "label284"));
            Ranges.Add(new Range("J10o", "label290", "label289"));
            Ranges.Add(new Range("1010", "label295", "label294"));
            Ranges.Add(new Range("T9s", "label300", "label299"));
            Ranges.Add(new Range("T8s", "label305", "label304"));
            Ranges.Add(new Range("T7s", "label310", "label309"));
            Ranges.Add(new Range("T6s", "label315", "label314"));
            Ranges.Add(new Range("T5s", "label320", "label319"));
            Ranges.Add(new Range("T4s", "label325", "label324"));
            Ranges.Add(new Range("T3s", "label330", "label329"));
            Ranges.Add(new Range("T2s", "label335", "label334"));
            /*
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
            Ranges.Add(new Range("55"));
            Ranges.Add(new Range("43s"));
            Ranges.Add(new Range("42s"));
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
            Ranges.Add(new Range("22"));*/
        }

        public void InitializeElements()
        {
            foreach (Range r in Ranges)
            {
                Console.WriteLine(r.ID + ":" + r.Result.ToString() + " : " + r.Hands.ToString());
            }
        }

        public async void Refresh()
        {
            InitializeRanges();

            while (true)
            {
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

                            if (isSuited)
                            {
                                range = range + "s";
                            }
                            else
                            {
                                range = range + "o";
                            }

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

                InitializeElements();

                foreach (Range r in Ranges)
                {
                    r.Result = 0;
                    r.Hands = 0;
                }

                Thread.Sleep(50000);
            }
        }

        private void Cards_Load(object sender, EventArgs e)
        {

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Label5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

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

    }
}
