namespace FinLib.Models.Base.View
{
    public class GeneralView : UpdatableView
    {
        [ViewColumn(nameof(Title), OrderNumber = 1)]
        public virtual string Title { get; set; }

        [ViewColumn("Is Active?", OrderNumber = 100)]
        public virtual bool IsActive { get; set; }
    }
}