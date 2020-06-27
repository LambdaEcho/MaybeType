using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using LambdaEcho.MaybeType.Exceptions;
using LambdaEcho.MaybeType.Extensions;

namespace LambdaEcho.MaybeType
{
    /// <summary>
    ///     The <see cref="Maybe{T}" /> is an immutable monad type with enhancements for functional programming that
    ///     encapsulates a variable of type <typeparamref name="T" /> that might or might not be set.
    /// </summary>
    /// <typeparam name="T">The type of the encapsulated variable.</typeparam>
    /// <remarks>
    ///     This <see cref="Maybe{T}" /> is called a Something, if the value of the encapsulated variable is set.
    ///     This <see cref="Maybe{T}" /> is called a Nothing, if the value of the encapsulated variable is not set.
    /// </remarks>
    public readonly partial struct Maybe<T>
    {
        private readonly T _value;

        /// <summary>
        ///     Instantiates a new <see cref="Maybe{T}" /> that encapsulates a variable of type <typeparamref name="T" />.
        /// </summary>
        /// <remarks>
        ///     The <see cref="Maybe{T}" /> cannot encapsulate variables of type <see cref="Maybe{T}" /> or variables of type
        ///     <see cref="Nullable{T}" />.
        /// </remarks>
        /// <exception cref="MaybeUnderlyingTypeNotAllowedException">
        ///     if the encapsulated type is a <see cref="Maybe{T}" /> or a <see cref="Nullable{T}" />.
        /// </exception>
        public Maybe(T value)
        {
            EnsureUnderlyingType();

            // PERFORMANCE NOTE: The comparison value != null implies a boxing for value types as well as for reference types!
            HasValue = value != null;
            _value = value;
        }

        /// <summary>
        ///     Indicates whether this <see cref="Maybe{T}" /> is a Something (i.e., the encapsulated value is set) or a Nothing
        ///     (i.e., the encapsulated value is not set).
        /// </summary>
        [Pure]
        public bool HasValue { get; }

        /// <summary>
        ///     Returns the encapsulated value if this <see cref="Maybe{T}" /> is a Something (i.e., the encapsulated value is
        ///     set).
        ///     Hint: Invoke <see cref="HasValue" /> first! Alternatively, consider to invoke <see cref="ValueOr(T)" /> or
        ///     <see cref="ValueOrDefault" />.
        /// </summary>
        /// <exception cref="MaybeUnderlyingTypeNotAllowedException">
        ///     if this <see cref="Maybe{T}" /> is a Nothing (i.e., the encapsulated value is not set).
        /// </exception>
        [Pure]
        public T Value
        {
            get
            {
                EnsureHasValue(this);

                return _value;
            }
        }

        /// <summary>
        ///     Explicitly casts a <see cref="Maybe{T}" /> to the underlying type <typeparamref name="T" /> if this
        ///     <see cref="Maybe{T}" /> is a Something (i.e., the encapsulated value is set).
        ///     Hint: Invoke <see cref="HasValue" /> first! Alternatively, consider to invoke <see cref="ValueOr(T)" /> or
        ///     <see cref="ValueOrDefault" />.
        /// </summary>
        /// <exception cref="MaybeUnderlyingTypeNotAllowedException">
        ///     if this <see cref="Maybe{T}" /> is a Nothing (i.e., the encapsulated value is not set).
        /// </exception>
        public static explicit operator T(in Maybe<T> maybe)
        {
            EnsureHasValue(maybe);

            return maybe.Value;
        }

        /// <summary>
        ///     Implicitly casts a <typeparamref name="T" /> to a <see cref="Maybe{T}" />.
        /// </summary>
        public static implicit operator Maybe<T>(T value)
        {
            return new Maybe<T>(value);
        }

        /// <summary>
        ///     Returns the string representation of this <see cref="Maybe{T}" />.
        /// </summary>
        [Pure]
        public override string ToString()
        {
            return HasValue ? $"Maybe<{_value?.ToString()}>" : $"Nothing<{typeof(T).Name}>";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void EnsureHasValue(in Maybe<T> maybe)
        {
            if (!maybe.HasValue)
            {
                throw new MaybeOperationNotAllowedOnNothingException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void EnsureUnderlyingType()
        {
            if (typeof(T).IsMaybeOrNullable())
            {
                throw new MaybeUnderlyingTypeNotAllowedException(typeof(T).Name);
            }
        }
    }
}