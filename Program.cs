using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyMagicWand
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm form = new MainForm();
            Application.Run();          
        }
        static Random rand = new Random();
        public static int Rnd(int min, int max)
        {
            int v;
            while((v = rand.Next(min,max)) == 0);
            return v;
        }
    }
}
