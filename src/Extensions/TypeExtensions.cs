namespace Functional.Primitives.Maybe.Extensions;

public static class TypeExtensions
{
    internal static bool IsOfTypeMaybeOrNullable(
        this Type type)
    {
        if (!type.IsGenericType)
        {
            return false;
        }

        var genericTypeDefinition = type.GetGenericTypeDefinition();

        return genericTypeDefinition == typeof(Maybe<>)
            || genericTypeDefinition == typeof(Nullable<>);
    }
}
