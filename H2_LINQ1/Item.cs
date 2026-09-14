using System;
using System.Collections.Generic;
using System.Text;

namespace H2_LINQ1
{
    internal class Item : ICatalogueItem
    {
        public string Name { get; set; } = "";
        public Category Category { get; set; }
        public Price Price { get; set; }


        public override string ToString()
        {
            return $"Name: {Name}, Category: {Category.ToString()}, Price: {Price}";
        }
    }
}
