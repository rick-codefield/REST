namespace Rest.Application.Entities;

public sealed class Company : Entity<Company>
{
    public required string Name { get; set; }

    public override Company Clone()
    {
        return new Company
        {
            Id = Id,
            Name = Name
        };
    }
}
