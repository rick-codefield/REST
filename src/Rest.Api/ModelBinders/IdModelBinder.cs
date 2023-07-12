using Microsoft.AspNetCore.Mvc.ModelBinding;
using Rest.Application.Entities;

namespace Rest.Api.ModelBinders;

public class IdModelBinder<TEntity> : IModelBinder
    where TEntity : Entity<TEntity>
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        return Task.CompletedTask;
    }
}
