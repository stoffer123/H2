using System;
using System.Collections.Generic;
using System.Text;

namespace H2_LINQ1
{
    internal interface ICatalogueItem
    {
        Guid Id { get; }
        string Name { get; set; }
        Category Category { get; set; }
        Price Price { get; set; }
        List<Tag> Tags { get; }

    }
}
