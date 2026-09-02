using System;
using H2_Lesson6.Cars;

namespace H2_Lesson7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestWithStrings();
            Console.WriteLine();
            TestWithCars();
        }

        // Test 1: samlingen brugt med en indbygget referencetype.
        private static void TestWithStrings()
        {
            Console.WriteLine("=== Collection<string> ===");

            Collection<string> names = new Collection<string>();
            names.Add("Christopher");
            names.Add("Mette");
            names.Add("Sofie");
            Console.WriteLine($"Tilføjet 3 navne. Count = {names.Count}");
            Console.WriteLine($"Indhold: {string.Join(", ", names.Items)}");

            string? startsWithM = names.Find(name => name.StartsWith("M"));
            Console.WriteLine($"Find(navn starter med 'M')   -> {Show(startsWithM)}");

            string? tooLong = names.Find(name => name.Length > 20);
            Console.WriteLine($"Find(navn længere end 20)   -> {Show(tooLong)}");

            Console.WriteLine($"Remove(\"Mette\")             -> {names.Remove("Mette")} (findes)");
            Console.WriteLine($"Remove(\"Mette\") igen        -> {names.Remove("Mette")} (findes ikke længere)");
            Console.WriteLine($"Count = {names.Count}, indhold: {string.Join(", ", names.Items)}");
        }

        // Test 2: samme samling brugt med vores egen type fra lektion 6.
        // Bemærk at både Van og PassengerCar kan ligge i den samme Collection<Car>.
        private static void TestWithCars()
        {
            Console.WriteLine("=== Collection<Car> ===");

            Collection<Car> fleet = new Collection<Car>();
            Car yaris = new PassengerCar("AB12345", "Toyota", "Yaris", 40000, 495m, 5);
            Car transit = new Van("CD54321", "Ford", "Transit", 120000, 750m, 1200);
            Car octavia = new PassengerCar("EF67890", "Skoda", "Octavia", 82000, 625m, 5);

            fleet.Add(yaris);
            fleet.Add(transit);
            fleet.Add(octavia);
            Console.WriteLine($"Tilføjet 3 biler. Count = {fleet.Count}");

            foreach (Car car in fleet.Items)
            {
                Console.WriteLine($"  {car.Describe()}");
            }

            Car? cheap = fleet.Find(car => car.DailyRate < 600m);
            Console.WriteLine($"Find(dagspris under 600)     -> {Show(cheap?.Describe())}");

            Car? byRegistration = fleet.Find(car => car.Registration == "CD54321");
            Console.WriteLine($"Find(reg.nr. CD54321)        -> {Show(byRegistration?.Describe())}");

            Car? unknown = fleet.Find(car => car.Registration == "XX99999");
            Console.WriteLine($"Find(reg.nr. XX99999)        -> {Show(unknown?.Describe())}");

            Console.WriteLine($"Remove(transit)              -> {fleet.Remove(transit)} (findes)");
            Console.WriteLine($"Remove(transit) igen         -> {fleet.Remove(transit)} (findes ikke længere)");
            Console.WriteLine($"Count = {fleet.Count}");
        }

        // Skriver "(intet match)" i stedet for tom tekst, naar Find returnerer default(T).
        private static string Show(string? value)
        {
            return value ?? "(intet match)";
        }
    }
}
