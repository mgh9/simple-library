using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FinLib.DomainClasses.Base;

namespace FinLib.DataLayer.Configurations.Base
{
    public abstract class UpdatableEntityConfiguration<TUpdatableEntity> : BaseEntityConfiguration<TUpdatableEntity>
        where TUpdatableEntity : class, IUpdatableEntity
    {
        public override void Configure(EntityTypeBuilder<TUpdatableEntity> builder)
        {
            base.Configure(builder);

            // descriptions
            builder.Property(x => x.UpdateDate).HasComment("زمان ویرایش");
            builder.Property(x => x.UpdatedByUserRoleId).HasComment("شناسه ی کاربر ویرایش کننده");
        }
    }
}
