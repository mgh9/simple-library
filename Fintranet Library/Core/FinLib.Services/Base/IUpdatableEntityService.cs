using FinLib.Models.Base.Dto;
using FinLib.Models.Base.SearchFilters;
using FinLib.Models.Base.View;
using System.Linq.Expressions;

namespace FinLib.Services.Base
{
    public interface IUpdatableEntityService<TUpdatableEntity, TUpdatableDto, TUpdatableView, TSearchFilter>
            : IBaseEntityService<TUpdatableEntity, TUpdatableDto, TUpdatableView, TSearchFilter>
        where TUpdatableEntity : class, DomainClasses.Base.IUpdatableEntity, new()
        where TUpdatableDto : UpdatableDto, new()
        where TUpdatableView : UpdatableView, new()
        where TSearchFilter : UpdatableEntitySearchFilter, new()
    {
        Task<int> InsertAsync(TUpdatableDto model);

        Task UpdateAsync(TUpdatableDto model);
        Task UpdateAsync(TUpdatableDto model, params Expression<Func<TUpdatableEntity, object>>[] excludeProperties);
        
        Task DeleteAsync(int id);

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
