namespace FinLib.Models.Base.SearchFilters
{
    public class GeneralEntitySearchFilter : UpdatableEntitySearchFilter
    {        
        public SearchFilterItem<string> Title { get; set; }
        public SearchFilterItem<bool?> IsActive { get; set; }
    }
}
