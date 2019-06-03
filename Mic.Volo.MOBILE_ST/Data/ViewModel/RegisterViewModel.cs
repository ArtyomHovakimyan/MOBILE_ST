using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mic.Volo.MOBILE_ST.Data.ViewModel
{
    public class User:IdentityUser
    {

    }
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }
        //[Required]
        //[DataType(DataType.Password)]
        //public string PasswordConfirm { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Enter the same passowrd")]
        [DataType(DataType.Password)]

        [Display(Name = "PasswordConfirm")]
        public string PasswordConfirm { get; set; }

       // public string ReturnUrl { get; set; }
    }
}
