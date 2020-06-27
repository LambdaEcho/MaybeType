using System;

namespace LambdaEcho.MaybeType.Exceptions
{
    internal abstract class MaybeInvariantViolationExceptionBase : Exception
    {
        protected MaybeInvariantViolationExceptionBase(string message) : base(
            @"An invariant of the Maybe<> type was violated due to improper usage! " + message)
        {
            // Intentionally empty
        }
    }
}