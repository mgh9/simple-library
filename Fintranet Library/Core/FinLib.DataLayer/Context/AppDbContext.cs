using FinLib.DomainClasses.CNT;
using FinLib.DomainClasses.DBO;
using FinLib.DomainClasses.SEC;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinLib.DataLayer.Context
{
    public class AppDbContext
        : IdentityDbContext<User, Role, int,
            IdentityUserClaim<int>,
            UserRole,
            IdentityUserLogin<int>,
            IdentityRoleClaim<int>,
            IdentityUserToken<int>>
        , IUnitOfWork
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        #region Dbo
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookBorrowing> BookBorrowings { get; set; }
        #endregion

        #region CNT
        public virtual DbSet<MenuLink> MenuLinks { get; set; }
        public virtual DbSet<MenuLinkOwner> MenuLinkOwners { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims", "SEC");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims", "SEC");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins", "SEC");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens", "SEC");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
            }
        }

        public void BeginTransaction()
        {
            if (this.Database.CurrentTransaction is null)
                Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (Database.CurrentTransaction is not null)
                Database.CurrentTransaction.Commit();
            else
                Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            if (Database.CurrentTransaction != null)
                Database.CurrentTransaction.Rollback();
            else
                Database.RollbackTransaction();
        }

        public void DisposeTransaction()
        {
            if (Database.CurrentTransaction is not null)
                this.Database.CurrentTransaction.Dispose();
        }
    }
}
