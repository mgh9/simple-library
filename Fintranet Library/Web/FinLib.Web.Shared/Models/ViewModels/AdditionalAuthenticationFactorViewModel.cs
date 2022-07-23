using System.ComponentModel.DataAnnotations;

namespace FinLib.Web.Shared.Models.ViewModels
{
    public class AdditionalAuthenticationFactorViewModel
    {
        [Required]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        public bool RememberLogin { get; set; }
        
        public string Provider  { get; set; }
        
        public string ProviderUserId  { get; set; }
    }
}