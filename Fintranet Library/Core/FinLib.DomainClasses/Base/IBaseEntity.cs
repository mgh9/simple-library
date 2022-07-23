namespace FinLib.DomainClasses.Base
{
    public interface IBaseEntity
    {
        int Id { get; set; }

        DateTime CreateDate { get; set; }
        int? CreatedByUserRoleId { get; set; }
    }
}
