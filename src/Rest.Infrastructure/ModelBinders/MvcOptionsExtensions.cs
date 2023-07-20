using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Rest.Infrastructure.ModelBinders;

public static class MvcOptionsExtensions
{
    public static MvcOptions AddInfrastructure(this MvcOptions options)
    {
        return options
            .AddId()
            .AddExpansion()
            .AddContinuationToken();
    }

    public static SwaggerGenOptions AddInfrastructure(this SwaggerGenOptions options)
    {
        return options
            .AddId()
            .AddExpansion()
            .AddContinuationToken();
    }
}