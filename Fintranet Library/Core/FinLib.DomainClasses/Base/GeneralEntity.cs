namespace FinLib.DomainClasses.Base
{
    public class GeneralEntity : Base.UpdatableEntity, IGeneralEntity
    {
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
    }
}
