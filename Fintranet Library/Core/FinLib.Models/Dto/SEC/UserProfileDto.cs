using FinLib.Models.Base.Dto;
using FinLib.Models.Enums;

namespace FinLib.Models.Dtos.SEC.User.Profile
{
    public sealed class UserProfileDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Mobile{ get; set; }
        
        public Gender? Gender { get; set; }
        
        public string ImageUrl { get; set; }
        public string ImageAbsoluteUrl { get; set; }

        public DateTime? BirthDate { get; set; }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLoggedInTime { get; set; }

        public List<RoleDto> Roles { get; set; }
    }
}