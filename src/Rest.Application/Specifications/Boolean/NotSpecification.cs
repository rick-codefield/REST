namespace Rest.Application.Specifications;

public sealed class NotSpecification<T> : UnarySpecification<T>
{
    public NotSpecification(ISpecification<T> inner):
        base(inner, System.Linq.Expressions.Expression.Not)
    {
    }
}

public static class NotSpecificationExtensions
{
    public static NotSpecification<T> Not<T>(this ISpecification<T> inner) =>
        new NotSpecification<T>(inner);
}