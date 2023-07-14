namespace Rest.Application.Entities;

public abstract class Entity<TDerived>: ICloneable
    where TDerived : Entity<TDerived>
{
    public Id<TDerived> Id { get; set; }

    object ICloneable.Clone() => Clone();
    public abstract TDerived Clone();
}