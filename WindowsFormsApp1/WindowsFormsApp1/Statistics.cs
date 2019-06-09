using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Statistics
    {
        public string Stake { get; set; }
        public Int64 Hands { get; set; }
        public Int64 Result { get; set; }

        public Statistics(string Stake)
        {
            Stake = Stake;
        }
    }
}
