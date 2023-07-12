namespace Rest.Application.Specifications;

public class NotSpecificationTests
{
    [Fact]
    public void ExtensionMethodCreatesSpecification()
    {
        var inner = new ConstantSpecification<int>(true);
        var not = inner.Not();

        not.Inner.ShouldBe(inner);
    }

    [Fact]
    public void CorrectlyEvaluatesBooleanLogic()
    {
        new ConstantSpecification<int>(true)
            .Not()
            .IsSatisfiedBy(0)
            .ShouldBe(false);

        new ConstantSpecification<int>(false)
            .Not()
            .IsSatisfiedBy(0)
            .ShouldBe(true);
    }
}
