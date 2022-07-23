namespace FinLib.Models.Dtos.CNT
{
    public class MenuLinkOwnerDto : Base.Dto.UpdatableDto
    {
        public int MenuLinkId { get; set; }
        public int RoleId { get; set; }
        public string RoleTitle { get; set; }
    }
}
