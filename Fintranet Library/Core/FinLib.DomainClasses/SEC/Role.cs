using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinLib.DomainClasses.SEC
{
    [System.ComponentModel.Description("Roels in the system")]
    public sealed class Role : IdentityRole<int>, Base.IBaseEntity
    {
        public string Title { get; set; }

        public override string ToString()
        {
            return Title;
        }

        public DateTime CreateDate { get; set; }

        [ForeignKey("CreateByUserRole")]
        public int? CreatedByUserRoleId { get; set; }
        public SEC.UserRole CreateByUserRole { get; set; }
    }
}
