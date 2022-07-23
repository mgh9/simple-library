using FinLib.DomainClasses.Base;
using FinLib.DomainClasses.SEC;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinLib.DomainClasses.CNT
{
    [Table("MenuLinkOwners", Schema = "CNT")]
    public class MenuLinkOwner : UpdatableEntity
    {
        [ForeignKey("MenuLink")]
        public int MenuLinkId { get; set; }
        public MenuLink MenuLink { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
