using Microsoft.AspNetCore.Mvc.ModelBinding;
using Rest.Application.Entities;

namespace Rest.Infrastructure.ModelBinders;

public class IdModelBinder<TEntity> : IModelBinder
    where TEntity : Entity<TEntity>
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueResult);

        if (int.TryParse(valueResult.FirstValue, out var id))
        {
            bindingContext.Result = ModelBindingResult.Success(new Id<TEntity>(id));
        }

        return Task.CompletedTask;
    }
}
