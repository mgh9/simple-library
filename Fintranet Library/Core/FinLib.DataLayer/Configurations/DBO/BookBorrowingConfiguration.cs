using FinLib.DomainClasses.DBO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinLib.DataLayer.Configurations.DBO
{
    public partial class BookBorrowingConfiguration
    {
        protected override void AdditionalConfigure(EntityTypeBuilder<BookBorrowing> builder)
        {
            base.AdditionalConfigure(builder);

            builder.HasOne(x => x.LibrarianUserRole)
                    .WithMany()
                    .IsRequired()
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
