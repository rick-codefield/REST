using Microsoft.AspNetCore.Http;
using Rest.Application.Repositories;

namespace Rest.Infrastructure.Repositories;

public static class ContinuationTokenExtensions
{
    public static void Add(this IHeaderDictionary headers, IContinuationToken continuationToken)
    {
        headers.Add("Continuation-Token", Convert.ToBase64String(continuationToken.Serialize()));
    }
}
