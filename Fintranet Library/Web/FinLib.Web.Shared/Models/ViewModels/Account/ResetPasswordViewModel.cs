using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinLib.Web.Shared.Models.ViewModels.Account
{
    public class ResetPasswordViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا 'رمز عبور جدید' خود را وارد نمایید")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "'رمز عبور جدید' و 'تکرار رمز عبور جدید' باید یکسان باشد")]
        [Required(ErrorMessage = "لطفا تکرار رمز عبور را وارد نمایید")]
        public string NewPasswordRepeat { get; set; }

        public string ReturnUrl { get; set; }

        public List<string> PasswordPolicies { get; set; }
    }
}