using System;
using System.Collections.Generic;
using System.Linq;
using H2_Lesson6.Cars;
using H2_Lesson6.Receipts;

namespace H2_Lesson6
{
    internal class RentalCompany
    {
        private readonly List<Car> _fleet = new List<Car>();
        private readonly List<Customer> _customers = new List<Customer>();
        private readonly List<Rental> _rentals = new List<Rental>();
        private readonly IReceiptSender _receiptSender;

        public string Name { get; private set; }

        public IReadOnlyList<Car> Fleet => _fleet;
        public IReadOnlyList<Customer> Customers => _customers;
        public IReadOnlyList<Rental> Rentals => _rentals;

        public RentalCompany(string name, IReceiptSender receiptSender)
        {
            Name = name;
            _receiptSender = receiptSender;
        }

        public void AddCar(Car car) => _fleet.Add(car);

        public void AddCustomer(Customer customer) => _customers.Add(customer);

        public IEnumerable<Car> FindAvailableCars() => _fleet.Where(car => !car.IsRented);

        public Rental RegisterRental(Customer customer, Car car, DateTime startDate)
        {
            if (car.IsRented)
            {
                throw new InvalidOperationException(
                    $"Bilen {car.Registration} er allerede udlejet og kan ikke lejes ud igen");
            }

            Rental rental = new Rental(customer, car, startDate, _receiptSender);
            _rentals.Add(rental);
            car.MarkAsRented();
            rental.SendReceipt();

            return rental;
        }

        public decimal ReturnCar(Rental rental, DateTime endDate, int kilometers)
        {
            rental.CompleteRental(endDate, kilometers);
            rental.Car.MarkAsAvailable();

            return rental.TotalPrice;
        }
    }
}
