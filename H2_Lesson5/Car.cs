using System;
using System.Collections.Generic;
using System.Text;

namespace H2_Lesson5
{
    public class Car : Vehicle, IRentable
    {
        public int DoorCount { get; set; }

        public Car(string brand, string model, int topSpeedKmh, int doorCount)
            : base(brand, model, topSpeedKmh)
        {
            DoorCount = doorCount;
        }

        // "override": samme signatur som Vehicle.Description() - dvs. samme
        // navn, samme returtype, samme parameterliste (her: ingen parametre).
        // Kun IMPLEMENTERINGEN er ny.
        public override string Description()
        {
            // base.Description() kalder basisklassens oprindelige version,
            // så vi kan BYGGE VIDERE på den i stedet for at gentage koden.
            return base.Description() + $", {DoorCount} døre";
        }

        public override decimal CalculateAnnualTax()
        {
            return 2400m;
        }

        public decimal CalculateRent(int days)
        {
            return days * 500;
        }
    }
}
