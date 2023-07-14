namespace Rest.Application.Repositories;

public sealed record BrandExpansion : Expansion<BrandExpansion>
{
    public CompanyExpansion? Company { get; init; }
    //public ProductExpansion? Products { get; init; }
}