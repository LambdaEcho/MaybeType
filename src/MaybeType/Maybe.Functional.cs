using System;
using System.Diagnostics.Contracts;

namespace LambdaEcho.MaybeType
{
    public readonly partial struct Maybe<T>
    {
        /// <summary>
        ///     Invokes an <see cref="Action" /> depending on whether this <see cref="Maybe{T}" /> is a Something (i.e., the
        ///     encapsulated value is set) or a Nothing (i.e., the encapsulated value is not set).
        /// </summary>
        /// <param name="actionIfSomething">
        ///     The <see cref="Action{T}" /> to be invoked if this <see cref="Maybe{T}" /> is a
        ///     Something (i.e., the encapsulated value is set).
        /// </param>
        /// <param name="actionIfNothing">
        ///     The <see cref="Action" /> to be invoked if this <see cref="Maybe{T}" /> is a Nothing
        ///     (i.e., the encapsulated value is not set).
        /// </param>
        public void Invoke(Action<T> actionIfSomething = null, Action actionIfNothing = null)
        {
            if (HasValue)
            {
                actionIfSomething?.Invoke(_value);
            }
            else
            {
                actionIfNothing?.Invoke();
            }
        }

        /// <summary>
        ///     Maps this <see cref="Maybe{T}" /> into a <see cref="Maybe{TResult}" />.
        /// </summary>
        /// <typeparam name="TResult">The type of the encapsulated return value.</typeparam>
        /// <param name="funcIfSomething">
        ///     An optional <see cref="Func{T, TResult}" /> to be invoked if this <see cref="Maybe{T}" />
        ///     is a Something (i.e., the encapsulated value is set).
        /// </param>
        /// <param name="funcIfNothing">
        ///     An optional <see cref="Func{TResult}" /> to be invoked if this <see cref="Maybe{T}" /> is a
        ///     Nothing (i.e., the encapsulated value is not set).
        /// </param>
        /// <returns>
        ///     A new <see cref="Maybe{TResult}" />, either a Something (i.e., the encapsulated value is set) or a Nothing
        ///     (i.e., the encapsulated value is not set).
        /// </returns>
        public Maybe<TResult> Map<TResult>(Func<T, TResult> funcIfSomething = null, Func<TResult> funcIfNothing = null)
        {
            return Map(
                funcIfSomething != null ? x => new Maybe<TResult>(funcIfSomething(x)) : (Func<T, Maybe<TResult>>) null,
                funcIfNothing != null ? () => new Maybe<TResult>(funcIfNothing()) : (Func<Maybe<TResult>>) null
            );
        }

        /// <summary>
        ///     Maps this <see cref="Maybe{T}" /> into a <see cref="Maybe{TResult}" />.
        /// </summary>
        /// <typeparam name="TResult">The type of the encapsulated return value.</typeparam>
        /// <param name="funcIfSomething">
        ///     An optional <see cref="Func{T, Maybe}" /> to be invoked if this <see cref="Maybe{T}" />
        ///     is a Something (i.e., the encapsulated value is set).
        /// </param>
        /// <param name="funcIfNothing">
        ///     An optional <see cref="Func{Maybe}" /> to be invoked if this <see cref="Maybe{T}" /> is a
        ///     Nothing (i.e., the encapsulated value is not set).
        /// </param>
        /// <returns>
        ///     A new <see cref="Maybe{TResult}" />, either a Something (i.e., the encapsulated value is set) or a Nothing
        ///     (i.e., the encapsulated value is not set).
        /// </returns>
        public Maybe<TResult> Map<TResult>(Func<T, Maybe<TResult>> funcIfSomething = null, Func<Maybe<TResult>> funcIfNothing = null)
        {
            if (HasValue)
            {
                if (funcIfSomething != null)
                {
                    return funcIfSomething(_value);
                }
            }
            else
            {
                if (funcIfNothing != null)
                {
                    return funcIfNothing();
                }
            }

            return default;
        }

        /// <summary>
        ///     Returns this <see cref="Maybe{T}" /> if it is a Something (i.e., the encapsulated value is set); otherwise, the
        ///     <paramref name="alternativeMaybe" />.
        /// </summary>
        /// <param name="alternativeMaybe">
        ///     The alternative <see cref="Maybe{T}" /> will be returned, if this
        ///     <see cref="Maybe{T}" /> is a Nothing (i.e., the encapsulated value is not set).
        /// </param>
        [Pure]
        public Maybe<T> Or(in Maybe<T> alternativeMaybe)
        {
            return HasValue ? this : alternativeMaybe;
        }

        /// <summary>
        ///     Returns this <see cref="Maybe{T}" /> if it is a Something (i.e., the encapsulated value is set); otherwise, the
        ///     return value of <paramref name="funcForAlternativeMaybe" />.
        /// </summary>
        /// <param name="funcForAlternativeMaybe">
        ///     The result of the Func will be returned, if this <see cref="Maybe{T}" /> is a
        ///     Nothing (i.e., the encapsulated value is not set).
        /// </param>
        public Maybe<T> Or(Func<Maybe<T>> funcForAlternativeMaybe)
        {
            return HasValue ? this : funcForAlternativeMaybe();
        }

        /// <summary>
        ///     Returns the <see cref="Value" /> of this <see cref="Maybe{T}" /> if it is a Something (i.e., the encapsulated value
        ///     is set); otherwise the <paramref name="alternativeValue" />.
        /// </summary>
        /// <param name="alternativeValue">
        ///     The alternative value will be returned, if this <see cref="Maybe{T}" /> is a Nothing (i.e., the encapsulated value
        ///     is not set).
        /// </param>
        [Pure]
        public T ValueOr(T alternativeValue)
        {
            return HasValue ? _value : alternativeValue;
        }

        /// <summary>
        ///     Returns the <see cref="Value" /> of this <see cref="Maybe{T}" /> if it is a Something (i.e., the encapsulated value
        ///     is set); otherwise the return value of <paramref name="funcForAlternativeValue" />.
        /// </summary>
        /// <param name="funcForAlternativeValue">
        ///     The result of the Func will be returned, if this <see cref="Maybe{T}" /> is a
        ///     Nothing (i.e., the encapsulated value is not set).
        /// </param>
        public T ValueOr(Func<T> funcForAlternativeValue)
        {
            return HasValue ? _value : funcForAlternativeValue();
        }

        /// <summary>
        ///     Returns the <see cref="Value" /> of this <see cref="Maybe{T}" /> if it is a Something (i.e., the encapsulated value
        ///     is set); otherwise, the default(<typeparamref name="T" />).
        /// </summary>
        [Pure]
        public T ValueOrDefault()
        {
            return ValueOr(default(T));
        }
    }
}