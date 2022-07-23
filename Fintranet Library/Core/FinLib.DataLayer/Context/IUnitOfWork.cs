using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Diagnostics.CodeAnalysis;

namespace FinLib.DataLayer.Context
{
    public interface IUnitOfWork : IDisposable, ITransactionProvider
    {
        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        int SaveChanges();

        public DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());

        IModel Model { get; }
        EntityEntry Entry([NotNull] object entity);
        EntityEntry<TEntity> Entry<TEntity>([NotNull] TEntity entity) where TEntity : class;
    }
}