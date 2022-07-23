using FinLib.DomainClasses.SEC;
using FinLib.Mappings;
using FinLib.Models.Dtos.SEC;
using FinLib.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace FinLib.Services.SEC
{
    public partial class RoleService 
    {
        public static ApplicationRole GetRole(string roleName)
        {
            return Common.Helpers.EnumHelper.ParseEnum<ApplicationRole>(roleName);
        }

        public RoleDto GetByName(ApplicationRole role)
        {
            return GetByName(role.ToString());
        }

        public RoleDto GetByName(string roleName)
        {
            return MapperHelper.MapTo<RoleDto>(DbContext.Set<Role>()
                                                    .SingleOrDefault(item => item.NormalizedName == roleName.ToUpperInvariant()));
        }

        public async Task<bool> IsExistsAsync(string roleName)
        {
            return await DbContext.Set<Role>().AnyAsync(item => item.NormalizedName == roleName.ToUpperInvariant());
        }

        public static string GetRoleName(ApplicationRole role)
        {
            return role.ToString();
        }
    }
}
