using Rest.Application.Entities;

namespace Rest.Application.Repositories;

public interface IPagedRepository<TEntity, TExpansion> : IRepository<TEntity>
    where TEntity : Entity<TEntity>
    where TExpansion : Expansion<TExpansion>, new()
{
    Task<PagedResult<TEntity>> GetPaged(IContinuationToken? continuationToken = null, TExpansion? expansion = null, CancellationToken cancellationToken = default);
}
