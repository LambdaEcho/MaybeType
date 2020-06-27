using System;

namespace LambdaEcho.MaybeType.Extensions
{
    public static class NullableExtensions
    {
        /// <summary>
        ///     Converts this <see cref="Nullable{T}" /> into a <see cref="Maybe{T}" />.
        /// </summary>
        /// <typeparam name="T">The underlying type of this <see cref="Nullable{T}" />.</typeparam>
        /// <returns>A new <see cref="Maybe{T}" /> containing the <see cref="Nullable{T}.Value" />.</returns>
        public static Maybe<T> ToMaybe<T>(this T? nullable) where T : struct
        {
            return nullable.HasValue ? new Maybe<T>(nullable.Value) : default;
        }
    }
}