using System;

namespace Kira.Async.ServeAndListen
{
    [Serializable]
    public class Command
    {
        public string Name { get; set; }

        public string Argument { get; set; }
    }
}