using FinLib.Models.Base.SearchFilters;

namespace FinLib.Models.SearchFilters.DBO
{
    public class BookSearchFilter : Base.SearchFilters.GeneralEntitySearchFilter
    {
        public SearchFilterItem<int?> CategoryId { get; set; }
    }
}
