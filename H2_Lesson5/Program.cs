namespace H2_Lesson5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // =========================================================
            // TRIN 1: Vehicle er abstract og kan IKKE laves med "new"
            // =========================================================
            // Linjen herunder ville give en COMPILERFEJL, hvis den ikke
            // stod som kommentar:
            //
            //     Vehicle v = new Vehicle("Generisk", "Model", 100);
            //
            // Fejl: CS0144 - "Cannot create an instance of the abstract
            // type or interface 'Vehicle'".
            //
            // En abstract klasse er en skabelon. Man kan kun lave objekter
            // af de KONKRETE klasser, der arver fra den:
            Car car = new Car("Toyota", "Corolla", 180, 4);
            Motorcycle motorcycle = new Motorcycle("Yamaha", "MT-07", 200, true);


            // =========================================================
            // TRIN 2: Polymorfi via BASISKLASSEN (Vehicle)
            // =========================================================
            // Listen har typen Vehicle, men kan indeholde alle afledte
            // typer. I foreach kalder vi Description() og CalculateAnnualTax(),
            // og .NET vælger automatisk den RIGTIGE override for hvert objekt
            // (kaldet "runtime binding").
            List<Vehicle> vehicles = new List<Vehicle>
            {
                car,
                motorcycle
            };

            Console.WriteLine("=== Polymorfi via basisklasse (Vehicle) ===");
            foreach (Vehicle vehicle in vehicles)
            {
                vehicle.Start();
                Console.WriteLine(vehicle.Description());
                Console.WriteLine($"Årlig afgift: {vehicle.CalculateAnnualTax()} kr.");
                Console.WriteLine();
            }


            // =========================================================
            // TRIN 3: Polymorfi via INTERFACE (IRentable)
            // =========================================================
            // Listen har typen IRentable. Den kan indeholde ALLE klasser,
            // der implementerer interfacet - uanset hvad de ellers arver fra.
            // Gennem en IRentable-reference kan vi kun se det, som interfacet
            // lover: metoden CalculateRent().
            List<IRentable> rentables = new List<IRentable>
            {
                car,
                motorcycle
            };

            int days = 3;
            Console.WriteLine($"=== Polymorfi via interface (IRentable) - {days} dages leje ===");
            foreach (IRentable rentable in rentables)
            {
                // Hvert objekt bruger sin EGEN implementering af CalculateRent().
                Console.WriteLine($"Lejepris: {rentable.CalculateRent(days)} kr.");
            }
        }
    }
}
