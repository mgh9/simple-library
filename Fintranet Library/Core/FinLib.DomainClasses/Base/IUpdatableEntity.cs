namespace FinLib.DomainClasses.Base
{
    public interface IUpdatableEntity : IBaseEntity
    {
        DateTime? UpdateDate { get; set; }
        int? UpdatedByUserRoleId { get; set; }
    }
}
