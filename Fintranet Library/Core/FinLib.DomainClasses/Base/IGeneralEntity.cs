namespace FinLib.DomainClasses.Base
{
    public interface IGeneralEntity : IUpdatableEntity
    {
        string Title { get; set; }

        string Description { get; set; }

        bool IsActive { get; set; }
    }
}