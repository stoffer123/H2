using System;

namespace H2_Lesson6.Receipts
{
    public class SmsReceiptSender : IReceiptSender
    {
        public void Send(string text)
        {
            Console.WriteLine($"[SMS]: {text}");
        }
    }
}
