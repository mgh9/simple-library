namespace FinLib.Models.Dtos.CNT
{
    public class MenuLinkDto : Base.Dto.GeneralDto
    {
        public int? ParentId { get; set; }
        public string Route { get; set; }
        public string Icon { get; set; }
        public int OrderNumber { get; set; }

        public List<MenuLinkDto> SubMenus { get; set; }

        public List<MenuLinkOwnerDto> Owners { get; set; }
    }
}
