using Rest.Application.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rest.Application.Converters;

public sealed class EntityOrIdJsonConverter<TEntity> : JsonConverter<EntityOrId<TEntity>>
    where TEntity : Entity<TEntity>
{
    public override EntityOrId<TEntity> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            return JsonSerializer.Deserialize<TEntity>(ref reader, options)!;
        }
        else
        {
            return JsonSerializer.Deserialize<Id<TEntity>>(ref reader, options);
        }
    }

    public override void Write(Utf8JsonWriter writer, EntityOrId<TEntity> value, JsonSerializerOptions options)
    {
        if (value.Entity != null)
        {
            JsonSerializer.Serialize(writer, value.Entity, options);
        }
        else
        {
            JsonSerializer.Serialize(writer, value.Id, options);
        }
    }
}
