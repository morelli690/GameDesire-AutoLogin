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
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        public static bool IsYourTurnActiveWindow()
        {
            IntPtr ActiveWindowHandle = GetHandleWindow(GetActiveWindowTitle());

            if (ActiveWindowHandle != IntPtr.Zero)
            {
                bool b = TemplateMatching.WaitForElement(ActiveWindowHandle, Fold, 0, similarity: 0.98f);
                if (b)
                {
                    RECT rct = new RECT();
                    GetWindowRect(ActiveWindowHandle, ref rct);

                    if (rct.Left != 621)
                    {
                        Console.WriteLine("Moved");
                        MoveWindow(ActiveWindowHandle, 621, 0, 0, 0, true);
                    }
                }

                return b ? true : false;
            }
            else
            {
                return false;
            }
        }
        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
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
                        RECT rct = new RECT();
                        GetWindowRect(ActiveWindowHandle, ref rct);

                        if (rct.Left != 621)
                        {
                            Console.WriteLine("Moved");
                            MoveWindow(ActiveWindowHandle, 621, 0, 0, 0, true);
                        }
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
