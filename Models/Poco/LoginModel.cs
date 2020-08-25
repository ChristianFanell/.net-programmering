using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LabbUppgift3.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Fyll i användarnamn")]
        [Display(Name = "Användarnamn:")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Fyll i lösenord")]
        [Display(Name = "Lösenord:")]
        [UIHint("password")]
        public string Password { get; set; }


        public string ReturnUrl { get; set; }
    }
}
