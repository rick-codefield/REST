using Rest.Application.Entities;
using Rest.Application.Repositories;

namespace Rest.Infrastructure;

public static class Reflection
{
    public static IEnumerable<Type> GetEntities()
    {
        return typeof(Entity<>).Assembly
            .GetExportedTypes()
            .Where(t =>
                t.BaseType != null &&
                t.BaseType.IsGenericType &&
                t.BaseType.GetGenericTypeDefinition() == typeof(Entity<>));
    }

    public static IEnumerable<Type> GetIds()
    {
        return GetEntities()
            .Select(e => typeof(Id<>).MakeGenericType(new[] { e }));
    }

    public static IEnumerable<Type> GetExpansions()
    {
        return typeof(Expansion<>).Assembly
            .GetExportedTypes()
            .Where(t =>
                t.BaseType != null &&
                t.BaseType.IsGenericType &&
                t.BaseType.GetGenericTypeDefinition() == typeof(Expansion<>));
    }
}
