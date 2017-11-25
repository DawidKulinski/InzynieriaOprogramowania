using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02
{
    class Zad7
    {
        public Zad7()
        {
            FileStream fs = new FileStream("text.txt", FileMode.Open, FileAccess.Read);
            var buff = new byte[fs.Length];

            var xd =fs.BeginRead(buff, 0, buff.Length, null, null);

            fs.EndRead(xd);

            Console.WriteLine(Encoding.ASCII.GetString(buff));
        }

    }
}
