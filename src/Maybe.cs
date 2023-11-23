using System.Diagnostics.CodeAnalysis;

namespace Functional.Primitives.Maybe;

public readonly partial struct Maybe<T>
    where T : class
{
    private readonly T _value;

    private readonly byte _hasValueFlag;

    public Maybe(
        T value)
    {
        _value = value;
        _hasValueFlag = 1;
    }

    public static Maybe<T> From(
        [AllowNull] T value) => 
            value is not null
                ? new Maybe<T>(value)
                : default;

    public static Maybe<T> From<U>(
        [AllowNull] U value) => 
            value is not null
                ? new Maybe<T>((T)(object)value)
                : default;

    public static explicit operator T(
        in Maybe<T> maybe) => 
            (maybe._hasValueFlag & 1) == 1
                ? maybe._value
                : throw new InvalidOperationException(
                    $"Cannot perform explicit cast on {nameof(Maybe<T>)} without a value to type {nameof(T)}.");

    public static implicit operator Maybe<T>(
        [AllowNull] T value) =>
            Maybe<T>.From(value);
}
