using System.Runtime.Serialization;

namespace Functional.Primitives.Maybe;

[Serializable]
public readonly partial struct Maybe<T>
    : ISerializable
{
    public Maybe(
        SerializationInfo info,
        StreamingContext context)
    {
        ArgumentNullException.ThrowIfNull(info);

        _value = (T)info.GetValue(nameof(_value), typeof(T))!;
    }

    public void GetObjectData(
        SerializationInfo info,
        StreamingContext context)
    {
        ArgumentNullException.ThrowIfNull(info);

        info.AddValue(nameof(_value), _value, typeof(T));
    }
}
