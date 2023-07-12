namespace Rest.Application.Specifications;

public sealed class OrSpecification<T> : BinarySpecification<T>
{
    public OrSpecification(ISpecification<T> left, ISpecification<T> right) :
        base(left, right, System.Linq.Expressions.Expression.OrElse)
    {
    }
}

public static class OrSpecificationExtensions
{
    public static OrSpecification<T> Or<T>(this ISpecification<T> left, ISpecification<T> right) =>
        new OrSpecification<T>(left, right);
}