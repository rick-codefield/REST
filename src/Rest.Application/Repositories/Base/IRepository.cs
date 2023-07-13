using Rest.Application.Entities;

namespace Rest.Application.Repositories;

public interface IRepository<TEntity>
    where TEntity: Entity<TEntity>
{
    Task Add(TEntity entity, CancellationToken cancellationToken = default);
}
