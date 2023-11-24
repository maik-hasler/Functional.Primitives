using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Functional.Primitives.Maybe
{
    public readonly partial struct Maybe<T>
        : IComparable,
        IComparable<T>,
        IComparable<Maybe<T>>
    {
        /// <summary>
        /// Compares the current <see cref="Maybe{T}"/> instance to the specified object.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared.
        /// </returns>
        [Pure]
        public int CompareTo(
#if NET6_0_OR_GREATER
            [AllowNull]
# endif
            object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (obj is Maybe<T> other)
                return CompareTo(other);

            throw new ArgumentException($"Object must be of type {GetType()}.", nameof(obj));
        }

        /// <summary>
        /// Compares the current <see cref="Maybe{T}"/> instance to another <see cref="Maybe{T}"/> instance.
        /// </summary>
        /// <param name="other">The <see cref="Maybe{T}"/> instance to compare with the current instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the instances being compared.
        /// </returns>
        [Pure]
        public int CompareTo(
            Maybe<T> other)
        {
            if (_hasValueFlag == 1 && other._hasValueFlag == 0)
                return 1;

            if (_hasValueFlag == 0 && other._hasValueFlag == 1)
                return 0;

            return Comparer<T>.Default.Compare(_value, other._value);
        }

        /// <summary>
        /// Compares the current <see cref="Maybe{T}"/> instance to a specified value of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="other">The value to compare with the current instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the instance and the specified value.
        /// </returns>
        [Pure]
        public int CompareTo(
#if NET6_0_OR_GREATER
            [AllowNull]
# endif
            T other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (_hasValueFlag == 1)
            {
                return Comparer<T>.Default.Compare(_value, other);
            }

            if (typeof(T).BaseType == typeof(ValueType))
            {
                return -1;
            }

            return Comparer<T>.Default.Compare(_value, other);
        }
    }
}
