namespace Rest.Application.Entities;

public sealed class Product : Entity<Product>
{
    public required string Name { get; set; }
    public required EntityOrId<Brand> Brand { get; set; }
    public required decimal Price { get; set; }

    public override Product Clone()
    {
        return new Product
        {
            Id = Id,
            Name = Name,
            Brand = Brand,
            Price = Price
        };
    }
}
