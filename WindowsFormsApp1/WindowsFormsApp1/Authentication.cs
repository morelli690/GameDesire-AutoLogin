using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameDesire
{
    public partial class Authentication : Form
    {
        public Authentication()
        {
            InitializeComponent();
        }

        private void Authentication_Load(object sender, EventArgs e)
        {

        }

        private void Label5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    string sql = "UPDATE Authentication SET Login=@Logi";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SQLiteParameter("@Logi", textBox1.Text));
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    string sql = "UPDATE Authentication SET Password=@Logi";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SQLiteParameter("@Logi", textBox2.Text));
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    string sql = "UPDATE Authentication SET Install=@Logi";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SQLiteParameter("@Logi", checkBox1.Checked));
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=Database.sqlite"))
            {
                con.Open();

                using (SQLiteCommand cmd = con.CreateCommand())
                {
                    string sql = "UPDATE Authentication SET PokerLauncherPath=@Logi";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(new SQLiteParameter("@Logi", textBox3.Text));
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
    }
}
