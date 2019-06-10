using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public static ArrayList players = new ArrayList();
        public static ArrayList l = new ArrayList();

        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();
            Task.Factory.StartNew(this.Refresh);

            comboBox3.SelectedItem = Global.RefreshType;
            comboBox4.SelectedItem = Global.RefreshAmount;
            textBox1.Text = Global.valueOfAMillionInEuro.ToString();
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void deinitialize()
        {

            label21.Text = "0";
            label22.Text = "0";
            label23.Text = "0";
            label24.Text = "0";
            label25.Text = "0";
            label26.Text = "0";
            label27.Text = "0";
            label28.Text = "0";
            label29.Text = "0";
            label30.Text = "0";
            label31.Text = "0";
            label32.Text = "0";
            label33.Text = "0";
            label34.Text = "0";
            label35.Text = "0";
            label36.Text = "0";
            label37.Text = "0";
            label38.Text = "0";
            label39.Text = "0";
            label40.Text = "0";
            label41.Text = "0";
            label42.Text = "0";
            label43.Text = "0";
            label44.Text = "0";
            label45.Text = "0";
            label46.Text = "0";
            label47.Text = "0";
            label48.Text = "0";
            label49.Text = "0";
            label50.Text = "0";
            label51.Text = "0";
            label52.Text = "0";


            //
            label57.Text = "";
            label70.Text = "";
            label72.Text = "";
            label91.Text = "";

            label60.Text = "";
            label73.Text = "";
            label74.Text = "";
            label93.Text = "";
            label61.Text = "";
            label75.Text = "";
            label76.Text = "";
            label94.Text = "";
            label62.Text = "";
            label77.Text = "";
            label78.Text = "";
            label95.Text = "";
            label63.Text = "";
            label79.Text = "";
            label80.Text = "";
            label96.Text = "";
            label64.Text = "";
            label81.Text = "";
            label82.Text = "";
            label97.Text = "";
            label65.Text = "";
            label83.Text = "";
            label84.Text = "";
            label98.Text = "";
            label66.Text = "";
            label85.Text = "";
            label86.Text = "";
            label99.Text = "";
            label67.Text = "";
            label87.Text = "";
            label88.Text = "";
            label100.Text = "";
            label68.Text = "";
            label89.Text = "";
            label92.Text = "";
            label101.Text = "";

            label105.Text = "0";
            label51.Text = "0 (0 per hour)";
            label52.Text = "0";
            label115.Text = "€0.00 (€0.00 per hour)";
            label108.Text = "0";
            label109.Text = "0";

        }

        private void Parse()
        {
            deinitialize();

            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\GameDesire";

            if (!Directory.Exists(filepath))
            {
                return;
            }

            l = new ArrayList();

            foreach (var file in new DirectoryInfo(filepath).GetFiles("*.txt"))
            {
                if (file.ToString().Contains("Poker Texas Hold'em") && !file.ToString().Contains("Tournament"))
                {
                    Hand h = new Hand();

                    foreach (var line in File.ReadLines(file.FullName))
                    {
                        if (line.Contains("GameDesire Game #"))
                        {
                            if (h.Lines.Count == 0)
                            {
                                h.Lines.Add(line);
                            }
                            else
                            {
                                l.Add(h);
                                h = new Hand();
                                h.Lines.Add(line);
                            }
                        }
                        else
                        {
                            h.Lines.Add(line);
                        }
                    }
                    l.Add(h);
                }
            }

            players = new ArrayList();

            foreach (Hand h in l)
            {
                foreach (string s in h.Lines)
                {
                    if (s.Contains("Dealt to"))
                    {
                        string[] splitted = s.Split(' ');
                        string toRemove = " " + splitted[splitted.Length - 2] + " " + splitted[splitted.Length - 1];
                        string toRemove2 = "Dealt to ";

                        string nickName = s.Replace(toRemove, "");
                        nickName = nickName.Replace(toRemove2, "");
                        h.Player = nickName;

                        if (!players.Contains(nickName))
                        {
                            players.Add(nickName);
                        }

                        h.Card1 = splitted[splitted.Length - 2].Replace("[", "");
                        h.Card2 = splitted[splitted.Length - 1].Replace("]", "");
                    }
                }
            }

            ArrayList temp = new ArrayList();
            foreach (Hand h in l)
            {
                if (h.Player != null)
                {
                    temp.Add(h);
                }
            }
            l = temp;

            foreach (Hand h in l)
            {
                bool isFlop = false;
                bool isTurn = false;
                bool isRiver = false;

                foreach (string s in h.Lines)
                {
                    if (s.Contains("GameDesire Game #"))
                    {
                        string[] sa = s.Split(' ');
                        h.ID = sa[2];
                        h.Stake = sa[8];
                        h.Table = sa[4];
                        h.dt = DateTime.ParseExact((sa[17] + " " + sa[14]).ToString(), "yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                    }

                    if (s.Contains("*** FLOP ***"))
                    {
                        isFlop = true;
                        string[] splitted = s.Split(' ');
                        h.flopCard1 = splitted[splitted.Length - 3].Replace("[", "");
                        h.flopCard2 = splitted[splitted.Length - 2];
                        h.flopCard3 = splitted[splitted.Length - 1].Replace("]", "");
                    }
                    else if (s.Contains("*** TURN ***"))
                    {
                        isFlop = false;
                        isTurn = true;
                        string[] splitted = s.Split(' ');
                        h.turnCard = splitted[splitted.Length - 1].Replace("[", "").Replace("]", "");
                    }
                    else if (s.Contains("*** RIVER ***"))
                    {
                        isFlop = false;
                        isTurn = false;
                        isRiver = true;
                        string[] splitted = s.Split(' ');
                        h.riverCard = splitted[splitted.Length - 1].Replace("[", "").Replace("]", "");
                    }

                    //blinds
                    //  Vit_M posts the big blind of $10
                    // Vit_M posts the small blind of $5
                    // Apocalypsae posts $10
                    // $$gr @cz$$ posts a dead small blind of $100
                    if (s.Contains(h.Player + " posts the big blind of $") || s.Contains(h.Player + " posts a dead big blind of $"))
                    {
                        string[] split = s.Split(' ');
                        h.investments = h.investments + Convert.ToInt64(split[split.Length - 1].Replace("$", ""));

                        Action a = new Action();
                        a.Street = "PF";
                        a.Type = "BLIND";
                        a.Amount = Convert.ToInt64(split[split.Length - 1].Replace("$", ""));

                        h.Actions.Add(a);
                    }
                    else if (s.Contains(h.Player + " posts the small blind of $") || s.Contains(h.Player + " posts a dead small blind of $"))
                    {
                        string[] split = s.Split(' ');
                        h.investments = h.investments + Convert.ToInt64(split[split.Length - 1].Replace("$", ""));

                        Action a = new Action();
                        a.Street = "PF";
                        a.Type = "BLIND";
                        a.Amount = Convert.ToInt64(split[split.Length - 1].Replace("$", ""));

                        h.Actions.Add(a);
                    }
                    else if (s.Contains(h.Player + " posts $"))
                    {
                        string[] split = s.Split(' ');
                        h.investments = h.investments + Convert.ToInt64(split[split.Length - 1].Replace("$", ""));

                        Action a = new Action();
                        a.Street = "PF";
                        a.Type = "BLIND";
                        a.Amount = Convert.ToInt64(split[split.Length - 1].Replace("$", ""));
                        h.Actions.Add(a);
                    }


                    //actions
                    //krzysztof156 calls $10
                    //Guest2190139551 calls $31, and is all-in

                    //Apocalypsae checks

                    //Apocalypsae raises to $20
                    //Omar Cabrera82 raises to $821, and is all-in

                    //Apocalypsae bets $10
                    //SaoPaolo bets $493, and is all -in

                    if (s.Contains(h.Player + " calls $"))
                    {
                        string when = "PF";
                        if (isFlop)
                        {
                            when = "F";
                        }
                        else if (isTurn)
                        {
                            when = "T";
                        }
                        else if (isRiver)
                        {
                            when = "R";
                        }

                        //Guest2190139551 calls $31, and is all-in
                        string[] split = s.Split(' ');
                        Action a = new Action();
                        a.Street = when;
                        a.Type = "CALL";


                        if (!s.Contains(", and is all-in")) {
                            a.Amount = Convert.ToInt64(split[split.Length - 1].Replace("$", ""));
                        } else
                        {
                            a.Amount = Convert.ToInt64(split[split.Length - 4].Replace("$", "").Replace(",", ""));
                        }

                        h.investments = h.investments + a.Amount;

                        h.Actions.Add(a);
                    }
                    else if (s.Contains(h.Player + " raises to $"))
                    {
                        string when = "PF";
                        if (isFlop)
                        {
                            when = "F";
                        }
                        else if (isTurn)
                        {
                            when = "T";
                        }
                        else if (isRiver)
                        {
                            when = "R";
                        }

                        string[] split = s.Split(' ');
                        Action a = new Action();
                        a.Street = when;
                        a.Type = "RAISE";

                        if (!s.Contains(", and is all-in"))
                        {
                            a.Amount = Convert.ToInt64(split[split.Length - 1].Replace("$", ""));
                        }
                        else
                        {
                            a.Amount = Convert.ToInt64(split[split.Length - 4].Replace("$", "").Replace(",", ""));
                        }

                        if (h.Actions.Count > 0)
                        {
                            h.investments = h.investments + a.Amount;

                            Action ck = new Action();

                            foreach (Action b in h.Actions)
                            {
                                if (b.Street == a.Street)
                                {
                                    ck = b;
                                }
                            }

                            h.investments = h.investments - ck.Amount;


                            h.Actions.Add(a);
                        }
                        else
                        {
                            h.investments = h.investments + a.Amount;
                            h.Actions.Add(a);
                        }

                    }
                    else if (s.Contains(h.Player + " checks"))
                    {
                        string when = "PF";
                        if (isFlop)
                        {
                            when = "F";
                        }
                        else if (isTurn)
                        {
                            when = "T";
                        }
                        else if (isRiver)
                        {
                            when = "R";
                        }
                    }
                    else if (s.Contains(h.Player + " bets $"))
                    {
                        string when = "PF";
                        if (isFlop)
                        {
                            when = "F";
                        }
                        else if (isTurn)
                        {
                            when = "T";
                        }
                        else if (isRiver)
                        {
                            when = "R";
                        }

                        bool isAllIn = false;

                        if (s.Contains(", and is all-in"))
                        {
                            isAllIn = true;
                        }

                        if (!isAllIn)
                        {
                            string[] split = s.Split(' ');
                            Action a = new Action();
                            a.Street = when;
                            a.Type = "BET";
                            a.Amount = Convert.ToInt64(split[split.Length - 1].Replace("$", ""));


                            Action o = (Action)h.Actions[h.Actions.Count - 1];

                            Int64 Difference = 0;

                            if (o.Street == a.Street && o.Type == "BLIND")
                            {
                                Difference = Math.Abs(a.Amount - o.Amount);
                            }
                            else
                            {
                                Difference = a.Amount;
                            }

                            h.investments = h.investments + Difference;

                            h.Actions.Add(a);
                        }
                        else
                        {
                            string[] split = s.Split(' ');
                            Action a = new Action();
                            a.Street = when;
                            a.Type = "BET";
                            a.Amount = Convert.ToInt64(split[split.Length - 4].Replace("$", "").Replace(",", ""));

                            Action o = (Action)h.Actions[h.Actions.Count - 1];

                            Int64 Difference = 0;

                            if (o.Street == a.Street && o.Type == "BLIND")
                            {
                                Difference = Math.Abs(a.Amount - o.Amount);
                            }
                            else
                            {
                                Difference = a.Amount;
                            }

                            h.investments = h.investments + Difference;

                            h.Actions.Add(a);
                        }

                    }

                    //WINNINGS
                    //Guest2190139551 wins the main pot ($363) with a straight, Ten high
                    //vulcano- wins the side pot ($1344) with a pair of Treys
                    //Uncalled bet of $940 returned to Apocalypsae
                    //vulcano- wins the pot ($2082) with a pair of Tens
                    //SaoPaolo wins the main pot ($884) with a flush, Ace high
                    //SaoPaolo wins the side pot #2 ($7121) with three of a kind, Aces
                    //SaoPaolo wins the side pot #1 ($1052) with three of a kind, Aces
                    if (s.Contains(h.Player + " wins the main pot ($"))
                    {
                        int lenght = 5 + h.Player.Split(' ').Length - 1;

                        string[] splt = s.Split(' ');
                        string dd = splt[lenght].Replace(" ", "");
                        dd = dd.Replace("$", "");
                        dd = dd.Replace("(", "");
                        dd = dd.Replace(")", "");

                        h.winnings = h.winnings + Convert.ToInt64(dd);
                    }
                    else if (s.Contains(h.Player + " wins the side pot ($"))
                    {
                        //SaoPaolo wins the side pot ($3706) with a flush, Ace high
                        int lenght = 5 + h.Player.Split(' ').Length - 1;

                        string[] splt = s.Split(' ');
                        string dd = splt[lenght];
                        dd = dd.Replace("$", "");
                        dd = dd.Replace("(", "");
                        dd = dd.Replace(")", "");
                        dd = dd.Replace(",", "");

                        h.winnings = h.winnings + Convert.ToInt64(dd);
                    }
                    else if (s.Contains("Uncalled bet of $") && s.Contains(h.Player))
                    {
                        string[] splt = s.Split(' ');
                        string dd = splt[3];
                        dd = dd.Replace("$", "");

                        h.winnings = h.winnings + Convert.ToInt64(dd);
                    }
                    else if (s.Contains(h.Player + " wins the pot ($"))
                    {
                        string split = s;
                        split = split.Replace(h.Player, "");
                        split = split.Replace("wins the pot", "");
                        string[] ss = split.Split(')');
                        string dd = ss[0].Replace(" ", "");
                        dd = dd.Replace("$", "");
                        dd = dd.Replace(")", "");
                        dd = dd.Replace("(", "");
                        dd = dd.Replace(",", "");

                        h.winnings = h.winnings + Convert.ToInt64(dd);
                    }
                    else if (s.Contains(h.Player + " wins the side pot #"))
                    {
                        int lenght = 6 + h.Player.Split(' ').Length - 1;

                        string[] splt = s.Split(' ');
                        string dd = splt[lenght];
                        dd = dd.Replace("$", "");
                        dd = dd.Replace("(", "");
                        dd = dd.Replace(")", "");
                        dd = dd.Replace(",", "");

                        h.winnings = h.winnings + Convert.ToInt64(dd);
                    }

                }
            }

            foreach (Hand h in l)
            {
                h.result = h.winnings - h.investments;
            }

            Int64 totalResult = 0;
            foreach (Hand h in l)
            {
                totalResult = totalResult + h.result;
            }

            InitializeElements();
        }

        private Int64 StringToNumber(string number)
        {
            Int64 n = 0;

            number = number.Replace(",", "");

            n = Convert.ToInt64(number);
            return n;
        }

        private void InitializeElements()
        {
            Statistics All = new Statistics("ALL");
            Statistics Stake1 = new Statistics("$5/$10");
            Statistics Stake2 = new Statistics("$10/$20");
            Statistics Stake3 = new Statistics("$25/$50");
            Statistics Stake4 = new Statistics("$50/$100");
            Statistics Stake5 = new Statistics("$100/$200");
            Statistics Stake6 = new Statistics("$250/$500");
            Statistics Stake7 = new Statistics("$500/$1000");
            Statistics Stake8 = new Statistics("$1000/$2000");
            Statistics Stake9 = new Statistics("$2500/$5000");
            Statistics Stake10 = new Statistics("$5000/$10000");
            Statistics Stake11 = new Statistics("$10000/$20000");
            Statistics Stake12 = new Statistics("$25000/$50000");
            Statistics Stake13 = new Statistics("$50000/$100000");
            Statistics Stake14 = new Statistics("$100000/$200000");
            Statistics Stake15 = new Statistics("$250000/$500000");

            foreach (Hand h in l)
            {
                All.Hands = All.Hands + 1;
                All.Result = All.Result + h.result;

                switch (h.Stake)
                {
                    case "$5/$10":
                        Stake1.Hands = Stake1.Hands + 1;
                        Stake1.Result = Stake1.Result + h.result;
                        break;
                    case "$10/$20":
                        Stake2.Hands = Stake2.Hands + 1;
                        Stake2.Result = Stake2.Result + h.result;
                        break;
                    case "$25/$50":
                        Stake3.Hands = Stake3.Hands + 1;
                        Stake3.Result = Stake3.Result + h.result;
                        break;
                    case "$50/$100":
                        Stake4.Hands = Stake4.Hands + 1;
                        Stake4.Result = Stake4.Result + h.result;
                        break;
                    case "$100/$200":
                        Stake5.Hands = Stake5.Hands + 1;
                        Stake5.Result = Stake5.Result + h.result;
                        break;
                    case "$250/$500":
                        Stake6.Hands = Stake6.Hands + 1;
                        Stake6.Result = Stake6.Result + h.result;
                        break;
                    case "$500/$1000":
                        Stake7.Hands = Stake7.Hands + 1;
                        Stake7.Result = Stake7.Result + h.result;
                        break;
                    case "$1000/$2000":
                        Stake8.Hands = Stake8.Hands + 1;
                        Stake8.Result = Stake8.Result + h.result;
                        break;
                    case "$2500/$5000":
                        Stake9.Hands = Stake9.Hands + 1;
                        Stake9.Result = Stake9.Result + h.result;
                        break;
                    case "$5000/$10000":
                        Stake10.Hands = Stake10.Hands + 1;
                        Stake10.Result = Stake10.Result + h.result;
                        break;
                    case "$10000/$20000":
                        Stake11.Hands = Stake11.Hands + 1;
                        Stake11.Result = Stake11.Result + h.result;
                        break;
                    case "$25000/$50000":
                        Stake12.Hands = Stake12.Hands + 1;
                        Stake12.Result = Stake12.Result + h.result;
                        break;
                    case "$50000/$100000":
                        Stake13.Hands = Stake13.Hands + 1;
                        Stake13.Result = Stake13.Result + h.result;
                        break;
                    case "$100000/$200000":
                        Stake14.Hands = Stake14.Hands + 1;
                        Stake14.Result = Stake14.Result + h.result;
                        break;
                    case "$250000/$500000":
                        Stake15.Hands = Stake15.Hands + 1;
                        Stake15.Result = Stake15.Result + h.result;
                        break;
                }
            }

            //set
            label49.Text = Stake1.Hands.ToString("N0");
            label50.Text = Stake1.Result.ToString("N0");
            if(Stake1.Result >= 0) {
                label50.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                label50.ForeColor = System.Drawing.Color.Maroon;
            }

            label47.Text = Stake2.Hands.ToString("N0");
            label48.Text = Stake2.Result.ToString("N0");
            if (Stake2.Result >= 0)
            {
                label48.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                label48.ForeColor = System.Drawing.Color.Maroon;
            }

            label45.Text = Stake3.Hands.ToString("N0");
            label46.Text = Stake3.Result.ToString("N0");

            if (Stake3.Result >= 0)
            {
                label46.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                label46.ForeColor = System.Drawing.Color.Maroon;
            }

            label43.Text = Stake4.Hands.ToString("N0");
            label44.Text = Stake4.Result.ToString("N0");

            if (Stake4.Result >= 0)
            {
                label44.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                label44.ForeColor = System.Drawing.Color.Maroon;
            }


            label41.Text = Stake5.Hands.ToString("N0");
            label42.Text = Stake5.Result.ToString("N0");

            if (Stake5.Result >= 0)
            {
                label42.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                label42.ForeColor = System.Drawing.Color.Maroon;
            }


            label39.Text = Stake6.Hands.ToString("N0");
            label40.Text = Stake6.Result.ToString("N0");
            if (Stake6.Result >= 0)
            {
                label40.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                label40.ForeColor = System.Drawing.Color.Maroon;
            }
            label37.Text = Stake7.Hands.ToString("N0");
            label38.Text = Stake7.Result.ToString("N0");
            if (Stake7.Result >= 0)
            {
                label38.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                label38.ForeColor = System.Drawing.Color.Maroon;
            }
            label35.Text = Stake8.Hands.ToString("N0");
            label36.Text = Stake8.Result.ToString("N0");
            if (Stake8.Result >= 0)
            {
                label36.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                label36.ForeColor = System.Drawing.Color.Maroon;
            }
            label33.Text = Stake9.Hands.ToString("N0");
            label34.Text = Stake9.Result.ToString("N0");
            if (Stake9.Result >= 0)
            {
                label34.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                label34.ForeColor = System.Drawing.Color.Maroon;
            }
            label31.Text = Stake10.Hands.ToString("N0");
            label32.Text = Stake10.Result.ToString("N0");
            if (Stake10.Result >= 0)
            {
                label32.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                label32.ForeColor = System.Drawing.Color.Maroon;
            }
            label29.Text = Stake11.Hands.ToString("N0");
            label30.Text = Stake11.Result.ToString("N0");
            if (Stake11.Result >= 0)
            {
                label30.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                label30.ForeColor = System.Drawing.Color.Maroon;
            }
            label27.Text = Stake12.Hands.ToString("N0");
            label28.Text = Stake12.Result.ToString("N0");
            if (Stake12.Result >= 0)
            {
                label28.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                label28.ForeColor = System.Drawing.Color.Maroon;
            }
            label25.Text = Stake13.Hands.ToString("N0");
            label26.Text = Stake13.Result.ToString("N0");

            if (Stake13.Result >= 0)
            {
                label26.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                label26.ForeColor = System.Drawing.Color.Maroon;
            }

            label23.Text = Stake14.Hands.ToString("N0");
            label24.Text = Stake14.Result.ToString("N0");

            if (Stake14.Result >= 0)
            {
                label24.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                label24.ForeColor = System.Drawing.Color.Maroon;
            }

            label21.Text = Stake15.Hands.ToString("N0");
            label22.Text = Stake15.Result.ToString("N0");

            if (Stake15.Result >= 0)
            {
                label22.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                label22.ForeColor = System.Drawing.Color.Maroon;
            }

            label51.Text = All.Hands.ToString("N0");
            label52.Text = All.Result.ToString("N0");

            if (All.Result >= 0)
            {
                label52.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else
            {
                label52.ForeColor = System.Drawing.Color.Maroon;
            }

            var list = l.Cast<Hand>().ToList();
            var sortedList = list.OrderByDescending(si => si.dt).ToList();


            //cards table;
            for (int i = 0; i < 10; i++)
            {
                switch (i)
                {
                    case 0:
                        try
                        {
                            if (sortedList[i] != null)
                            {
                                label57.Text = sortedList[i].Card1 + sortedList[i].Card2;
                                label70.Text = sortedList[i].dt.ToString();
                                label72.Text = sortedList[i].flopCard1 + sortedList[i].flopCard2 + sortedList[i].flopCard3 + " " + sortedList[i].turnCard + " " + sortedList[i].riverCard;
                                label91.Text = sortedList[i].result.ToString("N0");

                                if (sortedList[i].result >= 0)
                                {
                                    label91.ForeColor = System.Drawing.Color.ForestGreen;
                                }
                                else
                                {
                                    label91.ForeColor = System.Drawing.Color.Maroon;
                                }


                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        break;
                    case 1:
                        try
                        {
                            if (sortedList[i] != null)
                            {
                                label60.Text = sortedList[i].Card1 + sortedList[i].Card2;
                                label73.Text = sortedList[i].dt.ToString();
                                label74.Text = sortedList[i].flopCard1 + sortedList[i].flopCard2 + sortedList[i].flopCard3 + " " + sortedList[i].turnCard + " " + sortedList[i].riverCard;
                                label93.Text = sortedList[i].result.ToString("N0");

                                if (sortedList[i].result >= 0)
                                {
                                    label93.ForeColor = System.Drawing.Color.ForestGreen;
                                }
                                else
                                {
                                    label93.ForeColor = System.Drawing.Color.Maroon;
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        break;
                    case 2:
                        try
                        {
                            if (l[i] != null)
                            {
                                label61.Text = sortedList[i].Card1 + sortedList[i].Card2;
                                label75.Text = sortedList[i].dt.ToString();
                                label76.Text = sortedList[i].flopCard1 + sortedList[i].flopCard2 + sortedList[i].flopCard3 + " " + sortedList[i].turnCard + " " + sortedList[i].riverCard;
                                label94.Text = sortedList[i].result.ToString("N0");

                                if (sortedList[i].result >= 0)
                                {
                                    label94.ForeColor = System.Drawing.Color.ForestGreen;
                                }
                                else
                                {
                                    label94.ForeColor = System.Drawing.Color.Maroon;
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        break;
                    case 3:
                        try
                        {
                            if (sortedList[i] != null)
                            {
                                label62.Text = sortedList[i].Card1 + sortedList[i].Card2;
                                label77.Text = sortedList[i].dt.ToString();
                                label78.Text = sortedList[i].flopCard1 + sortedList[i].flopCard2 + sortedList[i].flopCard3 + " " + sortedList[i].turnCard + " " + sortedList[i].riverCard;
                                label95.Text = sortedList[i].result.ToString("N0");
                                if (sortedList[i].result >= 0)
                                {
                                    label95.ForeColor = System.Drawing.Color.ForestGreen;
                                }
                                else
                                {
                                    label95.ForeColor = System.Drawing.Color.Maroon;
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        break;
                    case 4:
                        try
                        {
                            if (sortedList[i] != null)
                            {
                                label63.Text = sortedList[i].Card1 + sortedList[i].Card2;
                                label79.Text = sortedList[i].dt.ToString();
                                label80.Text = sortedList[i].flopCard1 + sortedList[i].flopCard2 + sortedList[i].flopCard3 + " " + sortedList[i].turnCard + " " + sortedList[i].riverCard;
                                label96.Text = sortedList[i].result.ToString("N0");
                                if (sortedList[i].result >= 0)
                                {
                                    label96.ForeColor = System.Drawing.Color.ForestGreen;
                                }
                                else
                                {
                                    label96.ForeColor = System.Drawing.Color.Maroon;
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        break;
                    case 5:
                        try
                        {
                            if (sortedList[i] != null)
                            {
                                label64.Text = sortedList[i].Card1 + sortedList[i].Card2;
                                label81.Text = sortedList[i].dt.ToString();
                                label82.Text = sortedList[i].flopCard1 + sortedList[i].flopCard2 + sortedList[i].flopCard3 + " " + sortedList[i].turnCard + " " + sortedList[i].riverCard;
                                label97.Text = sortedList[i].result.ToString("N0");
                                if (sortedList[i].result >= 0)
                                {
                                    label97.ForeColor = System.Drawing.Color.ForestGreen;
                                }
                                else
                                {
                                    label97.ForeColor = System.Drawing.Color.Maroon;
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        break;
                    case 6:
                        try
                        {
                            if (sortedList[i] != null)
                            {
                                label65.Text = sortedList[i].Card1 + sortedList[i].Card2;
                                label83.Text = sortedList[i].dt.ToString();
                                label84.Text = sortedList[i].flopCard1 + sortedList[i].flopCard2 + sortedList[i].flopCard3 + " " + sortedList[i].turnCard + " " + sortedList[i].riverCard;
                                label98.Text = sortedList[i].result.ToString("N0");
                                if (sortedList[i].result >= 0)
                                {
                                    label98.ForeColor = System.Drawing.Color.ForestGreen;
                                }
                                else
                                {
                                    label98.ForeColor = System.Drawing.Color.Maroon;
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        break;
                    case 7:
                        try
                        {
                            if (sortedList[i] != null)
                            {
                                label66.Text = sortedList[i].Card1 + sortedList[i].Card2;
                                label85.Text = sortedList[i].dt.ToString();
                                label86.Text = sortedList[i].flopCard1 + sortedList[i].flopCard2 + sortedList[i].flopCard3 + " " + sortedList[i].turnCard + " " + sortedList[i].riverCard;
                                label99.Text = sortedList[i].result.ToString("N0");
                                if (sortedList[i].result >= 0)
                                {
                                    label99.ForeColor = System.Drawing.Color.ForestGreen;
                                }
                                else
                                {
                                    label99.ForeColor = System.Drawing.Color.Maroon;
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        break;
                    case 8:
                        try
                        {
                            if (sortedList[i] != null)
                            {
                                label67.Text = sortedList[i].Card1 + sortedList[i].Card2;
                                label87.Text = sortedList[i].dt.ToString();
                                label88.Text = sortedList[i].flopCard1 + sortedList[i].flopCard2 + sortedList[i].flopCard3 + " " + sortedList[i].turnCard + " " + sortedList[i].riverCard;
                                label100.Text = sortedList[i].result.ToString("N0");
                                if (sortedList[i].result >= 0)
                                {
                                    label100.ForeColor = System.Drawing.Color.ForestGreen;
                                }
                                else
                                {
                                    label100.ForeColor = System.Drawing.Color.Maroon;
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        break;
                    case 9:
                        try
                        {
                            if (sortedList[i] != null)
                            {
                                label68.Text = sortedList[i].Card1 + sortedList[i].Card2;
                                label89.Text = sortedList[i].dt.ToString();
                                label92.Text = sortedList[i].flopCard1 + sortedList[i].flopCard2 + sortedList[i].flopCard3 + " " + sortedList[i].turnCard + " " + sortedList[i].riverCard;
                                label101.Text = sortedList[i].result.ToString("N0");
                                if (sortedList[i].result >= 0)
                                {
                                    label101.ForeColor = System.Drawing.Color.ForestGreen;
                                }
                                else
                                {
                                    label101.ForeColor = System.Drawing.Color.Maroon;
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        break;
                }
            }


            try
            {
                DateTime one = sortedList[sortedList.Count - 1].dt;
                DateTime two = sortedList[0].dt;

                label105.Text = (two - one).TotalMinutes.ToString("N0");
            }
            catch (Exception ex)
            {

            }

            Int64 largest = 0;
            Int64 smallest = 0;

            foreach (Hand hhand in l)
            {

                if (hhand.result > largest)
                {
                    largest = hhand.result;
                }

                if (hhand.result < smallest)
                {
                    smallest = hhand.result;
                }

            }

            label108.Text = largest.ToString("N0");
            label109.Text = smallest.ToString("N0");


            try
            {
                decimal r = Convert.ToInt64(label51.Text.ToString().Replace(",", ""));
                decimal dura = Convert.ToInt64(label105.Text.ToString().Replace(",", ""));
                r = Decimal.Divide(r, dura) * 60;
                label51.Text = label51.Text + " (" + r.ToString("N0") + ") per hour";
            } catch (Exception ex)
            {


            }
            decimal dura2 = 0;

            try
            {
                decimal r2 = Convert.ToInt64(label52.Text.ToString().Replace(",", ""));
                dura2 = Convert.ToInt64(label105.Text.ToString().Replace(",", ""));
                decimal r3 = Decimal.Divide(r2, dura2) * 60;
                label52.Text = label52.Text + " (" + r3.ToString("N0") + ") per hour";
            } catch (Exception ex)
            {

            }



            decimal resineuro = 0;

            try
            {
                decimal valueOfOneChip = Decimal.Divide(Global.valueOfAMillionInEuro, 1000000);
                string ress = label52.Text;
                string[] ress2 = ress.Split(' ');
                ress = ress2[0].Replace(",", "");

                decimal r2 = Convert.ToInt64(ress);
                resineuro = valueOfOneChip * (decimal)r2;
                label115.Text = "€" + resineuro.ToString("N2");

                if (resineuro >= 0)
                {
                    label115.ForeColor = System.Drawing.Color.ForestGreen;
                }
                else
                {
                    label115.ForeColor = System.Drawing.Color.Maroon;
                }


            } catch (Exception ex)
            {

            }

            try
            {
                string[] conna = label115.Text.Split(' ');
                string resso = conna[0].ToString();
                resso = resso.Replace("€", "");

                decimal okko = Decimal.Parse(resso);

                decimal o = Decimal.Divide(okko, dura2) * 60;
                label115.Text = label115.Text + " (€" + o.ToString("N2") + ") per hour";
            }
            catch (Exception ex)
            {

            }
        }

  

        private async void Refresh()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            while (true)
            {
                Int64 mils = 0;

                if (comboBox3.Text == "Seconds")
                {
                    mils = (Int64)Convert.ToInt64(Global.RefreshAmount) * 1000;
                }
                else if (comboBox3.Text == "Minutes")
                {
                    mils = (Int64)Convert.ToInt64(Global.RefreshAmount) * 60000;
                }
                else
                {
                    mils = (Int64)Convert.ToInt64(Global.RefreshAmount) * 3600000;
                }

                if (Global.Refresh || stopWatch.ElapsedMilliseconds > mils)
                {
                    label59.Text = "Refresh in progress";
                    Task.Factory.StartNew(this.loadingInProgress);

                    comboBox3.Enabled = false;
                    comboBox4.Enabled = false;
                    checkBox1.Enabled = false;
                    button1.Enabled = false;
                    label58.ForeColor = System.Drawing.Color.Gray;
                    label59.ForeColor = System.Drawing.Color.Gray;
                    label53.ForeColor = System.Drawing.Color.Gray;
                    label116.ForeColor = System.Drawing.Color.Gray;
                    label111.ForeColor = System.Drawing.Color.Gray;
                    label112.ForeColor = System.Drawing.Color.Gray;

                    textBox1.Enabled = false;

                    Parse();
                    loadingDone();
                    checkBox1.Enabled = true;
                    label58.ForeColor = System.Drawing.Color.Black;
                    label59.ForeColor = System.Drawing.Color.Black;
                    label53.ForeColor = System.Drawing.Color.Black;
                    label116.ForeColor = System.Drawing.Color.Black;
                    label111.ForeColor = System.Drawing.Color.Black;
                    label112.ForeColor = System.Drawing.Color.Black;

                    textBox1.Enabled = true;

                    Global.Refresh = false;
                    stopWatch.Restart();
                    button1.Enabled = true;
                    if (checkBox1.Checked)
                    {
                        comboBox3.Enabled = true;
                        comboBox4.Enabled = true;
                    }
                    else
                    {
                        comboBox3.Enabled = false;
                        comboBox4.Enabled = false;
                    }

                }

                label59.Text = "Seconds until next refresh: " + ((mils - stopWatch.ElapsedMilliseconds) / 1000).ToString();

                if (Global.restartStopwatch)
                {
                    stopWatch.Restart();
                    Global.restartStopwatch = false;
                }

                if (!Global.RefreshPeriodicalActive)
                {
                    stopWatch.Stop();
                }
                else
                {
                    stopWatch.Start();
                }
            }
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global.restartStopwatch = true;

            Global.RefreshAmount = comboBox4.Text;
        }


        private async void loadingInProgress()
        {
            panel2.Visible = true;
            bool cont = true;
            while (cont)
            {
                Thread.Sleep(1000);


                if (!panel2.Visible)
                {
                    return;
                }
            }
        }

        private void loadingDone()
        {
            panel2.Visible = false;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Global.RefreshPeriodicalActive = true;
            }
            else
            {
                Global.RefreshPeriodicalActive = false;
                Global.restartStopwatch = true;
            }
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global.restartStopwatch = true;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\GameDesire";

            if (Directory.Exists(filepath))
            {
                Directory.Delete(filepath, true);
            }

            deinitialize();

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            Global.valueOfAMillionInEuro = Convert.ToInt64(textBox1.Text);
        }

    }
}
