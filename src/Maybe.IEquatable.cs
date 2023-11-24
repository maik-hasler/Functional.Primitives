using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Functional.Primitives.Maybe
{
    public readonly partial struct Maybe<T>
        : IEquatable<Maybe<T>>
    {
        /// <summary>
        /// Determines whether the current <see cref="Maybe{T}"/> instance is equal to another <see cref="Maybe{T}"/> instance.
        /// </summary>
        /// <param name="other">The <see cref="Maybe{T}"/> instance to compare with the current instance.</param>
        /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
        [Pure]
        public bool Equals(
            Maybe<T> other) =>
                _hasValueFlag == other._hasValueFlag 
                && EqualityComparer<T>.Default.Equals(_value, other._value);

        /// <summary>
        /// Determines whether the current <see cref="Maybe{T}"/> instance is equal to the specified object.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
        [Pure]
        public override bool Equals(
#if NET6_0_OR_GREATER
            [AllowNull]
# endif
            object obj) => 
                obj != null 
                && obj is Maybe<T> other 
                && Equals(other);

        /// <summary>
        /// Gets the hash code for the current <see cref="Maybe{T}"/> instance.
        /// </summary>
        /// <returns>The hash code of the instance.</returns>
        [Pure]
        public override int GetHashCode() =>
            (_hasValueFlag & 1) == 1 
                ? _value.GetHashCode() 
                : 0;

        /// <summary>
        /// Determines whether two <see cref="Maybe{T}"/> instances are equal.
        /// </summary>
        /// <param name="left">The first <see cref="Maybe{T}"/> instance to compare.</param>
        /// <param name="right">The second <see cref="Maybe{T}"/> instance to compare.</param>
        /// <returns><c>true</c> if the instances are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(
            Maybe<T> left,
            Maybe<T> right) =>
                left.Equals(right);

        /// <summary>
        /// Determines whether two <see cref="Maybe{T}"/> instances are not equal.
        /// </summary>
        /// <param name="left">The first <see cref="Maybe{T}"/> instance to compare.</param>
        /// <param name="right">The second <see cref="Maybe{T}"/> instance to compare.</param>
        /// <returns><c>true</c> if the instances are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(
            Maybe<T> left,
            Maybe<T> right) =>
                !left.Equals(right);
    }
}
