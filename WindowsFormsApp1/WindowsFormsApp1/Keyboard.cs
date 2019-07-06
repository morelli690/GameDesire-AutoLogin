using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameDesire
{
    public static class Keyboard
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool PostMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        public static void SendClose(IntPtr hWnd)
        {
            const UInt32 WM_CLOSE = 0x0010;

            PostMessage(hWnd, (int)WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        public static void sendKeys(IntPtr H, string keys)
        {
            const Int32 WM_CHAR = 0x0102;

            foreach (char c in keys)
            {
                PostMessage(H, WM_CHAR, (IntPtr)c, IntPtr.Zero);
            }
        }

        public static void sendTab(IntPtr H)
        {
            const uint WM_KEYDOWN = 0x0100;
            const uint WM_KEYUP = 0x0101;
            const int VK_TAB = 0x09;

            PostMessage(H, (int)WM_KEYDOWN, (IntPtr)VK_TAB, IntPtr.Zero);
            PostMessage(H, (int)WM_KEYUP, (IntPtr)VK_TAB, IntPtr.Zero);
        }

        public static void sendEnter(IntPtr H)
        {
            const uint WM_KEYDOWN = 0x0100;
            const uint WM_KEYUP = 0x0101;
            const int VK_TAB = 0x0D;

            PostMessage(H, (int)WM_KEYDOWN, (IntPtr)VK_TAB, IntPtr.Zero);
            PostMessage(H, (int)WM_KEYUP, (IntPtr)VK_TAB, IntPtr.Zero);
        }
    }
}
