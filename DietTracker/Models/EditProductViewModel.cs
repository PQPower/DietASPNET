using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DietTracker.Models
{
    public class EditProductViewModel
    {
        public Product Product { get; set; }
        [Required]
        [Remote(action: "CheckProductName", controller: "Product", ErrorMessage = "This Product name is already in use")]
        public string NewProductName { get; set; }
        [Required]
        public int? Fat { get; set; }
        [Required]
        public int? Protein { get; set; }
        [Required]
        public int? Carbs { get; set; }
        [Required]
        public int? Calories { get; set; }
    }
}
