namespace Rest.Application.Specifications;

public class ConstantSpecificationTests
{
    [Fact]
    public void CorrectlyEvaluatesBooleanLogic()
    {
        var trueSpecification = new ConstantSpecification<byte>(true);
        var falseSpecification = new ConstantSpecification<byte>(false);

        for (int i = byte.MinValue; i < byte.MaxValue; ++i)
        {
            trueSpecification
                .IsSatisfiedBy((byte)0)
                .ShouldBe(true);

            falseSpecification
                .IsSatisfiedBy((byte)0)
                .ShouldBe(false);
        }
    }
}
