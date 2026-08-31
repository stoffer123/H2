using System;
using System.Collections.Generic;
using System.Text;

namespace H2_Lesson4
{
    internal class HourlyEmployee : Employee
    {
        public decimal HourlyRate { get; set; }
        public double HoursWorked { get; set; }
        public HourlyEmployee(string name, string id, DateTime hireDate, decimal hourlyRate, double hoursWorked) 
            : base(name, id, hireDate)
        {
            HourlyRate = hourlyRate;
            HoursWorked = hoursWorked;
        }

        public override decimal CalculateSalary()
        {
            return HourlyRate * (decimal)HoursWorked;
        }

        public override string Description()
        {
            return base.Description() + $"over {HoursWorked} á {HourlyRate} kr.";
        }
    }
}
