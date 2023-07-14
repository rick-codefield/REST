namespace Rest.Application.Repositories;

public sealed record ProductExpansion : Expansion<ProductExpansion>
{
    public BrandExpansion? Brand { get; init; }
}