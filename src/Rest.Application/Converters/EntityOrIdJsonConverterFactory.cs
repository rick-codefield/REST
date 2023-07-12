using Rest.Application.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rest.Application.Converters;

public sealed class EntityOrIdJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert) =>
        typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(EntityOrId<>);

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var converterType = typeof(EntityOrIdJsonConverter<>)
            .MakeGenericType(new[] { typeToConvert.GenericTypeArguments[0] });

        return (JsonConverter?)Activator.CreateInstance(converterType);
    }
}
