using Rest.Application.Entities;
using Rest.Application.Repositories;
using Rest.Application.Specifications;

namespace Rest.Infrastructure.Repositories;

public abstract class InMemoryRepository<TEntity>: ISpecificationRepository<TEntity>
    where TEntity : Entity<TEntity>
{
    public IAsyncEnumerable<TEntity> Get(ISpecification<TEntity> specification, CancellationToken cancellationToken = default) =>
        GetSync(specification, cancellationToken).ToAsyncEnumerable();

    private IEnumerable<TEntity> GetSync(ISpecification<TEntity> specification, CancellationToken cancellationToken)
    {
        foreach (var entity in Entities.Values)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (specification.IsSatisfiedBy(entity))
            {
                yield return entity;
            }
        }
    }

    protected Dictionary<Id<TEntity>, TEntity> Entities { get; } = new();
}
