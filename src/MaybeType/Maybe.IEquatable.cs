using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace LambdaEcho.MaybeType
{
    public readonly partial struct Maybe<T> : IEquatable<Maybe<T>>
    {
        /// <summary>
        ///     Indicates whether this <see cref="Maybe{T}" /> is equal to the <paramref name="otherMaybe" />.
        /// </summary>
        /// <returns>True if both are equal; otherwise, False.</returns>
        [Pure]
        public bool Equals(Maybe<T> otherMaybe)
        {
            return HasValue == otherMaybe.HasValue && EqualityComparer<T>.Default.Equals(_value, otherMaybe._value);
        }

        /// <summary>
        ///     Indicates whether this <see cref="Maybe{T}" /> is equal to the <paramref name="obj" />.
        /// </summary>
        /// <returns>True if both are equal; otherwise, False.</returns>
        [Pure]
        public override bool Equals(object obj)
        {
            if (obj is Maybe<T> otherMaybe)
            {
                return Equals(otherMaybe);
            }

            return false;
        }

        /// <summary>
        ///     Returns the hash code of this <see cref="Maybe{T}" />.
        /// </summary>
        [Pure]
        public override int GetHashCode()
        {
            // When implementing IEquatable<T>, it is important to override GetHashCode().
            // This way, the type can be used as a key in an IDictionary<K,V>, or HashSet<T>, etc.,
            // because GetHashCode() is used to group instances of the type into buckets
            // (unless a custom IEqualityComparer<T> is available).

            unchecked
            {
                var hash = 27;
                hash = 13 * hash + HasValue.GetHashCode();
                hash = 13 * hash + EqualityComparer<T>.Default.GetHashCode(_value);
                return hash;
            }
        }

        /// <summary>
        ///     Indicates whether the <paramref name="leftMaybe" /> and the <paramref name="rightMaybe" /> are equal.
        /// </summary>
        /// <returns>True if both are equal; otherwise, False.</returns>
        public static bool operator ==(Maybe<T> leftMaybe, Maybe<T> rightMaybe)
        {
            return leftMaybe.Equals(rightMaybe);
        }

        /// <summary>
        ///     Indicates whether the <paramref name="leftMaybe" /> and the <paramref name="rightMaybe" /> are equal.
        /// </summary>
        /// <returns>True if both are equal; otherwise, False.</returns>
        public static bool operator !=(Maybe<T> leftMaybe, Maybe<T> rightMaybe)
        {
            return !leftMaybe.Equals(rightMaybe);
        }
    }
}