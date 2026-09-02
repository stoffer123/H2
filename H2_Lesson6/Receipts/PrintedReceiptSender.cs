using System;

namespace H2_Lesson6.Receipts
{
    public class PrintedReceiptSender : IReceiptSender
    {
        public void Send(string text)
        {
            Console.WriteLine($"[Printet i skranken]: {text}");
        }
    }
}
