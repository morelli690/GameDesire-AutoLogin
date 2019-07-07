﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        class MyApplicationContext : ApplicationContext
        {
            private void onFormClosed(object sender, EventArgs e)
            {
                if (Application.OpenForms.Count == 0)
                {
                    ExitThread();
                }
            }

            public MyApplicationContext()
            {
                var forms = new List<Form>() {
                    new Analyzer(),
                    new GameDesire.Login()
                    //new GameDesire.Main_Lobby()
                };

                foreach (var form in forms)
                {
                    form.FormClosed += onFormClosed;
                    form.Show();
                }
            }
        }

        [STAThread]
        static void Main()
        {
            Database.Create();

            Task.Factory.StartNew(Parser.Run);
            Task.Factory.StartNew(GameDesire.AutoBringToTop.Run);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyApplicationContext());
        }
    }
}
