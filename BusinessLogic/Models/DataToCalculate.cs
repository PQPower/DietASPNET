using DataAccessLayer.Enums;

namespace BusinessLogic.Models
{
    public class DataToCalculate
    {
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public LifeStyle LifeStyle { get; set; }
        public int Height { get; set; }
        public double Weight { get; set; }
        public Expectation Expectation { get; set; }
    }
}
