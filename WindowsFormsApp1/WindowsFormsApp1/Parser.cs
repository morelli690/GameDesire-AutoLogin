using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace WindowsFormsApp1
{
    class Parser
    {
        public class FileInformation
        {
            public string Username { get; set; }
            public string TableName { get; set; }
            public string Stake { get; set; }
        }
        public static FileInformation GetFileInformation(string fileName)
        {
            var r = fileName.Split(new string[] { " - " }, StringSplitOptions.None).ToList();

            string Table = r[r.Count - 2];
            string Stake = r[r.Count - 3];

            r.RemoveAt(0);
            r.RemoveAt(r.Count - 1);
            r.RemoveAt(r.Count - 1);
            r.RemoveAt(r.Count - 1);

            List<int> t = new List<int>();

            for (var i = 0; i < fileName.Length; i++)
            {
                if(fileName[i] == '-')
                {
                    t.Add(i);
                }
            }

            int first = fileName.IndexOf('-') + 2;
            int two = t[t.Count-3]-1;

            string username = fileName.Substring(first, (two - first));

            return new FileInformation{ Username = username, TableName = Table, Stake = Stake };
        }
        public static bool ShouldParseFile(string fileName)
        {
            var r = fileName.Split(new string[] { " - " }, StringSplitOptions.None).ToList();

            if (r[r.Count - 1].ToString() == "Poker Texas Hold'em.txt" && !fileName.Contains("#"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static List<Hand2> Hands = new List<Hand2>();
        public class Hand2
        {
            public string Player { get; set; }
            public string Table { get; set; }
            public string ID { get; set; }
            public string Stake { get; set; }
            public DateTime dt { get; set; }
            public string Card1 { get; set; }
            public string Card2 { get; set; }
            public string flopCard1 { get; set; }
            public string flopCard2 { get; set; }
            public string flopCard3 { get; set; }
            public string turnCard { get; set; }
            public string riverCard { get; set; }
            public Int64 result { get; set; }
        }
        public class Action2
        {
                public string Type { get; set; }
                public Int64 Amount { get; set; }
                public string Street { get; set; }

            public Action2(string type, Int64 amount, string street)
            {
                Type = type;
                Amount = amount;
                Street = street;
            }
        }

        public static void Parse()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            foreach (FileInfo FileInformation in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\GameDesire").GetFiles("*.txt"))
            {
                if (ShouldParseFile(FileInformation.ToString()))
                {
                    FileInformation FI = GetFileInformation(FileInformation.ToString());
                    Hand2 hand = new Hand2();
                    string street2 = "PF";
                    ArrayList Actions = new ArrayList();
                    bool takingPart = false;

                    foreach (string line in File.ReadLines(FileInformation.FullName))
                    {
                        if (line.Length > 9 && line.Substring(0, 10) == "GameDesire")
                        {
                            if (hand.ID != null)
                            {
                                if (takingPart)
                                {
                                    Hands.Add(hand);
                                }

                                hand = new Hand2();
                                street2 = "PF";
                                Actions = new ArrayList();
                                takingPart = false;
                            }

                            hand.ID = line.Split(' ')[2].Replace("#", "").Replace(":", "");
                            hand.Stake = FI.Stake;
                            hand.Table = FI.TableName;
                            hand.Player = FI.Username;

                            string[] sa = line.Split(' ');
                            hand.dt = DateTime.ParseExact((sa[17] + " " + sa[14]).ToString(), "yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        }
                        else if (line.Length > 4 && line.Substring(0, 5) == "Dealt")
                        {
                            string[] splitted = line.Split(' ');

                            hand.Card1 = splitted[splitted.Length - 2].Replace("[", "");
                            hand.Card2 = splitted[splitted.Length - 1].Replace("]", "");
                            takingPart = true;
                        }
                        else if (line.Length > 3 && line.Substring(0, 4) == "*** ")
                        {
                            string t = line.Substring(4, 1);
                            string[] splitted = line.Split(' ');

                            if (t == "F")
                            {
                                street2 = t;
                                hand.flopCard1 = splitted[splitted.Length - 3].Replace("[", "");
                                hand.flopCard2 = splitted[splitted.Length - 2];
                                hand.flopCard3 = splitted[splitted.Length - 1].Replace("]", "");
                            }
                            else if (t == "T")
                            {
                                street2 = t;
                                hand.turnCard = splitted[splitted.Length - 1].Replace("[", "").Replace("]", "");
                            }
                            else if (t == "R")
                            {
                                street2 = t;
                                hand.riverCard = splitted[splitted.Length - 1].Replace("[", "").Replace("]", "");
                            }
                        }
                        else if (line.Length > hand.Player.Length && line.Substring(0, hand.Player.Length) == hand.Player)
                        {
                            string[] split = line.Split(' ');
                            string act = line.Substring(hand.Player.Length + 1, (line.Length - (hand.Player.Length + 1)));

                            if (act.Contains("posts the big blind of $") || act.Contains("posts a dead big blind of $") || act.Contains("posts the small blind of $") || act.Contains("posts a dead small blind of $") || act.Contains("posts $"))
                            {
                                hand.result = hand.result - Convert.ToInt64(split[split.Length - 1].Replace("$", ""));
                                Actions.Add(new Action2(type: "BLIND", street: street2, amount: Convert.ToInt64(split[split.Length - 1].Replace("$", ""))));
                            }
                            else if (act.Contains("calls $"))
                            {
                                Int64 nr = ((!act.Contains(", and is all-in")) ? Convert.ToInt64(split[split.Length - 1].Replace("$", "")) : Convert.ToInt64(split[split.Length - 4].Replace("$", "").Replace(",", "")));
                                hand.result = hand.result - nr;
                                Actions.Add(new Action2(type: "CALL", street: street2, amount:nr));
                            }
                            else if (act.Contains("raises to $"))
                            {
                                Int64 nr = ((!act.Contains(", and is all-in")) ? Convert.ToInt64(split[split.Length - 1].Replace("$", "")) : Convert.ToInt64(split[split.Length - 4].Replace("$", "").Replace(",", "")));
                                hand.result = hand.result - nr;

                                if (Actions.Count > 0)
                                {
                                    Int64 amount = 0;

                                    foreach (Action2 b in Actions)
                                    {
                                        if (b.Street == street2)
                                        {
                                            amount = b.Amount;
                                        }
                                    }
                                    hand.result = hand.result + amount;
                                }
                                Actions.Add(new Action2(type: "RAISE", street: street2, amount: nr));
                            }
                            else if (act.Contains("bets $"))
                            {
                                Int64 nr = !act.Contains(", and is all-in") ? Convert.ToInt64(split[split.Length - 1].Replace("$", "")) : Convert.ToInt64(split[split.Length - 4].Replace("$", "").Replace(",", ""));

                                if (Actions.Count > 0)
                                {
                                    Action2 o = (Action2)Actions[Actions.Count - 1];
                                    hand.result = hand.result - ((o.Street == street2 && o.Type == "BLIND") ? Math.Abs(nr - o.Amount) : nr);
                                }

                                Actions.Add(new Action2(type: "BET", street: street2, amount: nr));
                            }
                            else if (act.Contains("wins the main pot ($"))
                            {
                                hand.result = hand.result + Convert.ToInt64(split[(5 + hand.Player.Split(' ').Length - 1)].Replace(" ", "").Replace("$", "").Replace("(", "").Replace(")", ""));
                            }
                            else if (act.Contains("wins the side pot ($"))
                            {
                                hand.result = hand.result + Convert.ToInt64(split[(5 + hand.Player.Split(' ').Length - 1)].Replace("$", "").Replace("(", "").Replace(")", "").Replace(",", ""));
                            }
                            else if (act.Contains("wins the pot ($"))
                            {
                                string l = line;
                                l = l.Replace(hand.Player, "");
                                l = l.Replace("wins the pot", "");
                                string[] ss = l.Split(')');
                                string dd = ss[0].Replace(" ", "");
                                dd = dd.Replace("$", "");
                                dd = dd.Replace(")", "");
                                dd = dd.Replace("(", "");
                                dd = dd.Replace(",", "");

                                hand.result = hand.result + Convert.ToInt64(dd);
                            }
                            else if (act.Contains("wins the side pot #"))
                            {
                                hand.result = hand.result + Convert.ToInt64(split[(6 + hand.Player.Split(' ').Length - 1)].Replace("$", "").Replace("(", "").Replace(")", "").Replace(",", ""));
                            }
                        }
                        else if (line.Length > 16 && line.Substring(0,17) == "Uncalled bet of $" && line.Contains(hand.Player))
                        {
                            hand.result = hand.result + Convert.ToInt64(line.Split(' ')[3].Replace("$", ""));
                        }
                    } 

                    if(hand.ID != null)
                    {
                        if (takingPart)
                        {
                            Hands.Add(hand);
                        }
                        hand = new Hand2();
                        street2 = "PF";
                        Actions = new ArrayList();
                        takingPart = false;
                    }
                }
            }

            Console.WriteLine(Hands.Count);
            Int64 total = Hands.Sum(item => item.result);
            Console.WriteLine("TOTAL: " + total);

            Console.WriteLine(stopWatch.Elapsed.TotalSeconds);
            Console.ReadLine();
            Hands.Clear();
        }
    }
}
