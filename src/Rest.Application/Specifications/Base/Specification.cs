using System;
using System.Linq.Expressions;

namespace Rest.Application.Specifications;

public abstract class Specification<T> : ISpecification<T>
{
    protected Specification(Expression<Func<T, bool>> expression)
    {
        Expression = expression;
    }

    public Expression<Func<T, bool>> Expression { get; }
}
