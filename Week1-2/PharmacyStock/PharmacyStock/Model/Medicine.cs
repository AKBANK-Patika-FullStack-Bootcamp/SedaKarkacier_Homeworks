namespace PharmacyStock.Model
{
    public class Medicine
    {
        public int Id { get; set; }
        public string? MedicineName { get; set; }
        public string? Company { get; set; }
        public string? InfluenceGroup { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
    }
}