using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Stakes : Form
    {
        public Stakes()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            Task.Factory.StartNew(this.Refresh);
        }

        public async void Refresh()
        {
            while (true)
            {
                Console.WriteLine("Hi");
                label49.Text = label49.Text + "9";
                Thread.Sleep(2000);
            }
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Label1_Click_2(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Label21_Click(object sender, EventArgs e)
        {

        }

        private void Label23_Click(object sender, EventArgs e)
        {

        }

        private void Label25_Click(object sender, EventArgs e)
        {

        }

        private void Label27_Click(object sender, EventArgs e)
        {

        }

        private void Label29_Click(object sender, EventArgs e)
        {

        }

        private void Label31_Click(object sender, EventArgs e)
        {

        }

        private void Label33_Click(object sender, EventArgs e)
        {

        }

        private void Label45_Click(object sender, EventArgs e)
        {

        }

        private void Label35_Click(object sender, EventArgs e)
        {

        }

        private void Label37_Click(object sender, EventArgs e)
        {

        }

        private void Label39_Click(object sender, EventArgs e)
        {

        }

        private void Label41_Click(object sender, EventArgs e)
        {

        }

        private void Label43_Click(object sender, EventArgs e)
        {

        }

        private void Label50_Click(object sender, EventArgs e)
        {

        }

        private void Label48_Click(object sender, EventArgs e)
        {

        }

        private void Label46_Click(object sender, EventArgs e)
        {

        }

        private void Label44_Click(object sender, EventArgs e)
        {

        }

        private void Label42_Click(object sender, EventArgs e)
        {

        }

        private void Label40_Click(object sender, EventArgs e)
        {

        }

        private void Label38_Click(object sender, EventArgs e)
        {

        }

        private void Label36_Click(object sender, EventArgs e)
        {

        }
    }
}
