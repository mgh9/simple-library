 
  
  
  
  

using FinLib.Web.Api.Base;
using FinLib.Services.Base;

namespace FinLib.Web.Api.SEC
{  
	public partial class RoleController : Base.BaseEntityController <FinLib.DomainClasses.SEC.Role, FinLib.Models.Dtos.SEC.RoleDto, FinLib.Models.Views.SEC.RoleView, FinLib.Services.SEC.RoleService, FinLib.Models.Base.SearchFilters.BaseEntitySearchFilter >
	{
		public RoleController(ICommonServicesProvider<Models.Configs.GlobalSettings>commonServicesProvider, FinLib.Services.SEC.RoleService service)
            :base(commonServicesProvider, service)
        {

        }
	}
}

namespace FinLib.Web.Api.SEC
{  
	public partial class UserController : Base.UpdatableEntityController <FinLib.DomainClasses.SEC.User, FinLib.Models.Dtos.SEC.UserDto, FinLib.Models.Views.SEC.UserView, FinLib.Services.SEC.UserService, FinLib.Models.SearchFilters.SEC.UserSearchFilter >
	{
		public UserController(ICommonServicesProvider<Models.Configs.GlobalSettings>commonServicesProvider, FinLib.Services.SEC.UserService service)
            :base(commonServicesProvider, service)
        {

        }
	}
}

namespace FinLib.Web.Api.SEC
{  
	public partial class UserRoleController : Base.UpdatableEntityController <FinLib.DomainClasses.SEC.UserRole, FinLib.Models.Dtos.SEC.UserRoleDto, FinLib.Models.Views.SEC.UserRoleView, FinLib.Services.SEC.UserRoleService, FinLib.Models.Base.SearchFilters.UpdatableEntitySearchFilter >
	{
		public UserRoleController(ICommonServicesProvider<Models.Configs.GlobalSettings>commonServicesProvider, FinLib.Services.SEC.UserRoleService service)
            :base(commonServicesProvider, service)
        {

        }
	}
}

namespace FinLib.Web.Api.DBO
{  
	public partial class BookController : Base.GeneralEntityController <FinLib.DomainClasses.DBO.Book, FinLib.Models.Dtos.DBO.BookDto, FinLib.Models.Views.DBO.BookView, FinLib.Services.DBO.BookService, FinLib.Models.SearchFilters.DBO.BookSearchFilter >
	{
		public BookController(ICommonServicesProvider<Models.Configs.GlobalSettings>commonServicesProvider, FinLib.Services.DBO.BookService service)
            :base(commonServicesProvider, service)
        {

        }
	}
}

namespace FinLib.Web.Api.DBO
{  
	public partial class BookBorrowingController : Base.UpdatableEntityController <FinLib.DomainClasses.DBO.BookBorrowing, FinLib.Models.Dtos.DBO.BookBorrowingDto, FinLib.Models.Views.DBO.BookBorrowingView, FinLib.Services.DBO.BookBorrowingService, FinLib.Models.SearchFilters.DBO.BookBorrowingSearchFilter >
	{
		public BookBorrowingController(ICommonServicesProvider<Models.Configs.GlobalSettings>commonServicesProvider, FinLib.Services.DBO.BookBorrowingService service)
            :base(commonServicesProvider, service)
        {

        }
	}
}

namespace FinLib.Web.Api.DBO
{  
	public partial class CategoryController : Base.GeneralEntityController <FinLib.DomainClasses.DBO.Category, FinLib.Models.Dtos.DBO.CategoryDto, FinLib.Models.Views.DBO.CategoryView, FinLib.Services.DBO.CategoryService, FinLib.Models.Base.SearchFilters.GeneralEntitySearchFilter >
	{
		public CategoryController(ICommonServicesProvider<Models.Configs.GlobalSettings>commonServicesProvider, FinLib.Services.DBO.CategoryService service)
            :base(commonServicesProvider, service)
        {

        }
	}
}

namespace FinLib.Web.Api.CNT
{  
	public partial class MenuLinkController : Base.GeneralEntityController <FinLib.DomainClasses.CNT.MenuLink, FinLib.Models.Dtos.CNT.MenuLinkDto, FinLib.Models.Views.CNT.MenuLinkView, FinLib.Services.CNT.MenuLinkService, FinLib.Models.SearchFilters.CNT.MenuLinkSearchFilter >
	{
		public MenuLinkController(ICommonServicesProvider<Models.Configs.GlobalSettings>commonServicesProvider, FinLib.Services.CNT.MenuLinkService service)
            :base(commonServicesProvider, service)
        {

        }
	}
}

namespace FinLib.Web.Api.CNT
{  
	public partial class MenuLinkOwnerController : Base.UpdatableEntityController <FinLib.DomainClasses.CNT.MenuLinkOwner, FinLib.Models.Dtos.CNT.MenuLinkOwnerDto, FinLib.Models.Views.CNT.MenuLinkOwnerView, FinLib.Services.CNT.MenuLinkOwnerService, FinLib.Models.Base.SearchFilters.UpdatableEntitySearchFilter >
	{
		public MenuLinkOwnerController(ICommonServicesProvider<Models.Configs.GlobalSettings>commonServicesProvider, FinLib.Services.CNT.MenuLinkOwnerService service)
            :base(commonServicesProvider, service)
        {

        }
	}
}

