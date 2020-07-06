using System;
using System.Threading;

namespace Kira.Async.Progress
{
    class Program
    {
        static void Main(string[] args)
        {
            var progressBar = new ConsoleProgressBar();
            var philSwiftHere = new PhilSwift();
            philSwiftHere.DoALottaDamage(progressBar);
        }

    }
}