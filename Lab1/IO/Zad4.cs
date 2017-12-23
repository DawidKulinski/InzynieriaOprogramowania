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
    class Zad4
    {
        object locker = new object();

        public Zad4()
        {
            ThreadPool.QueueUserWorkItem(Server);
            ThreadPool.QueueUserWorkItem(Client);
            ThreadPool.QueueUserWorkItem(Client);

            Thread.Sleep(10000);
        }

        void Server(object stateInfo)
        {
            var server = new TcpListener(IPAddress.Any, 2048);
            server.Start();

            while (true)
            {
                var client = server.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(Connection, client);

            }
        }

        void Client(object stateInfo)
        {
            var client = new TcpClient();
            var buffer = new byte[1024];
            var message = "Helloabcdefghijklmnoprstuvwxyz";

            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));

            var stream = new NetworkStream(client.Client, false);

            lock (locker)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Wysłałem wiadomość k:" + message);
            }
            stream.Write(Encoding.ASCII.GetBytes(message), 0, message.Length);

            stream.Read(buffer, 0, buffer.Length);
            lock (locker)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Otrzymałem wiadomość k:" + System.Text.Encoding.ASCII.GetString(buffer));
            }
        }

        void Connection(object stateInfo)
        {
            var client = (TcpClient)stateInfo;
            var buffer = new byte[1024];
            client.GetStream().Read(buffer, 0, buffer.Length);
            lock (locker)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Otrzymałem wiadomośc s: " + System.Text.Encoding.ASCII.GetString(buffer));
            }
            client.GetStream().Write(buffer, 0, buffer.Length);
            lock (locker)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Wysłałem wiadomośc s: " + System.Text.Encoding.ASCII.GetString(buffer));
            }
            client.Close();
        }
    }
}
