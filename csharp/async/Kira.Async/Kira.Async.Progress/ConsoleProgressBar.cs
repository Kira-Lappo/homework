using System;

namespace Kira.Async.Progress
{
    public class ConsoleProgressBar : IProgress<int>
    {
        public void Report(int value)
        {
            if (value < 0)
            {
                value = 0;
            }

            if (value > 100)
            {
                value = 100;
            }

            var width = Console.WindowWidth - 2;

            Console.Title = "Progress " + value;

            Console.Clear();

            Console.Write("[");
            var fullCount = width * value / 100;
            var emptyCount = width - fullCount;

            if (fullCount > 0)
            {
                Console.Write(new string('=', fullCount));
            }

            if (emptyCount > 0)
            {
                Console.Write(new string(' ', emptyCount));
            }

            Console.Write("]");
        }
    }
}