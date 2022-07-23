using FinLib.Common.Extensions;

namespace FinLib.Models.Dtos
{
    public class GetByUserRoleIdRequestDto<TSearchFilter> : GetRequestDto<TSearchFilter>
        where TSearchFilter : Models.Base.SearchFilters.BaseEntitySearchFilter, new()
    {
        public int UserRoleId { get; }

        public GetByUserRoleIdRequestDto(GetRequestDto<TSearchFilter> request, int userRoleId)
        {
            request.ThrowIfNull();

            PageSize = request.PageSize;
            PageIndex = request.PageIndex;
            PageOrder = request.PageOrder;
            SearchFilterModel = request.SearchFilterModel;
            UserRoleId = userRoleId;
        }
    }
}
