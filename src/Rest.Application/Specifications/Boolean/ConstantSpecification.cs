namespace Rest.Application.Specifications;

public class ConstantSpecification<T> : Specification<T>
{
    public ConstantSpecification(bool result):
        base(_ => result)
    {
    }
}
