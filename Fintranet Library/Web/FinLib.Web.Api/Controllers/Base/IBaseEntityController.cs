using FinLib.Models.Base;
using FinLib.Models.Base.Dto;
using FinLib.Models.Base.SearchFilters;
using FinLib.Models.Base.View;
using FinLib.Models.Dtos;
using FinLib.Services.Base;

namespace FinLib.Web.Api.Base
{
    public interface IBaseEntityController<TEntity, TDto, TView, in TEntityService, TSearchFilter>
        where TEntity : class, DomainClasses.Base.IBaseEntity, new()
        where TDto : BaseEntityDto, new()
        where TView : BaseView, new()
        where TEntityService : IBaseEntityService<TEntity, TDto, TView, TSearchFilter> //class
        where TSearchFilter : BaseEntitySearchFilter, new()
    {
        Task<JsonResult<TableData<TView>>> GetAsync(GetRequestDto<TSearchFilter> request);
        Task<JsonResult<TDto>> GetByIdAsync(int id);
    }
}
