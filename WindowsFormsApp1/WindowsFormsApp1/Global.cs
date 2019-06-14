using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Global
    {
        public static string RefreshAmount = "10";
        public static string RefreshType = "Seconds";
        public static bool Refresh = true;
        public static string SelectedPlayer = "All Players";
        public static string SelectedSession = "All Sessions";
        public static bool RefreshPeriodicalActive = true;
        public static bool restartStopwatch = false;

        public static Int64 valueOfAMillionInEuro = 100;

        public static List<Hand> Hands = new List<Hand>();


    }
}
