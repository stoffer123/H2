using System;
using System.Collections.Generic;
using System.Text;

namespace H2_Lesson5
{
    public class Motorcycle : Vehicle, IRentable
    {
        public bool RequiresHelmet { get; set; }

        public Motorcycle(string brand, string model, int topSpeedKmh, bool requiresHelmet)
            : base(brand, model, topSpeedKmh)
        {
            RequiresHelmet = requiresHelmet;
        }

        public override string Description()
        {
            string helmetInfo = RequiresHelmet ? "styrthjelm påkrævet" : "ingen hjelmkrav";
            return base.Description() + $", {helmetInfo}";
        }

        public override decimal CalculateAnnualTax()
        {
            return 2000m;
        }

        public decimal CalculateRent(int days)
        {
            return days * 200;
        }
    }
}
