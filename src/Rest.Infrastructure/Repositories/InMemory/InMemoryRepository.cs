using Rest.Application.Entities;
using Rest.Application.Repositories;
using Rest.Application.Specifications;

namespace Rest.Infrastructure.Repositories;

public abstract class InMemoryRepository<TEntity, TExpansion>: ISpecificationRepository<TEntity, TExpansion>, IPagedRepository<TEntity, TExpansion>
    where TEntity : Entity<TEntity>
    where TExpansion : Expansion<TExpansion>, new()
{
    public InMemoryRepository(InMemoryUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public InMemoryUnitOfWork UnitOfWork { get; }

    public Task Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.Id = new(++_idCounter);
        Entities.Add(entity.Id, entity);
        return Task.CompletedTask;
    }

    public virtual IAsyncEnumerable<TEntity> Get(ISpecification<TEntity> specification, TExpansion? expansion, CancellationToken cancellationToken = default) =>
        GetSync(specification, expansion, cancellationToken).ToAsyncEnumerable();

    protected IEnumerable<TEntity> GetSync(ISpecification<TEntity> specification, TExpansion? expansion, CancellationToken cancellationToken)
    {
        foreach (var entity in Entities.Values)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (specification.IsSatisfiedBy(entity))
            {
                yield return entity.Clone();
            }
        }
    }

    public virtual Task<PagedResult<TEntity>> GetPaged(IContinuationToken? continuationToken = null, TExpansion? expansion = null, CancellationToken cancellationToken = default)
    {
        IEnumerable<KeyValuePair<Id<TEntity>, TEntity>> query = Entities;

        switch (continuationToken)
        {
            case SeekContinuationToken seekContinuationToken:
            query = query
                .SkipWhile(kv => kv.Key.Value != seekContinuationToken.Id)
                .Skip(1);
                break;
        }

        var data = query
            .Select(kv => kv.Value.Clone())
            .Take(10)
            .ToArray();

        continuationToken = null;
        if (data.Length > 0 && data[^1].Id != Entities.Last().Key)
        {
            continuationToken = new SeekContinuationToken(data[^1].Id.Value);
        }

        var result = new PagedResult<TEntity>()
        {
            Data = data,
            ContinuationToken = continuationToken,
            TotalCount = Entities.Count
        };

        return Task.FromResult(result);
    }

    protected SortedDictionary<Id<TEntity>, TEntity> Entities { get; } = new();
    private int _idCounter = 0;
}
