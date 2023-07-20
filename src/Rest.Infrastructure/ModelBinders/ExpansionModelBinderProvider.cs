using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Rest.Application.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Rest.Infrastructure.ModelBinders;

public sealed class ExpansionModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        var type = context.Metadata.ModelType;

        if (type.BaseType != null && type.BaseType.IsGenericType && type.BaseType == typeof(Expansion<>).MakeGenericType(type))
        {
            var modelBinderType = typeof(ExpansionModelBinder<>).MakeGenericType(type);
            return Activator.CreateInstance(modelBinderType) as IModelBinder;
        }

        return null;
    }
}

public static class ExpansionModelBinderProviderExtensions
{
    public static MvcOptions AddExpansion(this MvcOptions options)
    {
        options.ModelBinderProviders.Insert(0, new ExpansionModelBinderProvider());

        foreach (var expansionType in Reflection.GetExpansions())
        {
            options.ModelMetadataDetailsProviders.Add(
                new BindingSourceMetadataProvider(expansionType, BindingSource.Query));
        }

        return options;
    }

    public static SwaggerGenOptions AddExpansion(this SwaggerGenOptions options)
    {
        foreach (var expansionType in Reflection.GetExpansions())
        {
            options.MapType(expansionType, () => new OpenApiSchema
            {
                Type = "string",
                Format = "string"
            });
        }

        return options;
    }
}