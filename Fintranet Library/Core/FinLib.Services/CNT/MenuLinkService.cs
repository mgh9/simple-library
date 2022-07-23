using FinLib.DomainClasses.CNT;
using FinLib.Mappings;
using FinLib.Models.Base;
using FinLib.Models.Dtos;
using FinLib.Models.Dtos.CNT;
using FinLib.Models.Enums;
using FinLib.Models.SearchFilters.CNT;
using FinLib.Models.Views.CNT;
using FinLib.Services.Base;
using FinLib.Services.SEC;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Net;

namespace FinLib.Services.CNT
{
    public partial class MenuLinkService
    {
        public override async Task<List<MenuLinkDto>> GetAsync()
        {
            List<MenuLinkDto> listOfAuthorizedMenus;

            var theLoggedInRole = CommonServicesProvider.LoggedInRole;
            if (theLoggedInRole.RoleKey == ApplicationRole.Admin)
            {
                listOfAuthorizedMenus = await GetAsync(item => item.IsActive);
            }
            else
            {
                listOfAuthorizedMenus = new MenuLinkOwnerService(CommonServicesProvider)
                                                            .GetByRoleId(theLoggedInRole.Id);
            }

            List<MenuLinkDto> listOfAuthorizedMenus_Prepared =
                (
                    from theAuthorizedMenuItem in listOfAuthorizedMenus
                    select new MenuLinkDto
                    {
                        Id = theAuthorizedMenuItem.Id,
                        ParentId = (theAuthorizedMenuItem.ParentId is null) 
                                        ? 0 
                                        : theAuthorizedMenuItem.ParentId,
                        Title = theAuthorizedMenuItem.Title,
                        Description = theAuthorizedMenuItem.Description,
                        Route = theAuthorizedMenuItem.Route,
                        IsActive = theAuthorizedMenuItem.IsActive,
                        Icon = theAuthorizedMenuItem.Icon,
                        OrderNumber = theAuthorizedMenuItem.OrderNumber,
                        SubMenus = null,
                    }
                ).OrderBy(item => item.OrderNumber)
                .ToList();

            return listOfAuthorizedMenus_Prepared;
        }

        public override async Task<GetResultDto<MenuLinkView>> GetAsync(GetRequestDto<MenuLinkSearchFilter> model)
        {
            ValidateOnGet(model);
            PrepareModelOnGet(model);

            var query = from theMenuLink in _repository

                        join parentMenuLink in DbContext.Set<MenuLink>() on theMenuLink.ParentId equals parentMenuLink.Id into parentMenuLinkLefJoined
                        from theParentMenuLinkJoinedFinal in parentMenuLinkLefJoined.DefaultIfEmpty()

                        select new MenuLinkView
                        {
                            Id = theMenuLink.Id,
                            IsActive = theMenuLink.IsActive,
                            Title = theMenuLink.Title,
                            Route = theMenuLink.Route,
                            UpdateDate = theMenuLink.UpdateDate,
                            OrderNumber = theMenuLink.OrderNumber,

                            ParentMenuLinkTitle = theParentMenuLinkJoinedFinal.Title
                        };

            query = FilterService.ParseFilter(query, model.SearchFilterModel);

            var count = await query.CountAsync();

            return new GetResultDto<MenuLinkView>(query.OrderBy(model.PageOrder)
                       .Skip(model.PageIndex * model.PageSize)
                       .Take(model.PageSize)
                       .ToList(),count);
        }

        public override async Task<MenuLinkDto> GetByIdAsync(int id)
        {
            var theMenu = await base.GetByIdAsync(id);

            var itsOwners = new MenuLinkOwnerService(CommonServicesProvider).GetByMenuLinkId(id);

            theMenu.Owners = new List<MenuLinkOwnerDto>();
            var theRoleService = new RoleService(CommonServicesProvider);

            foreach (var item in itsOwners)
            {
                var itsRole = await theRoleService.GetByIdAsync(item.RoleId);

                theMenu.Owners.Add(new MenuLinkOwnerDto
                {
                    RoleId = itsRole.Id,
                    RoleTitle = itsRole.Title
                });
            }

            return theMenu;
        }

        public override async Task UpdateAsync(MenuLinkDto model)
        {
            var theMenuLinkOwnerService = new MenuLinkOwnerService(CommonServicesProvider);

            // first, delete its Owners (then add again)
            await theMenuLinkOwnerService.DeleteByMenuLinkIdAsync(model.Id);

            // add its owners
            if (model.Owners != null)
            {
                foreach (var theOwnerRole in model.Owners)
                {
                    await theMenuLinkOwnerService.InsertAsync(new MenuLinkOwnerDto
                    {
                        RoleId = theOwnerRole.RoleId,
                        MenuLinkId = model.Id,
                    });
                }
            }

            // update the MenuLink itself
            await base.UpdateAsync(model);
        }

        public override async Task<int> InsertAsync(MenuLinkDto model)
        {
            var theMenuLinkOwnerService = new MenuLinkOwnerService(CommonServicesProvider);

            // add the menu itself
            var addedMenuLinkId = await base.InsertAsync(model);

            // add its Owners (if any)
            if (model.Owners != null)
            {
                foreach (var theOwnerRole in model.Owners)
                {
                    await theMenuLinkOwnerService.InsertAsync(new MenuLinkOwnerDto
                    {
                        RoleId = theOwnerRole.RoleId,
                        MenuLinkId = addedMenuLinkId
                    });
                }
            }

            return addedMenuLinkId;
        }

        public override async Task<List<TitleValue<int>>> GetTitleValueListAsync(bool includeEmptySelector = false, bool includeSelectAllSelector = false, string text = null)
        {
            return await base.GetTitleValueListAsync(includeEmptySelector, includeSelectAllSelector, text);
        }

        protected override void PrepareModelOnInsert(MenuLinkDto model)
        {
            base.PrepareModelOnInsert(model);

            model.Icon = WebUtility.HtmlEncode(model.Icon);
            model.Route = WebUtility.HtmlEncode(model.Route);
        }

        protected override void PrepareModelOnUpdate(MenuLinkDto model)
        {
            base.PrepareModelOnUpdate(model);
         
            model.Icon = WebUtility.HtmlEncode(model.Icon);
            model.Route = WebUtility.HtmlEncode(model.Route);
        }

        protected override void ValidateOnDelete(MenuLink entity)
        {
            base.ValidateOnDelete(entity);

            throw new Common.Exceptions.Business.IllegalRequestException("The menu cannot be deleted, If u want, please just deactivate it");
        }

        protected override void ValidateOnDelete(IQueryable<MenuLink> entitiesToRemove)
        {
            base.ValidateOnDelete(entitiesToRemove);

            throw new Common.Exceptions.Business.IllegalRequestException("The menu cannot be deleted, If u want, please just deactivate it");
        }
    }
}
