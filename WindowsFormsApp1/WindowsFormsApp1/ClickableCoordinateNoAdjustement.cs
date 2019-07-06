using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesire
{
    public class ClickableCoordinateNoAdjustement
    {
        public int X { get; set; }
        public int Y { get; set; }

        public ClickableCoordinateNoAdjustement(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
