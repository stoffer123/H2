using System;
using H2_Lesson6.Cars;
using H2_Lesson6.Receipts;

namespace H2_Lesson6
{
    internal class Rental
    {
        private readonly IReceiptSender _receiptSender;

        public Customer Customer { get; private set; }
        public Car Car { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public decimal TotalPrice { get; private set; }

        public Rental(Customer customer, Car car, DateTime startDate, IReceiptSender receiptSender)
        {
            Customer = customer;
            Car = car;
            StartDate = startDate;
            EndDate = null;
            TotalPrice = 0m;
            _receiptSender = receiptSender;
        }

        public bool IsActive() => EndDate is null;

        public void SendReceipt()
        {
            _receiptSender.Send(BuildReceiptText());
        }

        public void CompleteRental(DateTime endDate, int km)
        {
            if (!IsActive())
            {
                throw new InvalidOperationException("Udlejningen er allerede afsluttet");
            }

            if (endDate < StartDate)
            {
                throw new ArgumentException("Slutdato kan ikke ligge før startdato", nameof(endDate));
            }

            Car.UpdateKilometers(km);

            EndDate = endDate;
            TotalPrice = CalculateTotalPrice();
        }

        private string BuildReceiptText()
        {
            return $"Kvittering til {Customer.Name} ({Customer.PhoneNumber}): {Car.Describe()}, "
                 + $"lejet fra {StartDate:dd-MM-yyyy}. Prisen afregnes ved aflevering.";
        }

        private decimal CalculateTotalPrice()
        {
            if (EndDate is null)
            {
                throw new InvalidOperationException("Kan ikke udregne totalpris uden EndDate");
            }

            int days = (EndDate.Value.Date - StartDate.Date).Days;

            if (days < 1)
            {
                days = 1;
            }

            return days * Car.DailyRate;
        }
    }
}
