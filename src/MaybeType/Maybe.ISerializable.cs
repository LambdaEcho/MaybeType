using System;
using System.Runtime.Serialization;

namespace LambdaEcho.MaybeType
{
    [Serializable]
    public readonly partial struct Maybe<T> : ISerializable
    {
        /// <summary>
        ///     Instantiates a new <see cref="Maybe{T}" /> from an serialized stream.
        /// </summary>
        public Maybe(SerializationInfo info, StreamingContext context)
        {
            HasValue = info.GetBoolean("h");
            _value = (T) info.GetValue("m", typeof(T));
        }

        /// <summary>
        ///     Serializes this <see cref="Maybe{T}" /> to a stream.
        /// </summary>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("h", HasValue, typeof(bool));
            info.AddValue("m", _value, typeof(T));
        }
    }
}