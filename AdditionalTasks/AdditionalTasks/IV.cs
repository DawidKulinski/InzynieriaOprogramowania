using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Console = System.Console;

namespace AdditionalTasks
{
    public class Server
    {
        private bool _isListening;
        private readonly TcpListener _server;
        private readonly AutoResetEvent _wh;
        private int _connections;

        public Server(AutoResetEvent autoResetEvent)
        {
            _isListening = false;
            _wh = autoResetEvent;
            _connections = 0;
            _server = new TcpListener(IPAddress.Any,13377);
            ThreadPool.QueueUserWorkItem(DoWorkCallback, _server);
        }

        public void Start()
        {
            _server.Start();
            _isListening = true;
        }

        public void Stop()
        {
                _server.Stop();
                _isListening = false;
                _wh.Set();
        }

        public void DoWorkCallback(object state)
        {
            var server = (TcpListener) state;

            while (_isListening)
            {
                TcpClient client = server.AcceptTcpClient();
                _connections++;
                ThreadPool.QueueUserWorkItem(ConnectionCallback, client);
            }
        }

        public void ConnectionCallback(object state)
        {
            var client = (TcpClient) state;
            var message = "";

            //Przygotowane odpowiedzi na możliwe opcje które może zapytać klient.
            while (client.Connected)
            {
                var requestBytes = new byte[1024];
                client.GetStream().Read(requestBytes,0,requestBytes.Length);
                var request = Encoding.ASCII.GetString(requestBytes.Take(3).ToArray());
                
                if (request == "HEY")
                {
                    client.GetStream().Write(Encoding.ASCII.GetBytes("HEY"), 0, "HEY".Length);
                }
                else if (request == "BYE")
                {
                    client.GetStream().Write(Encoding.ASCII.GetBytes("BYE"), 0, "BYE".Length);

                    Console.WriteLine(message);
                }
                else
                {
                    message += Encoding.ASCII.GetString(requestBytes).Trim('\0');

                    client.GetStream().Write(Encoding.ASCII.GetBytes("ACK"), 0, "ACK".Length);
                }
            }
        }
    }

    public class Client
    {
        private readonly TcpClient _client;
        private readonly byte[] _buffer;

        public Client()
        {
            //Rozpoczęcie połączenia przez klienta. Wysłanie komendy HEY. Która oznajmia serwerowi połączenie.
            _buffer = new byte[3];
            _client = new TcpClient("127.0.0.1",13377);
            _client.GetStream().ReadTimeout = 3000;
            _client.GetStream().Write(Encoding.ASCII.GetBytes("HEY"),0,"HEY".Length);

            _client.GetStream().Read(_buffer, 0, _buffer.Length);
            Console.WriteLine(Encoding.ASCII.GetString(_buffer));
        }

        public void SendMessage(string message)
        {
            //Wysłanie przez klienta wiadomości do serwera. Oczekiwanie na komende ACK, która poświadcza otrzymanie wiadomości.
            _client.GetStream().Write(Encoding.ASCII.GetBytes(message),0,message.Length);

            _client.GetStream().Read(_buffer, 0, _buffer.Length);

            Console.WriteLine(Encoding.ASCII.GetString(_buffer));
        }

        public void SendBye()
        {
            //Zakonczenie połączenia odbywa się za pomocą komendy BYE.
            _client.GetStream().Write(Encoding.ASCII.GetBytes("BYE"),0,"BYE".Length);

            var asyncResult = _client.GetStream().BeginRead(_buffer, 0, _buffer.Length,CloseConnectionCallback,null);
            _client.GetStream().EndRead(asyncResult);

        }
        public void CloseConnectionCallback(object sender)
        {
            var response = Encoding.ASCII.GetString(_buffer);
            Console.WriteLine(response);

            _client.Close();
        }
    }

    class IV
    {

        public IV()
        {
            var wh = new AutoResetEvent(false);
            var server = new Server(wh);
            server.Start();
            //Przykładowo przeprowadzona wymiana wiadomości w konfiguracji jeden serwer, 2 klientów
            var client1 = new Client();
            var client2 = new Client();
            client1.SendMessage("Tutaj ");
            client2.SendMessage("Tutaj ");
            client1.SendMessage("klient nr 1");
            client2.SendMessage("klient");
            client1.SendBye();
            client2.SendMessage(" nr 2");
            client2.SendBye();
            server.Stop();
            wh.WaitOne();

        }
    }
}
