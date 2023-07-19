using Microsoft.AspNetCore.Mvc.ModelBinding;
using Rest.Application.Repositories;

namespace Rest.Infrastructure.ModelBinders;

public class ExpansionModelBinder<TExpansion> : IModelBinder
    where TExpansion : Expansion<TExpansion>, new()
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
            var expansion = Expansion<TExpansion>.Parse(valueResult.FirstValue, null);
            bindingContext.Result = ModelBindingResult.Success(expansion);
        }

        return Task.CompletedTask;
    }
}
