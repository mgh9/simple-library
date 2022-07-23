using FinLib.Common.Extensions;
using FinLib.DomainClasses.Base;
using FinLib.Models.Base;
using FinLib.Models.Base.Dto;
using FinLib.Models.Base.SearchFilters;
using FinLib.Models.Base.View;
using FinLib.Models.Configs;
using FinLib.Models.Enums;
using FinLib.Services.Base;
using FinLib.Web.Shared.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JsonResult = FinLib.Models.Base.JsonResult;

namespace FinLib.Web.Api.Base
{
    public class UpdatableEntityController<TUpdatableEntity, TUpdatableDto, TUpdatableView, TUpdatableEntityService, TUpdatableSearchFilter>
        : BaseEntityController<TUpdatableEntity, TUpdatableDto, TUpdatableView, TUpdatableEntityService, TUpdatableSearchFilter>
            , IUpdatableEntityController<TUpdatableEntity, TUpdatableDto, TUpdatableView, TUpdatableEntityService, TUpdatableSearchFilter>
        where TUpdatableEntity : class, IUpdatableEntity, new()
        where TUpdatableDto : UpdatableDto, new()
        where TUpdatableView : UpdatableView, new()
        where TUpdatableEntityService : UpdatableEntityService<TUpdatableEntity, TUpdatableDto, TUpdatableView, TUpdatableSearchFilter>
        where TUpdatableSearchFilter : UpdatableEntitySearchFilter, new()
    {
        public UpdatableEntityController(ICommonServicesProvider<GlobalSettings> commonServicesProvider, TUpdatableEntityService updatableEntityService)
            : base(commonServicesProvider, updatableEntityService)
        {

        }

        protected virtual void ValidateOnInsert(TUpdatableDto model)
        {
            model.ThrowIfNull();

            // validation logic in child classes
        }

        protected virtual void ValidateOnUpdate(TUpdatableDto model)
        {
            model.ThrowIfNull();

            // validation logic in child classes
        }

        protected virtual void ValidateOnDelete(int id)
        {
            // validation logic in child classes
        }

        [MyAuthorize(ApplicationRole.Admin)]
        [HttpPost]
        public async virtual Task<JsonResult<int>> InsertAsync([FromBody] TUpdatableDto model)
        {
            ValidateOnInsert(model);

            var result = new JsonResult<int>();

            await Service.InsertAsync(model);

            result.Data = model.Id;
            result.Success = true;

            return result;
        }

        [MyAuthorize(ApplicationRole.Admin)]
        [HttpPut]
        //[ValidateAntiForgeryToken]
        public async virtual Task<JsonResult> UpdateAsync([FromBody] TUpdatableDto model)
        {
            ValidateOnUpdate(model);

            var result = new JsonResult();

            await Service.UpdateAsync(model);
            result.Success = true;

            return result;
        }

        [MyAuthorize(ApplicationRole.Admin)]
        [HttpDelete]
        public async virtual Task<JsonResult> DeleteAsync(int id)
        {
            ValidateOnDelete(id);

            var result = new JsonResult();

            await Service.DeleteAsync(id);
            result.Success = true;

            return result;
        }
    }
}
