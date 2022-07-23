namespace FinLib.Models.Dtos.SEC
{
    public class UserRoleDto : Base.Dto.UpdatableDto
    {
        public int UserId { get; set; }
        
        public int RoleId { get; set; }

        public string RoleName { get; set; }
        public string RoleTitle { get; set; }

        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
    }
}
