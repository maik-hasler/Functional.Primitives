using System.Diagnostics.Contracts;

namespace Functional.Primitives.Maybe;

public readonly partial struct Maybe<T>
    : IEquatable<T>,
    IEquatable<Maybe<T>>
{
    /// <inheritdoc />
    [Pure]
    public bool Equals(
        T? other)
    {
        return EqualityComparer<T>.Default.Equals(_value, other);
    }

    /// <inheritdoc />
    [Pure]
    public bool Equals(
        Maybe<T> other)
    {
        return _hasValueFlag == other._hasValueFlag
            && EqualityComparer<T>.Default.Equals(_value, other._value);
    }

    /// <inheritdoc />
    [Pure]
    public override bool Equals(
        object? obj)
    {
        return obj switch
        {
            null => false,
            T value => Equals(value),
            Maybe<T> maybe => Equals(maybe),
            _ => false
        };
    }

    /// <inheritdoc />
    [Pure]
    public override int GetHashCode()
    {
        return (_hasValueFlag & 1) == 1
            ? _value!.GetHashCode()
            : 0;
    }

    [Pure]
    public static bool operator ==(
        Maybe<T> left,
        Maybe<T> right)
    {
        return left.Equals(right);
    }

    [Pure]
    public static bool operator !=(
        Maybe<T> left,
        Maybe<T> right)
    {
        return !left.Equals(right);
    }

    [Pure]
    public static bool operator ==(
        T? value,
        Maybe<T> maybe)
    {
        return maybe.Equals(value);
    }

    [Pure]
    public static bool operator !=(
        T? value,
        Maybe<T> maybe)
    {
        return !maybe.Equals(value);
    }

    [Pure]
    public static bool operator ==(
        Maybe<T> maybe,
        T? value)
    {
        return maybe.Equals(value);
    }

    [Pure]
    public static bool operator !=(
        Maybe<T> maybe,
        T? value)
    {
        return !maybe.Equals(value);
    }
}
