using FinLib.Models.Base;
using FinLib.Models.Base.Dto;
using FinLib.Models.Base.SearchFilters;
using FinLib.Models.Base.View;
using FinLib.Services.Base;

namespace FinLib.Web.Api.Base
{
    public interface IUpdatableEntityController
        <TUpdatableEntity, TUpdatableDto, TUpdatableView, in TUpdatableEntityService, TUpdatableSearchFilter>
            : IBaseEntityController<TUpdatableEntity, TUpdatableDto, TUpdatableView, TUpdatableEntityService, TUpdatableSearchFilter>
        where TUpdatableEntity : class, DomainClasses.Base.IUpdatableEntity, new()
        where TUpdatableDto : UpdatableDto, new()
        where TUpdatableView : UpdatableView, new()
        where TUpdatableEntityService : IUpdatableEntityService<TUpdatableEntity, TUpdatableDto, TUpdatableView, TUpdatableSearchFilter> //, new //class
        where TUpdatableSearchFilter : UpdatableEntitySearchFilter, new()
    {
        Task<JsonResult<int>> InsertAsync(TUpdatableDto model);

        Task<JsonResult> UpdateAsync(TUpdatableDto model);
       
        Task<JsonResult> DeleteAsync(int id);
    }
}
