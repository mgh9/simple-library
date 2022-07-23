using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinLib.DomainClasses.SEC
{
    [System.ComponentModel.Description("Roles of the user")]
    public class UserRole : IdentityUserRole<int>, Base.IUpdatableEntity
    {
        public int Id { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActive { get; set; }

        [ForeignKey("CreateByUserRole")]
        public int? CreatedByUserRoleId { get; set; }
        public SEC.UserRole CreateByUserRole { get; set; }
        public DateTime CreateDate { get; set; }

        [ForeignKey("UpdatedByUserRole")]
        public int? UpdatedByUserRoleId { get; set; }
        public SEC.UserRole UpdatedByUserRole { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
