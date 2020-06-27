using System;
using System.Collections.Generic;

namespace LambdaEcho.MaybeType.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>Gets the value associated with the specified <paramref name="key" />.</summary>
        /// <param name="dictionary">The <see cref="IDictionary{TKey,TValue}" />.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <returns>
        ///     A new <see cref="Maybe{T}" /> that contains the corresponding value if the key was found; otherwise, a new
        ///     Nothing.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">if <paramref name="key" /> is null.</exception>
        /// <exception cref="ArgumentNullException">if this method is invoked on a NULL dictionary.</exception>
        public static Maybe<TValue> TryGetValueMaybe<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            // We don't check 'key' for NULL to get the same exception behavior as the .NET Framework itself.
            return dictionary.TryGetValue(key, out var value) ? new Maybe<TValue>(value) : default;
        }

        /// <summary>Gets the value associated with the specified <paramref name="key" />.</summary>
        /// <param name="dictionary">The <see cref="IDictionary{TKey,TValue}" />.</param>
        /// <param name="key">The key whose value to get.</param>
        /// <returns>The corresponding <see cref="Maybe{T}" /> if the key was found; otherwise a new Nothing.</returns>
        /// <exception cref="T:System.ArgumentNullException">if <paramref name="key" /> is null.</exception>
        /// <exception cref="ArgumentNullException">if this method is invoked on a NULL dictionary.</exception>
        public static Maybe<TValue> TryGetValueMaybe<TKey, TValue>(this IDictionary<TKey, Maybe<TValue>> dictionary, TKey key)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            // We don't check 'key' for NULL to get the same exception behavior as the .NET Framework itself.
            return dictionary.TryGetValue(key, out var value) ? value : default;
        }
    }
}