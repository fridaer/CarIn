using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarIn.Models.ViewModels
{
    public class RegisterNewUserVm
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Användarnamn")]
        public string NewUsername { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Lösenord")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Upprepa lösenord")]
        [Compare("NewPassword", ErrorMessage = "Lösenorden stämmer inte")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }

        public string ErrorMessage { get; set; }

    }
}