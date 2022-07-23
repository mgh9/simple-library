namespace FinLib.Models.Base.SearchFilters
{
    /// <summary>
    /// فیلتر جستجو اطلاعات پایه
    /// </summary>
    public class GeneralEntitySearchFilter : UpdatableEntitySearchFilter
    {        
        public SearchFilterItem<string> Title { get; set; }
        public SearchFilterItem<bool?> IsActive { get; set; }
    }
}
