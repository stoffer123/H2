using System;

namespace H2_Lesson6.Cars
{
    // Abstrakt, fordi der ikke findes en "generisk bil" hos SjællandBil:
    // enhver bil er enten en varevogn eller en personbil.
    public abstract class Car
    {
        public string Registration { get; init; }
        public string Make { get; init; }
        public string Model { get; init; }
        public int Kilometers { get; private set; }
        public decimal DailyRate { get; private set; }
        public bool IsRented { get; private set; }

        protected Car(string registration, string make, string model, int kilometers, decimal dailyRate)
        {
            Registration = registration;
            Make = make;
            Model = model;
            Kilometers = kilometers;
            DailyRate = dailyRate;
            IsRented = false;
        }

        public abstract string Describe();

        public void MarkAsRented()
        {
            if (IsRented)
            {
                throw new InvalidOperationException($"Bilen {Registration} er allerede udlejet");
            }

            IsRented = true;
        }

        public void MarkAsAvailable()
        {
            if (!IsRented)
            {
                throw new InvalidOperationException($"Bilen {Registration} er allerede ledig");
            }

            IsRented = false;
        }

        public void UpdateKilometers(int newReading)
        {
            if (newReading < Kilometers)
            {
                throw new ArgumentException(
                    $"Ny kilometerstand ({newReading}) kan ikke være lavere end den nuværende ({Kilometers})",
                    nameof(newReading));
            }

            Kilometers = newReading;
        }
    }
}
