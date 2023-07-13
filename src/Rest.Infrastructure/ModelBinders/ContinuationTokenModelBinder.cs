using Microsoft.AspNetCore.Mvc.ModelBinding;
using Rest.Application.Repositories;

namespace Rest.Infrastructure.ModelBinders;

internal class ContinuationTokenModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueResult);

        if (valueResult.FirstValue != null)
        {
            var continuationToken = ContinuationToken.Deserialize(Convert.FromBase64String(valueResult.FirstValue));
            bindingContext.Result = ModelBindingResult.Success(continuationToken);
        }

        return Task.CompletedTask;
    }
}
