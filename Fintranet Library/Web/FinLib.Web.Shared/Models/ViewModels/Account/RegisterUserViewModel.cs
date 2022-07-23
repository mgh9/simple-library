using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinLib.Web.Shared.Models.ViewModels.Account
{
    public class RegisterUserViewModel : RegistrationInputModel
    {
        // credentials       
        [MaxLength(30, ErrorMessage = "طول نام کاربری باید بیشتر از 4 و کمتر از 30 باشد")]
        [MinLength(4, ErrorMessage = "طول نام کاربری باید بیشتر از 4 و کمتر از 30 باشد")]
        [Required(ErrorMessage = "نام کاربری را وارد نمایید")]
        public string UserName { get; set; }

        [MaxLength(30, ErrorMessage = "طول رمز عبور باید بین 4 تا 30 کاراکتر باشد")]
        [MinLength(4, ErrorMessage = "طول رمز عبور باید بین 4 تا 30 کارکتر باشد")]
        [Required(ErrorMessage = "رمز عبور را وارد نمایید")]
        [PasswordPropertyText()]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "رمز عبور و تکرار رمز عبور باید یکسان باشد")]
        [Required(ErrorMessage = "تکرار رمز عبور را وارد نمایید")]
        public string PasswordRepeat { get; set; }

        // claims 
        [Required(ErrorMessage = "نام خود را وارد نمایید")]
        [MaxLength(30, ErrorMessage = "طول نام باید کمتر از 30 کاراکتر باشد")]
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی را وارد نمایید")]
        [MaxLength(50, ErrorMessage = "طول نام خانوادگی باید کمتر از کاراکتر باشد")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "کد ملی را وارد نمایید")]
        [StringLength(10, ErrorMessage = "طول کد ملی باید 10 رقم باشد")]
        public string NationalCode { get; set; }
    }
}
