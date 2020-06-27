namespace LambdaEcho.MaybeType.Exceptions
{
    internal class MaybeOperationNotAllowedOnNothingException : MaybeInvariantViolationExceptionBase
    {
        internal MaybeOperationNotAllowedOnNothingException() : base("The operation is not allowed on a Nothing.")
        {
            // Intentionally empty
        }
    }
}