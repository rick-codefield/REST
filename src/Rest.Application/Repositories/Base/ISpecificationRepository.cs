using Rest.Application.Entities;
using Rest.Application.Specifications;

namespace Rest.Application.Repositories;

public interface ISpecificationRepository<TEntity> : IRepository<TEntity>
    where TEntity : Entity<TEntity>
{
    IAsyncEnumerable<TEntity> Get(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
}

public static class SpecificationRepositoryExtensions
{
    public static IAsyncEnumerable<TEntity> GetAll<TEntity>(
        this ISpecificationRepository<TEntity> repository,
        CancellationToken cancellationToken = default)
        where TEntity : Entity<TEntity> =>
        repository.Get(new ConstantSpecification<TEntity>(true), cancellationToken);


    public static Task<TEntity?> GetById<TEntity>(
        this ISpecificationRepository<TEntity> repository,
        Id<TEntity> id,
        CancellationToken cancellationToken = default)
        where TEntity : Entity<TEntity> =>
        repository
            .Get(new WithIdSpecification<TEntity>(id), cancellationToken)
            .FirstOrDefaultAsync(cancellationToken)
            .AsTask();

}