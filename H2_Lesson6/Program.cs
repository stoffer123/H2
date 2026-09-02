using System;
using H2_Lesson6;
using H2_Lesson6.Cars;
using H2_Lesson6.Receipts;
using MenuProject;

IReceiptSender receiptSender = new EmailReceiptSender();
RentalCompany company = new RentalCompany("SjællandBil", receiptSender);

company.AddCar(new PassengerCar("AB12345", "Toyota", "Yaris", 40000, 495m, 5));
company.AddCar(new PassengerCar("EF67890", "Skoda", "Octavia", 82000, 625m, 5));
company.AddCar(new Van("CD54321", "Ford", "Transit", 120000, 750m, 1200));

company.AddCustomer(new Customer("Christopher Mikkelsen", "DK123456", "12345678"));
company.AddCustomer(new Customer("Mette Hansen", "DK654321", "87654321"));

IMenuFactory menuFactory = new RentalMenuFactory(company);
IMenu menu = menuFactory.CreateMenu();

menu.Display();
