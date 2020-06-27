using System;
using System.Collections.Generic;
using System.Reflection;
using FakeItEasy;
using FluentAssertions;
using LambdaEcho.MaybeType;
using Xunit;

namespace MaybeType.Tests
{
    public partial class MaybeTests
    {
        #region Constructor

        [Fact]
        public void Constructor___Invoked_with_other_Maybe___Throws_MaybeUnderlyingTypeNotAllowedException()
        {
            var otherMaybe = new Maybe<int>(42);

            Action act = () =>
            {
                var _ = new Maybe<Maybe<int>>(otherMaybe);
            };

            act.Should()
                .Throw<Exception>()
                .WithMessage(
                    "An invariant of the Maybe<> type was violated due to improper usage! The specified type 'Maybe`1' is not allowed as underlying type for a Maybe<>.")
                .Which
                .GetType()
                .Name.Should()
                .Be("MaybeUnderlyingTypeNotAllowedException");
        }

        [Fact]
        public void Constructor___Invoked_with_a_Nullable___Throws_MaybeUnderlyingTypeNotAllowedException()
        {
            var nullable = new bool?(true);

            Action act = () =>
            {
                var _ = new Maybe<bool?>(nullable);
            };

            act.Should()
                .Throw<Exception>()
                .WithMessage(
                    "An invariant of the Maybe<> type was violated due to improper usage! The specified type 'Nullable`1' is not allowed as underlying type for a Maybe<>.")
                .And
                .GetType()
                .Name.Should()
                .Be("MaybeUnderlyingTypeNotAllowedException");
        }

        [Fact]
        public void Constructor___Invoked_with_Reference_Type___Instantiates_properly()
        {
            const string stringValue = "Hello World!";

            var maybe = new Maybe<string>(stringValue);

            maybe.GetType()
                .GetField("_value", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.GetValue(maybe)
                .Should()
                .BeEquivalentTo(stringValue);
        }

        [Fact]
        public void Constructor___Invoked_with_Value_Type___Instantiates_properly()
        {
            var maybe = new Maybe<int>(42);

            maybe.GetType()
                .GetField("_value", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.GetValue(maybe)
                .Should()
                .BeEquivalentTo(42);
        }

        #endregion Constructor

        #region HasValue

        [Fact]
        public void HasValue___On_a_Something_of_Value_Type___Return_True()
        {
            var maybe = new Maybe<int>(42);

            maybe.HasValue.Should().BeTrue();
        }

        [Fact]
        public void HasValue___On_a_Nothing_of_Value_Type___Returns_False()
        {
            var maybe = new Maybe<int>();

            maybe.HasValue.Should().BeFalse();
        }

        [Fact]
        public void HasValue___On_a_Something_of_Reference_Type___Returns_True()
        {
            var maybe = new Maybe<string>("Hello World!");

            maybe.HasValue.Should().BeTrue();
        }

        [Fact]
        public void HasValue___On_a_Nothing_of_Reference_Type___Returns_False()
        {
            var maybe = new Maybe<object>(null);

            maybe.HasValue.Should().BeFalse();
        }

        #endregion HasValue

        #region Value

        [Fact]
        public void Value___On_a_Something_of_Value_Type___Returns_the_identical_value()
        {
            var maybe = new Maybe<int>(42);

            maybe.Value.Should().Be(42);
        }

        [Fact]
        public void Value___On_a_Nothing_of_Value_Type___Throws_MaybeOperationNotAllowedOnNothingException()
        {
            var nothing = default(Maybe<int>);

            Action act = () =>
            {
                var _ = nothing.Value;
            };

            act.Should()
                .Throw<Exception>()
                .WithMessage(
                    "An invariant of the Maybe<> type was violated due to improper usage! The operation is not allowed on a Nothing.")
                .And
                .GetType()
                .Name
                .Should()
                .Be("MaybeOperationNotAllowedOnNothingException");
        }

        [Fact]
        public void Value___On_a_Something_of_Reference_Type___Returns_the_identical_reference()
        {
            const string stringValue = "Hello World!";

            var maybe = new Maybe<string>(stringValue);

            maybe.Value.Should().BeEquivalentTo(stringValue);
            ReferenceEquals(stringValue, maybe.Value).Should().BeTrue();
        }

        [Fact]
        public void Value___On_a_Nothing_of_Reference_Type___Throws_MaybeOperationNotAllowedOnNothingException()
        {
            var nothing = default(Maybe<object>);

            Action act = () =>
            {
                var _ = nothing.Value;
            };

            act.Should()
                .Throw<Exception>()
                .WithMessage(
                    "An invariant of the Maybe<> type was violated due to improper usage! The operation is not allowed on a Nothing.")
                .And
                .GetType()
                .Name
                .Should()
                .Be("MaybeOperationNotAllowedOnNothingException");
        }

        #endregion Value

        #region Explicit Cast

        private class DummyClassForExplicitCastTest
        {
            // Intentionally empty
        }

        [Fact]
        public void Explicit_Cast___On_a_Something_of_Value_Type___Returns_the_identical_underlying_value()
        {
            var something = new Maybe<int>(42);

            var value = (int) something;

            value.Should().Be(42);
        }

        [Fact]
        public void Explicit_Cast___On_a_Something_of_Reference_Type_String___Return_the_identical_underlying_value()
        {
            const string stringValue = "Hello World!";
            var something = new Maybe<string>(stringValue);

            var value = (string) something;

            value.Should().BeEquivalentTo(stringValue);
            ReferenceEquals(stringValue, value).Should().BeTrue();
        }

        [Fact]
        public void Explicit_Cast___On_a_Something_of_Reference_Type___Returns_the_identical_underlying_value()
        {
            var dummyClass = new DummyClassForExplicitCastTest();
            var something = new Maybe<object>(dummyClass);

            var value = (DummyClassForExplicitCastTest) something;

            ReferenceEquals(dummyClass, value).Should().BeTrue();
        }

        [Fact]
        public void Explicit_Cast___On_a_Nothing___Throws_MaybeOperationNotAllowedOnNothingException()
        {
            var nothing = default(Maybe<int>);

            Action act = () =>
            {
                var _ = (int) nothing;
            };

            act.Should()
                .Throw<Exception>()
                .WithMessage(
                    "An invariant of the Maybe<> type was violated due to improper usage! The operation is not allowed on a Nothing.")
                .And
                .GetType()
                .Name
                .Should()
                .Be("MaybeOperationNotAllowedOnNothingException");
        }

        [Fact]
        public void Explicit_Cast___On_a_Something_to_Nullable___Returns_Nullable()
        {
            var something = new Maybe<int>(42);

            // To clarify: Next line performs explicit cast from Maybe<int> to int, first. Then, a Nullable<int> is instantiated.
            var nullable = (int?) something;

            // ReSharper disable once PossibleInvalidOperationException -- Value of Nullable is always set.
            nullable.Value.Should().Be(something.Value);
        }

        #endregion Explicit Cast

        #region Implicit Cast

        private class DummyClassForImplicitCastTest
        {
            // Intentionally empty
        }

        [Fact]
        public void Implicit_Cast___On_Value_Type___Returns_a_Something()
        {
            Maybe<int> something = 42;

            something.Value.Should().Be(42);
        }

        [Fact]
        public void Implicit_Cast___On_Reference_Type_String___Returns_a_Something()
        {
            const string stringValue = "Hello World!";

            Maybe<string> something = stringValue;

            something.Value.Should().BeEquivalentTo(stringValue);
            ReferenceEquals(stringValue, something.Value).Should().BeTrue();
        }

        [Fact]
        public void Implicit_Cast___On_Reference_Type___Returns_a_Something()
        {
            var dummyClassForImplicitCastTest = new DummyClassForImplicitCastTest();

            Maybe<DummyClassForImplicitCastTest> something = dummyClassForImplicitCastTest;

            something.Value.Should().BeEquivalentTo(dummyClassForImplicitCastTest);
            ReferenceEquals(dummyClassForImplicitCastTest, something.Value).Should().BeTrue();
        }

        [Fact]
        public void Implicit_Cast___On_Null_Object___Returns_a_Nothing()
        {
            Maybe<object> nothing = null;

            nothing.HasValue.Should().BeFalse();
        }

        [Fact]
        public void Implicit_Cast___On_Nullable___Throws_MaybeUnderlyingTypeNotAllowedException()
        {
            var nullable = new int?(42);

            Action act = () =>
            {
                Maybe<int?> _ = nullable;
            };

            act.Should()
                .Throw<Exception>()
                .WithMessage(
                    "An invariant of the Maybe<> type was violated due to improper usage! The specified type 'Nullable`1' is not allowed as underlying type for a Maybe<>.")
                .And
                .GetType()
                .Name.Should()
                .Be("MaybeUnderlyingTypeNotAllowedException");
        }

        [Fact]
        public void Implicit_Cast___On_other_Maybe___Throws_MaybeUnderlyingTypeNotAllowedException()
        {
            var otherMaybe = new Maybe<string>();

            Action act = () =>
            {
                Maybe<Maybe<string>> _ = otherMaybe;
            };

            act.Should()
                .Throw<Exception>()
                .WithMessage(
                    "An invariant of the Maybe<> type was violated due to improper usage! The specified type 'Maybe`1' is not allowed as underlying type for a Maybe<>.")
                .And
                .GetType()
                .Name.Should()
                .Be("MaybeUnderlyingTypeNotAllowedException");
        }

        #endregion Implicit Cast

        #region ToString()

        [Theory]
        [MemberData(nameof(ToString___On_given_Maybe_of_Float___TestData))]
        public void ToString___On_given_Maybe_of_Float___Returns_expected_result(Maybe<float> maybe, string expectedResult)
        {
            maybe.ToString().Should().Be(expectedResult);
        }

        public static IEnumerable<object[]> ToString___On_given_Maybe_of_Float___TestData()
        {
            return new List<object[]>
            {
                new object[] { default(Maybe<float>), "Nothing<Single>" },
                new object[] { new Maybe<float>(42.17f), "Maybe<42.17>" }
            };
        }

        [Theory]
        [MemberData(nameof(ToString___On_given_Maybe_of_String___TestData))]
        public void ToString___On_given_Maybe_of_String___Returns_expected_result(Maybe<string> maybe, string expectedResult)
        {
            maybe.ToString().Should().Be(expectedResult);
        }

        public static IEnumerable<object[]> ToString___On_given_Maybe_of_String___TestData()
        {
            return new List<object[]>
            {
                new object[] { default(Maybe<string>), "Nothing<String>" },
                new object[] { new Maybe<string>("Hello World!"), "Maybe<Hello World!>" }
            };
        }

        [Theory]
        [MemberData(nameof(ToString___On_given_Maybe_of_IDisposable___TestData))]
        public void ToString___On_given_Maybe_of_IDisposable___Returns_expected_result(Maybe<IDisposable> maybe, string expectedResult)
        {
            maybe.ToString().Should().Be(expectedResult);
        }

        public static IEnumerable<object[]> ToString___On_given_Maybe_of_IDisposable___TestData()
        {
            var fake = A.Fake<IDisposable>();
            A.CallTo(() => fake.ToString()).Returns("DisposableObject");

            var something = new Maybe<IDisposable>(fake);
            var nothing = default(Maybe<IDisposable>);

            return new List<object[]>
            {
                new object[] { nothing, "Nothing<IDisposable>" },
                new object[] { something, "Maybe<DisposableObject>" }
            };
        }

        [Theory]
        [MemberData(nameof(ToString___On_given_Maybe_of_IFormattable___TestData))]
        public void ToString___On_given_Maybe_of_IFormattable___Returns_expected_result(Maybe<IFormattable> maybe, string expectedResult)
        {
            maybe.ToString().Should().Be(expectedResult);
        }

        public static IEnumerable<object[]> ToString___On_given_Maybe_of_IFormattable___TestData()
        {
            var fake = A.Fake<IFormattable>();
            A.CallTo(() => fake.ToString()).Returns("FormattableObject");

            var something = new Maybe<IFormattable>(fake);
            var nothing = default(Maybe<IFormattable>);

            return new List<object[]>
            {
                new object[] { nothing, "Nothing<IFormattable>" },
                new object[] { something, "Maybe<FormattableObject>" }
            };
        }

        #endregion ToString()
    }
}