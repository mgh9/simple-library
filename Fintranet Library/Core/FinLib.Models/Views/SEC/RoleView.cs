using FinLib.Models.Base;
using FinLib.Models.Base.View;

namespace FinLib.Models.Views.SEC
{
    public class RoleView : BaseView
    {
        [ViewColumn(nameof(Title))]
        public string Title { get; set; }

        [ViewColumn("Name")]
        public string Name { get; set; }
    }
}
