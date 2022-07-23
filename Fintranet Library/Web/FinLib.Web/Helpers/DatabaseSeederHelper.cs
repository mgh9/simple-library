using FinLib.Common.Extensions;
using FinLib.DataLayer.Context;
using FinLib.DomainClasses.CNT;
using FinLib.DomainClasses.SEC;
using FinLib.Models.Configs;
using FinLib.Models.Enums;
using FinLib.Providers.Configuration;
using FinLib.Providers.Identity;
using FinLib.Services.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinLib.Web.Helpers
{
    internal static class DatabaseSeederHelper
    {
        internal static void SeedDatabase(this IApplicationBuilder app
                                            , IAppSettingsProvider<GlobalSettings> globalSettingsProvider
                                            , ICommonServicesProvider<GlobalSettings> commonServicesProvider)
        {
            commonServicesProvider.DbContext.Database.Migrate();

            ensureDefaultUsersAndRolesAsync(app, globalSettingsProvider.Settings, commonServicesProvider).Wait();
            ensureMenusAsync(app, globalSettingsProvider.Settings, commonServicesProvider).Wait();
            ensureAuditingDatabaseCreatedAsync(app, globalSettingsProvider.Settings, commonServicesProvider).Wait();
        }

        private static async Task ensureAuditingDatabaseCreatedAsync(IApplicationBuilder app, GlobalSettings settings, ICommonServicesProvider<GlobalSettings> commonServicesProvider)
        {
            //commonServicesProvider.DbContext.Database.
        }

        private static async Task<bool> ensureMenusAsync(IApplicationBuilder app, GlobalSettings globalSettings, ICommonServicesProvider<GlobalSettings> commonServicesProvider)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var theServicesProvider = scope.ServiceProvider;

                // Resolve the services from the service provider
                int adminUserRoleId = getAdminUserRoleId(globalSettings, commonServicesProvider);
                int librarianRoleId = getRoleId(commonServicesProvider, ApplicationRole.Librarian);
                int customerRoleId = getRoleId(commonServicesProvider, ApplicationRole.Customer);

                /*
                    admin has access to all the menus
                 */

                /* 
                 * Dashboard menu (librarian, customer)
                 */
                var dashboardMenuId = await doEnsureMenuLinkExistsAsync(commonServicesProvider.DbContext, adminUserRoleId, "Dashboard", "restricted.dashboard", null, "dashboard", 1, true, null);
                await doEnsureRoleHasAccessToMenuLink(commonServicesProvider.DbContext, adminUserRoleId, librarianRoleId, dashboardMenuId);
                await doEnsureRoleHasAccessToMenuLink(commonServicesProvider.DbContext, adminUserRoleId, customerRoleId, dashboardMenuId);

                /* 
                 * Management menu : 
                 *      Menus (admin)
                 *      Users (admin)
                 *      
                 *      Categories (admin, librarian)
                 *      Books (admin, librarian)
                 */
                var managementMenuId = await doEnsureMenuLinkExistsAsync(commonServicesProvider.DbContext, adminUserRoleId, "Management", null, null, null, 2, true, null);
                await doEnsureMenuLinkExistsAsync(commonServicesProvider.DbContext, adminUserRoleId, "Menus", "restricted.management.menu-links", managementMenuId, null, 1, true, null);
                await doEnsureMenuLinkExistsAsync(commonServicesProvider.DbContext, adminUserRoleId, "Users", "restricted.management.users", managementMenuId, null, 2, true, null);

                var categoriesMenuId = await doEnsureMenuLinkExistsAsync(commonServicesProvider.DbContext, adminUserRoleId, "Categories", "restricted.management.categories", managementMenuId, null, 3, true, null);
                await doEnsureRoleHasAccessToMenuLink(commonServicesProvider.DbContext, adminUserRoleId, librarianRoleId, categoriesMenuId);

                var booksMenuId = await doEnsureMenuLinkExistsAsync(commonServicesProvider.DbContext, adminUserRoleId, "Books", "restricted.management.books", managementMenuId, null, 4, true, null);
                await doEnsureRoleHasAccessToMenuLink(commonServicesProvider.DbContext, adminUserRoleId, librarianRoleId, booksMenuId);

                /* 
                 * Options menu : 
                 *      Books Borrowing (librarian)
                 *      Books Returning (librarian)
                 *      
                 *      Books (customer)
                 *      My Books Borrwoing History (customer)
                 */
                var optionsMenuId = await doEnsureMenuLinkExistsAsync(commonServicesProvider.DbContext, adminUserRoleId, "Options", null, null, null, 3, true, null);
                await doEnsureRoleHasAccessToMenuLink(commonServicesProvider.DbContext, adminUserRoleId, customerRoleId, optionsMenuId);
                await doEnsureRoleHasAccessToMenuLink(commonServicesProvider.DbContext, adminUserRoleId, librarianRoleId, optionsMenuId);

                var booksBorrowingMenuId = await doEnsureMenuLinkExistsAsync(commonServicesProvider.DbContext, adminUserRoleId, "Book borrowing", "restricted.options.book-borrowing", optionsMenuId, null, 1, true, null);
                await doEnsureRoleHasAccessToMenuLink(commonServicesProvider.DbContext, adminUserRoleId, librarianRoleId, booksBorrowingMenuId);

                var booksListMenuId = await doEnsureMenuLinkExistsAsync(commonServicesProvider.DbContext, adminUserRoleId, "Books list", "restricted.options.books-list", optionsMenuId, null, 3, true, null);
                await doEnsureRoleHasAccessToMenuLink(commonServicesProvider.DbContext, adminUserRoleId, customerRoleId, booksListMenuId);

                var myBorrowingHistoryMenuId =  await doEnsureMenuLinkExistsAsync(commonServicesProvider.DbContext, adminUserRoleId, "My Borrowing History", "restricted.options.my-borrowing-history", optionsMenuId, null, 4, true, null);
                await doEnsureRoleHasAccessToMenuLink(commonServicesProvider.DbContext, adminUserRoleId, customerRoleId, myBorrowingHistoryMenuId);
            }

            return true;
        }

        private static async Task doEnsureRoleHasAccessToMenuLink(IUnitOfWork dbContext, int adminUserRoleId, int roleId, int menuId)
        {
            var menuLinkOwners = dbContext.Set<MenuLinkOwner>();

            var theDesiredMenuLinkOwner = await menuLinkOwners.SingleOrDefaultAsync(x => x.RoleId == roleId && x.MenuLinkId == menuId);
            if (theDesiredMenuLinkOwner is null)
            {
                theDesiredMenuLinkOwner = new MenuLinkOwner
                {
                    MenuLinkId = menuId,
                    RoleId = roleId,
                    CreateDate = DateTime.Now,
                    CreatedByUserRoleId = adminUserRoleId
                };
                menuLinkOwners.Add(theDesiredMenuLinkOwner);

                await dbContext.SaveChangesAsync();
            }
        }

        private static int getAdminUserRoleId(GlobalSettings globalSettings, ICommonServicesProvider<GlobalSettings> commonServicesProvider)
        {
            var adminUserName = globalSettings.SeedUsers.Single(x => x.IsAdmin).UserName.ToUpperInvariant();
            var queryAdminUserRole = from theUser in commonServicesProvider.DbContext.Set<User>()
                                     join theUserRole in commonServicesProvider.DbContext.Set<UserRole>() on theUser.Id equals theUserRole.UserId
                                     join theRole in commonServicesProvider.DbContext.Set<Role>() on theUserRole.RoleId equals theRole.Id
                                     where theRole.NormalizedName == adminUserName
                                     select theUserRole;

            var adminUserRole = queryAdminUserRole.Single();
            var adminUserRoleId = adminUserRole.Id;
            return adminUserRoleId;
        }

        private static int getRoleId(ICommonServicesProvider<GlobalSettings> commonServicesProvider, ApplicationRole role)
        {
            var roleNormalized = role.ToString().ToUpperInvariant();
            var queryRole = from theRole in commonServicesProvider.DbContext.Set<Role>()
                            where theRole.NormalizedName == roleNormalized
                            select theRole;

            var foundRole = queryRole.Single();
            return foundRole.Id;
        }

        private static async Task<int> doEnsureMenuLinkExistsAsync(DataLayer.Context.IUnitOfWork dbContext, int adminUserRoleId
            , string title, string route, int? parentId, string icon
            , int orderNumber, bool isActive, string description)
        {
            var menuLinks = dbContext.Set<MenuLink>();

            var theMenu = await menuLinks.SingleOrDefaultAsync(x => x.Title == title && x.Route == route);
            if (theMenu is null)
            {
                theMenu = new MenuLink
                {
                    Title = title,
                    ParentId = parentId,
                    Route = route,
                    Icon = icon,
                    OrderNumber = orderNumber,
                    IsActive = isActive,
                    Description = description,
                    CreateDate = DateTime.Now,
                    CreatedByUserRoleId = adminUserRoleId
                };
                menuLinks.Add(theMenu);

                await dbContext.SaveChangesAsync();
            }
            else
            {
                theMenu.Title = title;
                theMenu.ParentId = parentId;
                theMenu.Route = route;
                theMenu.Icon = icon;
                theMenu.OrderNumber = orderNumber;

                dbContext.Entry(theMenu).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
            }

            return theMenu.Id;
        }

        /// <summary>
        /// ensure default roles, users and userRoles exist
        /// </summary>
        /// <param name="app"></param>
        /// <param name="globalSettings"></param>
        /// <returns></returns>
        private static async Task<bool> ensureDefaultUsersAndRolesAsync(IApplicationBuilder app, GlobalSettings globalSettings, ICommonServicesProvider<GlobalSettings> commonServicesProvider)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var theServicesProvider = scope.ServiceProvider;

                // Resolve the services from the service provider
                var theRoleManager = theServicesProvider.GetService<RoleManager<Role>>();

                await doEnsureRoleExistsAsync(theRoleManager, ApplicationRole.Admin);
                await doEnsureRoleExistsAsync(theRoleManager, ApplicationRole.Librarian);
                await doEnsureRoleExistsAsync(theRoleManager, ApplicationRole.Customer);

                var theUserManager = theServicesProvider.GetRequiredService<AppUserManager>();
                await doEnsureUsersExistAsync(theUserManager, globalSettings.SeedUsers);
            }

            return true;
        }

        private static async Task doEnsureUsersExistAsync(AppUserManager userManager, List<SeedUser> seedUsers)
        {
            foreach (var seedUserItem in seedUsers)
            {
                if (await userManager.FindByNameAsync(seedUserItem.UserName) == null)
                {
                    var aUser = new User
                    {
                        FirstName = seedUserItem.FirstName,
                        LastName = seedUserItem.LastName,
                        UserName = seedUserItem.UserName,
                        Email = seedUserItem.Email,
                        LockoutEnabled = seedUserItem.LockoutEnabled,
                        IsActive = seedUserItem.IsActive
                    };

                    // create the User
                    var result = await userManager.CreateAsync(aUser, seedUserItem.Password);

                    // if ok, create his Roles and Claims
                    if (result.Succeeded)
                    {
                        // Customer role (everyone has the Customer role)
                        var addingRoleResult = await userManager.AddToRoleAsync(aUser, ApplicationRole.Customer.ToString(), true, true);
                        if (!addingRoleResult.Succeeded)
                        {
                            throw new Common.Exceptions.Infra.FatalException("Error in creating the User :" + addingRoleResult.GetAllErrors());
                        }

                        // Admin role if he has
                        if (seedUserItem.IsAdmin)
                        {                            
                            addingRoleResult = await userManager.AddToRoleAsync(aUser, ApplicationRole.Admin.ToString(), true, true);
                            if (!addingRoleResult.Succeeded)
                            {
                                throw new Common.Exceptions.Infra.FatalException("Error in creating the User :" + addingRoleResult.GetAllErrors());
                            }
                        }

                        // Librarian role if he has
                        if (seedUserItem.IsLibrarian)
                        {
                            addingRoleResult = await userManager.AddToRoleAsync(aUser, ApplicationRole.Librarian.ToString(), true, false);
                            if (!addingRoleResult.Succeeded)
                            {
                                throw new Common.Exceptions.Infra.FatalException("Error in creating the User :" + addingRoleResult.GetAllErrors());
                            }
                        }
                    }
                }
            }
        }

        private static async Task doEnsureRoleExistsAsync(RoleManager<Role> roleManager, ApplicationRole role)
        {
            if (!(await roleManager.RoleExistsAsync(role.ToString())))
            {
                var aAdminRole = new Role
                {
                    Name = role.ToString(),
                    Title = role.GetDescription()
                };

                var result = await roleManager.CreateAsync(aAdminRole);
                if (!result.Succeeded)
                {
                    throw new Common.Exceptions.Infra.FatalException($"Error in creating the Role {role} :" + result.GetAllErrors());
                }
            }
        }
    }
}