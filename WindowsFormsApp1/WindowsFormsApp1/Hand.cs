using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Hand
    {
        public string Player { get; set; }
        public string Table { get; set; }

        public string ID { get; set; }
        public string Stake { get; set; }
        public DateTime dt { get; set; }
        public ArrayList Lines { get; set; } = new ArrayList();

        public string Card1 { get; set; }
        public string Card2 { get; set; }

        public string flopCard1 { get; set; }
        public string flopCard2 { get; set; }
        public string flopCard3 { get; set; }

        public string turnCard { get; set; }
        public string riverCard { get; set; }

        public Int64 winnings { get; set; }
        public Int64 investments { get; set; }
        public Int64 result { get; set; }


        public ArrayList Actions { get; set; } = new ArrayList();

    }
}
