namespace Rest.Application.Entities;

public sealed class Company : Entity<Company>
{
    public required string Name { get; set; }
}
