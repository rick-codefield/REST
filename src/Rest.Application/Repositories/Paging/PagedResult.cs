namespace Rest.Application.Repositories;

public class PagedResult<T>
{
    public required T[] Data { get; init; }
    public int? TotalCount { get; init; }
    public IContinuationToken? ContinuationToken { get; init; }
}
