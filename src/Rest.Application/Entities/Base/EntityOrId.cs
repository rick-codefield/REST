namespace Rest.Application.Entities;

/// <summary>
/// Represents a reference to an entity that may or may not have been expanded.
/// If the entity has not been expanded, this will only store the id of that entity.
/// </summary>
public struct EntityOrId<TEntity>
    where TEntity : Entity<TEntity>
{
    /// <summary>
    /// The id of the entity, which is available whether or not the entity has been expanded. 
    /// </summary>
    public Id<TEntity> Id => _entity?.Id ?? _id;

    /// <summary>
    /// Returns the entity if it has been expanded, <c>null</c> otherwise.
    /// </summary>
    public TEntity? Entity => _entity;

    // TODO: Reduce memory usage with a tagged union
    private Id<TEntity> _id;
    private TEntity? _entity;
}