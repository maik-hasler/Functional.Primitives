using FluentAssertions;
using Functional.Primitives.Maybe.Exceptions;
using Xunit;

namespace Functional.Primitives.Maybe.Test;

public partial class MaybeTests
{
    [Fact]
    public void Constructor_ShouldThrowInvalidUnderlyingTypeException_WhenMaybeAsUnderlyingTypeGiven()
    {
        // Arrange
        var maybe = new Maybe<string>("Hello");

        // Act
        var act = () =>
        {
            var _ = new Maybe<Maybe<string>>(maybe);
        };

        // Assert
        act.Should().Throw<InvalidUnderlyingTypeException>();
    }

    [Fact]
    public void Constructor_ShouldCreateInstanceWithValue_WhenNonNullableReferenceTypeWithValueGiven()
    {
        // Arrange
        var value = "Hello Test!";

        // Act
        var maybe = new Maybe<string>(value);

        // Assert
        maybe.Should().NotBeNull();
        maybe.Should().BeOfType(typeof(Maybe<string>));
        var hasValue = maybe.TryGetValue(out string actualValue);
        hasValue.Should().BeTrue();
        actualValue.Should().Be(value);
    }

    [Fact]
    public void Constructor_ShouldCreateDefaultInstance_WhenNonNullableReferenceTypeWithNullGiven()
    {
        // Arrange
        string value = null!;
        Maybe<string> defaultMaybe = default;

        // Act
        var maybe = new Maybe<string>(value);

        // Assert
        maybe.Should().NotBeNull();
        maybe.Should().BeOfType(typeof(Maybe<string>));
        maybe.Should().Be(defaultMaybe);
        var hasValue = maybe.TryGetValue(out string _);
        hasValue.Should().BeFalse();
    }
}
