using FinLib.Models.Base.View;
using FinLib.Models.Base;

namespace FinLib.Models.Views.CNT
{
    public class MenuLinkView : GeneralView
    {
        [ViewColumn("Main Menu", OrderNumber = 1)]
        public string ParentMenuLinkTitle { get; set; }

        [ViewColumn(nameof(Route), OrderNumber = 2)]
        public string Route { get; set; }

        [ViewColumn("Order", OrderNumber = 3)]
        public int OrderNumber { get; set; }
    }
}
