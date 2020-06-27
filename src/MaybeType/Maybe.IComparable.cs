using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace LambdaEcho.MaybeType
{
    public readonly partial struct Maybe<T> : IComparable, IComparable<T>, IComparable<Maybe<T>>
    {
        /// <summary>
        ///     Compares this <see cref="Maybe{T}" /> with another <see cref="object" /> that is of the same type.
        /// </summary>
        /// <returns>
        ///     A value that indicates the relative order of the objects being compared. The return value has these meanings:
        ///     Less than zero, if this <see cref="Maybe{T}" /> precedes <paramref name="obj" /> in the sort order.
        ///     Zero, if this <see cref="Maybe{T}" /> occurs in the same position in the sort order as <paramref name="obj" />.
        ///     Greater than zero, if this <see cref="Maybe{T}" /> follows <paramref name="obj" /> in the sort order.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     if the given <paramref name="obj" /> does not have the same type as this <see cref="Maybe{T}" />.
        /// </exception>
        [Pure]
        public int CompareTo(object obj)
        {
            if (obj is Maybe<T> other)
            {
                return CompareTo(other);
            }

            throw new ArgumentException($"Object must be of type {GetType()}.", nameof(obj));
        }

        /// <summary>
        ///     Compares the this <see cref="Maybe{T}" /> with another <see cref="Maybe{T}" />.
        /// </summary>
        /// <returns>
        ///     A value that indicates the relative order of the objects being compared. The return value has these meanings:
        ///     Less than zero, if this <see cref="Maybe{T}" /> precedes <paramref name="other" /> in the sort order.
        ///     Zero, if this <see cref="Maybe{T}" /> occurs in the same position in the sort order as <paramref name="other" />.
        ///     Greater than zero, if this <see cref="Maybe{T}" /> follows <paramref name="other" /> in the sort order.
        /// </returns>
        [Pure]
        public int CompareTo(Maybe<T> other)
        {
            if (HasValue && !other.HasValue)
            {
                return 1;
            }

            if (!HasValue && other.HasValue)
            {
                return -1;
            }

            return Comparer<T>.Default.Compare(_value, other._value);
        }

        /// <summary>
        ///     Compares this <see cref="Maybe{T}" /> with another value of the same underlying type <typeparamref name="T" />.
        /// </summary>
        /// <returns>
        ///     A value that indicates the relative order of the objects being compared. The return value has these meanings:
        ///     Less than zero, if this <see cref="Maybe{T}" /> precedes <paramref name="other" /> in the sort order.
        ///     Zero, if this <see cref="Maybe{T}" /> occurs in the same position in the sort order as <paramref name="other" />.
        ///     Greater than zero, if this <see cref="Maybe{T}" /> follows <paramref name="other" /> in the sort order.
        /// </returns>
        [Pure]
        public int CompareTo(T other)
        {
            // If this Maybe<T> is a Something, then compare the encapsulated value with the other one.
            if (HasValue)
            {
                return Comparer<T>.Default.Compare(_value, other);
            }

            // If this Maybe<T> is a Nothing, then the underlying type must be distinguished.
            // A Nothing<ValueType> always precedes the other one.
            if (typeof(T).BaseType == typeof(ValueType))
            {
                return -1;
            }

            // Hand the comparison of a Nothing<Object> on to the default Comparer<T>.
            return Comparer<T>.Default.Compare(_value, other);
        }

        /// <summary>
        ///     Indicates whether the <paramref name="leftMaybe" /> is greater than the <paramref name="rightMaybe" />.
        /// </summary>
        /// <returns>
        ///     True if the <paramref name="leftMaybe" /> is greater than the <paramref name="rightMaybe" />; otherwise, False.
        /// </returns>
        public static bool operator >(Maybe<T> leftMaybe, Maybe<T> rightMaybe)
        {
            return leftMaybe.CompareTo(rightMaybe) > 0;
        }

        /// <summary>
        ///     Indicates whether the <paramref name="leftMaybe" /> is greater than or equal the <paramref name="rightMaybe" />.
        /// </summary>
        /// <returns>
        ///     True if the <paramref name="leftMaybe" /> is greater than or equal the <paramref name="rightMaybe" />;
        ///     otherwise, False.
        /// </returns>
        public static bool operator >=(Maybe<T> leftMaybe, Maybe<T> rightMaybe)
        {
            return leftMaybe.CompareTo(rightMaybe) >= 0;
        }

        /// <summary>
        ///     Indicates whether the <paramref name="leftMaybe" /> is less than the <paramref name="rightMaybe" />.
        /// </summary>
        /// <returns>True if the <paramref name="leftMaybe" /> is less than the <paramref name="rightMaybe" />; otherwise, False.</returns>
        public static bool operator <(Maybe<T> leftMaybe, Maybe<T> rightMaybe)
        {
            return leftMaybe.CompareTo(rightMaybe) < 0;
        }

        /// <summary>
        ///     Indicates whether the <paramref name="leftMaybe" /> is less than or equal the <paramref name="rightMaybe" />.
        /// </summary>
        /// <returns>
        ///     True if the <paramref name="leftMaybe" /> is less than or equal the <paramref name="rightMaybe" />; otherwise,
        ///     False.
        /// </returns>
        public static bool operator <=(Maybe<T> leftMaybe, Maybe<T> rightMaybe)
        {
            return leftMaybe.CompareTo(rightMaybe) <= 0;
        }
    }
}