using FinLib.DomainClasses.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinLib.DataLayer.Configurations.Base
{
    public abstract class GeneralEntityConfiguration<TGeneralEntity> 
        : UpdatableEntityConfiguration<TGeneralEntity>
        where TGeneralEntity : class, IGeneralEntity
    {
        public override void Configure(EntityTypeBuilder<TGeneralEntity> builder)
        {
            base.Configure(builder);
            
            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.Title);

            builder.Property(x => x.IsActive).IsRequired();

            builder.Property(x => x.Description).HasMaxLength(1000);

            // description
        }
    }
}
