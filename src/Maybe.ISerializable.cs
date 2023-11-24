using System.Runtime.Serialization;

namespace Functional.Primitives.Maybe;

[Serializable]
public readonly partial struct Maybe<T>
    : ISerializable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Maybe{T}"/> struct
    /// with serialized data.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> containing the serialized data.</param>
    /// <param name="context">The <see cref="StreamingContext"/> representing the streaming context.</param>
    public Maybe(
        SerializationInfo info,
        StreamingContext context)
    {
        ArgumentNullException.ThrowIfNull(info);

        _value = (T)info.GetValue(nameof(_value), typeof(T))!;
        _hasValueFlag = 1;
    }

    /// <inheritdoc />
    public void GetObjectData(
        SerializationInfo info,
        StreamingContext context)
    {
        ArgumentNullException.ThrowIfNull(info);

        info.AddValue(nameof(_value), _value, typeof(T));
    }
}
