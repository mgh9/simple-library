using FinLib.Web.Api.Base;

namespace FinLib.Web.Api.SEC
{
    public partial class RoleController : BaseEntityController<DomainClasses.SEC.Role
        , Models.Dtos.SEC.RoleDto
        , Models.Views.SEC.RoleView
        , Services.SEC.RoleService
        , Models.Base.SearchFilters.BaseEntitySearchFilter>
    {
    }
}
