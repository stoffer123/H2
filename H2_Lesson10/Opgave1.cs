using System;
using System.Threading;

namespace H2_Lesson10
{
    internal class Opgave1
    {
        private const int Threads = 4;
        private const int Iterations = 100_000;

        private readonly object _lock = new object();
        private int _counter = 0;

        /*
         * Interlocked.Increment er nok her, fordi hele læs-beregn-skriv sker som én
         * udelelig CPU-instruktion på ét enkelt felt. Ingen anden tråd kan nå ind imellem.
         * Skulle jeg opdatere to sammenhængende felter — fx både _counter og en
         * "sidst opdateret"-tekst — ville to Interlocked-kald være to separate atomare
         * operationer, og en anden tråd kunne læse imellem dem og se en tæller, der ikke
         * passer til teksten. Der skal jeg bruge lock, fordi lock beskytter hele
         * kodeblokken som én samlet enhed og ikke bare en enkelt variabel.
         */

        public void RunAll()
        {
            for (int run = 1; run <= 5; run++)
            {
                Console.Write($"Lock, kørsel {run}:        ");
                Run(CountUpWithLock);
            }

            for (int run = 1; run <= 5; run++)
            {
                Console.Write($"Interlocked, kørsel {run}: ");
                Run(CountUpWithInterlocked);
            }
        }

        private void Run(ThreadStart work)
        {
            _counter = 0;

            Thread[] threads = new Thread[Threads];
            for (int i = 0; i < Threads; i++)
            {
                threads[i] = new Thread(work);
                threads[i].Start();
            }

            foreach (Thread t in threads)
            {
                t.Join();
            }

            int expected = Threads * Iterations;
            Console.WriteLine($"Forventet: {expected}  Faktisk: {_counter}  {(_counter == expected ? "OK" : "FEJL")}");
        }

        private void CountUpWithLock()
        {
            for (int i = 0; i < Iterations; i++)
            {
                lock (_lock)
                {
                    _counter++;
                }
            }
        }

        private void CountUpWithInterlocked()
        {
            for (int i = 0; i < Iterations; i++)
            {
                Interlocked.Increment(ref _counter);
            }
        }
    }
}