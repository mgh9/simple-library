using System.ComponentModel.DataAnnotations;

namespace FinLib.Web.Shared.Models.ViewModels.Account
{
    public class RecoverPasswordViewModel
    {
        //[Required(ErrorMessage = "لطفا نوع بازیابی را انتخاب نمایید")]
        //public MessageProviderType SendOtpType { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "لطفا 'نام کاربری' خود را وارد نمایید")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false)]//, ErrorMessage = "لطفا 'شماره همراه' خود را وارد نمایید")]
        public string UserContact { get; set; }
    }
}