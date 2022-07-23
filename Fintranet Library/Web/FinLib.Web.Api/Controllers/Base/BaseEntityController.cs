using FinLib.Common.Exceptions.Infra;
using FinLib.Common.Extensions;
using FinLib.DomainClasses.Base;
using FinLib.Models.Base;
using FinLib.Models.Base.Dto;
using FinLib.Models.Base.SearchFilters;
using FinLib.Models.Base.View;
using FinLib.Models.Configs;
using FinLib.Models.Dtos;
using FinLib.Models.Enums;
using FinLib.Providers.Database;
using FinLib.Services.Base;
using FinLib.Web.Shared.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace FinLib.Web.Api.Base
{
    public abstract class BaseEntityController<TEntity, TDto, TView, TEntityService, TSearchFilter>
            : BaseController, IBaseEntityController<TEntity, TDto, TView, TEntityService, TSearchFilter>, ITitleValueProvider
                    where TEntity : class, IBaseEntity, new()
                    where TDto : BaseEntityDto, new()
                    where TView : BaseView, new()
                    where TEntityService : BaseEntityService<TEntity, TDto, TView, TSearchFilter>//, new()
                    where TSearchFilter : BaseEntitySearchFilter, new()
    {
        protected BaseEntityController(ICommonServicesProvider<GlobalSettings> commonServicesProvider, TEntityService entityService)
            : base(commonServicesProvider)
        {
            createServiceInstance(commonServicesProvider, entityService);
        }

        private void createServiceInstance(ICommonServicesProvider<GlobalSettings> commonServicesProvider, TEntityService entityService)
        {
            var typeOfService = typeof(TEntityService);

            var constructors = typeOfService.GetConstructors();
            var firstConstrutor = constructors.FirstOrDefault(); //assume we will have only one constructor
            var parameters = new List<object>();

            foreach (var param in firstConstrutor.GetParameters())
            {
                var service = commonServicesProvider.ServiceProvider.GetService(param.ParameterType); //get instance of the class
                parameters.Add(service);
            }

            this.Service = entityService;
        }

        /// <summary>
        /// بیزینس-سرویس موجودیت جاری در این کنترلر
        /// </summary>
        protected TEntityService Service { get; private set; }

        /// <summary>
        /// دریافت لیست موجودیت ها با پارامترهای جستجو و صفحه بندی
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [
            // بخاطر پارامتر ورودی که کامپلکس هست، یه راه همینه که بصورت پٌست دریافت کنیم و نه گِت
            HttpPost,
            // چون ماهیت اصلی این عملیات، گِت هست، پس اعتبارسنجی توکن آنتی فورجری رو ایگنور میکنیم براش
            IgnoreAntiforgeryToken
        ]
        [MyAuthorize()]
        public virtual async Task<JsonResult<TableData<TView>>> GetAsync([FromBody] GetRequestDto<TSearchFilter> request)
        {
            var result = new JsonResult<TableData<TView>>();

            ValidateGetRequest(request);

            var columns = EntityMetaDataProvider.GetColumns(typeof(TView));
            var serviceResult = await Service.GetAsync(request);

            result.Data = new TableData<TView>(columns, serviceResult.Data, serviceResult.Count);
            result.Success = true;

            return result;
        }

        /// <summary>
        /// دریافت لیست موجودیت بصورت کد-مقدار و بر اساس 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, IgnoreAntiforgeryToken]
        [MyAuthorize()]
        public virtual async Task<JsonResult<List<TitleValue<int>>>> GetTitleValueListAsync([FromQuery] SearchAutocompleteDto model)
        {
            model.ThrowIfNull();

            var result = new JsonResult<List<TitleValue<int>>>();

            result.Data = await Service.GetTitleValueListAsync(text: model.Text);

            result.Success = true;
            return result;
        }

        [HttpGet]
        [MyAuthorize()]
        public virtual async Task<JsonResult<List<TitleValue<int>>>> GetTitleValueListAsync(bool includeEmptySelector = false, bool includeSelectAllSelector = false, string text = "")
        {
            var result = new JsonResult<List<TitleValue<int>>>
            {
                Data = await Service.GetTitleValueListAsync(includeEmptySelector, includeSelectAllSelector, text),
                Success = true
            };

            return result;
        }

        [HttpGet]
        [MyAuthorize()]
        public virtual async Task<JsonResult<TDto>> GetByIdAsync(int id)
        {
            var result = new JsonResult<TDto>
            {
                Data = await Service.GetByIdAsync(id),
                Success = true
            };

            return result;
        }

        [HttpGet]
        [MyAuthorize()]
        public virtual async Task<JsonResult<TView>> GetAsViewByIdAsync(int id)
        {
            var result = new JsonResult<TView>
            {
                Data = await Service.GetAsViewByIdAsync(id),
                Success = true
            };

            return result;
        }

        protected void ValidateGetRequest(GetRequestDto<TSearchFilter> request)
        {
            request.ThrowIfNull();
            request.SearchFilterModel.ThrowIfNull();
        }

        protected void ValidateGetByUserRoleIdRequest(GetByUserRoleIdRequestDto<TSearchFilter> request)
        {
            ValidateGetRequest(request);

            if (request.UserRoleId <= 0)
            {
                throw new InvalidPrimaryKeyException(nameof(request.UserRoleId));
            }
        }
    }
}
