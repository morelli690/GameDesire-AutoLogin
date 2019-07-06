using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameDesire
{
    public static class Mouse
    {
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_MOUSEWHEEL = 0x020A;
        public const int WM_MOUSEHWHEEL = 0x020E;
        public const int WM_MOUSEMOVE = 0x0200;
        public static int MakeLParam(int LoWord, int HiWord)
        {
            return (int)((HiWord << 16) | (LoWord & 0xFFFF));
        }

        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);


        public static void leftClick(IntPtr Handle, ClickableCoordinate cc)
        {
            PostMessage(Handle, WM_LBUTTONDOWN, 1, MakeLParam(cc.X, cc.Y));
            PostMessage(Handle, WM_LBUTTONUP, 0, MakeLParam(cc.X, cc.Y));
        }

        [DllImport("user32.dll", EntryPoint = "PostMessageA", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hwnd, uint Msg, long wParam, long lParam);

        public static void dragAndDrop(IntPtr Handle, int xFrom, int yFrom, int xTo, int yTo)
        {
            PostMessage(Handle, (int)WM_MOUSEMOVE, 0, MakeLParam(xFrom, yFrom));
            PostMessage(Handle, WM_LBUTTONDOWN, 1, MakeLParam(xFrom, yFrom));
            Thread.Sleep(1000);
            PostMessage(Handle, (int)WM_MOUSEMOVE, 0, MakeLParam(xTo, yTo));
            PostMessage(Handle, WM_LBUTTONUP, 0, MakeLParam(xTo, yTo));
        }

    }
}
