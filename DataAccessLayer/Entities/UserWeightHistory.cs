namespace DataAccessLayer.Entities
{
    public class UserWeightHistory : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime UpdatedDate { get; set; }
        public double Weight { get; set; }
    }
}
