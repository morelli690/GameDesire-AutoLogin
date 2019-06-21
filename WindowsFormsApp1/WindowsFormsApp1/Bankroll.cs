using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameDesire
{


    public partial class Bankroll : Form
    {
        public Bankroll()
        {
            InitializeComponent();
            InitializeElements("250K/500K");
        }

        private void InitializeElements(String stake)
        {
            ComboBox BuyIn = this.Controls.Find("comboBox1", true).FirstOrDefault() as ComboBox;
            BuyIn.Items.Clear();

            Int64 bb = 100;

            for (int i = 200; i >= 10; i--)
            {
                BuyIn.Items.Add(i + " BB (" + (i * 500000).ToString("N0") + ") ");
            }

            ComboBox Above = this.Controls.Find("comboBox2", true).FirstOrDefault() as ComboBox;
            Above.Items.Clear();

            for (int i = 200; i >= 1; i--)
            {
                Above.Items.Add(i + " BI (" + (i * 500000 * bb).ToString("N0") + ") ");
            }


        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
