using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace H2_LINQ1
{
    internal class App1 : IApplication
    {
        public void Run()
        {
            List<ICatalogueItem> catalogue = GetTestCatalogue();

            //Items over 5000kr
            var itemList = catalogue.ExpensiveProducts();
            foreach(var item in itemList)
            {
                Console.WriteLine(item.ToString());
            }

            //Items imellem 1000 og 10000kr og ikke i tilbehør ordered by pris dyrest til billigst
            itemList = catalogue.Where(i => i.Price >= 1000m &&
                i.Price <= 10000m &&
                i.Category != Category.Accessory)
                .OrderByDescending(i => i.Price)
                .ToList();

            foreach (var item in itemList)
            {
                Console.WriteLine(item.ToString());
            }


            //Anonymous types
            var result = catalogue.Select(i => new
            {
                i.Name,
                i.Category,
                i.Price
            });

            foreach (var item in result)
            {
                Console.WriteLine(item.ToString());
            }

            
            //Query Operators
            var alleComputer = from item in catalogue
                               where item.Category == Category.Computer
                               select item;

            var over1000 = from item in catalogue
                           where item.Price > 1000m
                           select item;

            var sortedByPrice = from item in catalogue
                                orderby item.Price
                                select item;

            var dyresteProduct = (from item in catalogue
                                 orderby item.Price descending
                                 select item).First();

            var produktCount = (from item in catalogue
                                select item).Count(); //Bør vi ikke bare kalde catalogue.Count() ??? 


            //Expression tree: Se selve træet i stedet for at køre det:
            Expression<Func<ICatalogueItem, bool>> expensiveFilter = i => i.Price > new Price(1000m);
            Console.WriteLine(expensiveFilter.Body);        // "(i.Price > new Price(1000))"
            Console.WriteLine(expensiveFilter.Parameters[0]); // "i"

            // Kompiler til rigtig kørbar kode når vil eksekvere den:
            Func<ICatalogueItem, bool> compiled = expensiveFilter.Compile();
            var rereer = catalogue.Where(compiled);
            bool expressresult = compiled(catalogue[0]);


        }

        private List<ICatalogueItem> GetTestCatalogue()
        {
             return
            [
                new Item()
                {
                    Name = "Gaming Laptop",
                    Category = Category.Computer,
                    Price = new Price(12500m)
                },
                new Item()
                {
                    Name = "Office Laptop",
                    Category = Category.Computer,
                    Price = new Price(7500m)
                },
                new Item()
                {
                    Name = "Gaming Mus",
                    Category = Category.Accessory,
                    Price = new Price(650m)
                },
                new Item()
                {
                    Name = "Keyboard",
                    Category = Category.Computer,
                    Price = new Price(1100m)
                },
                new Item()
                {
                    Name = "4k Skærm",
                    Category = Category.Monitor,
                    Price = new Price(4500m)
                }
            ];
        }
    }
}
