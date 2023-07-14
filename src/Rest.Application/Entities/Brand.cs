namespace Rest.Application.Entities;

public sealed class Brand : Entity<Brand>
{
    public required string Name { get; set; }
    public required EntityOrId<Company> Company { get; set; }

    public override Brand Clone()
    {
        return new Brand
        {
            Id = Id,
            Name = Name,
            Company = Company.Clone()
        };
    }
}
