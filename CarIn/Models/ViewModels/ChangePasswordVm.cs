using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarIn.Models.ViewModels
{
    public class ChangePasswordVm
    {
        [Required(ErrorMessage = "*")]
        public string Username { get; set; }

        [Required(ErrorMessage = "*")]
<<<<<<< HEAD
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "*")]
        public string ConfirmeNewPassword { get; set; }
=======
        [Display(Name = "Nuvarande lösenord")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Nytt lösenord")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Upprepa lösenord")]
        [Compare("NewPassword", ErrorMessage = "Lösenorden stämmer inte")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
>>>>>>> changePass

    }
}