using Microsoft.AspNetCore.Mvc.ModelBinding;
using Rest.Application.Repositories;

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
