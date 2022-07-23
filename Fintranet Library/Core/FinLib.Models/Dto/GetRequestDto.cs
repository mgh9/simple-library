namespace FinLib.Models.Dtos
{
    public class GetRequestDto<TSearchFilter> : Base.Dto.BaseDto
        where TSearchFilter : Models.Base.SearchFilters.BaseEntitySearchFilter, new()
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string PageOrder { get; set; }
        public TSearchFilter SearchFilterModel { get; set; }
    }
}
