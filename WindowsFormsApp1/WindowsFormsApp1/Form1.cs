using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\GameDesire";
            DirectoryInfo d = new DirectoryInfo(filepath);

            foreach (var file in d.GetFiles("*.txt"))
            {
                Hand h = new Hand();


                var lines = File.ReadLines(file.FullName);

                foreach (var line in lines) {
                    Console.WriteLine(line);
                }


            }
        }
    }
}
