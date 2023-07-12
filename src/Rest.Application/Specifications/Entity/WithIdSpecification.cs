using Rest.Application.Entities;
using System.Linq.Expressions;

namespace Rest.Application.Specifications;

public class WithIdSpecification<TEntity> : Specification<TEntity>
    where TEntity : Entity<TEntity>
{
    public WithIdSpecification(Id<TEntity> id) : base(entity => entity.Id == id)
    {
        Id = id;
    }

    public Id<TEntity> Id { get; }
}
