using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Rest.Application.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Rest.Infrastructure.ModelBinders;

public sealed class ContinuationTokenModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType.IsAssignableTo(typeof(IContinuationToken)))
        {
            return new ContinuationTokenModelBinder();
        }

        return null;
    }
}

public static class ContinuationTokenModelBinderProviderExtensions
{
    public static MvcOptions AddContinuationToken(this MvcOptions options)
    {
        options.ModelBinderProviders.Insert(0, new ContinuationTokenModelBinderProvider());

        options.ModelMetadataDetailsProviders.Add(
                new BindingSourceMetadataProvider(typeof(IContinuationToken), BindingSource.Header));

        return options;
    }

    public static SwaggerGenOptions AddContinuationToken(this SwaggerGenOptions options)
    {
        options.MapType<IContinuationToken>(() => new OpenApiSchema
        {
            Type = "string",
            Format = "string"
        });

        return options;
    }
}