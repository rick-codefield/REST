using Rest.Application.Converters;
using System.Text.Json.Serialization;

namespace Rest.Application.Entities;

[JsonConverter(typeof(IdJsonConverterFactory))]
public record struct Id<TEntity>(int Value)
    where TEntity : Entity<TEntity>
{
}