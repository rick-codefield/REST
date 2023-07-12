using Rest.Application.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rest.Application.Converters;

public sealed class IdJsonConverter<TEntity> : JsonConverter<Id<TEntity>>
    where TEntity : Entity<TEntity>
{
    public override Id<TEntity> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        new(reader.GetInt32());

    public override void Write(Utf8JsonWriter writer, Id<TEntity> value, JsonSerializerOptions options) =>
        writer.WriteNumberValue(value.Value);
}
