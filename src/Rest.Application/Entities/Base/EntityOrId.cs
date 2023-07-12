using Rest.Application.Converters;
using System.Text.Json.Serialization;

namespace Rest.Application.Entities;

/// <summary>
/// Represents a reference to an entity that may or may not have been expanded.
/// If the entity has not been expanded, this will only store the id of that entity.
/// </summary>
[JsonConverter(typeof(EntityOrIdJsonConverterFactory))]
public readonly struct EntityOrId<TEntity>
    where TEntity : Entity<TEntity>
{
    public EntityOrId(Id<TEntity> id)
    {
        _id = id;
    }

    public EntityOrId(TEntity entity)
    {
        _entity = entity;
    }

    /// <summary>
    /// The id of the entity, which is available whether or not the entity has been expanded. 
    /// </summary>
    public Id<TEntity> Id => _entity?.Id ?? _id;

    /// <summary>
    /// Returns the entity if it has been expanded, <c>null</c> otherwise.
    /// </summary>
    public TEntity? Entity => _entity;

    public static implicit operator EntityOrId<TEntity>(Id<TEntity> id) => new(id);
    public static implicit operator EntityOrId<TEntity>(TEntity entity) => new(entity);

    // TODO: Reduce memory usage with a tagged union
    private readonly Id<TEntity> _id;
    private readonly TEntity? _entity;
}