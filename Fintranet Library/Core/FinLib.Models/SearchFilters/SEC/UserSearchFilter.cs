using FinLib.Models.Base.SearchFilters;

namespace FinLib.Models.SearchFilters.SEC
{
    public class UserSearchFilter : Base.SearchFilters.UpdatableEntitySearchFilter
    {
        public SearchFilterItem<string> FirstName { get; set; }
        public SearchFilterItem<string> LastName { get; set; }
        public SearchFilterItem<string> UserName { get; set; }
        public SearchFilterItem<bool?> IsActive { get; set; }
    }
}
