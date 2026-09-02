using System;

namespace H2_Lesson6.Receipts
{
    public class EmailReceiptSender : IReceiptSender
    {
        public void Send(string text)
        {
            Console.WriteLine($"[E-mail]: {text}");
        }
    }
}
