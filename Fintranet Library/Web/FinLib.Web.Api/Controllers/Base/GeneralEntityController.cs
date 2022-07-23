using FinLib.DomainClasses.Base;
using FinLib.Models.Base;
using FinLib.Models.Base.Dto;
using FinLib.Models.Base.SearchFilters;
using FinLib.Models.Base.View;
using FinLib.Models.Enums;
using FinLib.Services.Base;
using FinLib.Web.Shared.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinLib.Web.Api.Base
{
    public abstract class GeneralEntityController<TGeneralEntity, TGeneralDto, TGeneralView, TGeneralEntityService, TGeneralEntitySearchFilter>
        : UpdatableEntityController<TGeneralEntity, TGeneralDto, TGeneralView, TGeneralEntityService, TGeneralEntitySearchFilter>
            where TGeneralEntity : class, IGeneralEntity, new()
            where TGeneralDto : GeneralDto, new()
            where TGeneralView : GeneralView, new()
            where TGeneralEntityService : GeneralEntityService<TGeneralEntity, TGeneralDto, TGeneralView, TGeneralEntitySearchFilter>//, new()
            where TGeneralEntitySearchFilter : GeneralEntitySearchFilter, new()
    {
        protected GeneralEntityController(ICommonServicesProvider<FinLib.Models.Configs.GlobalSettings> commonServicesProvider, TGeneralEntityService generalEntityService)
            : base(commonServicesProvider, generalEntityService)
        {

        }

        [HttpGet]
        [MyAuthorize(ApplicationRole.Admin)]
        public virtual async Task<JsonResult<List<TitleValue<int>>>> GetGeneralEntitiesTitleValueListAsync(bool includeEmptySelector = false, bool includeSelectAllSelector = false, bool onlyActives = true, string text = null)
        {
            var result = new JsonResult<List<TitleValue<int>>>
            {
                Data = await Service.GetGeneralEntitiesTitleValueListAsync(includeEmptySelector, includeSelectAllSelector, onlyActives, text),
                Success = true
            };

            return result;
        }
    }
}
