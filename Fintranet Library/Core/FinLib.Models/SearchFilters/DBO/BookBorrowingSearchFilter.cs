using FinLib.Models.Base.SearchFilters;

namespace FinLib.Models.SearchFilters.DBO
{
    public class BookBorrowingSearchFilter : Base.SearchFilters.UpdatableEntitySearchFilter
    {
        public SearchFilterItem<int?> BookId { get; set; }
        public SearchFilterItem<int?> CustomerUserRoleId { get; set; }
    }
}
