using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATESTAT_LIBRARIE
{
    static class Program
    {

        public static int id_client;
        public static int id_angajat;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        
        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /*
            (new Form1()).Show();
            Application.Run();*/
            Application.Run(new Form1());
            
        }
    }
}
