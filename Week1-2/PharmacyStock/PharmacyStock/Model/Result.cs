namespace PharmacyStock.Model
{
    public class Result
    {
        public int status { get; set; }
        public string? message { get; set; }
        public List<Medicine>? MedicinesList { get; set; }
    }
}
