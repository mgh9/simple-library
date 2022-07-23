using FinLib.Models.Base.SearchFilters;

namespace FinLib.Models.SearchFilters.DBO
{
    public class MyBorrowingHistorySearchFilter : Base.SearchFilters.BaseEntitySearchFilter
    {
        public SearchFilterItem<int?> BookId { get; set; }
    }
}
