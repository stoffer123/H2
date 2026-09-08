using System;
using System.Threading;

namespace H2_Lesson9
{
    /*
     * _counter++ ser ud som én handling, men er i virkeligheden tre: læs værdien,
     * beregn værdi+1, skriv resultatet tilbage. To tråde kan nå at læse den samme
     * værdi, før nogen af dem har skrevet tilbage. Så beregner de begge det samme
     * resultat og skriver det samme tal — og den ene optælling forsvinder.
     * Derfor bliver facit lavere end forventet, og hvor meget lavere afhænger af
     * hvordan operativsystemet tilfældigt skifter mellem trådene. Fejlen sker ikke
     * hver gang, og det er præcis det, der gør en race condition farlig.
     */
    internal class Opgave2
    {
        private const int Threads = 4;
        private const int Iterations = 100_000;

        private int _counter = 0;

        public void Run()
        {
            _counter = 0;

            Thread[] threads = new Thread[Threads];
            for (int i = 0; i < Threads; i++)
            {
                threads[i] = new Thread(CountUp);
                threads[i].Start();
            }

            foreach (Thread t in threads)
            {
                t.Join();
            }

            int expected = Threads * Iterations;
            Console.WriteLine($"Forventet: {expected}  Faktisk: {_counter}  Mistet: {expected - _counter}");
        }

        private void CountUp()
        {
            for (int i = 0; i < Iterations; i++)
            {
                _counter++;
            }
        }
    }
}