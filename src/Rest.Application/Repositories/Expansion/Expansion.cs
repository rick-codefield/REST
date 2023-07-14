using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Rest.Application.Repositories;

public abstract record Expansion<TDerived> : IParsable<TDerived>
    where TDerived : Expansion<TDerived>, new()
{
    static Expansion()
    {
        var properties = typeof(TDerived)
            .GetProperties()
            .Where(p => p.PropertyType.IsAssignableTo(typeof(Expansion<>).MakeGenericType(p.PropertyType)))
            .ToArray();

        _children = properties
            .ToDictionary(p => p.Name.ToLower(), p => (p, p.PropertyType.GetMethod(nameof(Parse), BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)!));
    }

    public static TDerived Parse(string s, IFormatProvider? provider)
    {
        if (!TryParse(s, provider, out var result))
        {
            throw new FormatException("Failed to parse expansion");
        }

        return result;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out TDerived result)
    {
        var childExpansions = new Dictionary<string, string>();

        if (s != null)
        {
            foreach (var expansion in s.Split(',').Select(e => e.Trim()))
            {
                var components = expansion.Split('.');
                if (!childExpansions.TryGetValue(components[0], out var children))
                {
                    children = string.Empty;
                    childExpansions.Add(components[0], children);
                }

                if (components.Length > 1)
                {
                    childExpansions[components[0]] += string.Join('.', components[1..]);
                }
            }
        }

        result = new TDerived();

        foreach (var (child, childExpansion) in childExpansions)
        {
            if (_children.TryGetValue(child, out var childInfo))
            {
                childInfo.Property.SetValue(result, childInfo.Parse.Invoke(null, new object?[] { childExpansion, null }));
            }
        }

        return true;
    }

    private static readonly IReadOnlyDictionary<string, (PropertyInfo Property, MethodInfo Parse)> _children;
}