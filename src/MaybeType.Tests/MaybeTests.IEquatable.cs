using System;
using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using LambdaEcho.MaybeType;
using Xunit;

namespace MaybeType.Tests
{
    public partial class MaybeTests
    {
        [Fact]
        public void IEquatable___Equality_between_Maybe_and_different_type___Returns_False()
        {
            var disposableFake = A.Fake<IDisposable>();
            var somethingOfIDisposable = new Maybe<IDisposable>(disposableFake);

            var cloneableFake = A.Fake<ICloneable>();

            // ReSharper disable SuspiciousTypeConversion.Global
            somethingOfIDisposable.Equals(cloneableFake).Should().BeFalse();
            cloneableFake.Equals(somethingOfIDisposable).Should().BeFalse();
            // ReSharper restore SuspiciousTypeConversion.Global
            (somethingOfIDisposable.GetHashCode() == cloneableFake.GetHashCode()).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(IEquatable___On_two_Maybe_of_Reference_Type___TestData))]
        public void IEquatable___On_two_Maybe_of_Reference_Type___Returns_expected_result(Maybe<IFormattable> leftMaybe,
            Maybe<IFormattable> rightMaybe, bool areEqual)
        {
            leftMaybe.Equals(rightMaybe).Should().Be(areEqual);
            rightMaybe.Equals(leftMaybe).Should().Be(areEqual);
            leftMaybe.Equals((object) rightMaybe).Should().Be(areEqual);
            rightMaybe.Equals((object) leftMaybe).Should().Be(areEqual);
            (leftMaybe.GetHashCode() == rightMaybe.GetHashCode()).Should().Be(areEqual);
            (leftMaybe == rightMaybe).Should().Be(areEqual);
            (rightMaybe == leftMaybe).Should().Be(areEqual);
            (leftMaybe != rightMaybe).Should().Be(!areEqual);
            (rightMaybe != leftMaybe).Should().Be(!areEqual);
        }

        public static IEnumerable<object[]> IEquatable___On_two_Maybe_of_Reference_Type___TestData()
        {
            var firstFake = A.Fake<IFormattable>();
            A.CallTo(() => firstFake.ToString()).Returns(nameof(firstFake));
            var firstSomething = new Maybe<IFormattable>(firstFake);

            var sameFirstFake = A.Fake<IFormattable>();
            A.CallTo(() => sameFirstFake.ToString()).Returns(nameof(firstFake)); // same object, different instance
            var sameFirstSomething = new Maybe<IFormattable>(sameFirstFake);

            var secondFake = A.Fake<IFormattable>();
            A.CallTo(() => secondFake.ToString()).Returns(nameof(secondFake));
            var secondSomething = new Maybe<IFormattable>(secondFake);

            var identicalNothing = default(Maybe<IFormattable>);

            return new List<object[]>
            {
                new object[] { firstSomething, sameFirstSomething, false }, // same Something
                new object[] { firstSomething, firstSomething, true }, // identical Something
                new object[] { firstSomething, secondSomething, false }, // different Something
                new object[] { default(Maybe<IFormattable>), default(Maybe<IFormattable>), true }, // same Nothing
                new object[] { identicalNothing, identicalNothing, true }, // identical Nothing
                new object[] { firstSomething, default(Maybe<IFormattable>), false }
            };
        }

        [Theory]
        [MemberData(nameof(IEquatable___On_two_Maybe_of_String___TestData))]
        public void IEquatable___On_two_Maybe_of_String___Returns_expected_result(Maybe<string> leftMaybe, Maybe<string> rightMaybe,
            bool areEqual)
        {
            leftMaybe.Equals(rightMaybe).Should().Be(areEqual);
            rightMaybe.Equals(leftMaybe).Should().Be(areEqual);
            leftMaybe.Equals((object) rightMaybe).Should().Be(areEqual);
            rightMaybe.Equals((object) leftMaybe).Should().Be(areEqual);
            (leftMaybe.GetHashCode() == rightMaybe.GetHashCode()).Should().Be(areEqual);
            (leftMaybe == rightMaybe).Should().Be(areEqual);
            (rightMaybe == leftMaybe).Should().Be(areEqual);
            (leftMaybe != rightMaybe).Should().Be(!areEqual);
            (rightMaybe != leftMaybe).Should().Be(!areEqual);
        }

        public static IEnumerable<object[]> IEquatable___On_two_Maybe_of_String___TestData()
        {
            const string firstString = "Hello World!";
            var firstSomething = new Maybe<string>(firstString);

            const string sameFirstString = "Hello World!"; // same object, different instance
            var sameFirstSomething = new Maybe<string>(sameFirstString);

            const string secondString = "Yet another String";
            var secondSomething = new Maybe<string>(secondString);

            var identicalNothing = default(Maybe<string>);

            return new List<object[]>
            {
                new object[] { firstSomething, sameFirstSomething, true }, // same Something
                new object[] { firstSomething, firstSomething, true }, // identical Something
                new object[] { firstSomething, secondSomething, false }, // different Something
                new object[] { default(Maybe<string>), default(Maybe<string>), true }, // same Nothing
                new object[] { identicalNothing, identicalNothing, true }, // identical Nothing
                new object[] { firstSomething, default(Maybe<string>), false }
            };
        }

        [Theory]
        [MemberData(nameof(IEquatable___On_two_Maybe_of_Value_Type___TestData))]
        public void IEquatable___On_two_Maybe_of_Value_Type___Returns_expected_result(Maybe<int> leftMaybe, Maybe<int> rightMaybe,
            bool areEqual)
        {
            leftMaybe.Equals(rightMaybe).Should().Be(areEqual);
            rightMaybe.Equals(leftMaybe).Should().Be(areEqual);
            leftMaybe.Equals((object) rightMaybe).Should().Be(areEqual);
            rightMaybe.Equals((object) leftMaybe).Should().Be(areEqual);
            (leftMaybe.GetHashCode() == rightMaybe.GetHashCode()).Should().Be(areEqual);
            (leftMaybe == rightMaybe).Should().Be(areEqual);
            (rightMaybe == leftMaybe).Should().Be(areEqual);
            (leftMaybe != rightMaybe).Should().Be(!areEqual);
            (rightMaybe != leftMaybe).Should().Be(!areEqual);
        }

        public static IEnumerable<object[]> IEquatable___On_two_Maybe_of_Value_Type___TestData()
        {
            var identicalSomething = new Maybe<int>(17);
            var identicalNothing = default(Maybe<int>);

            return new List<object[]>
            {
                new object[] { new Maybe<int>(42), new Maybe<int>(42), true }, // same Something
                new object[] { identicalSomething, identicalSomething, true }, // identical Something
                new object[] { new Maybe<int>(42), new Maybe<int>(17), false }, // different Something
                new object[] { default(Maybe<int>), default(Maybe<int>), true }, // same Nothing
                new object[] { identicalNothing, identicalNothing, true }, // identical Nothing
                new object[] { new Maybe<int>(42), default(Maybe<int>), false }
            };
        }

        [Fact]
        public void IEquatable_GetHashCode___Implicitly_invoked_by_IDictionary___Returns_proper_result()
        {
            var dictionary = new Dictionary<Maybe<int>, bool>
            {
                { new Maybe<int>(42), true }, // invokes GetHashCode implicitly
                { new Maybe<int>(23), false } // invokes GetHashCode implicitly
            };

            dictionary.ContainsKey(new Maybe<int>(42)).Should().BeTrue(); // invokes GetHashCode implicitly
            dictionary.ContainsKey(new Maybe<int>(23)).Should().BeTrue(); // invokes GetHashCode implicitly
            dictionary.ContainsKey(new Maybe<int>(17)).Should().BeFalse(); // invokes GetHashCode implicitly
        }
    }
}