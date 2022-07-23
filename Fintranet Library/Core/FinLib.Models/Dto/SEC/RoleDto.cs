using FinLib.Common.Helpers;

namespace FinLib.Models.Dtos.SEC
{
    public class RoleDto : Base.Dto.BaseEntityDto
    {
        public string Name { get; set; }
        
        public string Title { get; set; }

        public Models.Enums.ApplicationRole RoleKey
        {
            get
            {
                return EnumHelper.ParseEnum<Enums.ApplicationRole>(Name);
            }
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
