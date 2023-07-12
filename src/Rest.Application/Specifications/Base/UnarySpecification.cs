using System.Linq.Expressions;

namespace Rest.Application.Specifications;

public class UnarySpecification<T> : Specification<T>
{
    public UnarySpecification(
        ISpecification<T> inner,
        Func<Expression<Func<T, bool>>, Expression<Func<T, bool>>> combinator) :
        base(combinator(inner.Expression))
    {
        Inner = inner;
    }

    public UnarySpecification(
        ISpecification<T> inner,
        Func<Expression, UnaryExpression> combinator) :
        this(inner, (inner) =>
        {
            var body = combinator(inner.Body);
            return System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(body, inner.Parameters[0]);
        })
    {
    }

    public ISpecification<T> Inner { get; }
}
