using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Lab02
{

    public class Zad11
    {
        public BackgroundWorker BackgroundWorker1 = new BackgroundWorker();
        public event EventHandler BackgroundWorkFinished;


        public Zad11()
        {
            BackgroundWorker1.DoWork += backgroundWorker1_DoWork;
            BackgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            BackgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            BackgroundWorker1.Disposed += backgroundWorker1_Disposed;
            BackgroundWorker1.WorkerReportsProgress = true;

            BackgroundWorker1.RunWorkerAsync();


            var client = new TcpClient("127.0.0.1",12345);
            var client2 = new TcpClient("127.0.0.1",12345);

            Thread.Sleep(1);

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            TcpListener tcp = new TcpListener(IPAddress.Any,12345);
            tcp.Start();

            int i = 0;
            while (i != 2)
            {
                TcpClient client = tcp.AcceptTcpClient();
                BackgroundWorker1.ReportProgress(++i);
            }
        }

        private void backgroundWorker1_Disposed(object sender, EventArgs e)
        {
        Console.WriteLine("Usuniety!!");    
        
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
          Console.WriteLine(e.ProgressPercentage);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           Console.WriteLine("Koniec!");
            BackgroundWorker1.Dispose();
        }
    }
}
