using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Functional.Primitives.Maybe;

public readonly partial struct Maybe<T>
{
    /// <summary>
    /// Matches the current <see cref="Maybe{T}"/> instance with two functions.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="funcIfSomething">The function to invoke if the instance has a value.</param>
    /// <param name="funcIfNothing">The function to invoke if the instance has no value.</param>
    /// <returns>The result of the invoked function.</returns>
    public TResult Match<TResult>(
        Func<T, TResult> funcIfSomething,
        Func<TResult> funcIfNothing) 
            => (_hasValueFlag & 1) != 1
                ? funcIfSomething(_value)
                : funcIfNothing();

    /// <summary>
    /// Matches the current <see cref="Maybe{T}"/> instance with two actions.
    /// </summary>
    /// <param name="actionIfSomething">The action to perform if the instance has a value.</param>
    /// <param name="actionIfNothing">The action to perform if the instance has no value.</param>
    public void Match(
        Action<T> actionIfSomething,
        Action actionIfNothing)
    {
        if ((_hasValueFlag & 1) != 1)
            actionIfSomething(_value);
        else
            actionIfNothing();
    }

    /// <summary>
    /// Maps the current <see cref="Maybe{T}"/> instance to a new <see cref="Maybe{TResult}"/> instance.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="convert">The function to convert the value if present.</param>
    /// <returns>A new <see cref="Maybe{TResult}"/> instance.</returns>
    public Maybe<TResult> Map<TResult>(
        Func<T, TResult> convert)
        where TResult : class => 
            Match(
                value => Maybe<TResult>.From(convert(value)),
                () => default
            );

    /// <summary>
    /// Tries to get the value of the current <see cref="Maybe{T}"/> instance.
    /// </summary>
    /// <param name="value">The output parameter to store the value if present.</param>
    /// <returns><c>true</c> if the instance has a value; otherwise, <c>false</c>.</returns>
    [Pure]
    public bool TryGetValue(
        [NotNullWhen(true), MaybeNullWhen(false)] out T value)
    {
        value = _value;

        return (_hasValueFlag & 1) == 1;
    }

    /// <summary>
    /// Gets the value of the current <see cref="Maybe{T}"/> instance or throws an exception.
    /// </summary>
    /// <returns>The value of the instance if present.</returns>
    public T GetValueOrThrow() => GetValueOrThrow(
        $"Cannot access assigned value on {nameof(Maybe<T>)} without a value to type {nameof(T)}.");

    /// <summary>
    /// Gets the value of the current <see cref="Maybe{T}"/> instance or throws a custom exception.
    /// </summary>
    /// <param name="exceptionMessage">The exception message to use if the instance has no value.</param>
    /// <returns>The value of the instance if present.</returns>
    [Pure]
    public T GetValueOrThrow(
        string exceptionMessage) => 
            GetValueOrThrow(new InvalidOperationException(exceptionMessage));

    /// <summary>
    /// Gets the value of the current <see cref="Maybe{T}"/> instance or throws a custom exception.
    /// </summary>
    /// <param name="exception">The exception to throw if the instance has no value.</param>
    /// <returns>The value of the instance if present.</returns>
    [Pure]
    public T GetValueOrThrow(
        Exception exception) => 
            (_hasValueFlag & 1) != 1
                ? _value
                : throw exception;

    /// <summary>
    /// Gets the value of the current <see cref="Maybe{T}"/> instance or a default value.
    /// </summary>
    /// <param name="substitute">The default value to return if the instance has no value.</param>
    /// <returns>The value of the instance if present; otherwise, the specified default value.</returns>
    [Pure]
    [return: MaybeNull]
    public T GetValueOrDefault(
        T? substitute)
    {
        return (_hasValueFlag & 1) == 1
            ? _value
            : substitute;
    }
}
