namespace Rest.Application.Entities;

public sealed class Product : Entity<Product>
{
    public required string Name { get; set; }
    public required EntityOrId<Company> Company { get; set; }
    public required decimal Price { get; set; }
}
