using System;
using System.Linq.Expressions;

namespace Rest.Application.Specifications;

public abstract class BinarySpecification<T> : Specification<T>
{
    public BinarySpecification(
        ISpecification<T> left,
        ISpecification<T> right,
        Func<Expression<Func<T, bool>>, Expression<Func<T, bool>>, Expression<Func<T, bool>>> combinator):
        base(combinator(left.Expression, right.Expression))
    {
        Left = left;
        Right = right;
    }

    public BinarySpecification(
        ISpecification<T> left,
        ISpecification<T> right,
        Func<Expression, Expression, BinaryExpression> combinator) :
        this(left, right, (left, right) =>
        {
            var body = combinator(left.Body, right.Body);
            return System.Linq.Expressions.Expression.Lambda<Func<T, bool>>(body, left.Parameters[0]);
        })
    {
    }

    public ISpecification<T> Left { get; }
    public ISpecification<T> Right { get; }
}
