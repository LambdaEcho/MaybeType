using System.Collections.Generic;
using FluentAssertions;
using LambdaEcho.MaybeType.Extensions;
using Xunit;

namespace MaybeType.Tests.Extensions
{
    public class NullableExtensionsTests
    {
        [Theory]
        [MemberData(nameof(ToMaybeTestData))]
        public void ToMaybe___On_given_Nullable_of_Value_Type___Returns_Maybe_of_Value_Type(int? nullable, bool expectedHasValue)
        {
            var maybe = nullable.ToMaybe();

            maybe.HasValue.Should().Be(expectedHasValue);
        }

        public static IEnumerable<object[]> ToMaybeTestData()
        {
            return new List<object[]>
            {
                new object[] { default(int?), false },
                new object[] { 42, true }
            };
        }
    }
}