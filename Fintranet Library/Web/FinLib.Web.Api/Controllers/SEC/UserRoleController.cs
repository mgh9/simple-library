using FinLib.Web.Api.Base;

namespace FinLib.Web.Api.SEC
{
    public partial class UserRoleController : UpdatableEntityController<FinLib.DomainClasses.SEC.UserRole, FinLib.Models.Dtos.SEC.UserRoleDto, FinLib.Models.Views.SEC.UserRoleView, FinLib.Services.SEC.UserRoleService, FinLib.Models.Base.SearchFilters.UpdatableEntitySearchFilter>
    {
    }
}
