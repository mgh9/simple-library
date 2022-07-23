using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FinLib.Web.Shared.Models.ViewModels.Account
{
    public class LoginViewModel 
    {
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        public bool IsCaptchaEnabled { get; set; }
        public string ReturnUrl { get; set; }
    }
}