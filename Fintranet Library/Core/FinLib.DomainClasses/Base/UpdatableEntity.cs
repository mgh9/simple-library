using System.ComponentModel.DataAnnotations.Schema;

namespace FinLib.DomainClasses.Base
{
    public class UpdatableEntity : BaseEntity, Base.IUpdatableEntity
    {
        [ForeignKey("UpdatedByUserRole")]
        public int? UpdatedByUserRoleId { get; set; }
        public SEC.UserRole UpdatedByUserRole { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
