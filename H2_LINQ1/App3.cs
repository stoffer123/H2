using H2_LINQ1.Ef;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace H2_LINQ1
{
    internal class App3 : IApplication
    {
        public void Run()
        {
            //Opret context og migrate
            using var ctx = new App3DbContext();
            ctx.Database.Migrate();

            //Seed data hvis db er tom
            if(!ctx.Items.Any())
            {
                ctx.Items.AddRange(GetTestCatalogue());
                ctx.SaveChanges();
            }

            SøgOgFiltrer(ctx);
            Crud(ctx);
            Analyse(ctx);
            Specifikationer(ctx);
        }

        //1.1 Soeg og filtrer data
        private void SøgOgFiltrer(App3DbContext ctx)
        {
            Console.WriteLine("=== 1.1 Soeg og filtrer ===");
            Console.WriteLine();

            //a. Alle produkter i kategorien "Computer"
            PrintQuery("a. Kategorien \"Computer\"",
                ctx.Items.Where(i => i.Category == Category.Computer));

            //b. Alle produkter der koster mere end 5.000 kr.
            PrintQuery("b. Dyrere end 5.000 kr.",
                ctx.Items.Where(i => i.Price > new Price(5000m)));

            //c. Alle produkter mellem 1.000 og 5.000 kr.
            PrintQuery("c. Mellem 1.000 og 5.000 kr.",
                ctx.Items.Where(i => i.Price > new Price(1000m) && i.Price < new Price(5000m)));

            //d. Kategorien "Tilbehoer" (Category.Accessory) over 1.000 kr.
            PrintQuery("d. Tilbehoer over 1.000 kr.",
                ctx.Items.Where(i => i.Category == Category.Accessory && i.Price > new Price(1000m)));

            //e. Navnet indeholder "Gaming"
            PrintQuery("e. Navn indeholder \"Gaming\"",
                ctx.Items.Where(i => i.Name.Contains("Gaming")));
        }

        //2.1 CRUD
        private void Crud(App3DbContext ctx)
        {
            Console.WriteLine("=== 2.1 CRUD ===");
            Console.WriteLine();

            //a. Create - tilfoej Gaming Keyboard
            Console.WriteLine("a. Tilfoej \"Gaming Keyboard\" (Tilbehoer, 1.200 kr.)");
            var nytProdukt = new Item
            {
                Name = "Gaming Keyboard",
                Category = Category.Accessory,
                Price = new Price(1200m)
            };
            ctx.Items.Add(nytProdukt);
            ctx.SaveChanges();
            Console.WriteLine($"   Oprettet med Id: {nytProdukt.Id}");
            Console.WriteLine();

            //b. Read - hent alle produkter
            PrintQuery("b. Alle produkter i databasen", ctx.Items);

            //c. Update - saet prisen til 1.350 kr.
            Console.WriteLine("c. Aendr prisen til 1.350 kr.");
            var tilOpdatering = ctx.Items.First(i => i.Name == "Gaming Keyboard");
            tilOpdatering.Price = new Price(1350m);
            //EF sporer objektet, saa SaveChanges finder selv aendringen og laver et UPDATE
            ctx.SaveChanges();
            Console.WriteLine($"   {tilOpdatering}");
            Console.WriteLine();

            //d. Delete - slet produktet igen
            Console.WriteLine("d. Slet \"Gaming Keyboard\"");
            var tilSletning = ctx.Items.First(i => i.Name == "Gaming Keyboard");
            ctx.Items.Remove(tilSletning);
            ctx.SaveChanges();
            Console.WriteLine();

            PrintQuery("   Databasen efter sletning", ctx.Items);
            Console.WriteLine($"   Findes \"Gaming Keyboard\" stadig? {ctx.Items.Any(i => i.Name == "Gaming Keyboard")}");
            Console.WriteLine();
        }

        //3. Analyse af databasen
        private void Analyse(App3DbContext ctx)
        {
            //3.2 Opdigtede salgstal - vi ændrer objekterne, og EF laver selv UPDATE ved SaveChanges
            Console.WriteLine("=== 3.2 Opdater UnitsSold ===");
            Console.WriteLine();

            var salgstal = new Dictionary<string, int>
            {
                ["Gaming Laptop"] = 45,
                ["Office Laptop"] = 120,
                ["Gaming Mus"] = 310,
                ["Keyboard"] = 180,
                ["4k Skærm"] = 75,
                ["Gaming Headset"] = 220,
                ["27\" Gaming Skærm"] = 95,
                ["USB-C Dock"] = 140,
                ["Macbook Air"] = 85,
                ["Gaming PC"] = 30,
                ["Webkamera"] = 260,
                ["32\" 4K Skærm"] = 60
            };

            foreach (var item in ctx.Items.ToList())
            {
                if (salgstal.TryGetValue(item.Name, out int solgt))
                {
                    item.UnitsSold = solgt;
                }
            }

            //Sættes en property til samme værdi som før, laver EF intet UPDATE - derfor 0 rækker ved næste kørsel
            int opdateret = ctx.SaveChanges();
            Console.WriteLine($"   {opdateret} rækker opdateret");
            Console.WriteLine();

            //3.3 Gruppér efter kategori og summér solgte enheder
            Console.WriteLine("=== 3.3 Solgte enheder pr. kategori ===");
            Console.WriteLine();

            var query = ctx.Items
                .GroupBy(i => i.Category)
                .Select(g => new { Kategori = g.Key, Solgt = g.Sum(i => i.UnitsSold) })
                .OrderByDescending(x => x.Solgt);

            PrintSql(query);

            //ToList() én gang, så diagrammet nedenfor ikke rammer databasen igen
            var salgPrKategori = query.ToList();
            foreach (var kategori in salgPrKategori)
            {
                Console.WriteLine($"   {kategori.Kategori}: {kategori.Solgt} stk.");
            }
            Console.WriteLine();

            //3.4 Søjlediagram
            Console.WriteLine("=== 3.4 Søjlediagram ===");
            Console.WriteLine();
            PrintSøjlediagram(salgPrKategori.Select(k => (k.Kategori.ToString(), k.Solgt)).ToList());
        }

        //4.1 Schema-flexible specifikationer gemt som JSON
        private void Specifikationer(App3DbContext ctx)
        {
            Console.WriteLine("=== 4.1 Specifikationer ===");
            Console.WriteLine();

            //Kun laptops og skærme får specifikationer - og de to typer har forskellige felter
            var specifikationer = new Dictionary<string, Specifications>
            {
                ["Gaming Laptop"] = new() { Brand = "Lenovo", Processor = "Intel Core i7", Ram = "16 GB", Storage = "1 TB SSD" },
                ["Office Laptop"] = new() { Brand = "Dell", Processor = "Intel Core i5", Ram = "8 GB", Storage = "512 GB SSD" },
                ["Macbook Air"] = new() { Brand = "Apple", Processor = "Apple M3", Ram = "16 GB", Storage = "512 GB SSD" },
                ["4k Skærm"] = new() { Brand = "Samsung", Resolution = "3840x2160", Size = "27\"", RefreshRate = "144 Hz" },
                ["27\" Gaming Skærm"] = new() { Brand = "Lenovo", Resolution = "2560x1440", Size = "27\"", RefreshRate = "165 Hz" },
                ["32\" 4K Skærm"] = new() { Brand = "LG", Resolution = "3840x2160", Size = "32\"", RefreshRate = "60 Hz" }
            };

            foreach (var item in ctx.Items.ToList())
            {
                //Kun hvis varen ikke allerede har specifikationer - så kan programmet køres igen
                if (item.Specifications == null && specifikationer.TryGetValue(item.Name, out var specs))
                {
                    item.Specifications = specs;
                }
            }

            int opdateret = ctx.SaveChanges();
            Console.WriteLine($"   {opdateret} rækker fik specifikationer");
            Console.WriteLine();

            //Find alle produkter fra et bestemt brand - EF slår op inde i JSON-dokumentet
            string brand = "Lenovo";
            Console.WriteLine($"Produkter fra brand: {brand}");

            var query = ctx.Items
                .Where(i => i.Specifications != null && i.Specifications.Brand == brand);

            PrintSql(query);
            foreach (var item in query)
            {
                Console.WriteLine($"   - {item}");
                Console.WriteLine($"     {item.Specifications}");
            }
            Console.WriteLine();
        }

        //Tegner et lodret søjlediagram i konsollen
        private void PrintSøjlediagram(List<(string Navn, int Værdi)> data, int højde = 10)
        {
            if (data.Count == 0)
            {
                return;
            }

            //'█' kræver UTF-8 i konsollen
            Console.OutputEncoding = Encoding.UTF8;

            int max = Math.Max(1, data.Max(d => d.Værdi));
            int bredde = data.Max(d => d.Navn.Length) + 2;

            //Hver søjles højde skaleres i forhold til den største værdi
            var søjler = data
                .Select(d => (int)Math.Round((double)d.Værdi / max * højde))
                .ToList();

            for (int række = højde; række >= 1; række--)
            {
                Console.Write("   |");
                foreach (var søjle in søjler)
                {
                    char tegn = søjle >= række ? '█' : ' ';
                    Console.Write(" " + new string(tegn, bredde - 2) + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("   +" + new string('-', bredde * data.Count));
            Console.WriteLine("    " + string.Concat(data.Select(d => Centrér(d.Navn, bredde))));
            Console.WriteLine("    " + string.Concat(data.Select(d => Centrér(d.Værdi.ToString(), bredde))));
            Console.WriteLine();
        }

        private string Centrér(string tekst, int bredde)
        {
            return tekst.PadLeft((bredde + tekst.Length) / 2).PadRight(bredde);
        }

        //Udskriver den SQL EF genererer for en forespørgsel
        private void PrintSql<T>(IQueryable<T> query)
        {
            Console.WriteLine("   SQL:");
            foreach (var linje in query.ToQueryString().Split(Environment.NewLine))
            {
                Console.WriteLine("        " + linje);
            }
        }

        //Udskriver den SQL EF genererer, og derefter resultatet
        private void PrintQuery(string overskrift, IQueryable<Item> query)
        {
            Console.WriteLine(overskrift);
            PrintSql(query);
            foreach (var item in query)
            {
                Console.WriteLine($"   - {item}");
            }
            Console.WriteLine();
        }


        private List<Item> GetTestCatalogue()
        {
            //Hvert tag oprettes EEN gang og genbruges - ellers får hver vare sit eget "Gaming"-tag i databasen
            var gaming = new Tag { Name = "Gaming" };
            var laptop = new Tag { Name = "Laptop" };
            var highPerformance = new Tag { Name = "HighPerformance" };

            return
           [
               new Item()
                {
                    Name = "Gaming Laptop",
                    Category = Category.Computer,
                    Price = new Price(12500m),
                    Tags = [gaming, laptop, highPerformance]
                },
                new Item()
                {
                    Name = "Office Laptop",
                    Category = Category.Computer,
                    Price = new Price(7500m),
                    Tags = [laptop]
                },
                new Item()
                {
                    Name = "Gaming Mus",
                    Category = Category.Accessory,
                    Price = new Price(650m),
                    Tags = [gaming]
                },
                new Item()
                {
                    Name = "Keyboard",
                    Category = Category.Computer,
                    Price = new Price(1100m),
                    Tags = [gaming]
                },
                new Item()
                {
                    Name = "4k Skærm",
                    Category = Category.Monitor,
                    Price = new Price(4500m),
                    Tags = [highPerformance]
                },
                new Item()
                {
                    Name = "Gaming Headset",
                    Category = Category.Accessory,
                    Price = new Price(1500m),
                    Tags = [gaming]
                },
                new Item()
                {
                    Name = "27\" Gaming Skærm",
                    Category = Category.Monitor,
                    Price = new Price(3500m),
                    Tags = [gaming, highPerformance]
                },
                new Item()
                {
                    Name = "USB-C Dock",
                    Category = Category.Accessory,
                    Price = new Price(1800m),
                    Tags = []
                },
                new Item()
                {
                    Name = "Macbook Air",
                    Category = Category.Computer,
                    Price = new Price(9500m),
                    Tags = [laptop, highPerformance]
                },
                new Item()
                {
                    Name = "Gaming PC",
                    Category = Category.Computer,
                    Price = new Price(15000m),
                    Tags = [gaming, highPerformance]
                },
                new Item()
                {
                    Name = "Webkamera",
                    Category = Category.Accessory,
                    Price = new Price(850m),
                    Tags = []
                },
                new Item()
                {
                    Name = "32\" 4K Skærm",
                    Category = Category.Monitor,
                    Price = new Price(5500m),
                    Tags = [highPerformance]
                }
           ];
        }
    }
}
