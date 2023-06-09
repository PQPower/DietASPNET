﻿using DataAccessLayer.Entities;
using DataAccessLayer.Enums;

namespace DietTracker.Models
{
    public class WeightViewModel
    {
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public LifeStyle LifeStyle { get; set; }
        public int Height { get; set; }
        public double Weight { get; set; }
        public Expectation Expectation { get; set; }
        public int CaloriesPerDay { get; set; }
    }
}
