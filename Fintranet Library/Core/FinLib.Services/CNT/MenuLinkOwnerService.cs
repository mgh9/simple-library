using FinLib.DomainClasses.CNT;
using FinLib.Mappings;
using FinLib.Models.Dtos.CNT;

namespace FinLib.Services.CNT
{
    public partial class MenuLinkOwnerService
    {
        public List<MenuLinkOwnerDto> GetByMenuLinkId(int menuLinkId)
        {
            var query = from theMenuLinkOwner in DbContext.Set<MenuLinkOwner>()
                        where theMenuLinkOwner.MenuLinkId == menuLinkId
                        select theMenuLinkOwner;

            return query.ProjectToList<MenuLinkOwnerDto>();
        }

        public List<MenuLinkDto> GetByRoleId(int roleId)
        {
            // TODO: بهتره سر فرصت، ظاهر فرم دسترسی ها در برنامه، بصورت درختی پیاده شود

            var listOfHisRootMenus = (
                            from theMenuLinkOwner in DbContext.Set<MenuLinkOwner>()
                            join theMenuLink in DbContext.Set<MenuLink>() on theMenuLinkOwner.MenuLinkId equals theMenuLink.Id
                            where theMenuLinkOwner.RoleId == roleId
                                && theMenuLink.IsActive
                            select theMenuLink
                        )
                        .ProjectToList<MenuLinkDto>();

            var listOfAuthorizedMenus = new List<MenuLinkDto>();
            foreach (var menuItem in listOfHisRootMenus)
            {
                if (menuItem.ParentId.HasValue)
                {
                    if (listOfHisRootMenus.Any(x => x.Id == menuItem.ParentId))
                    {
                        listOfAuthorizedMenus.Add(menuItem);
                    }
                }
                else
                {
                    listOfAuthorizedMenus.Add(menuItem);
                }
            }
            //

            return listOfAuthorizedMenus;
        }

        public async Task DeleteByMenuLinkIdAsync(int menuLinkId)
        {
            var query = from theMenuLinkOwner in DbContext.Set<MenuLinkOwner>()
                        where theMenuLinkOwner.MenuLinkId == menuLinkId
                        select theMenuLinkOwner;

            await DeleteAsync(query);
        }
    }
}
