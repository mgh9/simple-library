using FinLib.DomainClasses.SEC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinLib.DataLayer.Configurations.SEC
{
    public partial class UserRoleConfiguration
    {
        protected override void AdditionalConfigure(EntityTypeBuilder<UserRole> builder)
        {
            base.AdditionalConfigure(builder);

            builder.ToTable("UserRoles", "SEC");
        }
    }
}
