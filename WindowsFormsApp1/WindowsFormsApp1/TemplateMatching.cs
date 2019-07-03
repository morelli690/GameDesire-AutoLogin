using Accord.Imaging;
using Accord.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDesire
{
    class TemplateMatching
    {
        public static Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);

        public static Bitmap generateFormattedBitmap(Bitmap b)
        {
            return filter.Apply(b);
        }

        public static bool WaitForElement(Bitmap Source, Bitmap Template, int Seconds)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            while (true)
            {
                ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0);

                Bitmap Source2 = generateFormattedBitmap(Source);

                TemplateMatch[] matchings = tm.ProcessImage(Source2, Template);

                if (matchings[0].Similarity > 0.95f)
                {
                    Console.WriteLine("Found a match at: " + matchings[0].Rectangle.X + ":" + matchings[0].Rectangle.Y + " | " + matchings[0].Similarity + "\n Elapsed: " + stopWatch.Elapsed.TotalSeconds);
                    return true;
                }

                if ((stopWatch.Elapsed.TotalSeconds > Seconds))
                {
                    return false;
                }
            }
        }
    }
}
