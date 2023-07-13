using Microsoft.AspNetCore.Mvc.ModelBinding;
using Rest.Application.Entities;

namespace Rest.Infrastructure.ModelBinders;

public class IdModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        var type = context.Metadata.ModelType;

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Id<>))
        {
            var modelBinderType = typeof(IdModelBinder<>).MakeGenericType(type.GenericTypeArguments[0]);
            return Activator.CreateInstance(modelBinderType) as IModelBinder;
        }

        return null;
    }
}
