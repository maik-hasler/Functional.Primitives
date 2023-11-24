namespace Functional.Primitives.Maybe.Exceptions;

[Serializable]
public class InvalidUnderlyingTypeException 
    : Exception
{
    public Type UnderlyingType { get; private set; }

    public InvalidUnderlyingTypeException(
        Type underlyingType)
        : base($"Invalid underlying type: {underlyingType.Name}")
    {
        UnderlyingType = underlyingType;
    }
}
