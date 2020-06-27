namespace LambdaEcho.MaybeType.Exceptions
{
    internal class MaybeUnderlyingTypeNotAllowedException : MaybeInvariantViolationExceptionBase
    {
        internal MaybeUnderlyingTypeNotAllowedException(string typeName) : base(
            $"The specified type '{typeName}' is not allowed as underlying type for a Maybe<>.")
        {
            // Intentionally empty
        }
    }
}