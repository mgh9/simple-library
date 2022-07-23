using FinLib.DomainClasses.SEC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinLib.DataLayer.Configurations.SEC
{
    public partial class RoleConfiguration
    {
        protected override void AdditionalConfigure(EntityTypeBuilder<Role> builder)
        {
            base.AdditionalConfigure(builder);

            builder.ToTable("Roles", "SEC");
        }
    }
}
