namespace H2_Lesson6.Cars
{
    public class Van : Car
    {
        public int LoadCapacityKg { get; init; }

        public Van(string registration, string make, string model, int kilometers, decimal dailyRate, int loadCapacityKg)
            : base(registration, make, model, kilometers, dailyRate)
        {
            LoadCapacityKg = loadCapacityKg;
        }

        public override string Describe()
        {
            return $"Varevogn {Make} {Model} ({Registration}), lastevne {LoadCapacityKg} kg, {DailyRate} kr./dag";
        }
    }
}
