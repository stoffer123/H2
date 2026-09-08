using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace H2_Lesson10
{
    internal class Opgave2
    {
        private const int Threads = 4;
        private const int WordCount = 1000;

        private readonly ConcurrentDictionary<string, int> _counts = new ConcurrentDictionary<string, int>();

        public void Run()
        {
            string[] vocabulary = { "æble", "banan", "citron", "drue", "elderbær" };
            Random random = new Random(42);

            List<string> words = new List<string>();
            for (int i = 0; i < WordCount; i++)
            {
                words.Add(vocabulary[random.Next(vocabulary.Length)]);
            }

            int chunkSize = WordCount / Threads;
            Thread[] threads = new Thread[Threads];

            for (int t = 0; t < Threads; t++)
            {
                int start = t * chunkSize;
                int end = (t == Threads - 1) ? WordCount : start + chunkSize;

                threads[t] = new Thread(() => CountWords(words, start, end));
                threads[t].Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            int total = 0;
            foreach (KeyValuePair<string, int> entry in _counts)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
                total += entry.Value;
            }

            Console.WriteLine($"Forventet: {WordCount}  Faktisk: {total}  {(total == WordCount ? "OK" : "FEJL")}");
        }

        private void CountWords(List<string> words, int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                _counts.AddOrUpdate(words[i], 1, (key, oldValue) => oldValue + 1);
            }
        }
    }
}