using System;
using System.Runtime.Serialization;

namespace Functional.Primitives.Maybe
{
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
            if (info == null)
                throw new ArgumentNullException(nameof(info));
#if NET6_0_OR_GREATER
            _value = (T)info.GetValue(nameof(_value), typeof(T))!;
#else
            _value = (T)info.GetValue(nameof(_value), typeof(T));
#endif
            _hasValueFlag = 1;
        }

        /// <summary>
        /// Populates a <see cref="SerializationInfo"/> with the data needed to serialize the current <see cref="Maybe{T}"/> instance.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> to populate with data.</param>
        /// <param name="context">The <see cref="StreamingContext"/> representing the streaming context.</param>
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
