using System.Diagnostics.Contracts;

namespace Functional.Primitives.Maybe;

public readonly partial struct Maybe<T>
    : IEquatable<Maybe<T>>
{
    [Pure]
    public bool Equals(
        Maybe<T> other) =>
            _hasValueFlag == other._hasValueFlag 
            && EqualityComparer<T>.Default.Equals(_value, other._value);

    [Pure]
    public override bool Equals(
        object? obj) => 
            obj != null 
            && obj is Maybe<T> other 
            && Equals(other);

    [Pure]
    public override int GetHashCode() =>
        HashCode.Combine(_value, _hasValueFlag);

    public static bool operator ==(
        Maybe<T> left,
        Maybe<T> right) =>
            left.Equals(right);

    public static bool operator !=(
        Maybe<T> left,
        Maybe<T> right) =>
            !left.Equals(right);
}
