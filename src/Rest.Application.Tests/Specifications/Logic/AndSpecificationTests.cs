namespace Rest.Application.Specifications;

public class AndSpecificationTests
{
    [Fact]
    public void ExtensionMethodCreatesSpecification()
    {
        var left = new ConstantSpecification<int>(true);
        var right = new ConstantSpecification<int>(true);
        var and = left.And(right);

        and.Left.ShouldBe(left);
        and.Right.ShouldBe(right);
    }

    [Fact]
    public void CorrectlyEvaluatesBooleanLogic()
    {
        new ConstantSpecification<int>(true)
            .And(new ConstantSpecification<int>(true))
            .IsSatisfiedBy(0)
            .ShouldBe(true);

        new ConstantSpecification<int>(false)
            .And(new ConstantSpecification<int>(true))
            .IsSatisfiedBy(0)
            .ShouldBe(false);

        new ConstantSpecification<int>(true)
            .And(new ConstantSpecification<int>(false))
            .IsSatisfiedBy(0)
            .ShouldBe(false);

        new ConstantSpecification<int>(false)
            .And(new ConstantSpecification<int>(false))
            .IsSatisfiedBy(0)
            .ShouldBe(false);
    }
}
