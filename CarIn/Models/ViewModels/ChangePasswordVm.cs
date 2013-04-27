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
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "*")]
        public string ConfirmeNewPassword { get; set; }

    }
}