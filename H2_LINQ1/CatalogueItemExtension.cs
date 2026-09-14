using System;
using System.Collections.Generic;
using System.Text;

namespace H2_LINQ1
{
    internal static class CatalogueItemExtension
    {
        public static List<ICatalogueItem> ExpensiveProducts(this List<ICatalogueItem> list)
        {
            return list.Where(i => i.Price > 5000m).ToList();
        }
    }
}
