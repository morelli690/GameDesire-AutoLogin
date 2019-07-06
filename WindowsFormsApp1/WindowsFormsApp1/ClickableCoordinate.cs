using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesire
{
    public class ClickableCoordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public ClickableCoordinate(int x, int y, bool adjust = true)
        {
            if (adjust)
            {
                X = x - 8;
                Y = y - 31;
            }
            else
            {
                X = x;
                Y = y;
            }
        }
    }
}
