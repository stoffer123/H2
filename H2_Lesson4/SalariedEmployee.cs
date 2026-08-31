using System;
using System.Collections.Generic;
using System.Text;

namespace H2_Lesson4
{
    internal class SalariedEmployee : Employee
    {
        public decimal BaseSalary { get; set; }
        public decimal Bonus { get; set; }
        public SalariedEmployee(string name, string id, DateTime hireDate, decimal baseSalary, decimal bonus) 
            : base(name, id, hireDate)
        {
            BaseSalary = baseSalary;
            Bonus = bonus;
        }

        public override decimal CalculateSalary()
        {
            return BaseSalary + Bonus;
        }

        public override string Description()
        {
            return base.Description();
        }
    }
}
