namespace LambdaEcho.MaybeType.Extensions
{
    public static class GenericTypeExtensions
    {
        /// <summary>
        ///     Returns a new <see cref="Maybe{T}" /> that contains this <paramref name="value" /> of type
        ///     <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">The type of this <paramref name="value" />.</typeparam>
        public static Maybe<T> ToMaybe<T>(this T value)
        {
            return new Maybe<T>(value);
        }

        /// <remarks>
        ///     The implementation of <see cref="Maybe{T}" /> restricts the set of allowed underlying types
        ///     <typeparamref name="T" />.
        ///     That is, a <see cref="Maybe{T}" /> is not allowed to be the underlying type of another <see cref="Maybe{T}" />.
        ///     The purpose of this method is to ensure that an invocation of <see cref="ToMaybe{T}(T)" /> on an existing
        ///     <see cref="Maybe{T}" /> does not violate this invariant. In other words, this method represents a "where not"
        ///     constraint for <see cref="ToMaybe{T}(T)" />.
        /// </remarks>
        public static Maybe<T> ToMaybe<T>(this Maybe<T> maybe)
        {
            return maybe;
        }
    }
}