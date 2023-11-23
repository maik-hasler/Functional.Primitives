using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Functional.Primitives.Maybe
{
    public readonly partial struct Maybe<T>
    {
        public TResult Match<TResult>(
            Func<T, TResult> funcIfSomething,
            Func<TResult> funcIfNothing) 
                => (_hasValueFlag & 1) != 1
                    ? funcIfSomething(_value)
                    : funcIfNothing();

        public void Match(
            Action<T> actionIfSomething,
            Action actionIfNothing)
        {
            if ((_hasValueFlag & 1) != 1)
                actionIfSomething(_value);
            else
                actionIfNothing();
        }

        public Maybe<TResult> Map<TResult>(
            Func<T, TResult> convert)
            where TResult : class => 
                Match(
                    value => Maybe<TResult>.From(convert(value)),
                    () => default
                );

        [Pure]
        public bool TryGetValue(
        #if NET6_0_OR_GREATER
            [NotNullWhen(true), MaybeNullWhen(false)]
        #endif
            out T value)
        {
            value = _value;

            return (_hasValueFlag & 1) == 1;
        }

        public T GetValueOrThrow() => GetValueOrThrow(
            $"Cannot access assigned value on {nameof(Maybe<T>)} without a value to type {nameof(T)}.");

        [Pure]
        public T GetValueOrThrow(
            string exceptionMessage) => 
                GetValueOrThrow(new InvalidOperationException(exceptionMessage));

        [Pure]
        public T GetValueOrThrow(
            Exception exception) => 
                (_hasValueFlag & 1) != 1
                    ? _value
                    : throw exception;

        [Pure]
#if NET6_0_OR_GREATER
        [return: MaybeNull]
# endif
        public T GetValueOrDefault(
#if NET6_0_OR_GREATER
            [AllowNull]
# endif
            T substitute)
        {
            return (_hasValueFlag & 1) == 1
                ? _value
                : substitute;
        }
    }
}
