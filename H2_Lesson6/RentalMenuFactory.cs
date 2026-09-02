using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using H2_Lesson6.Cars;
using MenuProject;

namespace H2_Lesson6
{
    // Menuen er brugerfladen - den kender domænet, men domænet kender ikke menuen.
    public class RentalMenuFactory : IMenuFactory
    {
        private static readonly string[] DateFormats = { "dd-MM-yyyy", "dd/MM/yyyy", "yyyy-MM-dd" };

        private readonly RentalCompany _company;

        internal RentalMenuFactory(RentalCompany company)
        {
            _company = company;
        }

        public IMenu CreateMenu()
        {
            return new MenuBuilder(_company.Name)
                .AddOption("Vis flåden", ShowFleet)
                .AddOption("Vis ledige biler", ShowAvailableCars)
                .AddOption("Vis kunder", ShowCustomers)
                .AddOption("Opret udlejning", RegisterRental)
                .AddOption("Aflever bil", ReturnCar)
                .AddOption("Vis udlejninger", ShowRentals)
                .AddOption("Kør demo-flow", RunDemoFlow)
                .AddOption("Afslut", () => Environment.Exit(0))
                .Build();
        }

        private void ShowFleet()
        {
            Console.WriteLine("=== Flåden ===");
            foreach (Car car in _company.Fleet)
            {
                string status = car.IsRented ? "UDLEJET" : "ledig  ";
                Console.WriteLine($"  [{status}] {car.Describe()} - {car.Kilometers} km");
            }
        }

        private void ShowAvailableCars()
        {
            Console.WriteLine("=== Ledige biler ===");
            List<Car> available = _company.FindAvailableCars().ToList();

            if (available.Count == 0)
            {
                Console.WriteLine("  Alle biler er udlejet.");
                return;
            }

            foreach (Car car in available)
            {
                Console.WriteLine($"  {car.Describe()}");
            }
        }

        private void ShowCustomers()
        {
            Console.WriteLine("=== Kunder ===");
            foreach (Customer customer in _company.Customers)
            {
                Console.WriteLine($"  {customer.Name} - kørekort {customer.DriverLicenseId} - tlf. {customer.PhoneNumber}");
            }
        }

        private void ShowRentals()
        {
            Console.WriteLine("=== Udlejninger ===");

            if (_company.Rentals.Count == 0)
            {
                Console.WriteLine("  Der er ingen udlejninger endnu.");
                return;
            }

            foreach (Rental rental in _company.Rentals)
            {
                string status = rental.IsActive()
                    ? "aktiv"
                    : $"afsluttet {rental.EndDate:dd-MM-yyyy}, {rental.TotalPrice} kr.";

                Console.WriteLine($"  {rental.Car.Registration} - {rental.Customer.Name} - "
                                + $"fra {rental.StartDate:dd-MM-yyyy} ({status})");
            }
        }

        private void RegisterRental()
        {
            List<Car> available = _company.FindAvailableCars().ToList();

            if (available.Count == 0)
            {
                Console.WriteLine("Der er ingen ledige biler lige nu.");
                return;
            }

            Customer customer = SelectCustomer();
            Car car = SelectCar(available);
            DateTime start = ReadDate("Startdato (dd-mm-åååå, tom = i dag): ");

            Rental rental = _company.RegisterRental(customer, car, start);

            Console.WriteLine();
            Console.WriteLine($"Udlejning oprettet: {rental.Car.Registration} til {rental.Customer.Name} "
                            + $"fra {rental.StartDate:dd-MM-yyyy}.");
        }

        private void ReturnCar()
        {
            List<Rental> active = _company.Rentals.Where(rental => rental.IsActive()).ToList();

            if (active.Count == 0)
            {
                Console.WriteLine("Der er ingen aktive udlejninger.");
                return;
            }

            Rental selected = SelectRental(active);
            DateTime end = ReadDate("Slutdato (dd-mm-åååå, tom = i dag): ");
            int kilometers = ReadInt($"Kilometerstand ved aflevering (nuværende {selected.Car.Kilometers}): ");

            decimal price = _company.ReturnCar(selected, end, kilometers);

            Console.WriteLine();
            Console.WriteLine($"Bilen {selected.Car.Registration} er afleveret og er ledig igen.");
            Console.WriteLine($"Lejeperiode  : {selected.StartDate:dd-MM-yyyy} - {selected.EndDate:dd-MM-yyyy}");
            Console.WriteLine($"Kilometerstand: {selected.Car.Kilometers} km");
            Console.WriteLine($"Samlet pris  : {price} kr.");
        }

        private void RunDemoFlow()
        {
            Car? car = _company.FindAvailableCars().FirstOrDefault();
            Customer? customer = _company.Customers.FirstOrDefault();

            if (car is null || customer is null)
            {
                Console.WriteLine("Demoen kræver mindst én ledig bil og én kunde.");
                return;
            }

            Console.WriteLine("=== Opret udlejning ===");
            Rental rental = _company.RegisterRental(customer, car, new DateTime(2026, 9, 1));
            Console.WriteLine($"  Udlejning oprettet. Bilen er udlejet: {car.IsRented}");

            Console.WriteLine();
            Console.WriteLine("=== Samme bil kan ikke lejes to gange ===");
            try
            {
                _company.RegisterRental(customer, car, new DateTime(2026, 9, 2));
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"  Afvist: {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("=== Aflevering ===");
            decimal price = _company.ReturnCar(rental, new DateTime(2026, 9, 4), car.Kilometers + 850);
            Console.WriteLine($"  Lejeperiode : {rental.StartDate:dd-MM-yyyy} - {rental.EndDate:dd-MM-yyyy}");
            Console.WriteLine($"  Kilometerstand: {car.Kilometers} km");
            Console.WriteLine($"  Bilen er ledig igen: {!car.IsRented}");
            Console.WriteLine($"  Samlet pris : {price} kr. (3 dage x {car.DailyRate} kr.)");
        }

        private Customer SelectCustomer()
        {
            Console.WriteLine("Vælg kunde:");
            IReadOnlyList<Customer> customers = _company.Customers;

            for (int i = 0; i < customers.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {customers[i].Name} (tlf. {customers[i].PhoneNumber})");
            }

            int choice = ReadInt($"Nummer (1-{customers.Count}): ");

            if (choice < 1 || choice > customers.Count)
            {
                throw new InvalidOperationException("Der findes ingen kunde med det nummer");
            }

            return customers[choice - 1];
        }

        private Car SelectCar(IReadOnlyList<Car> cars)
        {
            Console.WriteLine();
            Console.WriteLine("Vælg bil:");

            for (int i = 0; i < cars.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {cars[i].Describe()}");
            }

            int choice = ReadInt($"Nummer (1-{cars.Count}): ");

            if (choice < 1 || choice > cars.Count)
            {
                throw new InvalidOperationException("Der findes ingen bil med det nummer");
            }

            return cars[choice - 1];
        }

        private Rental SelectRental(IReadOnlyList<Rental> rentals)
        {
            Console.WriteLine("Vælg aktiv udlejning:");

            for (int i = 0; i < rentals.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {rentals[i].Car.Registration} - {rentals[i].Customer.Name} "
                                + $"- fra {rentals[i].StartDate:dd-MM-yyyy}");
            }

            int choice = ReadInt($"Nummer (1-{rentals.Count}): ");

            if (choice < 1 || choice > rentals.Count)
            {
                throw new InvalidOperationException("Der findes ingen udlejning med det nummer");
            }

            return rentals[choice - 1];
        }

        private static int ReadInt(string prompt)
        {
            Console.Write(prompt);
            string input = (Console.ReadLine() ?? string.Empty).Trim();

            if (!int.TryParse(input, out int value))
            {
                throw new FormatException($"'{input}' er ikke et tal");
            }

            return value;
        }

        private static DateTime ReadDate(string prompt)
        {
            Console.Write(prompt);
            string input = (Console.ReadLine() ?? string.Empty).Trim();

            if (input.Length == 0)
            {
                return DateTime.Today;
            }

            if (!DateTime.TryParseExact(input, DateFormats, CultureInfo.InvariantCulture,
                                        DateTimeStyles.None, out DateTime date))
            {
                throw new FormatException($"'{input}' er ikke en gyldig dato (brug dd-mm-åååå)");
            }

            return date;
        }
    }
}
