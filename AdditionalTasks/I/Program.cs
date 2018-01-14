using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace I
{
    static class Program
    {
        /// <summary>
        /// Zadanie numer I dodane jako inny projekt z powodu wymagań co do stworzenia go za pomocą Windows Forms.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
