using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarIn.Models.ViewModels
{
    public class RegisterNewUserVm
    {
        [ScaffoldColumn(false)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Användarnamn")]
        public string Username { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Lösenord")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Upprepa lösenord")]
        [Compare("Password", ErrorMessage = "Lösenorden stämmer inte")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }

        public string ErrorMessage { get; set; }

    }
}