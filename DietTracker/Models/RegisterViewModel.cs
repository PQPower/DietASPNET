using DataAccessLayer.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DietTracker.Models
{
    public class RegisterViewModel
    {
        [Remote(action: "CheckLogin", controller: "Account", ErrorMessage = "This login is already in use")]
        [Required(ErrorMessage = "Enter Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "ConfirmPassword")]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public LifeStyle LifeStyle { get; set; }

    }
}
