using System.Threading.Tasks;

namespace Kira.Async.ServeAndListen
{
    class Program
    {
        static void Main(string[] args)
        {
            StartServerAsync();
        }

        private static Task StartServerAsync()
        {
            return Task.Run(() =>
            {
                var server = new Server("127.0.0.1", 8888);
                server.StartServe();
            });
        }
    }
}