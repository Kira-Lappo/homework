using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Kira.Async.ServeAndListen
{
    public class Server
    {
        public int Port { get; }

        public string Host { get; }

        public Server(string host, int port)
        {
            Host = host;
            Port = port;
        }

        public void StartServe()
        {
            var listener = CreateTcpListener();

            listener.Start();
            try
            {
                HandleClientRequests(listener);
            }
            finally
            {
                listener?.Stop();
            }
        }

        private void HandleClientRequests(TcpListener listener)
        {
            while (true)
            {
                var client = listener.AcceptTcpClient();
                var clientStream = client.GetStream();

                try
                {
                    if (!TryHandleStream(clientStream))
                    {
                        break;
                    }
                }
                finally
                {
                    clientStream?.Close();
                    client?.Close();
                }
            }
        }

        private bool TryHandleStream(NetworkStream clientStream)
        {
            var command = ReadCommand(clientStream)?.ToLower();

            switch (command)
            {
                case null:
                    Console.WriteLine("Can't read command from client");
                    break;

                case "exit":
                    Console.WriteLine("Shutting down the  server...");
                    return false;

                default:
                    Console.WriteLine("Incoming command: " + command);
                    break;
            }

            return true;
        }

        private string ReadCommand(NetworkStream stream)
        {
            var buffer = new Span<byte>();
            stream.Read(buffer);
            return Encoding.UTF8.GetString(buffer);
        }

        private TcpListener CreateTcpListener()
        {
            return new TcpListener(IPAddress.Parse(Host), Port);
        }
    }
}