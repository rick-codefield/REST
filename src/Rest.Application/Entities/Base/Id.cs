namespace Rest.Application.Entities;

public record struct Id<TEntity>(int Value)
    where TEntity : Entity<TEntity>
{
}