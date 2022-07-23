using FinLib.Models.Base.Dto;
using FinLib.Models.Dtos.SEC;

namespace FinLib.Models.Dto.SEC
{
    public class UserInfoDto : BaseEntityDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

        public string ImageUrl { get; set; }
        public string ImageAbsoluteUrl { get; set; }

        /// <summary>
        /// نقش فعال این کاربر
        /// </summary>
        public RoleDto ActiveRole { get; set; }
        public int DefaultUserRoleId { get; set; }
        public List<UserRoleDto> UserRoles { get; set; }
    }
}
