using Rest.Application.Entities;

namespace Rest.Application.Repositories;

public interface IRepository<TEntity>
    where TEntity: Entity<TEntity>
{
}
