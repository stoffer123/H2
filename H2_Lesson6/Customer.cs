using System;
using System.Collections.Generic;
using System.Text;

namespace H2_Lesson6
{
    internal class Customer
    {
        public string Name { get; set; }
        public string DriverLicenseId { get; private set; }
        public string PhoneNumber { get; set; }

        public Customer(string name, string driverLicenseId, string phoneNumber)
        {
            Name = name;
            DriverLicenseId = driverLicenseId;
            PhoneNumber = phoneNumber;
        }
    }
}
