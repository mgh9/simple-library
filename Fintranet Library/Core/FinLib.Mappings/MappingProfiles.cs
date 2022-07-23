using AutoMapper;
using FinLib.DomainClasses.CNT;
using FinLib.DomainClasses.DBO;
using FinLib.DomainClasses.SEC;
using FinLib.Models.Dtos.CNT;
using FinLib.Models.Dtos.DBO;
using FinLib.Models.Dtos.SEC;
using FinLib.Models.Dtos.SEC.User.Profile;
using FinLib.Models.Views.CNT;
using FinLib.Models.Views.DBO;
using FinLib.Models.Views.SEC;

namespace FinLib.Mappings
{
    internal static class MappingProfiles
    {
        internal static IMapper Init()
        {
            var theMappingAssembly = typeof(MapperHelper).Assembly;
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                var profiles = theMappingAssembly
                                    .GetTypes()
                                    .Where(x => !x.ContainsGenericParameters
                                                && x != typeof(Profile)
                                                && typeof(Profile).IsAssignableFrom(x));

                injectOtherMappingProfiles(cfg, profiles);

                configGlobalDateTimeMapping(cfg);
                
                configEntitiesMapping(cfg);

                //cfg.CreateMap<IBaseEntity, BaseDto>().IncludeAllDerived();

                // no need anymore. Dto's doesnt have CreateDate property to worry about
                //cfg.AddGlobalIgnore("CreateDate");
            });

            mappingConfig.AssertConfigurationIsValid();

            return mappingConfig.CreateMapper();
        }

        private static void configEntitiesMapping(IMapperConfigurationExpression cfg)
        {
            #region DBO
            cfg.CreateMap<Category, Category>();
            cfg.CreateMap<Category, CategoryDto>()
                .ReverseMap();
            cfg.CreateMap<Category, CategoryView>();

            cfg.CreateMap<Book, Book>();
            cfg.CreateMap<Book, BookDto>()
                .ReverseMap();
            cfg.CreateMap<Book, BookView>()
            ;

            cfg.CreateMap<BookBorrowing, BookBorrowing>();
            cfg.CreateMap<BookBorrowing, BookBorrowingDto>()
                .ReverseMap();
            cfg.CreateMap<BookBorrowing, BookBorrowingView>()
                .ForMember(dst => dst.CustomerUserFullName, dst => dst.Ignore())
                .ForMember(dst => dst.CategoryTitle, dst => dst.Ignore())
                .ForMember(dst => dst.LibrarianUserFullName, dst => dst.Ignore());

            cfg.CreateMap<BookBorrowing, MyBorrowingHistoryView>()
                .ForMember(dst => dst.CategoryTitle, dst => dst.Ignore());
            #endregion

            #region CNT

            cfg.CreateMap<MenuLink, MenuLink>();
            cfg.CreateMap<MenuLink, MenuLinkDto>()
                .ForMember(dst => dst.SubMenus, dst => dst.Ignore())
                .ForMember(dst => dst.Owners, dst => dst.Ignore())
                .ReverseMap();
            cfg.CreateMap<MenuLink, MenuLinkView>();

            cfg.CreateMap<MenuLinkOwner, MenuLinkOwner>();
            cfg.CreateMap<MenuLinkOwner, MenuLinkOwnerDto>()
                .ReverseMap()
                .ForMember(x => x.Role, opt => opt.Ignore());
            cfg.CreateMap<MenuLinkOwner, MenuLinkOwnerView>();

            #endregion

            #region SEC

            cfg.CreateMap<User, User>();
            cfg.CreateMap<User, UserDto>()
                .ForMember(dst => dst.Password, dst => dst.Ignore())
                .ForMember(dst => dst.UserRoles, dst => dst.Ignore())
                .ForMember(dst => dst.LastLoggedInTime, dst => dst.Ignore())
                .ForMember(dst => dst.FullName, dst => dst.Ignore())
                .ReverseMap();
            cfg.CreateMap<User, UserView>()
                .ForMember(dst => dst.FullName, dst => dst.Ignore());
            cfg.CreateMap<UserView, UserProfileDto>().IgnoreAllUnmapped().ReverseMap();

            cfg.CreateMap<Role, Role>();
            cfg.CreateMap<Role, RoleDto>().ReverseMap();
            cfg.CreateMap<Role, RoleView>();

            cfg.CreateMap<UserRole, UserRole>();
            cfg.CreateMap<UserRole, UserRoleDto>()
                .ForMember(x => x.RoleName, x => x.Ignore())
                .ForMember(x => x.RoleTitle, x => x.Ignore())
                .ReverseMap();
            cfg.CreateMap<UserRole, UserRoleView>();

            #endregion
        }

        private static void configGlobalDateTimeMapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<DateTimeOffset, DateTime>().ConvertUsing(x => x.LocalDateTime);
        }

        /// <summary>
        /// Inject other profiles if there are any
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="profiles"></param>
        private static void injectOtherMappingProfiles(IMapperConfigurationExpression cfg, IEnumerable<Type> profiles)
        {
            foreach (var theProfile in profiles)
            {
                cfg.AddProfile((AutoMapper.Profile)Activator.CreateInstance(theProfile));
            }
        }
    }
}