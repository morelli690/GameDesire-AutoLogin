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
        public static ScreenCapture sc = new ScreenCapture();


        public static Bitmap generateFormattedBitmap(Bitmap b)
        {
            return filter.Apply(b);
        }

        public static bool WaitForElement(IntPtr Handle, Bitmap Template, int Seconds, Rectangle searchZone = new Rectangle(), float similarity = 0.95f)
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                while (true)
                {
                    ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0);

                    Bitmap Source = sc.GetScreenshot(Handle);
                    Bitmap Source2 = generateFormattedBitmap(Source);

                    TemplateMatch[] matchings;

                    if (searchZone.IsEmpty)
                    {
                        matchings = tm.ProcessImage(Source2, Template);
                    }
                    else
                    {
                        Console.WriteLine("WITH Search Zone");
                        matchings = tm.ProcessImage(Source2, Template, searchZone);
                    }

                    if (matchings[0].Similarity > similarity)
                    {
                        Console.WriteLine("Found a match at: " + matchings[0].Rectangle.X + ":" + matchings[0].Rectangle.Y + " | " + matchings[0].Similarity + "\n Elapsed: " + stopWatch.Elapsed.TotalSeconds);
                        Console.WriteLine("Size: " + matchings[0].Rectangle.Width + ":" + matchings[0].Rectangle.Height);
                        return true;
                    }

                    if ((stopWatch.Elapsed.TotalSeconds > Seconds))
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static ClickableCoordinate getClickableCoordinate(IntPtr Handle, Bitmap Template, int Seconds = 0, int xOffset = 0, int yOffset = 0, Rectangle searchZone = new Rectangle())
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            while (true)
            {
                ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0);

                Bitmap Source = sc.GetScreenshot(Handle);
                Bitmap Source2 = generateFormattedBitmap(Source);

                TemplateMatch[] matchings;

                if (searchZone.IsEmpty)
                {
                    matchings = tm.ProcessImage(Source2, Template);
                }
                else
                {
                    Console.WriteLine("WITH Search Zone");
                    matchings = tm.ProcessImage(Source2, Template, searchZone);
                }

                if (matchings[0].Similarity > 0.95f)
                {
                    Console.WriteLine("Found a match at: " + matchings[0].Rectangle.X + ":" + matchings[0].Rectangle.Y + " | " + matchings[0].Similarity + "\n Elapsed: " + stopWatch.Elapsed.TotalSeconds);
                    Console.WriteLine("Size: " + matchings[0].Rectangle.Width + ":" + matchings[0].Rectangle.Height);
                    return new ClickableCoordinate(xOffset + matchings[0].Rectangle.X + (matchings[0].Rectangle.Width / 2), yOffset + matchings[0].Rectangle.Y + (matchings[0].Rectangle.Height / 2));
                }

                if ((stopWatch.Elapsed.TotalSeconds > Seconds))
                {
                    return null;
                }
            }
        }



    }
}