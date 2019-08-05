using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopAPI.Models
{
    public class RegisterViewModel
    {
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "Enter the email")]
        public string Login { get; set; }
        [Required(ErrorMessage ="Enter the name ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter the surname ")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Enter the password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are not identical")]
        public string ConfirmPassword { get; set; }
    }
}
