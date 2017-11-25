using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab02
{
    class Zad6
    {
        public Zad6()
        {
            const string str = "text.txt";
            FileStream fs = new FileStream(str,FileMode.Open,FileAccess.Read);
            var buffer = new byte[fs.Length];
            var wh = new WaitHandle[1];
            var handle = new AutoResetEvent(false);
            wh[0] = handle;
            fs.BeginRead(buffer, 0, buffer.Length, ReadCallback, new object[] { fs,buffer,handle });
            WaitHandle.WaitAll(wh);
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
