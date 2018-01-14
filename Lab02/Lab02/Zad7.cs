using System;
using System.IO;
using System.Text;

namespace Lab02
{
    class Zad7
    {
        public Zad7()
        {

            //Otwarcie pliku text.txt.
            var fs = new FileStream("text.txt", FileMode.Open, FileAccess.Read);
            var buff = new byte[fs.Length];


            //Czytanie danego pliku za pomoca funkcji BeginRead, która zwraca IAsyncResult.
            var asyncResult =fs.BeginRead(buff, 0, buff.Length, null, null);
            //Wywolanie funkcji EndRead z argumentem typu IAsyncResult.
            fs.EndRead(asyncResult);

            Console.WriteLine(Encoding.ASCII.GetString(buff));
        }

    }
}
