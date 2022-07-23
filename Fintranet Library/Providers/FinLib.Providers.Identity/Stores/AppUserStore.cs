using FinLib.DataLayer.Context;
using FinLib.DomainClasses.SEC;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinLib.Providers.Identity.Stores
{
    public class AppUserStore : UserStore<User,
                                            Role,
                                            AppDbContext,
                                            int,
                                            IdentityUserClaim<int>,
                                            UserRole,
                                            IdentityUserLogin<int>,
                                            IdentityUserToken<int>,
                                            IdentityRoleClaim<int>>
    {
        private readonly AppDbContext _dbContext;

        public AppUserStore(AppDbContext dbContext, IdentityErrorDescriber appErrorDescriber)
            : base(dbContext, appErrorDescriber)
        {
            _dbContext = dbContext;
        }

        protected override async Task<UserRole> FindUserRoleAsync(int userId, int roleId, CancellationToken cancellationToken)
        {
            return await _dbContext.Set<UserRole>().SingleOrDefaultAsync(x => x.UserId == userId && x.RoleId == roleId);
        }
    }
}
