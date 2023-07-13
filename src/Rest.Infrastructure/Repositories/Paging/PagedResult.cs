using Microsoft.AspNetCore.Http;
using Rest.Application.Repositories;

namespace Rest.Infrastructure.Repositories;

public static class PagedResultExtensions
{
    public static void Add<T>(this IHeaderDictionary headers, PagedResult<T> pagedResult)
    {
        if (pagedResult.TotalCount.HasValue)
        {
            headers.Add("Pagination-Total", pagedResult.TotalCount.Value.ToString());
        }

        if (pagedResult.ContinuationToken != null)
        {
            headers.Add(pagedResult.ContinuationToken);
        }
    }
}
