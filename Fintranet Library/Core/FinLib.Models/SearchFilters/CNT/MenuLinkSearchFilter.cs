using FinLib.Models.Base.SearchFilters;

namespace FinLib.Models.SearchFilters.CNT
{
    public class MenuLinkSearchFilter : Base.SearchFilters.GeneralEntitySearchFilter
    {
        public SearchFilterItem<string> ParentMenuLinkTitle { get; set; }
    }
}
