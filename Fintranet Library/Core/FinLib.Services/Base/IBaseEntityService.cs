using FinLib.Models.Base;
using FinLib.Models.Base.SearchFilters;
using FinLib.Models.Base.View;
using FinLib.Models.Dtos;
using System.Linq.Expressions;

namespace FinLib.Services.Base
{
    public interface IBaseEntityService<TEntity, TDto, TView, TSearchFilter>
        where TEntity : class, DomainClasses.Base.IBaseEntity, new()
        where TDto : class, new()
        where TView : BaseView, new()
        where TSearchFilter : BaseEntitySearchFilter, new()
    {
        Task<List<TDto>> GetAsync();
        Task<List<TDto>> GetAsync(Expression<Func<TEntity, bool>> where);

        Task<GetResultDto<TView>> GetAsync(GetRequestDto<TSearchFilter> model);

        Task<List<TitleValue<int>>> GetTitleValueListAsync(bool includeEmptySelector = false, bool includeSelectAllSelector = false, string text = null);

        Task<int> CountAsync(TSearchFilter searchFilters);

        Task<TDto> GetByIdAsync(int id);
    }
}
