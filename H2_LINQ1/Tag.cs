namespace H2_LINQ1
{
    internal class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        //Modsatrettet navigation - EF kraever den for at lave en many-to-many join-tabel
        public List<Item> Items { get; set; } = [];

        public override string ToString() => Name;
    }
}
