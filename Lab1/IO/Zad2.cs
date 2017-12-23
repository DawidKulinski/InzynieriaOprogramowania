using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO
{
    public class Zad2
    {
        public Zad2()
        {
			/*Serwer moze obsługiwać tylko jednego klienta na raz. */
            ThreadPool.QueueUserWorkItem(Server);
            ThreadPool.QueueUserWorkItem(Client);
            ThreadPool.QueueUserWorkItem(Client);

            Thread.Sleep(10000);
        }



        void Server(object stateInfo)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 2048);
            server.Start();
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                byte[] buffer = new byte[1024];
                client.GetStream().Read(buffer, 0, 1024);
                client.GetStream().Write(buffer, 0, buffer.Length);
                client.Close();
            }
        }


        void Client(object stateInfo)
        {
            TcpClient client = new TcpClient();

            var stream = new NetworkStream(client.Client, false);
            var buffer = new byte[1024];
            var message = "Helloabcdefghijklmnoprstuvwxyz";


            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));

            stream.Write(Encoding.ASCII.GetBytes(message), 0, message.Length);
            Console.WriteLine("Wysłałem wiadomość " + message);

            stream.Read(buffer, 0, buffer.Length);
            Console.WriteLine("Otrzymałem wiadomość" + System.Text.Encoding.ASCII.GetString(buffer));
        }
    }
}
