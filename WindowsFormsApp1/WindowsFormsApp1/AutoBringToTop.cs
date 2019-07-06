using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameDesire
{
    public class AutoBringToTop
    {
        public static Bitmap Fold = TemplateMatching.generateFormattedBitmap(new Bitmap("Fold.png"));

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        public static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        public static IntPtr GetHandleWindow(string title)
        {
            return FindWindow(null, title);
        }

        public static bool IsYourTurnActiveWindow()
        {
            IntPtr ActiveWindowHandle = GetHandleWindow(GetActiveWindowTitle());

            if (ActiveWindowHandle != IntPtr.Zero)
            {
                bool b = TemplateMatching.WaitForElement(ActiveWindowHandle, Fold, 0, similarity: 0.98f);
                return b ? true : false;
            }
            else
            {
                return false;
            }
        }
        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);


        public static void ActivateTableWithMyTurn()
        {
            OpenWindows so = new OpenWindows();

            string[] windows = so.GetDesktopWindowsCaptions();

            foreach (string s in windows)
            {
                if (s.Contains("Poker Texas Hold'em,") && s.Contains("Table"))
                {
                    IntPtr ActiveWindowHandle = GetHandleWindow(s);

                    bool b = TemplateMatching.WaitForElement(ActiveWindowHandle, Fold, 0, similarity: 0.98f);

                    if (b)
                    {
                        SetForegroundWindow(ActiveWindowHandle.ToInt32());
                        return;
                    }
                }
            }
        }

        public static void Run()
        {
            while (true)
            {
                if (!IsYourTurnActiveWindow())
                {
                    ActivateTableWithMyTurn();
                }
            }
        }
    }
}
