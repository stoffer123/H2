using System;
using System.Collections.Generic;
using System.Text;

namespace H2_LINQ1
{
    internal interface ICatalogueItem
    {
        string Name { get; set; }
        Category Category { get; set; }
        Price Price { get; set; }
        
    }
}
