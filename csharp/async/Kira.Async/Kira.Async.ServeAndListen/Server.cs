using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;

namespace Kira.Async.ServeAndListen
{
    public class Server
    {
        private static readonly IFormatter Serializer = CommandSerializer.Serializer;

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
            var command = ReadCommand(clientStream);
            var commandName = command?.Name;
            var commandArgument = command?.Argument;

            switch (commandName)
            {
                case "":
                    Console.WriteLine("Empty command came from client");
                    break;

                case null:
                    Console.WriteLine("Can't read command from client");
                    break;

                case "exit":
                    Console.WriteLine("Shutting down the  server...");
                    return false;

                default:
                    Console.WriteLine($"Incoming command: {commandName} ({commandArgument})");
                    break;
            }

            return true;
        }

        private Command ReadCommand(NetworkStream stream)
        {
            return (Command) Serializer.Deserialize(stream);
        }

        private TcpListener CreateTcpListener()
        {
            return new TcpListener(IPAddress.Parse(Host), Port);
        }
    }
}