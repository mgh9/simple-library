using FinLib.DomainClasses.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinLib.DomainClasses.CNT
{
    [Table("MenuLinks", Schema = "CNT")]
    public class MenuLink : GeneralEntity
    {
        [ForeignKey("ParentMenuLink")]
        public int? ParentId { get; set; }
        public MenuLink ParentMenuLink { get; set; }

        public string Route { get; set; }

        public string Icon { get; set; }

        public int OrderNumber { get; set; }
    }
}
