using FinLib.Models.Configs.Identity;

namespace FinLib.Models.Dto.SEC
{
    public sealed class ChangePasswordConfigDto : Base.Dto.BaseConfigDto
    {
        public PasswordPolicy PasswordPolicy { get; set; }
    }
}