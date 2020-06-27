using System;
using System.Diagnostics.Contracts;

namespace LambdaEcho.MaybeType.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        ///     Indicates whether this type is a <see cref="Maybe{T}" />.
        /// </summary>
        [Pure]
        public static bool IsMaybe(this Type type)
        {
            if (type == null)
            {
                return false;
            }

            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Maybe<>);
        }

        [Pure]
        internal static bool IsMaybeOrNullable(this Type type)
        {
            // This is a performance optimized implementation for internal usage.

            if (type == null)
            {
                return false;
            }

            var isGenericType = type.IsGenericType;

            if (!isGenericType)
            {
                return false;
            }

            var genericTypeDefinition = type.GetGenericTypeDefinition();
            var isMaybe = genericTypeDefinition == typeof(Maybe<>);
            var isNullable = genericTypeDefinition == typeof(Nullable<>);
            return isMaybe || isNullable;
        }
    }
}