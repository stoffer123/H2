namespace H2_Lesson6.Cars
{
    public class PassengerCar : Car
    {
        public int SeatCount { get; init; }

        public PassengerCar(string registration, string make, string model, int kilometers, decimal dailyRate, int seatCount)
            : base(registration, make, model, kilometers, dailyRate)
        {
            SeatCount = seatCount;
        }

        public override string Describe()
        {
            return $"Personbil {Make} {Model} ({Registration}), {SeatCount} sæder, {DailyRate} kr./dag";
        }
    }
}
