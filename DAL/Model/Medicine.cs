namespace DAL.Model
{
    public class Medicine
    {
        public int MedicineId { get; set; }
        public string? MedicineName { get; set; }
        public string? Company { get; set; }
        public string? InfluenceGroup { get; set; }
        public string? Price { get; set; }
        public string? Stock { get; set; }
        public int FeatureId { get; set; }
    }
}