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
        public void IComparable___Compare_Maybe_of_Int_with_Maybe_of_String___Throws_ArgumentException()
        {
            var maybeOfInt = new Maybe<int>(42);
            var maybeOfString = new Maybe<string>("A");

            Action compareInt2String = () =>
            {
                var _ = maybeOfInt.CompareTo(maybeOfString);
            };

            Action compareString2Int = () =>
            {
                var _ = maybeOfString.CompareTo(maybeOfInt);
            };

            compareInt2String.Should()
                .Throw<ArgumentException>(
                    "because Maybe<int> cannot be compared with Maybe<string> and Int32 cannot be compared with String.");
            compareString2Int.Should()
                .Throw<ArgumentException>(
                    "because Maybe<string> cannot be compared with Maybe<int> and String cannot be compared with Int32.");
        }

        [Fact]
        public void IComparable___Compare_Nothing_with_its_underlying_type___Return_sort_order_of_underlying_type()
        {
            var nothingOfInt = default(Maybe<int>);

            nothingOfInt.CompareTo(default).Should().Be(-1);
            nothingOfInt.CompareTo(1).Should().Be(-1);
            nothingOfInt.CompareTo(-1).Should().Be(-1);

            var nothingOfString = default(Maybe<string>);

            nothingOfString.CompareTo(string.Empty).Should().Be(-1);
            nothingOfString.CompareTo(default).Should().Be(0);
            nothingOfString.CompareTo(null).Should().Be(0);
        }

        [Fact]
        public void IComparable___Compare_Something_with_its_underlying_type___Return_sort_order_of_underlying_type()
        {
            var somethingOfInt = new Maybe<int>(42);

            somethingOfInt.CompareTo(42).Should().Be(0);
            somethingOfInt.CompareTo(41).Should().Be(1);
            somethingOfInt.CompareTo(43).Should().Be(-1);

            var somethingOfString = new Maybe<string>("B");

            somethingOfString.CompareTo("B").Should().Be(0);
            somethingOfString.CompareTo("A").Should().Be(1);
            somethingOfString.CompareTo("C").Should().Be(-1);
        }

        [Theory]
        [MemberData(nameof(IComparable___On_two_Maybe_of_Reference_Type___TestData))]
        public void IComparable___On_two_Maybe_of_Reference_Type___Returns_expected_result(Maybe<IComparable> leftMaybe,
            Maybe<IComparable> rightMaybe, int relativeOrder)
        {
            leftMaybe.CompareTo(rightMaybe).Should().Be(relativeOrder);
            rightMaybe.CompareTo(leftMaybe).Should().Be(relativeOrder * -1);
            leftMaybe.CompareTo((object) rightMaybe).Should().Be(relativeOrder);
            rightMaybe.CompareTo((object) leftMaybe).Should().Be(relativeOrder * -1);

            (leftMaybe < rightMaybe).Should().Be(relativeOrder == -1);
            (rightMaybe < leftMaybe).Should().Be(relativeOrder == 1);
            (leftMaybe > rightMaybe).Should().Be(relativeOrder == 1);
            (rightMaybe > leftMaybe).Should().Be(relativeOrder == -1);
            (leftMaybe <= rightMaybe).Should().Be(relativeOrder == -1 || relativeOrder == 0);
            (rightMaybe <= leftMaybe).Should().Be(relativeOrder == 1 || relativeOrder == 0);
            (leftMaybe >= rightMaybe).Should().Be(relativeOrder == 1 || relativeOrder == 0);
            (rightMaybe >= leftMaybe).Should().Be(relativeOrder == -1 || relativeOrder == 0);
        }

        public static IEnumerable<object[]> IComparable___On_two_Maybe_of_Reference_Type___TestData()
        {
            var fake0 = A.Fake<IComparable>();
            A.CallTo(() => fake0.ToString()).Returns(nameof(fake0));
            var fake1 = A.Fake<IComparable>();
            A.CallTo(() => fake1.ToString()).Returns(nameof(fake1));
            var fake2 = A.Fake<IComparable>();
            A.CallTo(() => fake2.ToString()).Returns(nameof(fake2));

            A.CallTo(() => fake1.CompareTo(fake1)).Returns(0);
            A.CallTo(() => fake1.CompareTo(fake0)).Returns(1);
            A.CallTo(() => fake1.CompareTo(fake2)).Returns(-1);

            A.CallTo(() => fake0.CompareTo(fake1)).Returns(-1);

            A.CallTo(() => fake2.CompareTo(fake1)).Returns(1);

            return new List<object[]>
            {
                new object[] { new Maybe<IComparable>(fake1), new Maybe<IComparable>(fake1), 0 },
                new object[] { new Maybe<IComparable>(fake2), new Maybe<IComparable>(fake1), 1 },
                new object[] { new Maybe<IComparable>(fake1), new Maybe<IComparable>(fake2), -1 },
                new object[] { new Maybe<IComparable>(fake1), default, 1 },
                new object[] { default, new Maybe<IComparable>(fake1), -1 },
                new object[] { default, default, 0 }
            };
        }

        [Theory]
        [MemberData(nameof(IComparable___On_two_Maybe_of_String___TestData))]
        public void IComparable___On_two_Maybe_of_String___Returns_expected_result(Maybe<string> leftMaybe, Maybe<string> rightMaybe,
            int relativeOrder)
        {
            leftMaybe.CompareTo(rightMaybe).Should().Be(relativeOrder);
            rightMaybe.CompareTo(leftMaybe).Should().Be(relativeOrder * -1);
            leftMaybe.CompareTo((object) rightMaybe).Should().Be(relativeOrder);
            rightMaybe.CompareTo((object) leftMaybe).Should().Be(relativeOrder * -1);

            (leftMaybe < rightMaybe).Should().Be(relativeOrder == -1);
            (rightMaybe < leftMaybe).Should().Be(relativeOrder == 1);
            (leftMaybe > rightMaybe).Should().Be(relativeOrder == 1);
            (rightMaybe > leftMaybe).Should().Be(relativeOrder == -1);
            (leftMaybe <= rightMaybe).Should().Be(relativeOrder == -1 || relativeOrder == 0);
            (rightMaybe <= leftMaybe).Should().Be(relativeOrder == 1 || relativeOrder == 0);
            (leftMaybe >= rightMaybe).Should().Be(relativeOrder == 1 || relativeOrder == 0);
            (rightMaybe >= leftMaybe).Should().Be(relativeOrder == -1 || relativeOrder == 0);
        }

        public static IEnumerable<object[]> IComparable___On_two_Maybe_of_String___TestData()
        {
            return new List<object[]>
            {
                new object[] { new Maybe<string>("Z"), new Maybe<string>("Z"), 0 },
                new object[] { new Maybe<string>("Z"), new Maybe<string>("A"), 1 },
                new object[] { new Maybe<string>("A"), new Maybe<string>("Z"), -1 },
                new object[] { new Maybe<string>("A"), default, 1 },
                new object[] { default, new Maybe<string>("A"), -1 },
                new object[] { default, default, 0 }
            };
        }

        [Theory]
        [MemberData(nameof(IComparable___On_two_Maybe_of_Value_Type___TestData))]
        public void IComparable___On_two_Maybe_of_Value_Type___Returns_expected_result(Maybe<int> leftMaybe, Maybe<int> rightMaybe,
            int relativeOrder)
        {
            leftMaybe.CompareTo(rightMaybe).Should().Be(relativeOrder);
            rightMaybe.CompareTo(leftMaybe).Should().Be(relativeOrder * -1);
            leftMaybe.CompareTo((object) rightMaybe).Should().Be(relativeOrder);
            rightMaybe.CompareTo((object) leftMaybe).Should().Be(relativeOrder * -1);

            (leftMaybe < rightMaybe).Should().Be(relativeOrder == -1);
            (rightMaybe < leftMaybe).Should().Be(relativeOrder == 1);
            (leftMaybe > rightMaybe).Should().Be(relativeOrder == 1);
            (rightMaybe > leftMaybe).Should().Be(relativeOrder == -1);
            (leftMaybe <= rightMaybe).Should().Be(relativeOrder == -1 || relativeOrder == 0);
            (rightMaybe <= leftMaybe).Should().Be(relativeOrder == 1 || relativeOrder == 0);
            (leftMaybe >= rightMaybe).Should().Be(relativeOrder == 1 || relativeOrder == 0);
            (rightMaybe >= leftMaybe).Should().Be(relativeOrder == -1 || relativeOrder == 0);
        }

        public static IEnumerable<object[]> IComparable___On_two_Maybe_of_Value_Type___TestData()
        {
            return new List<object[]>
            {
                new object[] { new Maybe<int>(42), new Maybe<int>(42), 0 },
                new object[] { new Maybe<int>(42), new Maybe<int>(17), 1 },
                new object[] { new Maybe<int>(17), new Maybe<int>(42), -1 },
                new object[] { new Maybe<int>(42), default, 1 },
                new object[] { default, new Maybe<int>(42), -1 },
                new object[] { default, default, 0 }
            };
        }
    }
}