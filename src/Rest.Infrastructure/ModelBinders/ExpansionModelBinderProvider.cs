using Microsoft.AspNetCore.Mvc.ModelBinding;
using Rest.Application.Repositories;

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
