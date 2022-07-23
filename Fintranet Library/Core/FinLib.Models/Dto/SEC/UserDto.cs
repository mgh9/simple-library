using FinLib.Models.Enums;

namespace FinLib.Models.Dtos.SEC
{
    public class UserDto : Base.Dto.UpdatableDto
    {
        #region Personal Related
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }

        public Gender? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        
        public string Mobile { get; set; }
        public string ImageUrl { get; set; }
        #endregion


        #region Identity Related

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLoggedInTime { get; set; }
        public string LockoutDescription { get; set; }

        public List<UserRoleDto> UserRoles { get; set; }

        #endregion

        public override string ToString()
        {
            return UserName;
        }
    }
}