 
  
  
  
  
  
  

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;

namespace FinLib.DataLayer.Configurations.SEC
{  
	public partial class RoleConfiguration : Base.BaseEntityConfiguration <FinLib.DomainClasses.SEC.Role>
	{
	    public override void Configure(EntityTypeBuilder<FinLib.DomainClasses.SEC.Role> builder)
        {
            base.Configure(builder);

			this.AdditionalConfigure(builder);
        }
	}
}

namespace FinLib.DataLayer.Configurations.SEC
{  
	public partial class UserConfiguration : Base.UpdatableEntityConfiguration <FinLib.DomainClasses.SEC.User>
	{
	    public override void Configure(EntityTypeBuilder<FinLib.DomainClasses.SEC.User> builder)
        {
            base.Configure(builder);

			this.AdditionalConfigure(builder);
        }
	}
}

namespace FinLib.DataLayer.Configurations.SEC
{  
	public partial class UserRoleConfiguration : Base.UpdatableEntityConfiguration <FinLib.DomainClasses.SEC.UserRole>
	{
	    public override void Configure(EntityTypeBuilder<FinLib.DomainClasses.SEC.UserRole> builder)
        {
            base.Configure(builder);

			this.AdditionalConfigure(builder);
        }
	}
}

namespace FinLib.DataLayer.Configurations.DBO
{  
	public partial class BookConfiguration : Base.GeneralEntityConfiguration <FinLib.DomainClasses.DBO.Book>
	{
	    public override void Configure(EntityTypeBuilder<FinLib.DomainClasses.DBO.Book> builder)
        {
            base.Configure(builder);

			this.AdditionalConfigure(builder);
        }
	}
}

namespace FinLib.DataLayer.Configurations.DBO
{  
	public partial class BookBorrowingConfiguration : Base.UpdatableEntityConfiguration <FinLib.DomainClasses.DBO.BookBorrowing>
	{
	    public override void Configure(EntityTypeBuilder<FinLib.DomainClasses.DBO.BookBorrowing> builder)
        {
            base.Configure(builder);

			this.AdditionalConfigure(builder);
        }
	}
}

namespace FinLib.DataLayer.Configurations.DBO
{  
	public partial class CategoryConfiguration : Base.GeneralEntityConfiguration <FinLib.DomainClasses.DBO.Category>
	{
	    public override void Configure(EntityTypeBuilder<FinLib.DomainClasses.DBO.Category> builder)
        {
            base.Configure(builder);

			this.AdditionalConfigure(builder);
        }
	}
}

namespace FinLib.DataLayer.Configurations.CNT
{  
	public partial class MenuLinkConfiguration : Base.GeneralEntityConfiguration <FinLib.DomainClasses.CNT.MenuLink>
	{
	    public override void Configure(EntityTypeBuilder<FinLib.DomainClasses.CNT.MenuLink> builder)
        {
            base.Configure(builder);

			this.AdditionalConfigure(builder);
        }
	}
}

namespace FinLib.DataLayer.Configurations.CNT
{  
	public partial class MenuLinkOwnerConfiguration : Base.UpdatableEntityConfiguration <FinLib.DomainClasses.CNT.MenuLinkOwner>
	{
	    public override void Configure(EntityTypeBuilder<FinLib.DomainClasses.CNT.MenuLinkOwner> builder)
        {
            base.Configure(builder);

			this.AdditionalConfigure(builder);
        }
	}
}

