using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kira.Async.ServeAndListen
{
    class Program
    {
        private static readonly string _host = "127.0.0.1";
        private static readonly int _port = 8888;

        static void Main(string[] args)
        {
            var serverTask = StartServerAsync();

            var commandSender = CreateCommandSender();

            // Sending 10 commands just because we want to =)
            var sendTasks = Enumerable.Range(1, 10)
                .Select(i => Task.Run(() =>
                {
                    var command = new Command
                    {
                        Name = "COMMAND#" + i,
                        Argument = "ARGUMENT#" + i
                    };

                    commandSender.Send(command);
                }))
                .ToList();

            Task.WhenAll(sendTasks).Wait();

            var command = new Command
            {
                Name = "exit",
            };
            commandSender.Send(command);

            serverTask.Wait();
        }

        private static CommandSender CreateCommandSender()
        {
            return new CommandSender(_host, _port);
        }

        private static Task StartServerAsync()
        {
            return Task.Run(() =>
            {
                var server = new Server(_host, _port);
                server.StartServe();
            });
        }
    }
}