namespace LambdaEcho.MaybeType
{
    /// <summary>
    ///     A factory to create new instances of <see cref="Maybe{T}" />.
    /// </summary>
    public static class Maybe
    {
        /// <summary>
        ///     Creates a new <see cref="Maybe{T}" /> that encapsulates the given <paramref name="value" />.
        /// </summary>
        /// <typeparam name="T">The type of the encapsulated variable.</typeparam>
        /// <param name="value">The value to be encapsulated by the new <see cref="Maybe{T}" />.</param>
        /// <returns>Returns a new <see cref="Maybe{T}" />.</returns>
        public static Maybe<T> Create<T>(T value)
        {
            return new Maybe<T>(value);
        }

        /// <summary>
        ///     Creates a new <see cref="Maybe{T}" /> that is a Nothing (i.e., the encapsulated value is not set).
        /// </summary>
        /// <typeparam name="T">The type of the encapsulated variable.</typeparam>
        /// <returns>Returns a new Nothing (i.e., the encapsulated value is not set).</returns>
        public static Maybe<T> Nothing<T>()
        {
            return default;
        }
    }
}