using Rest.Application.Entities;

namespace Rest.Application.Repositories;

public interface IPagedRepository<TEntity> : IRepository<TEntity>
    where TEntity : Entity<TEntity>
{
    Task<PagedResult<TEntity>> GetPaged(IContinuationToken? continuationToken = null, CancellationToken cancellationToken = default);
}
