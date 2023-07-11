namespace Rest.Application.Entities;

public abstract class Entity<TDerived>
    where TDerived : Entity<TDerived>
{
    public Id<TDerived> Id { get; set; }
}