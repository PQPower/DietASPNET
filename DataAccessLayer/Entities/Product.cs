namespace DataAccessLayer.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public double? Fat { get; set; }
        public double? Protein { get; set; }
        public double? Carbs { get; set; }
        public double? Calories { get; set; }
    }
}
