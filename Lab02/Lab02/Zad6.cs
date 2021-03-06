﻿using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Lab02
{
    class Zad6
    {
        public Zad6()
        {

            //Pobranie pliku text.txt.
            const string str = "text.txt";
            var fs = new FileStream(str,FileMode.Open,FileAccess.Read);

            var buffer = new byte[fs.Length];
            var handle = new AutoResetEvent(false);

            //Rozpoczęcie czytania z pliku za pomocą funkcji BeginRead
            fs.BeginRead(buffer, 0, buffer.Length, ReadCallback, new object[] { fs,buffer,handle });
            //AutoResetEvent oczekujacy na zakonczenie operacji czytania.
            handle.WaitOne();
        }

        public void ReadCallback(IAsyncResult state)
        {
            var tempState = (object[]) state.AsyncState;
            var fs = (FileStream) tempState[0];
            var buff = (byte[]) tempState[1];
            var wh = (AutoResetEvent) tempState[2];

            Console.WriteLine(Encoding.ASCII.GetString(buff));

            fs.Close();
            wh.Set();
        }

    }

}
