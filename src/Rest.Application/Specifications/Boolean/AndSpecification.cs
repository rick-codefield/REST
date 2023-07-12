namespace Rest.Application.Specifications;

public sealed class AndSpecification<T> : BinarySpecification<T>
{
    public AndSpecification(ISpecification<T> left, ISpecification<T> right) :
        base(left, right, System.Linq.Expressions.Expression.AndAlso)
    {
    }
}

public static class AndSpecificationExtensions
{
    public static AndSpecification<T> And<T>(this ISpecification<T> left, ISpecification<T> right) =>
        new AndSpecification<T>(left, right);
}
