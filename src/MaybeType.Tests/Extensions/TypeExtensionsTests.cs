using System;
using System.Collections.Generic;
using FluentAssertions;
using LambdaEcho.MaybeType;
using LambdaEcho.MaybeType.Extensions;
using Xunit;

namespace MaybeType.Tests.Extensions
{
    public class TypeExtensionsTests
    {
        public static IEnumerable<object[]> IsMaybeTestData =>
            new List<object[]>
            {
                new object[] { typeof(Maybe<int>), true },
                new object[] { typeof(Maybe<object>), true },
                new object[] { typeof(int), false },
                new object[] { typeof(string), false },
                new object[] { typeof(int?), false },
                new object[] { null, false }
            };

        [Theory]
        [MemberData(nameof(IsMaybeTestData))]
        public void IsMaybe___Invoked_on_given_type___Returns_expectedResult(Type type, bool expectedResult)
        {
            type.IsMaybe().Should().Be(expectedResult);
        }
    }
}