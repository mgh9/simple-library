using AutoMapper.QueryableExtensions;
using FinLib.Common.Exceptions.Infra;
using FinLib.Common.Extensions;
using FinLib.DataLayer.Context;
using FinLib.DomainClasses.Base;
using FinLib.Mappings;
using FinLib.Models.Base;
using FinLib.Models.Base.Dto;
using FinLib.Models.Base.SearchFilters;
using FinLib.Models.Base.View;
using FinLib.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinLib.Services.Base
{
    public abstract class BaseEntityService<TEntity, TDto, TView, TSearchFilter>
                                    : BaseService, IRepositoryProvider,
        IBaseEntityService<TEntity, TDto, TView, TSearchFilter>
        where TEntity : class, IBaseEntity, new()
        where TDto : BaseEntityDto, new()
        where TView : BaseView, new()
        where TSearchFilter : BaseEntitySearchFilter, new()
    {
        private readonly IUnitOfWork _dbContext;
        protected readonly DbSet<TEntity> _repository;
        protected readonly string _entityName;
        protected readonly string _entityTitle;

        protected BaseEntityService(ICommonServicesProvider<FinLib.Models.Configs.GlobalSettings> commonServicesProvider)
            : base(commonServicesProvider)
        {
            _dbContext = commonServicesProvider.DbContext;
            _repository = DbContext.Set<TEntity>();
            _entityName = typeof(TEntity).Name;
            _entityTitle = typeof(TEntity).GetDescription(true);
        }

        public IUnitOfWork DbContext { get { return _dbContext; } }
        protected virtual async Task<TEntity> FindEntityAndThrowExceptionIfNotFoundAsync(int entityId)
        {
            var theEntity = await GetEntityByIdAsync(entityId);
            if (theEntity is null)
            {
                throw new EntityNotFoundException(entityId);
            }

            return theEntity;
        }

        protected virtual async Task<TDto> FindAndThrowExceptionIfNotFoundAsync(int entityId)
        {
            var theEntity = await FindEntityAndThrowExceptionIfNotFoundAsync(entityId);
            return theEntity.MapTo<TDto>();
        }

        /// <summary>
        /// Get entities to another Type (instead of TDto)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual async Task<List<T>> GetAsAsync<T>(bool asNoTracking = false)
            where T : class, new()
        {
            if (asNoTracking)
            {
                return await DbContext.Set<TEntity>()
                                      .AsNoTracking()
                                      .ProjectTo<T>(MapperHelper.Mapper.ConfigurationProvider)
                                      .ToListAsync();
            }
            else
            {
                return await DbContext.Set<TEntity>()
                                    .ProjectTo<T>(MapperHelper.Mapper.ConfigurationProvider)
                                    .ToListAsync();
            }
        }

        public virtual async Task<List<TDto>> GetAsync()
        {
            var list = await DbContext.Set<TEntity>()
                                    .ToListAsync();

            var retval = MapperHelper.MapTo<List<TDto>>(list);

            return retval;
        }

        public virtual async Task<List<TDto>> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            where.ThrowIfNull();

            var list = await DbContext.Set<TEntity>().Where(where).ToListAsync();
            var retval = MapperHelper.MapTo<List<TDto>>(list);

            return retval;
        }

        public virtual string DefaultOrderbyColumnName
        {
            get
            {
                return nameof(IBaseEntity.Id);
            }
        }

        public virtual async Task<GetResultDto<TView>> GetAsync(GetRequestDto<TSearchFilter> model)
        {
            return await GetAsync(model, DbContext.Set<TEntity>().AsQueryable());
        }

        protected async Task<GetResultDto<TView>> GetAsync(GetRequestDto<TSearchFilter> model, IQueryable<TEntity> query)
        {
            ValidateOnGet(model);
            PrepareModelOnGet(model, DefaultOrderbyColumnName);

            query = FilterService.ParseFilter(query, model.SearchFilterModel);

            int count = await query.CountAsync();

            var retval = new GetResultDto<TView>(query
                        .OrderBy(model.PageOrder)
                        .Skip(model.PageIndex * model.PageSize)
                        .Take(model.PageSize)
                        .ProjectToList<TView>(), count);

            // auditing
            CommonServicesProvider.AppLogger.Info(Models.Enums.EventCategory.EntityManagement
                , Models.Enums.EventId.EntityReadList, Models.Enums.EventType.Success
                , _entityName, _entityTitle, null, null, customData: model.ToJson());

            return retval;
        }

        public virtual async Task<TDto> GetByIdAsync(int id)
        {
            var theFoundEntity = await DbContext.Set<TEntity>().FindAsync(id);
            var retval = MapperHelper.MapTo<TDto>(theFoundEntity);

            // auditing
            CommonServicesProvider.AppLogger.Info(Models.Enums.EventCategory.EntityManagement
                , Models.Enums.EventId.EntityRead, Models.Enums.EventType.Success, _entityName
                                                                                    , _entityTitle
                                                                                    , theFoundEntity.ToJson()
                                                                                    , null);

            return retval;
        }

        public virtual async Task<TView> GetAsViewByIdAsync(int id)
        {
            var theFoundEntity = await FindEntityAndThrowExceptionIfNotFoundAsync(id);
            return MapperHelper.MapTo<TView>(theFoundEntity);
        }

        internal virtual async Task<TEntity> GetEntityByIdAsync(int id)
        {
            var retval = await DbContext.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);

            return retval;
        }

        protected virtual void InsertEmptySelector(List<TitleValue<int>> list, string title = null, string description = null)
        {
            list.ThrowIfNull();

            var theTitle = title ?? "{empty}";
            var theDescription = description ?? "{no items selected}";

            list.Insert(0, new TitleValue<int> { Value = -2, Title = theTitle, Description = theDescription });
        }

        protected virtual void InsertSelectAllSelector(List<TitleValue<int>> list, string title = null, string description = null)
        {
            list.ThrowIfNull();

            var theTitle = title ?? "{all}";
            var theDescription = description ?? "{all items has been selected}";

            list.Insert(0, new TitleValue<int> { Value = -1, Title = theTitle, Description = theDescription });
        }

        public virtual async Task<List<TitleValue<int>>> GetTitleValueListAsync(bool includeEmptySelector = false, bool includeSelectAllSelector = false, string text = null)
        {
            var query = DbContext.Set<TEntity>().AsQueryable();

            var result = (await query.ToListAsync())
                              .ProjectEntityToTitleValueList();

            if (text.IsNotEmpty())
            {
                result = result.Where(item => item.Title.Contains(text))
                                .OrderBy(item => item.Title).ToList();
            }
            else
            {
                result = result.OrderBy(item => item.Title).ToList();
            }

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

        public virtual async Task<int> CountAsync(TSearchFilter searchFilters)
        {
            var query = DbContext.Set<TEntity>().AsQueryable();

            query = FilterService.ParseFilter(query, searchFilters);

            return await query.CountAsync();
        }

        protected virtual void ValidateOnGet(GetRequestDto<TSearchFilter> model)
        {
            model.ThrowIfNull();
        }

        protected virtual void ValidateOnGetByUserRoleId(GetByUserRoleIdRequestDto<TSearchFilter> model)
        {
            ValidateOnGet(model);

            if (model.UserRoleId <= 0)
            {
                throw new InvalidPrimaryKeyException(nameof(model.UserRoleId));
            }
        }

        protected virtual void PrepareModelOnGet(GetRequestDto<TSearchFilter> model, string defaultOrderbyColumnName = null)
        {
            if (model.PageSize == 0)
                model.PageSize = int.MaxValue;

            if (model.PageOrder?.StartsWith("default", StringComparison.OrdinalIgnoreCase) == true)
            {
                if (defaultOrderbyColumnName.IsEmpty())
                {
                    model.PageOrder = $"{nameof(IBaseEntity.Id)} asc";
                }
                else
                {
                    model.PageOrder = defaultOrderbyColumnName;
                }
            }
        }
    }
}
