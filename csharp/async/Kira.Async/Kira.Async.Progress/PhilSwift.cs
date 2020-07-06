using System;
using System.Threading;

namespace Kira.Async.Progress
{
    public class PhilSwift
    {
        public void DoALottaDamage(IProgress<int> progress)
        {
            var value = 1;
            progress.Report(value);

            var random = new Random();

            while (value < 100)
            {
                Thread.Sleep(random.Next(100, 2000));
                value += random.Next(50);
                progress.Report(value);
            }
        }
    }
}