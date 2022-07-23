using FinLib.DomainClasses.Base;
using FinLib.Mappings;
using FinLib.Models.Base;
using FinLib.Models.Base.Dto;
using FinLib.Models.Base.SearchFilters;
using FinLib.Models.Base.View;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FinLib.Services.Base
{
    public class GeneralEntityService<TGeneralEntity, TGeneralDto, TGeneralView, TGeneralEntitySearchFilter>
        : UpdatableEntityService<TGeneralEntity, TGeneralDto, TGeneralView, TGeneralEntitySearchFilter>
        where TGeneralEntity : class, IGeneralEntity, new()
        where TGeneralDto : GeneralDto, new()
        where TGeneralView : GeneralView, new()
        where TGeneralEntitySearchFilter : GeneralEntitySearchFilter, new()
    {
        public GeneralEntityService(ICommonServicesProvider<FinLib.Models.Configs.GlobalSettings> commonServicesProvider)
            : base(commonServicesProvider)
        { }

        public override string DefaultOrderbyColumnName => nameof(IGeneralEntity.Title);

        internal protected virtual IQueryable<TGeneralEntity> GetOrderedList()
        {
            return DbContext.Set<TGeneralEntity>()
                            .OrderBy(x => x.Title);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="onlyActives">true : faghat Active haa ro return mikone, false: tamame item haa</param>
        /// <param name="text"></param>
        /// <param name="userRoleID"></param>
        /// <returns></returns>
        public virtual async Task<List<TitleValue<int>>> GetGeneralEntitiesTitleValueListAsync(bool includeEmptySelector = false, bool includeSelectAllSelector = false, bool onlyActives = true, string text = null)
        {
            var query = GetOrderedList();

            if (onlyActives)
                query = query.Where(item => item.IsActive);

            if (!string.IsNullOrWhiteSpace(text))
                query = query.Where(item => item.Title.Contains(text));

            var result = await query.Select(item => new TitleValue<int>()
            {
                Value = item.Id,
                Title = item.Title,
            }).ToListAsync();

            if (includeSelectAllSelector)
            {
                InsertSelectAllSelector(result);
            }

            if (includeEmptySelector)
            {
                InsertEmptySelector(result);
            }

            return result;
        }

        public override async Task<List<TitleValue<int>>> GetTitleValueListAsync(bool includeEmptySelector = false, bool includeSelectAllSelector = false, string text = null)
        {
            return await GetGeneralEntitiesTitleValueListAsync(includeEmptySelector, includeSelectAllSelector, true, text);
        }

        protected override void PrepareModelOnInsert(TGeneralDto model)
        {
            base.PrepareModelOnInsert(model);

            model.Title = WebUtility.HtmlEncode(model.Title);
            model.Description = WebUtility.HtmlEncode(model.Description);
        }

        protected override void PrepareModelOnUpdate(TGeneralDto model)
        {
            base.PrepareModelOnUpdate(model);

            model.Title = WebUtility.HtmlEncode(model.Title);
            model.Description = WebUtility.HtmlEncode(model.Description);
        }

    }
}
