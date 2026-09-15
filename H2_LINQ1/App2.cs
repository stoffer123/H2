using System;
using System.Collections.Generic;
using System.Text;

namespace H2_LINQ1
{
    internal class App2 : IApplication
    {
        public void Run()
        {
            List<ICatalogueItem> catalogue = GetTestCatalogue();

            //Find alle produkter i kategori "Computer" og udskriv
            Console.WriteLine("Find alle produkter i kategori \"Computer\" og udskriv");
            PrintList(catalogue.Where(i => i.Category == Category.Computer).ToList());
            Console.WriteLine();

            //Find  alle produkter der koster mere end 5000dkk
            Console.WriteLine("Find  alle produkter der koster mere end 5000dkk");
            PrintList(catalogue.Where(i => i.Price > 5000m).ToList());
            Console.WriteLine();

            //Find alle produkter der koster mellem 1000 og 5000dkk
            Console.WriteLine("Find alle produkter der koster mellem 1000 og 5000dkk");
            PrintList(catalogue.Where(i => i.Price > 1000 && i.Price < 5000).ToList());
            Console.WriteLine();

            //Find alle produkter i kategorien tilbehør der koster mere end 1000dkk
            Console.WriteLine("Find alle produkter i kategorien tilbehør der koster mere end 1000dkk");
            PrintList(catalogue.Where(i => i.Category == Category.Accessory && i.Price > 1000m).ToList());
            Console.WriteLine();

            //Find all produkter hvis navn indeholder "Gaming"
            Console.WriteLine("Find all produkter hvis navn indeholder \"Gaming\"");
            PrintList(catalogue.Where(i => i.Name.ToLowerInvariant().Contains("Gaming".ToLowerInvariant())).ToList());
            Console.WriteLine();

            //Linq der kun returnere item.name
            Console.WriteLine("Linq der kun returnere item.name");
            PrintList(catalogue.Select(i => i.Name).ToList());
            Console.WriteLine();

            //Returnere Navn og Pris
            Console.WriteLine("Returnere Navn og Pris");
            PrintList(catalogue.Select(i => new { i.Name, i.Price }).ToList());
            Console.WriteLine();

            //Lav forespørgsel med anonym type der returnere name, category og price
            Console.WriteLine("Lav forespørgsel med anonym type der returnere name, category og price");
            var navnKategoriPris = catalogue.Select(i => new { i.Name, i.Category, i.Price });
            PrintList(navnKategoriPris.ToList());
            Console.WriteLine();

            //Lav der viser produktets navn og pris med teksten "[Produktnavn] koster [produktpris]"
            Console.WriteLine("Lav der viser produktets navn og pris med teksten \"[Produktnavn] koster [produktpris]\"");
            var navnKosterPris = catalogue.Select(i => $"{i.Name} koster {i.Price}");
            PrintList(navnKosterPris.ToList());
            Console.WriteLine();


            //Lav linq der sortere alle produkter efter pris stigende
            Console.WriteLine("Lav linq der sortere alle produkter efter pris stigende");
            var prisStigende = catalogue.OrderBy(i => i.Price);
            PrintList(prisStigende.ToList());
            Console.WriteLine();

            //Lav linq der sortere alle produkter efter pris faldende
            Console.WriteLine("Lav linq der sortere alle produkter efter pris faldende");
            var prisFaldende = catalogue.OrderByDescending(i => i.Price);
            PrintList(prisFaldende.ToList());
            Console.WriteLine();

            //Sorter produkter efter kategori
            Console.WriteLine("Sorter produkter efter kategori");
            var sorteretKategori = catalogue.OrderBy(i => i.Category);
            PrintList(sorteretKategori.ToList());
            Console.WriteLine();

            //Sorter først efter kategori, derefter pris
            Console.WriteLine("Sorter først efter kategori, derefter pris");
            var kategoriSaaPris = catalogue.OrderBy(i => i.Category).ThenBy(i => i.Price);
            PrintList(kategoriSaaPris.ToList());
            Console.WriteLine();

            //Sorter først efter kategori, derefter produktnavn
            Console.WriteLine("Sorter først efter kategori, derefter produktnavn");
            var kategoriSaaNavn = catalogue.OrderBy(i => i.Category).ThenBy(i => i.Name);
            PrintList(kategoriSaaNavn.ToList());
            Console.WriteLine();

            //Find forskellige distinct kategorier
            Console.WriteLine("Find forskellige distinct kategorier");
            PrintList(catalogue.Select(i => i.Category).Distinct().ToList());
            Console.WriteLine();

            //Find antal distinct kategorier
            Console.WriteLine("Find antal distinct kategorier");
            Console.WriteLine(catalogue.Select(i => i.Category).Distinct().Count());
            Console.WriteLine();

            //Er der mindst et produkt der koster mere end 10.000dkk
            Console.WriteLine("Er der mindst et produkt der koster mere end 10.000dkk");
            Console.WriteLine(catalogue.Any(i => i.Price > 10000m));
            Console.WriteLine();

            //Er der mindst et produkt i kategorien "Monitor"
            Console.WriteLine("Er der mindst et produkt i kategorien \"Monitor\"");
            Console.WriteLine(catalogue.Any(i => i.Category == Category.Monitor));
            Console.WriteLine();

            //Koster alle produkter mere end 500dkk
            Console.WriteLine("Koster alle produkter mere end 500dkk");
            Console.WriteLine(catalogue.All(i => i.Price > 500m));
            Console.WriteLine();

            //Koster alle computere mere end 5000dkk
            Console.WriteLine("Koster alle computere mere end 5000dkk");
            Console.WriteLine(catalogue.Where(i => i.Category == Category.Computer).All(i => i.Price > 500m));
            Console.WriteLine();

            //Udskriv samlet antal produkter
            Console.WriteLine("Udskriv samlet antal produkter");
            Console.WriteLine(catalogue.Count());
            Console.WriteLine();

            //Den samlede værdi af alle produkter
            Console.WriteLine("Den samlede værdi af alle produkter");
            Console.WriteLine(catalogue.Sum(i => i.Price));
            Console.WriteLine();

            //Gennemsnitsprisen på alle produkter
            Console.WriteLine("Gennemsnitsprisen på alle produkter");
            Console.WriteLine(catalogue.Average(i => i.Price));
            Console.WriteLine();

            //Den billigste varer
            Console.WriteLine("Den billigste varer");
            Console.WriteLine(catalogue.Min(i => i.Price));
            Console.WriteLine();

            //Den dyreste varer
            Console.WriteLine("Den dyreste varer");
            Console.WriteLine(catalogue.Max(i => i.Price));
            Console.WriteLine();

            //Antallet af produkter i kategori computer
            Console.WriteLine("Antallet af produkter i kategori computer");
            Console.WriteLine(catalogue.Where(i => i.Category == Category.Computer).Count());
            Console.WriteLine();

            //Gennemsnitlige pris i kategory tilbehør
            Console.WriteLine("Gennemsnitlige pris i kategory tilbehør");
            Console.WriteLine(catalogue.Where(i => i.Category == Category.Accessory).Average(i => i.Price));
            Console.WriteLine();

            //Gruppér produkterne efter kategori
            var grupperetEfterKategori = catalogue.GroupBy(i => i.Category);

            //Vis alle produkter under deres kategori
            Console.WriteLine("Produkter grupperet efter kategori");
            foreach (var gruppe in grupperetEfterKategori)
            {
                Console.WriteLine(gruppe.Key);
                foreach (var item in gruppe)
                {
                    Console.WriteLine($"• {item.Name}");
                }
            }
            Console.WriteLine();

            //Antallet af produkter i hver kategori
            Console.WriteLine("Antallet af produkter i hver kategori");
            foreach (var gruppe in grupperetEfterKategori)
            {
                Console.WriteLine($"{gruppe.Key}: {gruppe.Count()}");
            }
            Console.WriteLine();

            //Gennemsnitlige pris for hver kategori
            Console.WriteLine("Gennemsnitlige pris for hver kategori");
            foreach (var gruppe in grupperetEfterKategori)
            {
                Console.WriteLine($"{gruppe.Key}: {gruppe.Average(i => i.Price)}");
            }
            Console.WriteLine();

            //Den dyreste vare i hver kategori
            Console.WriteLine("Den dyreste vare i hver kategori");
            foreach (var gruppe in grupperetEfterKategori)
            {
                var dyreste = gruppe.OrderByDescending(i => i.Price).First();
                Console.WriteLine($"{gruppe.Key}: {dyreste.Name} ({dyreste.Price})");
            }
            Console.WriteLine();

            //a. Find de 3 dyreste produkter
            Console.WriteLine("De 3 dyreste produkter");
            var treDyreste = catalogue.OrderByDescending(i => i.Price).Take(3);
            PrintList(treDyreste.ToList());
            Console.WriteLine();

            //b. Find de 5 billigste produkter
            Console.WriteLine("De 5 billigste produkter");
            var femBilligste = catalogue.OrderBy(i => i.Price).Take(5);
            PrintList(femBilligste.ToList());
            Console.WriteLine();

            //c. Sortéret listen af produkterne efter faldende pris
            Console.WriteLine("Produkter sorteret efter faldende pris");
            var faldendePris = catalogue.OrderByDescending(i => i.Price);
            PrintList(faldendePris.ToList());
            Console.WriteLine();

            //d. Fra listen i c, spring over produkt 1.-3., behold kun 4.-6.
            Console.WriteLine("Produkt 4.-6. plads (faldende pris)");
            var fireTilSeks = faldendePris.Skip(3).Take(3);
            PrintList(fireTilSeks.ToList());
            Console.WriteLine();

            //e. Webshop med 3 produkter pr. side (Side 1, 2, 3)
            int sideStoerrelse = 3;
            for (int side = 1; side <= 3; side++)
            {
                Console.WriteLine($"Side {side}");
                var sideProdukter = catalogue.Skip((side - 1) * sideStoerrelse).Take(sideStoerrelse);
                PrintList(sideProdukter.ToList());
                Console.WriteLine();
            }

            //a. Find alle tags fra alle produkter (fladet ud til individuelle tags, inkl. duplikater)
            Console.WriteLine("Alle tags fra alle produkter");
            var alleTags = catalogue
                .SelectMany(i => Enum.GetValues<Tag>()
                    .Where(t => t != Tag.None && i.Tag.HasFlag(t)));
            foreach (var tag in alleTags)
            {
                Console.WriteLine(tag);
            }
            Console.WriteLine();

            //b. Find alle unikke tags
            Console.WriteLine("Alle unikke tags");
            var unikkeTags = alleTags.Distinct();
            foreach (var tag in unikkeTags)
            {
                Console.WriteLine(tag);
            }
            Console.WriteLine();

            //c. Find alle produkter, der har et bestemt tag
            Tag efterspurgtTag = Tag.Gaming;
            Console.WriteLine($"Produkter med tag: {efterspurgtTag}");
            var produkterMedTag = catalogue.Where(i => i.Tag.HasFlag(efterspurgtTag));
            PrintList(produkterMedTag.ToList());
            Console.WriteLine();

            //d. Find hvor mange forskellige tags der findes i datastrukturen
            Console.WriteLine("Antal forskellige tags i datastrukturen");
            Console.WriteLine(unikkeTags.Count());
            Console.WriteLine();




            GenererProduktAnalyse(catalogue);

        }

        private void GenererProduktAnalyse(List<ICatalogueItem> catalogue)
        {
            //=== Produktanalyse ===
            Console.WriteLine("=== Produktanalyse ===");
            Console.WriteLine($"Antal produkter: {catalogue.Count()}");
            Console.WriteLine($"Billigste produkt: {catalogue.OrderBy(i => i.Price).First()}");
            Console.WriteLine($"Dyreste produkt: {catalogue.OrderByDescending(i => i.Price).First()}");
            Console.WriteLine($"Gennemsnitspris: {catalogue.Average(i => i.Price)}");
            Console.WriteLine($"Samlet produktværdi: {catalogue.Sum(i => i.Price)}");
            Console.WriteLine();

            //=== Kategorianalyse ===
            Console.WriteLine("=== Kategorianalyse ===");
            var grupperetEfterKategori = catalogue.GroupBy(i => i.Category);
            foreach (var gruppe in grupperetEfterKategori)
            {
                Console.WriteLine(gruppe.Key);
                Console.WriteLine($"  Antal produkter: {gruppe.Count()}");
                Console.WriteLine($"  Gennemsnitspris: {gruppe.Average(i => i.Price)}");
                Console.WriteLine($"  Billigste produkt: {gruppe.OrderBy(i => i.Price).First()}");
                Console.WriteLine($"  Dyreste produkt: {gruppe.OrderByDescending(i => i.Price).First()}");
            }
            Console.WriteLine();

            //=== Topprodukter ===
            Console.WriteLine("=== Topprodukter ===");
            Console.WriteLine("De 3 dyreste produkter:");
            PrintList(catalogue.OrderByDescending(i => i.Price).Take(3).ToList());
            Console.WriteLine("De 3 billigste produkter:");
            PrintList(catalogue.OrderBy(i => i.Price).Take(3).ToList());
            Console.WriteLine();

            //=== Produktfiltrering ===
            Console.WriteLine("=== Produktfiltrering ===");
            Console.WriteLine("Alle produkter over 5.000 kr.:");
            var over5000 = catalogue.Where(i => i.Price > 5000m);
            PrintList(over5000.ToList());

            Console.WriteLine("Alle produkter i kategorien \"Computer\":");
            PrintList(catalogue.Where(i => i.Category == Category.Computer).ToList());

            Console.WriteLine("Alle produkter med \"Gaming\" i navnet:");
            PrintList(catalogue.Where(i => i.Name.Contains("Gaming")).ToList());
            Console.WriteLine();

            //=== Tags ===
            Console.WriteLine("=== Tags ===");
            var alleTags = catalogue
                .SelectMany(i => Enum.GetValues<Tag>()
                    .Where(t => t != Tag.None && i.Tag.HasFlag(t)));
            var unikkeTags = alleTags.Distinct();

            Console.WriteLine("Alle forskellige tags:");
            foreach (var tag in unikkeTags)
            {
                Console.WriteLine($"  {tag}");
            }
            Console.WriteLine($"Antallet af forskellige tags: {unikkeTags.Count()}");


            //Select tags på produkter over 5000dkk
            Console.WriteLine("Tags på produkter over 5.000 kr.:");
            var tagsOver5000 = over5000
                .SelectMany(i => Enum.GetValues<Tag>()
                    .Where(t => t != Tag.None && i.Tag.HasFlag(t)))
                .Distinct();
            foreach (var tag in tagsOver5000)
            {
                Console.WriteLine($"  {tag}");
            }
        }

        private void PrintList<T>(List<T> list)
        {
            foreach(var item in list)
            {
                Console.WriteLine(item.ToString());
            }
        }

        private List<ICatalogueItem> GetTestCatalogue()
        {
            return
           [
               new Item()
        {
            Name = "Gaming Laptop",
            Category = Category.Computer,
            Price = new Price(12500m),
            Tag = Tag.Gaming | Tag.Laptop | Tag.HighPerformance
        },
        new Item()
        {
            Name = "Office Laptop",
            Category = Category.Computer,
            Price = new Price(7500m),
            Tag = Tag.Laptop
        },
        new Item()
        {
            Name = "Gaming Mus",
            Category = Category.Accessory,
            Price = new Price(650m),
            Tag = Tag.Gaming
        },
        new Item()
        {
            Name = "Keyboard",
            Category = Category.Computer,
            Price = new Price(1100m),
            Tag = Tag.Gaming
        },
        new Item()
        {
            Name = "4k Skærm",
            Category = Category.Monitor,
            Price = new Price(4500m),
            Tag = Tag.HighPerformance
        },
        new Item()
        {
            Name = "Gaming Headset",
            Category = Category.Accessory,
            Price = new Price(1500m),
            Tag = Tag.Gaming
        },
        new Item()
        {
            Name = "27\" Gaming Skærm",
            Category = Category.Monitor,
            Price = new Price(3500m),
            Tag = Tag.Gaming | Tag.HighPerformance
        },
        new Item()
        {
            Name = "USB-C Dock",
            Category = Category.Accessory,
            Price = new Price(1800m),
            Tag = Tag.None
        },
        new Item()
        {
            Name = "Macbook Air",
            Category = Category.Computer,
            Price = new Price(9500m),
            Tag = Tag.Laptop | Tag.HighPerformance
        },
        new Item()
        {
            Name = "Gaming PC",
            Category = Category.Computer,
            Price = new Price(15000m),
            Tag = Tag.Gaming | Tag.HighPerformance
        },
        new Item()
        {
            Name = "Webkamera",
            Category = Category.Accessory,
            Price = new Price(850m),
            Tag = Tag.None
        },
        new Item()
        {
            Name = "32\" 4K Skærm",
            Category = Category.Monitor,
            Price = new Price(5500m),
            Tag = Tag.HighPerformance
        }
           ];
        }
    }
}
