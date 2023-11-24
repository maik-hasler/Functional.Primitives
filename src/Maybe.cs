using System;
using System.Diagnostics.CodeAnalysis;

namespace Functional.Primitives.Maybe
{
    /// <summary>
    /// Represents a value of type <typeparamref name="T"/> that may or may not be present.
    /// </summary>
    /// <typeparam name="T">The type of the optional value.</typeparam>
    public readonly partial struct Maybe<T>
        where T : class
    {
        private readonly T _value;

        private readonly byte _hasValueFlag;

        /// <summary>
        /// Initializes a new instance of the <see cref="Maybe{T}"/> struct.
        /// </summary>
        /// <param name="value">The optional value to store.</param>
        public Maybe(
#if NET6_0_OR_GREATER
            [AllowNull]
#endif
            T value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
            _hasValueFlag = 1;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Maybe{T}"/> struct.
        /// </summary>
        /// <param name="value">The optional value to store.</param>
        /// <returns>A <see cref="Maybe{T}"/> struct containing the value, or default if the value is null</returns>
        public static Maybe<T> From(
#if NET6_0_OR_GREATER
            [AllowNull]
# endif
        T value) =>
                value != null
                    ? new Maybe<T>(value)
                    : default;

        /// <summary>
        /// Creates a new instance of the <see cref="Maybe{T}"/> struct.
        /// </summary>
        /// <typeparam name="U">The explicit nullable type.</typeparam>
        /// <param name="value">The optional value of type <typeparamref name="U"/>.</param>
        /// <returns>
        /// A new instance of the <see cref="Maybe{T}"/> struct containing the
        /// value if the input value is not null; otherwise, an default instance.
        /// </returns>
        public static Maybe<T> From<U>(
#if NET6_0_OR_GREATER
            [AllowNull]
# endif
            U value) => 
                value != null
                    ? new Maybe<T>((T)(object)value)
                    : default;

        /// <summary>
        /// Performs an explicit cast from <see cref="Maybe{T}"/> to the underlying type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="maybe">The <see cref="Maybe{T}"/> instance to cast.</param>
        /// <returns>The value if the instance has a value; otherwise, throws an <see cref="InvalidOperationException"/>.</returns>
        public static explicit operator T(
            in Maybe<T> maybe) => 
                (maybe._hasValueFlag & 1) == 1
                    ? maybe._value
                    : throw new InvalidOperationException(
                        $"Cannot perform explicit cast on {nameof(Maybe<T>)} without a value to type {nameof(T)}.");

        /// <summary>
        /// Performs an implicit cast from the underlying type <typeparamref name="T"/> to <see cref="Maybe{T}"/>.
        /// </summary>
        /// <param name="value">The value to wrap in a <see cref="Maybe{T}"/> instance.</param>
        /// <returns>A <see cref="Maybe{T}"/> instance containing the specified value.</returns>
        public static implicit operator Maybe<T>(
#if NET6_0_OR_GREATER
            [AllowNull]
# endif
            T value) =>
                Maybe<T>.From(value);
    }
}
