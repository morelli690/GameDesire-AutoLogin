using System;
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
                    new GameDesire.Authentication(),
                    new Analyzer()
                };

                foreach (var form in forms)
                {
                    //form.FormClosed += onFormClosed;
                }

                foreach (var form in forms)
                {
                    form.Show();
                }
            }
        }

        [STAThread]
        static void Main()
        {
            Database.Create();

            Task.Factory.StartNew(Parser.Run);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyApplicationContext());
        }
    }
}
