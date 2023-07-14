using Rest.Application.Entities;
using Rest.Application.Specifications;

namespace Rest.Application.Repositories;

public interface ISpecificationRepository<TEntity, TExpansion> : IRepository<TEntity>
    where TEntity : Entity<TEntity>
    where TExpansion : Expansion<TExpansion>, new()
{
    IAsyncEnumerable<TEntity> Get(ISpecification<TEntity> specification, TExpansion? expansion = null, CancellationToken cancellationToken = default);
}

public static class SpecificationRepositoryExtensions
{
    public static IAsyncEnumerable<TEntity> GetAll<TEntity, TExpansion>(
        this ISpecificationRepository<TEntity, TExpansion> repository,
        TExpansion? expansion = null,
        CancellationToken cancellationToken = default)
        where TEntity : Entity<TEntity>
        where TExpansion : Expansion<TExpansion>, new() =>
        repository.Get(new ConstantSpecification<TEntity>(true), expansion, cancellationToken);


    public static Task<TEntity?> GetById<TEntity, TExpansion>(
        this ISpecificationRepository<TEntity, TExpansion> repository,
        Id<TEntity> id,
        TExpansion? expansion = null,
        CancellationToken cancellationToken = default)
        where TEntity : Entity<TEntity>
        where TExpansion : Expansion<TExpansion>, new() =>
        repository
            .Get(new WithIdSpecification<TEntity>(id), expansion, cancellationToken)
            .FirstOrDefaultAsync(cancellationToken)
            .AsTask();

}