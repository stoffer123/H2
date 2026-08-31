using System;
using System.Collections.Generic;
using System.Text;

namespace H2_Lesson4
{
    internal abstract class Employee
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public DateTime HireDate { get; set; }

        protected Employee(string name, string id, DateTime hireDate)
        {
            Name = name;
            Id = id;
            HireDate = hireDate;
        }

        public virtual decimal CalculateSalary()
        {
            return 0;
        }

        public virtual string Description()
        {
            return $"{Name} tjener {CalculateSalary()} kr om måneden";
        }
    }
}
