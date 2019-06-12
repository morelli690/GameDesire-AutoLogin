using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Parser
    {
        public static string getUsername(string fileName)
        {

            //20190609 - SaoPaolo - 5_10 - Loken - Poker Texas Hold'em
            var r = fileName.Split(new string[] { " - " }, StringSplitOptions.None).ToList();

            string tableName = r[r.Count-2];
            Console.WriteLine("Table Name: " + tableName);

            string stake = r[r.Count - 3];
            Console.WriteLine("Stake: " + stake);

            r.RemoveAt(0);
            r.RemoveAt(r.Count - 1);
            r.RemoveAt(r.Count - 1);
            r.RemoveAt(r.Count - 1);

            string userName = "";

            List<int> t = new List<int>();

            for (var i = 0; i < fileName.Length; i++)
            {
                if(fileName[i] == '-')
                {
                    t.Add(i);
                    Console.WriteLine(i);
                }
            }

            int first = fileName.IndexOf('-') + 1;
            int two = t[t.Count-3];

            Console.WriteLine(first + ":" + two + "-" + (two-first) );



            Console.ReadLine();
            return "";
        }

        public static void Parse()
        {
            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\GameDesire";

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            foreach (var file in new DirectoryInfo(filepath).GetFiles("*.txt"))
            {
                if (file.ToString().Contains("Poker Texas Hold'em") && !file.ToString().Contains("Tournament"))
                {
                    string username = getUsername(file.ToString());
                    Console.WriteLine(username);

                    foreach (var line in File.ReadLines(file.FullName))
                    {
                        if (line.Contains("GameDesire Game #"))
                        {
                            string id = line.Split(' ')[2].Replace("#","").Replace(":", "");

                            /*if (!Global.Hands.Any(Hand => Hand.ID == id))
                            {
                                Global.Hands.Add(hand);
                                hand = new Hand();
                            }*/



                        }
                    }
                }
            }

            Console.WriteLine(Global.Hands.Count);
            Console.WriteLine(stopWatch.Elapsed.TotalSeconds);
        }
    }
}
