using System.Net.Sockets;
using System.Runtime.Serialization;

namespace Kira.Async.ServeAndListen
{
    public class CommandSender
    {
        private static readonly IFormatter Serializer = CommandSerializer.Serializer;

        public int Port { get; }

        public string Host { get; }


        public CommandSender(string host, int port)
        {
            Host = host;
            Port = port;
        }

        public void Send(Command command)
        {
            var tcpClient = new TcpClient();
            tcpClient.Connect(Host, Port);
            try
            {
                SendCommand(tcpClient, command);
            }
            finally
            {
                tcpClient?.Close();
            }
        }

        private static void SendCommand(TcpClient tcpClient, Command command)
        {
            var clientStream = tcpClient.GetStream();
            try
            {
                Serializer.Serialize(clientStream, command);
            }
            finally
            {
                clientStream?.Close();
            }
        }
    }
}