using System;
using System.Collections.Generic;

namespace LambdaEcho.MaybeType.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        ///     Invokes an <see cref="Action" /> on each item of this <see cref="IEnumerable{T}" /> depending on whether an
        ///     enumerated <see cref="Maybe{T}" /> is a Something (i.e., the encapsulated value is set) or a Nothing (i.e., the
        ///     encapsulated value is not set).
        /// </summary>
        /// <param name="maybes">An <see cref="IEnumerable{T}" /> containing instances of <see cref="Maybe{T}" />.</param>
        /// <param name="actionIfSomething">
        ///     The <see cref="Action{T}" /> to be invoked if an enumerated <see cref="Maybe{T}" /> is a Something (i.e., the
        ///     encapsulated value is set).
        /// </param>
        /// <param name="actionIfNothing">
        ///     The <see cref="Action" /> to be invoked if an enumerated <see cref="Maybe{T}" /> is a Nothing (i.e., the
        ///     encapsulated value is not set).
        /// </param>
        public static void InvokeEach<T>(this IEnumerable<Maybe<T>> maybes, Action<T> actionIfSomething = null,
            Action actionIfNothing = null)
        {
            if (maybes == null)
            {
                throw new ArgumentNullException(nameof(maybes));
            }

            foreach (var maybe in maybes)
            {
                maybe.Invoke(actionIfSomething, actionIfNothing);
            }
        }

        /// <summary>
        ///     Maps each <see cref="Maybe{T}" /> of this <see cref="IEnumerable{T}" /> into a <see cref="Maybe{TResult}" />.
        /// </summary>
        /// <typeparam name="TResult">The type of the encapsulated return value.</typeparam>
        /// <param name="maybes">An <see cref="IEnumerable{T}" /> containing instances of <see cref="Maybe{T}" />.</param>
        /// <param name="funcIfSomething">
        ///     An optional <see cref="Func{T, TResult}" /> to be invoked if an enumerated <see cref="Maybe{T}" /> is a Something
        ///     (i.e., the encapsulated value is set).
        /// </param>
        /// <param name="funcIfNothing">
        ///     An optional <see cref="Func{TResult}" /> to be invoked if an enumerated <see cref="Maybe{T}" /> is a Nothing (i.e.,
        ///     the encapsulated value is not set).
        /// </param>
        /// <returns>
        ///     A new <see cref="IEnumerable{TResult}" /> containing all mapped <see cref="Maybe{TResult}" />.
        /// </returns>
        public static IEnumerable<Maybe<TResult>> MapEach<T, TResult>(this IEnumerable<Maybe<T>> maybes,
            Func<T, TResult> funcIfSomething = null, Func<TResult> funcIfNothing = null)
        {
            if (maybes == null)
            {
                throw new ArgumentNullException(nameof(maybes));
            }

            foreach (var maybe in maybes)
            {
                yield return maybe.Map(funcIfSomething, funcIfNothing);
            }
        }

        /// <summary>
        ///     Maps each <see cref="Maybe{T}" /> of this <see cref="IEnumerable{T}" /> into a <see cref="Maybe{TResult}" />.
        /// </summary>
        /// <typeparam name="TResult">The type of the encapsulated return value.</typeparam>
        /// <param name="maybes">An <see cref="IEnumerable{T}" /> containing instances of <see cref="Maybe{T}" />.</param>
        /// <param name="funcIfSomething">
        ///     An optional <see cref="Func{T, TResult}" /> to be invoked if an enumerated <see cref="Maybe{T}" /> is a Something
        ///     (i.e., the encapsulated value is set).
        /// </param>
        /// <param name="funcIfNothing">
        ///     An optional <see cref="Func{TResult}" /> to be invoked if an enumerated <see cref="Maybe{T}" /> is a Nothing (i.e.,
        ///     the encapsulated value is not set).
        /// </param>
        /// <returns>
        ///     A new <see cref="IEnumerable{TResult}" /> containing all mapped <see cref="Maybe{TResult}" />.
        /// </returns>
        public static IEnumerable<Maybe<TResult>> MapEach<T, TResult>(this IEnumerable<Maybe<T>> maybes,
            Func<T, Maybe<TResult>> funcIfSomething = null, Func<Maybe<TResult>> funcIfNothing = null)
        {
            if (maybes == null)
            {
                throw new ArgumentNullException(nameof(maybes));
            }

            foreach (var maybe in maybes)
            {
                yield return maybe.Map(funcIfSomething, funcIfNothing);
            }
        }
    }
}