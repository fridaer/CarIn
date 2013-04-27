using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarIn.Models.ViewModels
{
    public class ChangePasswordVm
    {
        [ScaffoldColumn(false)]
        public int userId { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Användarnamn")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "*")]
        [Display(Name = "Nuvarande lösenord")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Nytt lösenord")]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Upprepa lösenord")]
        [Compare("NewPassword", ErrorMessage = "*")]
        public string ConfirmNewPassword { get; set; }

    }
}