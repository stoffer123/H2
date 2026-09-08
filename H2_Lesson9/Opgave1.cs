using System;
using System.Collections.Generic;
using System.Text;

namespace H2_Lesson9
{
    internal class Opgave1
    {

        public void StartThreadCounter()
        {
            Thread thread1 = new Thread(CountTo10);
            thread1.Start();
        }

        private void CountTo10()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Thread {Environment.CurrentManagedThreadId}: {i}");
                Thread.Sleep(20);
            }
        }
    }
}
