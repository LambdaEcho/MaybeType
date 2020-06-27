using System;
using FakeItEasy;
using FluentAssertions;
using LambdaEcho.MaybeType;
using LambdaEcho.MaybeType.Extensions;
using Xunit;

namespace MaybeType.Tests.Extensions
{
    public class GenericTypeExtensionsTests
    {
        [Fact]
        public void ToMaybe___On_other_Maybe___Returns_identical_other_Maybe()
        {
            var otherMaybe = new Maybe<int>(42);

            var maybe = otherMaybe.ToMaybe();

            maybe.Should().BeEquivalentTo(otherMaybe);
        }

        [Fact]
        public void ToMaybe___On_Reference_Type___Returns_Maybe_of_Reference_Type()
        {
            var referenceType = A.Fake<IFormattable>();

            var maybe = referenceType.ToMaybe();

            maybe.Value.Should().BeEquivalentTo(referenceType);
            ReferenceEquals(referenceType, maybe.Value).Should().BeTrue();
        }

        [Fact]
        public void ToMaybe___On_Reference_Type_String___Returns_Maybe_of_String()
        {
            const string stringValue = "Hello World!";

            var maybe = stringValue.ToMaybe();

            maybe.Value.Should().BeEquivalentTo(stringValue);
            ReferenceEquals(stringValue, maybe.Value).Should().BeTrue();
        }

        [Fact]
        public void ToMaybe___On_Value_Type___Returns_Maybe_of_Value_Type()
        {
            const int integer = 42;

            var maybe = integer.ToMaybe();

            maybe.Value.Should().Be(42);
        }
    }
}