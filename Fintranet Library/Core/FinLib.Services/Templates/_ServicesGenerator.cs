 
  
  
  
  

using Microsoft.AspNetCore.Identity;
using FinLib.DataLayer.Context;
using FinLib.DomainClasses.SEC;
using FinLib.Services.Base;

namespace FinLib.Services.SEC
{  
	public partial class RoleService : Base.BaseEntityService <FinLib.DomainClasses.SEC.Role, FinLib.Models.Dtos.SEC.RoleDto, FinLib.Models.Views.SEC.RoleView, FinLib.Models.Base.SearchFilters.BaseEntitySearchFilter >
	{	
		public RoleService(ICommonServicesProvider<FinLib.Models.Configs.GlobalSettings> commonServicesProvider)
            :base(commonServicesProvider)
        {

        }
	}
}

namespace FinLib.Services.SEC
{  
	public partial class UserService : Base.UpdatableEntityService <FinLib.DomainClasses.SEC.User, FinLib.Models.Dtos.SEC.UserDto, FinLib.Models.Views.SEC.UserView, FinLib.Models.SearchFilters.SEC.UserSearchFilter >
	{	
		public UserService(ICommonServicesProvider<FinLib.Models.Configs.GlobalSettings> commonServicesProvider)
            :base(commonServicesProvider)
        {

        }
	}
}

namespace FinLib.Services.SEC
{  
	public partial class UserRoleService : Base.UpdatableEntityService <FinLib.DomainClasses.SEC.UserRole, FinLib.Models.Dtos.SEC.UserRoleDto, FinLib.Models.Views.SEC.UserRoleView, FinLib.Models.Base.SearchFilters.UpdatableEntitySearchFilter >
	{	
		public UserRoleService(ICommonServicesProvider<FinLib.Models.Configs.GlobalSettings> commonServicesProvider)
            :base(commonServicesProvider)
        {

        }
	}
}

namespace FinLib.Services.DBO
{  
	public partial class BookService : Base.GeneralEntityService <FinLib.DomainClasses.DBO.Book, FinLib.Models.Dtos.DBO.BookDto, FinLib.Models.Views.DBO.BookView, FinLib.Models.SearchFilters.DBO.BookSearchFilter >
	{	
		public BookService(ICommonServicesProvider<FinLib.Models.Configs.GlobalSettings> commonServicesProvider)
            :base(commonServicesProvider)
        {

        }
	}
}

namespace FinLib.Services.DBO
{  
	public partial class BookBorrowingService : Base.UpdatableEntityService <FinLib.DomainClasses.DBO.BookBorrowing, FinLib.Models.Dtos.DBO.BookBorrowingDto, FinLib.Models.Views.DBO.BookBorrowingView, FinLib.Models.SearchFilters.DBO.BookBorrowingSearchFilter >
	{	
		public BookBorrowingService(ICommonServicesProvider<FinLib.Models.Configs.GlobalSettings> commonServicesProvider)
            :base(commonServicesProvider)
        {

        }
	}
}

namespace FinLib.Services.DBO
{  
	public partial class CategoryService : Base.GeneralEntityService <FinLib.DomainClasses.DBO.Category, FinLib.Models.Dtos.DBO.CategoryDto, FinLib.Models.Views.DBO.CategoryView, FinLib.Models.Base.SearchFilters.GeneralEntitySearchFilter >
	{	
		public CategoryService(ICommonServicesProvider<FinLib.Models.Configs.GlobalSettings> commonServicesProvider)
            :base(commonServicesProvider)
        {

        }
	}
}

namespace FinLib.Services.CNT
{  
	public partial class MenuLinkService : Base.GeneralEntityService <FinLib.DomainClasses.CNT.MenuLink, FinLib.Models.Dtos.CNT.MenuLinkDto, FinLib.Models.Views.CNT.MenuLinkView, FinLib.Models.SearchFilters.CNT.MenuLinkSearchFilter >
	{	
		public MenuLinkService(ICommonServicesProvider<FinLib.Models.Configs.GlobalSettings> commonServicesProvider)
            :base(commonServicesProvider)
        {

        }
	}
}

namespace FinLib.Services.CNT
{  
	public partial class MenuLinkOwnerService : Base.UpdatableEntityService <FinLib.DomainClasses.CNT.MenuLinkOwner, FinLib.Models.Dtos.CNT.MenuLinkOwnerDto, FinLib.Models.Views.CNT.MenuLinkOwnerView, FinLib.Models.Base.SearchFilters.UpdatableEntitySearchFilter >
	{	
		public MenuLinkOwnerService(ICommonServicesProvider<FinLib.Models.Configs.GlobalSettings> commonServicesProvider)
            :base(commonServicesProvider)
        {

        }
	}
}

