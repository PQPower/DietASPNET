using DataAccessLayer.Entities;

namespace DietTracker.Models
{
    public class WeightViewModel
    {
        public User User { get; set; }

        public int CaloriesPerDay { get; set; }
    }
}
