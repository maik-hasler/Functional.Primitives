using FluentAssertions;
using Xunit;

namespace Functional.Primitives.Maybe.Test;

public partial class MaybeTests
{
    [Fact]
    public void Equals_ShouldReturnTrue_WhenBothHaveSameReferenceTypeValue()
    {
        // Arrange
        string value = "correct";
        Maybe<string> left = new(value);
        string right = value;

        // Act
        var result = left.Equals(right);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenValuesAreDifferent()
    {
        // Arrange
        Maybe<string> left = new("correct");
        string right = "wrong";

        // Act
        var result = left.Equals(right);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Equals_ShouldReturnTrue_WhenBothHaveSameNullReferenceTypeValue()
    {
        // Arrange
        string value = null!;
        Maybe<string> left = new(value);
        string right = value;

        // Act
        var result = left.Equals(right);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenOneHasValueAndTheOtherIsNull()
    {
        // Arrange
        Maybe<string> left = new("correct");
        string right = null!;

        // Act
        var result = left.Equals(right);

        // Assert
        result.Should().BeFalse();
    }
}
