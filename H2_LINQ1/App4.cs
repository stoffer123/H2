using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace H2_LINQ1
{
    internal class App4 : IApplication
    {
        public void Run()
        {
            string xmlPath = Path.Combine(AppContext.BaseDirectory, "xml", "ItemData.xml");
            XDocument xDoc = XDocument.Load(xmlPath);

            SøgOgFiltrer(LoadItems(xDoc));
            Crud(xDoc, xmlPath);
        }

        //Laver <Item>-elementerne om til Item-objekter
        private List<Item> LoadItems(XDocument xDoc)
        {
            return xDoc.Descendants("Item").Select(e => new Item
            {
                Name = (string)e.Element("Name")!,
                Category = (string)e.Element("Category")! switch
                {
                    "Computer" => Category.Computer,
                    "Tilbehør" => Category.Accessory,
                    "Skærm" => Category.Monitor,
                    var ukendt => throw new InvalidDataException($"Ukendt kategori: {ukendt}")
                },
                Price = new Price((decimal)e.Element("Price")!)
            }).ToList();
        }

        //2. CRUD i XML - ændringen gemmes i filen, og filen læses ind igen for at vise at den er gemt
        private void Crud(XDocument xDoc, string xmlPath)
        {
            Console.WriteLine("=== 2. CRUD i XML ===");
            Console.WriteLine();

            const string navn = "Gaming Keyboard";

            //2.1 Create - tilføj et nyt <Item> under roden <Items>
            //Tjek først om det allerede findes, hvis en tidligere kørsel stoppede før sletningen
            if (!xDoc.Descendants("Item").Any(e => (string?)e.Element("Name") == navn))
            {
                xDoc.Root!.Add(new XElement("Item",
                    new XElement("Name", navn),
                    new XElement("Category", "Tilbehør"),
                    new XElement("Price", 1200m)));
            }
            xDoc.Save(xmlPath);
            Print("2.1 Efter oprettelse af \"Gaming Keyboard\" (1.200 kr.)", LoadItems(XDocument.Load(xmlPath)));

            //2.2 Update - find elementet og sæt en ny værdi i <Price>
            XElement tilOpdatering = xDoc.Descendants("Item").First(e => (string?)e.Element("Name") == navn);
            tilOpdatering.SetElementValue("Price", 1350m);
            xDoc.Save(xmlPath);
            Print("2.2 Efter prisændring til 1.350 kr.", LoadItems(XDocument.Load(xmlPath)));

            //2.3 Delete - fjern hele <Item>-elementet fra dokumentet
            XElement tilSletning = xDoc.Descendants("Item").First(e => (string?)e.Element("Name") == navn);
            tilSletning.Remove();
            xDoc.Save(xmlPath);
            Print("2.3 Efter sletning af \"Gaming Keyboard\"", LoadItems(XDocument.Load(xmlPath)));
        }

        //1.1 Søg og filtrér data
        private void SøgOgFiltrer(List<Item> list)
        {
            Console.WriteLine("=== 1.1 Søg og filtrér (XML) ===");
            Console.WriteLine();

            //a. Alle produkter i kategorien "Computer"
            Print("a. Kategorien \"Computer\"",
                list.Where(i => i.Category == Category.Computer));

            //b. Alle produkter der koster mere end 5.000 kr.
            Print("b. Dyrere end 5.000 kr.",
                list.Where(i => i.Price > 5000m));

            //c. Alle produkter mellem 1.000 og 5.000 kr.
            Print("c. Mellem 1.000 og 5.000 kr.",
                list.Where(i => i.Price > 1000m && i.Price < 5000m));

            //d. Kategorien "Tilbehør" over 1.000 kr.
            Print("d. Tilbehør over 1.000 kr.",
                list.Where(i => i.Category == Category.Accessory && i.Price > 1000m));

            //e. Navnet indeholder "Gaming" - uden forskel på store og små bogstaver
            Print("e. Navn indeholder \"Gaming\"",
                list.Where(i => i.Name.Contains("Gaming", StringComparison.OrdinalIgnoreCase)));
        }

        private void Print(string overskrift, IEnumerable<Item> resultat)
        {
            Console.WriteLine(overskrift);
            foreach (var item in resultat)
            {
                Console.WriteLine($"   - {item}");
            }
            Console.WriteLine();
        }
    }
}
