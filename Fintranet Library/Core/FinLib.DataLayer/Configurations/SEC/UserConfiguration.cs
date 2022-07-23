using FinLib.DomainClasses.SEC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinLib.DataLayer.Configurations.SEC
{
    public partial class UserConfiguration
    {
        protected override void AdditionalConfigure(EntityTypeBuilder<User> builder)
        {
            base.AdditionalConfigure(builder);

            builder.ToTable("Users", "SEC");
        }
    }
}
