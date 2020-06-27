using System;

namespace LambdaEcho.MaybeType.Extensions
{
    public static class MaybeExtensions
    {
        /// <summary>
        ///     Converts this <see cref="Maybe{T}" /> into a <see cref="Nullable{T}" />.
        /// </summary>
        /// <typeparam name="T">The underlying type of this <see cref="Maybe{T}" />.</typeparam>
        /// <returns>A new <see cref="Nullable{T}" /> containing the <see cref="Maybe{T}.Value" />.</returns>
        public static T? ToNullable<T>(this Maybe<T> maybe) where T : struct
        {
            return maybe.HasValue ? maybe.Value : default(T?);
        }
    }
}