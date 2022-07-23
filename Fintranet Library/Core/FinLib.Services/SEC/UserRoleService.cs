using FinLib.Common.Exceptions.Business;
using FinLib.Common.Exceptions.Infra;
using FinLib.Common.Extensions;
using FinLib.DomainClasses.SEC;
using FinLib.Mappings;
using FinLib.Models.Base;
using FinLib.Models.Constants.Database;
using FinLib.Models.Dtos.SEC;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FinLib.Services.SEC
{
    public partial class UserRoleService
    {
        public List<UserRoleDto> GetByUserId(int userId)
        {
            var query = from theUserRole in DbContext.Set<UserRole>()
                        join theRole in DbContext.Set<Role>() on theUserRole.RoleId equals theRole.Id
                        where theUserRole.UserId == userId
                        select theUserRole;

            return query.MapTo<List<UserRoleDto>>();
        }

        protected override void ValidateOnDelete(UserRole entity)
        {
            base.ValidateOnDelete(entity);

            var hisActiveRolesCount = CountOfActiveRolesOfUser(entity.UserId);
            if (hisActiveRolesCount <= 1) // age k 1 Role faghat daare (va gharare k alan un 1 role hazf beshe), error
                throw new GeneralBusinessLogicException("این نقش، تنها 'نقش فعال' کاربر می باشد. هر کاربر حداقل باید 'یک نقش فعال' داشته باشد. در صورت نیاز می توانید خود کاربر را غیرفعال نمایید");
        }

        public int CountOfActiveRolesOfUser(int userId)
        {
            return GetByUserId(userId).Count;
        }

        public override async Task DeleteAsync(int id)
        {
            var theEntity = await DbContext.Set<UserRole>().FindAsync(id);

            ValidateOnDelete(theEntity);

            Debug.Assert(theEntity != null, "entity != null");
            DbContext.Set<UserRole>().Remove(theEntity);

            try
            {
                await DbContext.SaveChangesAsync();

                //// auditing
                //CommonServicesProvider.LoggingService.AuditInfo(new EntityDeleteEvent(_entityName
                //                                                                        , _entityTitle
                //                                                                        , theEntity.ToJson()
                //                                                                        , Models.Enums.EventType.Success));
            }
            catch (DbUpdateException dbex)
            {
                if (dbex.GetBaseException() is SqlException sqlException && sqlException.Number == ErrorCodes.ForeignKeyReferencedByOthers)
                {
                    var errorMessage = "برای این نقشِ کاربر در سامانه، سابقه ی عملیاتی ثبت شده و قابل حذف نمی باشد";

                    //// auditing
                    //CommonServicesProvider.LoggingService.AuditError(new EntityDeleteEvent(_entityName
                    //                                                                        , _entityTitle
                    //                                                                        , theEntity.ToJson()
                    //                                                                        , Models.Enums.EventType.Failure
                    //                                                                        , "این رکورد اطلاعاتی، در بخش های دیگری از سیستم استفاده شده و قابل حذف نمی باشد")
                    //                                                                    , sqlException);

                    throw new GeneralBusinessLogicException(errorMessage);
                }
                else
                {
                    //// auditing
                    //CommonServicesProvider.LoggingService.AuditError(new EntityDeleteEvent(_entityName
                    //                                                                        , _entityTitle
                    //                                                                        , theEntity.ToJson()
                    //                                                                        , Models.Enums.EventType.Error)
                    //                                                                    , dbex);
                }

                throw;
            }
            catch (Exception ex)
            {
                //// auditing
                //CommonServicesProvider.LoggingService.AuditError(new EntityDeleteEvent(_entityName
                //                                                                        , _entityTitle
                //                                                                        , theEntity.ToJson()
                //                                                                        , Models.Enums.EventType.Error)
                //                                                                    , ex);

                throw;
            }
        }

        public string[] GetUsersInRole(string roleName)
        {
            roleName.ThrowIfNull();

            var theRole = new RoleService(CommonServicesProvider).GetByName(roleName);
            if (theRole == null)
            {
                throw new EntityNotFoundException(roleName, "نقشی بر اساس مقدار ورودی یافت نشد!");
            }

            var query =
                from theUserRole in DbContext.Set<UserRole>()
                join theUser in DbContext.Set<User>() on theUserRole.UserId equals theUser.Id
                where theUserRole.RoleId == theRole.Id
                select theUser.UserName;

            return query.ToArray();
        }

        public async Task<List<TitleValue<int>>> GetTitleValueByUserIdAsync(int userId)
        {
            var query = from userRole in DbContext.Set<UserRole>()
                        join role in DbContext.Set<Role>() on userRole.RoleId equals role.Id
                        where userRole.UserId == userId
                        select new TitleValue<int>
                        {
                            Title = role.Name,
                            Value = role.Id
                        };

            return await query.ToListAsync();
        }

        public override async Task<int> InsertAsync(UserRoleDto model)
        {
            ValidateOnInsert(model);

            var theEntity = MapperHelper.MapTo<UserRole>(model);
            theEntity.CreateDate = DateTime.Now;

            DbContext.Set<UserRole>().Add(theEntity);

            try
            {
                await DbContext.SaveChangesAsync();

                //// auditing
                //CommonServicesProvider.LoggingService.AuditInfo(new EntityCreateEvent(_entityName
                //                                                                        , _entityTitle
                //                                                                        , theEntity.ToJson()
                //                                                                        , Models.Enums.EventType.Success));

                return theEntity.Id;

            }
            catch (Exception ex)
            {
                //// auditing
                //CommonServicesProvider.LoggingService.AuditError(new EntityCreateEvent(_entityName
                //                                                                        , _entityTitle
                //                                                                        , theEntity.ToJson()
                //                                                                        , Models.Enums.EventType.Error
                //                                                                        , ex.Message)
                //                                                                    , ex);

                throw;
            }
        }

        protected override void ValidateOnUpdate(UserRoleDto model)
        {
            model.ThrowIfNull();

            if (model.Id <= 0)
            {
                throw new InvalidPrimaryKeyException(nameof(model.Id));
            }

            // validation logic in child classes
        }

        public override async Task UpdateAsync(UserRoleDto model)
        {
            ValidateOnUpdate(model);

            var theEntity = await FindEntityAndThrowExceptionIfNotFoundAsync(model.Id);
            var theEntityCloned = theEntity.Clone();

            MapperHelper.Map(model, theEntity);
            theEntity.UpdateDate = DateTime.Now;

            DbContext.Entry(theEntity).State = EntityState.Modified;

            try
            {
                await DbContext.SaveChangesAsync();

                //// auditing
                //CommonServicesProvider.LoggingService.AuditInfo(new EntityUpdateEvent(_entityName
                //                                                                        , _entityTitle
                //                                                                        , theEntityCloned.ToJson()
                //                                                                        , theEntity.ToJson()
                //                                                                        , Models.Enums.EventType.Success));
            }
            catch (Exception ex)
            {
                //// auditing
                //CommonServicesProvider.LoggingService.AuditError(new EntityUpdateEvent(_entityName
                //                                                                        , _entityTitle
                //                                                                        , theEntityCloned.ToJson()
                //                                                                        , theEntity.ToJson()
                //                                                                        , Models.Enums.EventType.Error)
                //                                                                    , ex);

                throw;
            }
        }

        /// <summary>
        /// نقش های فعال کاربر را برمیگرداند
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<RoleDto> GetRolesOfUser(int userId)
        {
            var query = from theUserRole in DbContext.Set<UserRole>()
                        join theRole in DbContext.Set<Role>() on theUserRole.RoleId equals theRole.Id
                        where theUserRole.UserId == userId
                            && theUserRole.IsActive
                        select theRole;

            return query.ProjectToList<RoleDto>();
        }

        public async Task<RoleDto> GetDefaultRoleOfUserAsync(int userId)
        {
            var query = from theUserRole in DbContext.Set<UserRole>()
                        join theRole in DbContext.Set<Role>() on theUserRole.RoleId equals theRole.Id
                        where theUserRole.UserId == userId
                            && theUserRole.IsActive
                            && theUserRole.IsDefault
                        select theRole;

            var theRoleEntity = await query.SingleOrDefaultAsync();
            return MapperHelper.MapTo<RoleDto>(theRoleEntity);
        }

        /// <summary>
        /// آیا کاربر مورد نظر، نقش مورد نظر را (که باید فعال هم باشد) دارد؟
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public bool IsUserInActiveRole(int userId, int roleId)
        {
            var result = DbContext.Set<UserRole>().Any(x =>
                                        x.IsActive
                                        && x.UserId == userId
                                        && x.RoleId == roleId
                                    );

            return result;
        }

        public async Task DeleteByRoleIdAsync(int userId, int roleId)
        {
            var theEntity = DbContext.Set<UserRole>().Where(item => item.UserId == userId && item.RoleId == roleId).Single();

            DbContext.Set<UserRole>().Remove(theEntity);

            try
            {
                DbContext.SaveChanges();

                // auditing
                //CommonServicesProvider.LoggingService.AuditInfo(new EntityDeleteEvent(_entityName
                //                                                                        , _entityTitle
                //                                                                        , theEntity.ToJson()
                //                                                                        , Models.Enums.EventType.Success));
            }
            catch (DbUpdateException dbex)
            {
                if (dbex.GetBaseException() is Microsoft.Data.SqlClient.SqlException sqlException && sqlException.Number == ErrorCodes.ForeignKeyReferencedByOthers)
                // reference khorde az in UserRole b jahaye dg va ghabele hazf nmibaashad
                {
                    var theRole = await new RoleService(CommonServicesProvider).GetByIdAsync(theEntity.RoleId);
                    var errorMessage = $"برای این نقشِ کاربر ({theRole.Name}) در سامانه، سابقه ی عملیاتی ثبت شده و قابل حذف نمی باشد. در صورت تمایل می توانید نقش ایشان را غیرفعال کنید";

                    //// auditing
                    //CommonServicesProvider.LoggingService.AuditError(new EntityDeleteEvent(_entityName
                    //                                                                        , _entityTitle
                    //                                                                        , theEntity.ToJson()
                    //                                                                        , Models.Enums.EventType.Failure
                    //                                                                        , errorMessage)
                    //                                                                    , sqlException);

                    throw new GeneralBusinessLogicException(errorMessage);
                }
                else
                {
                    // auditing
                    //CommonServicesProvider.LoggingService.AuditError(new EntityDeleteEvent(_entityName
                    //                                                                        , _entityTitle
                    //                                                                        , theEntity.ToJson()
                    //                                                                        , Models.Enums.EventType.Failure)
                    //                                                                    , dbex);

                    throw;
                }
            }
            catch (Exception ex)
            {
                // auditing
                //CommonServicesProvider.LoggingService.AuditError(new EntityDeleteEvent(_entityName
                //                                                                        , _entityTitle
                //                                                                        , theEntity.ToJson()
                //                                                                        , Models.Enums.EventType.Error)
                //                                                                    , ex);
                throw;
            }
        }
    }
}
