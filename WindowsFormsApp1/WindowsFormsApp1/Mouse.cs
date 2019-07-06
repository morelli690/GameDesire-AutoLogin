using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameDesire
{
    public static class Mouse
    {
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;

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



    }
}
