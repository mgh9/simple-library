using FinLib.Models.Base.Dto;

namespace FinLib.Models.Dto.SEC
{
    public class UserProfileConfigDto : BaseConfigDto
    {
        public ChangePasswordConfigDto ChangePasswordConfig { get; set; }
    }
}
