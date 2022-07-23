using System.ComponentModel.DataAnnotations.Schema;

namespace FinLib.DomainClasses.Base
{
    public class BaseEntity : Base.IBaseEntity
    {
        public int Id { get; set; }

        [ForeignKey("CreateByUserRole")]
        public int? CreatedByUserRoleId { get; set; }
        public SEC.UserRole CreateByUserRole { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
