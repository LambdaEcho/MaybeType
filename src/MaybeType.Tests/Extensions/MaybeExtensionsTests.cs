using System.Collections.Generic;
using FluentAssertions;
using LambdaEcho.MaybeType;
using LambdaEcho.MaybeType.Extensions;
using Xunit;

namespace MaybeType.Tests.Extensions
{
    public class MaybeExtensionsTests
    {
        [Theory]
        [MemberData(nameof(ToNullableTestData))]
        public void ToNullable___On_given_Maybe_of_Value_Type___Returns_Nullable_of_Value_Type(Maybe<int> maybe, bool expectedHasValue)
        {
            var nullable = maybe.ToNullable();

            nullable.HasValue.Should().Be(expectedHasValue);
        }

        public static IEnumerable<object[]> ToNullableTestData()
        {
            return new List<object[]>
            {
                new object[] { default(Maybe<int>), false },
                new object[] { new Maybe<int>(42), true }
            };
        }
    }
}