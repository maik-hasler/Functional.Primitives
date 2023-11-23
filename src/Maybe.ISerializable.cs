using System.Runtime.Serialization;

namespace Functional.Primitives.Maybe
{
    [Serializable]
    public readonly partial struct Maybe<T>
        : ISerializable
    {
        public Maybe(
            SerializationInfo info,
            StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
#if NET6_0_OR_GREATER
            _value = (T)info.GetValue(nameof(_value), typeof(T))!;
#else
            _value = (T)info.GetValue(nameof(_value), typeof(T));
#endif
            _hasValueFlag = 1;
        }

        public void GetObjectData(
            SerializationInfo info,
            StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            info.AddValue(nameof(_value), _value, typeof(T));
        }
    }
}
