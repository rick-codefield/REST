using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Rest.Application.Entities;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Rest.Infrastructure.ModelBinders;

public class IdModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        var type = context.Metadata.ModelType;

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Id<>))
        {
            if (context.BindingInfo.BindingSource == BindingSource.Path)
            {
                var modelBinderType = typeof(IdModelBinder<>).MakeGenericType(type.GenericTypeArguments[0]);
                return Activator.CreateInstance(modelBinderType) as IModelBinder;
            }
        }

        return null;
    }
}

public static class IdModelBinderProviderExtensions
{
    public static MvcOptions AddId(this MvcOptions options)
    {
        options.ModelBinderProviders.Insert(0, new IdModelBinderProvider());

        foreach (var idType in Reflection.GetIds())
        {
            options.ModelMetadataDetailsProviders.Add(
                new BindingSourceMetadataProvider(idType, BindingSource.Path));
        }

        return options;
    }

    public static SwaggerGenOptions AddId(this SwaggerGenOptions options)
    {
        foreach (var idType in Reflection.GetIds())
        {
            options.MapType(idType, () => new OpenApiSchema
            {
                Type = "integer",
                Format = "int64"
            });
        }

        return options;
    }
}