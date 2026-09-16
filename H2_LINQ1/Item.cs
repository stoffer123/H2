using System;
using System.Collections.Generic;
using System.Text;

namespace H2_LINQ1
{
    internal class Item : ICatalogueItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public Category Category { get; set; }
        public Price Price { get; set; }
        public int UnitsSold { get; set; }

        //Kun laptops og skærme har specifikationer - for alle andre er den null
        public Specifications? Specifications { get; set; }

        //Many-to-many: EF opretter selv join-tabellen ItemTag
        public List<Tag> Tags { get; set; } = [];

        public override string ToString()
        {
            return $"Name: {Name}, Category: {Category.ToString()}, Price: {Price}";
        }
    }
}
