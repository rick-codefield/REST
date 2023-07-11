using System;
using System.Linq.Expressions;

namespace Rest.Application.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Expression { get; }
}

public static class SpecificationExtensions
{
    public static bool IsSatisfiedBy<T>(this ISpecification<T> specification, T obj) =>
        specification.Expression.Compile()(obj);
}