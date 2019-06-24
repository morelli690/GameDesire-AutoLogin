using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameDesire
{
    public partial class Cards : Form
    {
        public Cards()
        {
            InitializeComponent();
            Task.Factory.StartNew(this.Refresh);
        }

        public async void Refresh()
        {
            while (true)
            {
                Console.WriteLine("s");

                string card = "KA";
                Console.WriteLine(card);
                string sorted = String.Concat(card.OrderByDescending(c => c));
                Console.WriteLine(sorted);

                Thread.Sleep(5000);
            }
        }

        private void Cards_Load(object sender, EventArgs e)
        {

        }
    }
}
