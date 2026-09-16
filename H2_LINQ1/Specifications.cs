namespace H2_LINQ1
{
    //Indlejret struktur der gemmes som ét JSON-dokument i kolonnen Items.Specifications.
    //Laptops og skærme har forskellige felter - de felter en vare ikke bruger, er bare null.
    internal class Specifications
    {
        //Fælles for alle
        public string Brand { get; set; } = "";

        //Laptop
        public string? Processor { get; set; }
        public string? Ram { get; set; }
        public string? Storage { get; set; }

        //Skærm
        public string? Resolution { get; set; }
        public string? Size { get; set; }
        public string? RefreshRate { get; set; }

        public override string ToString()
        {
            (string Navn, string? Værdi)[] felter =
            [
                ("brand", Brand),
                ("processor", Processor),
                ("ram", Ram),
                ("storage", Storage),
                ("resolution", Resolution),
                ("size", Size),
                ("refreshRate", RefreshRate)
            ];

            //Udskriv kun de felter varen faktisk har
            return string.Join(", ", felter.Where(f => f.Værdi != null).Select(f => $"{f.Navn}: {f.Værdi}"));
        }
    }
}
