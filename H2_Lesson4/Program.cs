namespace H2_Lesson4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Employee> Employees = new()
            {
                new SalariedEmployee("Lars Larsen", "1234", new DateTime(1992, 6, 23), 22000m, 500m),
                new SalariedEmployee("Kim Kimsen", "1235", new DateTime(1998, 6, 23), 26000m, 20m),
                new HourlyEmployee("John Johnsen", "1236", new DateTime(2000, 1, 29), 185, 37),
                new HourlyEmployee("Navn Navnesen", "1237", new DateTime(2000, 8, 29), 210, 28)
            };

            decimal samletLønsum = 0;

            foreach(var e in Employees)
            {
                samletLønsum += e.CalculateSalary();
                Console.WriteLine(e.Description());
            }

            Console.WriteLine($"Afdelingens samlede lønsun er: {samletLønsum} kr.");


        }
    }
}
