using FinLib.Common.Exceptions.Business;
using FinLib.Common.Exceptions.Infra;
using FinLib.Common.Extensions;
using FinLib.DataLayer.Context;
using FinLib.DomainClasses.Base;
using FinLib.Mappings;
using FinLib.Models.Base.Dto;
using FinLib.Models.Base.SearchFilters;
using FinLib.Models.Base.View;
using FinLib.Models.Constants.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace FinLib.Services.Base
{
    public abstract class UpdatableEntityService<TUpdatableEntity, TUpdatableDto, TUpdatableView, TUpdatableSearchFilter>
        : BaseEntityService<TUpdatableEntity, TUpdatableDto, TUpdatableView, TUpdatableSearchFilter>
            , IUpdatableEntityService<TUpdatableEntity, TUpdatableDto, TUpdatableView, TUpdatableSearchFilter>
                        where TUpdatableEntity : class, IUpdatableEntity, new()
                        where TUpdatableDto : UpdatableDto, new()
                        where TUpdatableView : UpdatableView, new()
                        where TUpdatableSearchFilter : UpdatableEntitySearchFilter, new()
    {
        private IDbContextTransaction _transaction;
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected UpdatableEntityService(ICommonServicesProvider<FinLib.Models.Configs.GlobalSettings> commonServicesProvider)
                    : base(commonServicesProvider)
        {
            _httpContextAccessor = (IHttpContextAccessor)commonServicesProvider.ServiceProvider.GetService(typeof(IHttpContextAccessor));
        }

        public async Task BeginTransactionAsync()
        {
            if (DbContext.Database.CurrentTransaction is null)
                _transaction = await DbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if (DbContext.Database.CurrentTransaction is not null)
                await _transaction?.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null && DbContext.Database.CurrentTransaction is not null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
            else
            {
                throw new Common.Exceptions.Infra.FatalException("There is no CurrentTransactions pending to rollback...");
            }
        }

        protected virtual void ValidateOnInsert(TUpdatableDto model)
        {
            model.ThrowIfNull();

            validateOnSavingModel(model);

            // validation logic in child classes
        }

        private void validateOnSavingModel(TUpdatableDto model)
        {
            //var theEntityType = DbContext.Model.FindEntityType(typeof(TUpdatableEntity));
            var theEntityType = (DbContext as AppDbContext).GetService<IDesignTimeModel>().Model.FindEntityType(typeof(TUpdatableEntity));
            foreach (var propertyItem in theEntityType.GetProperties())
            {
                var isNullable = propertyItem.IsColumnNullable();
                string itsComment = "";
                itsComment = propertyItem.GetComment();

                var maxLen = propertyItem.GetMaxLength();

                //
                if (!model.GetType().HasProperty(propertyItem.Name))
                    continue;

                var itsValueOnModel = model.GetType().GetProperty(propertyItem.Name).GetValue(model);
                if (!isNullable && propertyItem.GetDefaultValue() is null && propertyItem.GetDefaultValueSql().IsEmpty())
                {
                    switch (itsValueOnModel)
                    {
                        case null:
                        case string itsValueAsString when itsValueAsString.IsEmpty():
                            throw new BusinessValidationException($"the value of '{itsComment}' cannot be empty");
                    }
                }

                if (itsValueOnModel is null)
                    continue;

                if (maxLen is not null && itsValueOnModel.ToString().Length > maxLen)
                {
                    throw new BusinessValidationException($"the length of the value of '{itsComment}' cannot exceeded of {maxLen}");
                }
            }
        }

        protected virtual void PrepareModelOnInsert(TUpdatableDto model)
        {
            // in child classes, if needed
        }

        public async virtual Task<int> InsertAsync(TUpdatableDto model)
        {
            try
            {
                ValidateOnInsert(model);
                PrepareModelOnInsert(model);

                var theEntity = MapperHelper.MapTo<TUpdatableEntity>(model);

                theEntity.Id = 0;
                theEntity.CreatedByUserRoleId = CommonServicesProvider.LoggedInUserRoleId;
                theEntity.CreateDate = DateTime.Now;

                DbContext.Set<TUpdatableEntity>().Add(theEntity);

                await DbContext.SaveChangesAsync();
                model.Id = theEntity.Id;

                // auditing
                CommonServicesProvider.AppLogger.Info
                    (Models.Enums.EventCategory.EntityManagement
                    , Models.Enums.EventId.EntityCreate
                    , Models.Enums.EventType.Success
                    , _entityName, _entityTitle
                                    , null, theEntity.ToJson());


                return theEntity.Id;
            }
            catch (Exception ex)
            {
                // auditing
                CommonServicesProvider.AppLogger.Error
                    (Models.Enums.EventCategory.EntityManagement
                    , Models.Enums.EventId.EntityCreate, Models.Enums.EventType.Error
                    , _entityName, _entityTitle, null, model.ToJson()
                    , ex.Message, ex);

                throw;
            }
        }

        protected virtual void ValidateOnUpdate(TUpdatableDto model)
        {
            model.ThrowIfNull();

            if (model.Id <= 0)
            {
                throw new InvalidPrimaryKeyException(nameof(model.Id));
            }

            validateOnSavingModel(model);

            // validation logic in child classes
        }

        protected virtual void PrepareModelOnUpdate(TUpdatableDto model)
        {
            model.ThrowIfNull();

            // and maybe in child classes, if needed
        }

        /// <summary>
        /// بروزرسانی زمان 'آخرین تغییر' در موجودیت
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task UpdateModifiedDateAsync(int id)
        {
            var theEntity = await FindEntityAndThrowExceptionIfNotFoundAsync(id);

            theEntity.UpdateDate = DateTime.Now;
            theEntity.UpdatedByUserRoleId = CommonServicesProvider.LoggedInUserRoleId;

            DbContext.Entry(theEntity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TUpdatableDto model)
        {
            ValidateOnUpdate(model);
            PrepareModelOnUpdate(model);

            var theEntityToUpdate = await FindEntityAndThrowExceptionIfNotFoundAsync(model.Id);
            var theEntityClonedBeforeUpdated = theEntityToUpdate.ToJson();

            MapperHelper.Map(model, theEntityToUpdate);

            theEntityToUpdate.UpdateDate = DateTime.Now;
            theEntityToUpdate.UpdatedByUserRoleId = CommonServicesProvider.LoggedInUserRoleId;

            DbContext.Entry(theEntityToUpdate).State = EntityState.Modified;

            await doUpdateAsync(theEntityToUpdate, theEntityClonedBeforeUpdated);
        }

        public virtual async Task UpdateAsync(TUpdatableDto model, params Expression<Func<TUpdatableEntity, object>>[] excludeProperties)
        {
            ValidateOnUpdate(model);
            PrepareModelOnUpdate(model);

            var theEntityToUpdate = await FindEntityAndThrowExceptionIfNotFoundAsync(model.Id);
            var theEntityClonedBeforeUpdated = theEntityToUpdate.ToJson();

            MapperHelper.Map(model, theEntityToUpdate);
            theEntityToUpdate.UpdateDate = DateTime.Now;
            theEntityToUpdate.UpdatedByUserRoleId = CommonServicesProvider.LoggedInUserRoleId;

            DbContext.Entry(theEntityToUpdate).State = EntityState.Modified;

            // exclude some properties if any
            foreach (var excludeProperty in excludeProperties)
            {
                DbContext.Entry(theEntityToUpdate).Property(excludeProperty).IsModified = false;
            }

            await doUpdateAsync(theEntityToUpdate, theEntityClonedBeforeUpdated);
        }

        private async Task doUpdateAsync(TUpdatableEntity theEntityToUpdate, string theEntityClonedBeforeUpdated)
        {
            try
            {
                await DbContext.SaveChangesAsync();

                // auditing
                CommonServicesProvider.AppLogger.Info
                    (Models.Enums.EventCategory.EntityManagement
                    , Models.Enums.EventId.EntityUpdate,
                    Models.Enums.EventType.Success
                    , _entityName, _entityTitle
                    , theEntityClonedBeforeUpdated, theEntityToUpdate.ToJson());
            }
            catch (Exception ex)
            {
                // auditing
                CommonServicesProvider.AppLogger.Error(
                    Models.Enums.EventCategory.EntityManagement,
                      Models.Enums.EventId.EntityUpdate,
                      Models.Enums.EventType.Error,
                      _entityName, _entityTitle
                      , theEntityClonedBeforeUpdated
                      , theEntityToUpdate.ToJson(), ex.Message, ex);

                throw;
            }
        }

        public virtual async Task SaveAsync(TUpdatableDto model)
        {
            model.ThrowIfNull();

            if (model.Id > 0)
                await UpdateAsync(model);
            else
                await InsertAsync(model);
        }

        protected virtual void ValidateOnDelete(TUpdatableEntity entity)
        {
            entity.ThrowIfNull();

            // validation logic in child classes
        }

        protected virtual void ValidateOnDelete(IQueryable<TUpdatableEntity> entitiesToRemove)
        {
            entitiesToRemove.ThrowIfNull();
        }

        public virtual async Task DeleteAsync(IQueryable<TUpdatableEntity> entitiesToRemove)
        {
            ValidateOnDelete(entitiesToRemove);

            if (!entitiesToRemove.Any())
                return;

            var entitiesToRemoveCloned = entitiesToRemove.ToJson();
            DbContext.Set<TUpdatableEntity>().RemoveRange(entitiesToRemove);

            try
            {
                await DbContext.SaveChangesAsync();

                // auditing
                CommonServicesProvider.AppLogger.Info(
                    Models.Enums.EventCategory.EntityManagement, Models.Enums.EventId.EntityDeleteList
                    , Models.Enums.EventType.Success,
                    _entityName, _entityTitle, entitiesToRemoveCloned, null);
            }
            catch (DbUpdateException ex)
            {
                if (ex.GetBaseException() is SqlException sqlException
                    && sqlException.Number == ErrorCodes.ForeignKeyReferencedByOthers)
                {
                    CommonServicesProvider.AppLogger.Error(Models.Enums.EventCategory.EntityManagement
                        , Models.Enums.EventId.EntityDeleteList, Models.Enums.EventType.Error
                        , _entityName, _entityTitle, entitiesToRemoveCloned, null, "some data used in the system and cannot be deleted"
                                                                    , sqlException);

                    throw new GeneralBusinessLogicException("This entity used in the system and cannot be deleted");
                }
                else
                {
                    CommonServicesProvider.AppLogger.Error(Models.Enums.EventCategory.EntityManagement
                        , Models.Enums.EventId.EntityDeleteList, Models.Enums.EventType.Error, _entityName
                                                    , _entityTitle
                                                    , entitiesToRemoveCloned
                                                    , null, exception: ex);
                }

                throw;
            }
            catch (Exception ex)
            {
                CommonServicesProvider.AppLogger.Error(Models.Enums.EventCategory.EntityManagement
                     , Models.Enums.EventId.EntityDeleteList, Models.Enums.EventType.Error, _entityName
                                                 , _entityTitle
                                                 , entitiesToRemoveCloned
                                                 , null, exception: ex);

                throw;
            }
        }

        /// <summary>
        /// حذف مجموعه ای از موجودیت ها با استفاده از اکسپرشن
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(Expression<Func<TUpdatableEntity, bool>> where)
        {
            where.ThrowIfNull();

            var entitiesToRemove = DbContext.Set<TUpdatableEntity>().Where(where);
            await DeleteAsync(entitiesToRemove);
        }

        public virtual async Task DeleteAsync(int id)
        {
            string theEntityCloned = null;

            try
            {
                TUpdatableEntity theEntity = await FindEntityAndThrowExceptionIfNotFoundAsync(id);

                ValidateOnDelete(theEntity);

                theEntityCloned = theEntity.ToJson();
                DbContext.Set<TUpdatableEntity>().Remove(theEntity);
                await DbContext.SaveChangesAsync();

                // auditing
                CommonServicesProvider.AppLogger.Info(Models.Enums.EventCategory.EntityManagement,
                     Models.Enums.EventId.EntityDelete, Models.Enums.EventType.Success, _entityName
                                                                                        , _entityTitle
                                                                                        , theEntityCloned
                                                                                        , null);
            }
            catch (DbUpdateException dbex)
            {
                if (dbex.GetBaseException() is Microsoft.Data.SqlClient.SqlException sqlException && sqlException.Number == ErrorCodes.ForeignKeyReferencedByOthers)
                {
                    // auditing
                    CommonServicesProvider.AppLogger.Error(Models.Enums.EventCategory.EntityManagement
                        , Models.Enums.EventId.EntityDelete, Models.Enums.EventType.Error, _entityName
                                                                                            , _entityTitle
                                                                                            , theEntityCloned ?? id.ToJson()
                                                                                            , null
                                                                                            , "This data has been used in the system and cannot be deleted", sqlException);

                    throw new GeneralBusinessLogicException("This data has been used in the system and cannot be deleted");
                }
                else
                {
                    // auditing
                    CommonServicesProvider.AppLogger.Error(Models.Enums.EventCategory.EntityManagement
                        , Models.Enums.EventId.EntityDelete, Models.Enums.EventType.Error, _entityName
                                                                                            , _entityTitle
                                                                                            , theEntityCloned ?? id.ToJson()
                                                                                            , null, exception: dbex);

                    throw;
                }
            }
            catch (Exception ex)
            {
                // auditing
                CommonServicesProvider.AppLogger.Error(Models.Enums.EventCategory.EntityManagement
                    , Models.Enums.EventId.EntityDelete, Models.Enums.EventType.Error, _entityName
                                                                                        , _entityTitle
                                                                                        , theEntityCloned ?? id.ToJson()
                                                                                        , null, exception: ex);
                throw;
            }
        }
    }
}
