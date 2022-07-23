using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FinLib.DomainClasses.Base;

namespace FinLib.DataLayer.Configurations.Base
{
    public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity :class, IBaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x=> x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.CreateDate)
                    .IsRequired()
                    .HasDefaultValueSql("getdate()");

            // descriptions
            builder.Property(x => x.Id).HasComment("Entity PrimaryKey");

            this.AdditionalConfigure(builder);
        }

        protected virtual void AdditionalConfigure(EntityTypeBuilder<TEntity> builder)
        {
            // if any additional configs needed, in the childs
        }
    }
}
