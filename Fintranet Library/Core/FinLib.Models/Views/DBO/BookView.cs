using FinLib.Models.Base.View;
using FinLib.Models.Base;

namespace FinLib.Models.Views.DBO
{
    public class BookView : GeneralView
    {
        [ViewColumn("Category")]
        public string CategoryTitle { get; set; }
    }
}
