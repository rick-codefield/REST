namespace Rest.Application.Specifications;

public class OrSpecificationTests
{
    [Fact]
    public void ExtensionMethodCreatesSpecification()
    {
        var left = new ConstantSpecification<int>(true);
        var right = new ConstantSpecification<int>(true);
        var and = left.Or(right);

        and.Left.ShouldBe(left);
        and.Right.ShouldBe(right);
    }

    [Fact]
    public void CorrectlyEvaluatesBooleanLogic()
    {
        new ConstantSpecification<int>(true)
            .Or(new ConstantSpecification<int>(true))
            .IsSatisfiedBy(0)
            .ShouldBe(true);

        new ConstantSpecification<int>(false)
            .Or(new ConstantSpecification<int>(true))
            .IsSatisfiedBy(0)
            .ShouldBe(true);

        new ConstantSpecification<int>(true)
            .Or(new ConstantSpecification<int>(false))
            .IsSatisfiedBy(0)
            .ShouldBe(true);

        new ConstantSpecification<int>(false)
            .Or(new ConstantSpecification<int>(false))
            .IsSatisfiedBy(0)
            .ShouldBe(false);
    }
}
