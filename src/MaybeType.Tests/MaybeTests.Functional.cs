using System;
using FakeItEasy;
using FluentAssertions;
using LambdaEcho.MaybeType;
using Xunit;

namespace MaybeType.Tests
{
    public partial class MaybeTests
    {
        #region ValueOr and ValueOrDefault

        [Fact]
        public void ValueOr___On_a_Nothing___Returns_alternative_value()
        {
            var nothing = default(Maybe<int>);

            var result = nothing.ValueOr(17);

            result.Should().Be(17);
        }

        [Fact]
        public void ValueOr___On_a_Nothing_with_Func___Returns_result_of_Func()
        {
            var nothing = default(Maybe<int>);

            var result = nothing.ValueOr(() => 23);

            result.Should().Be(23);
        }

        [Fact]
        public void ValueOr___On_a_Something___Returns_encapsulated_value()
        {
            var something = new Maybe<int>(42);

            var result = something.ValueOr(17);

            result.Should().Be(42);
        }

        [Fact]
        public void ValueOr___On_a_Something_with_Func___Returns_encapsulated_value()
        {
            var something = new Maybe<int>(42);

            var result = something.ValueOr(() => 23);

            result.Should().Be(42);
        }

        [Fact]
        public void ValueOrDefault___On_a_Nothing___Returns_default_of_underlying_type()
        {
            var nothing = default(Maybe<int>);

            var result = nothing.ValueOrDefault();

            result.Should().Be(0);
        }

        [Fact]
        public void ValueOrDefault___On_a_Something___Returns_encapsulated_value()
        {
            var something = new Maybe<int>(42);

            var result = something.ValueOrDefault();

            result.Should().Be(42);
        }

        #endregion ValueOr and ValueOrDefault

        #region Or

        [Fact]
        public void Or__On_a_Nothing___Returns_alternative_Maybe()
        {
            var altnativeFake = A.Fake<IDisposable>();

            var nothing = default(Maybe<IDisposable>);

            var result = nothing.Or(new Maybe<IDisposable>(altnativeFake));

            result.Should().NotBeEquivalentTo(nothing);
            result.Value.Should().BeEquivalentTo(altnativeFake);
        }

        [Fact]
        public void Or___On_a_Something___Returns_identical_Something()
        {
            var fake = A.Fake<IDisposable>();
            var altnativeFake = A.Fake<IDisposable>();

            var something = new Maybe<IDisposable>(fake);

            var result = something.Or(new Maybe<IDisposable>(altnativeFake));

            result.Should().BeEquivalentTo(something);
            result.Value.Should().BeEquivalentTo(fake);
            ReferenceEquals(something.Value, result.Value).Should().BeTrue();
        }

        [Fact]
        public void Or__On_a_Nothing_with_Func___Returns_result_of_Func()
        {
            var altnativeFake = A.Fake<IDisposable>();

            var nothing = default(Maybe<IDisposable>);

            var result = nothing.Or(() => new Maybe<IDisposable>(altnativeFake));

            result.Should().NotBeEquivalentTo(nothing);
            result.Value.Should().BeEquivalentTo(altnativeFake);
        }

        [Fact]
        public void Or___On_a_Something_with_Func___Returns_identical_Something()
        {
            var fake = A.Fake<IDisposable>();
            var altnativeFake = A.Fake<IDisposable>();

            var something = new Maybe<IDisposable>(fake);

            var result = something.Or(() => new Maybe<IDisposable>(altnativeFake));

            result.Should().BeEquivalentTo(something);
            result.Value.Should().BeEquivalentTo(fake);
            ReferenceEquals(something.Value, result.Value).Should().BeTrue();
        }

        #endregion Or

        #region Map

        [Fact]
        public void Map_Maybe___On_a_Something_without_arguments___Returns_a_Nothing()
        {
            /*
            var something = new Maybe<int>(42);

            // Compiler error:
            // The call is ambiguous between the following methods
            // - 'Maybe<T>.Map<TResult>(Func<T, TResult>, Func<TResult>)' and
            // - 'Maybe<T>.Map<TResult>(Func<T, Maybe<TResult>>, Func<Maybe<TResult>>)'
            var result = something.Map<double>();
            */
        }

        [Fact]
        public void Map_Maybe___On_a_Something_with_SomethingFunc___Returns_result_of_SomethingFunc()
        {
            var something = new Maybe<string>("42");

            var result = something.Map(value => new Maybe<int>(Convert.ToInt32(value)));

            result.Should().BeEquivalentTo(new Maybe<int>(42));
        }

        [Fact]
        public void Map_Maybe___On_a_Something_with_NothingFunc___Returns_a_Nothing()
        {
            var something = new Maybe<string>("42");

            var result = something.Map(funcIfNothing: () => new Maybe<int>(27));

            result.HasValue.Should().BeFalse();
            result.Should().BeEquivalentTo(default(Maybe<int>));
        }

        [Fact]
        public void Map_Maybe___On_a_Something_with_SomethingFunc_and_NothingFunc___Returns_result_of_SomethingFunc()
        {
            var something = new Maybe<string>("42");

            var result = something.Map(value => new Maybe<int>(Convert.ToInt32(value)), () => new Maybe<int>(27));

            result.Should().BeEquivalentTo(new Maybe<int>(42));
        }

        [Fact]
        public void Map_Maybe___On_a_Nothing_without_arguments___Returns_a_Nothing()
        {
            /*
            var nothing = default(Maybe<int>);

            // Compiler error:
            // The call is ambiguous between the following methods
            // - 'Maybe<T>.Map<TResult>(Func<T, TResult>, Func<TResult>)' and
            // - 'Maybe<T>.Map<TResult>(Func<T, Maybe<TResult>>, Func<Maybe<TResult>>)'
            var result = nothing.Map();
            */
        }

        [Fact]
        public void Map_Maybe___On_a_Nothing_with_SomethingFunc___Returns_a_Nothing()
        {
            var nothing = default(Maybe<int>);

            var result = nothing.Map(value => new Maybe<string>(value.ToString()));

            result.HasValue.Should().BeFalse();
            result.Should().BeEquivalentTo(default(Maybe<string>));
        }

        [Fact]
        public void Map_Maybe___On_a_Nothing_with_NothingFunc___Returns_result_of_NothingFunc()
        {
            var nothing = default(Maybe<int>);

            var result = nothing.Map(funcIfNothing: () => new Maybe<string>("42"));

            result.Should().BeEquivalentTo(new Maybe<string>("42"));
        }

        [Fact]
        public void Map_Maybe___On_a_Nothing_with_SomethingFunc_and_NothingFunc___Returns_result_of_NothingFunc()
        {
            var nothing = default(Maybe<int>);

            var result = nothing.Map(value => new Maybe<string>(value.ToString()), () => new Maybe<string>("42"));

            result.Should().BeEquivalentTo(new Maybe<string>("42"));
        }


        [Fact]
        public void Map___On_a_Something_without_arguments___Returns_a_Nothing()
        {
            /*
            var something = new Maybe<int>(42);

            // Compiler error:
            // The call is ambiguous between the following methods
            // - 'Maybe<T>.Map<TResult>(Func<T, TResult>, Func<TResult>)' and
            // - 'Maybe<T>.Map<TResult>(Func<T, Maybe<TResult>>, Func<Maybe<TResult>>)'
            var result = something.Map<double>();
            */
        }

        [Fact]
        public void Map___On_a_Something_with_SomethingFunc___Returns_result_of_SomethingFunc()
        {
            var something = new Maybe<string>("42");

            var result = something.Map(Convert.ToInt32);

            result.Should().BeEquivalentTo(new Maybe<int>(42));
        }

        [Fact]
        public void Map___On_a_Something_with_NothingFunc___Returns_a_Nothing()
        {
            var something = new Maybe<string>("42");

            var result = something.Map(funcIfNothing: () => 27);

            result.HasValue.Should().BeFalse();
            result.Should().BeEquivalentTo(default(Maybe<int>));
        }

        [Fact]
        public void Map___On_a_Something_with_SomethingFunc_and_NothingFunc___Returns_result_of_SomethingFunc()
        {
            var something = new Maybe<string>("42");

            var result = something.Map(Convert.ToInt32, () => 27);

            result.Should().BeEquivalentTo(new Maybe<int>(42));
        }

        [Fact]
        public void Map___On_a_Nothing_without_arguments___Returns_a_Nothing()
        {
            /*
            var nothing = default(Maybe<int>);

            // Compiler error:
            // The call is ambiguous between the following methods
            // - 'Maybe<T>.Map<TResult>(Func<T, TResult>, Func<TResult>)' and
            // - 'Maybe<T>.Map<TResult>(Func<T, Maybe<TResult>>, Func<Maybe<TResult>>)'
            var result = nothing.Map();
            */
        }

        [Fact]
        public void Map___On_a_Nothing_with_SomethingFunc___Returns_a_Nothing()
        {
            var nothing = default(Maybe<int>);

            var result = nothing.Map(value => value.ToString());

            result.HasValue.Should().BeFalse();
            result.Should().BeEquivalentTo(default(Maybe<string>));
        }

        [Fact]
        public void Map___On_a_Nothing_with_NothingFunc___Returns_result_of_NothingFunc()
        {
            var nothing = default(Maybe<int>);

            var result = nothing.Map(funcIfNothing: () => "42");

            result.Should().BeEquivalentTo(new Maybe<string>("42"));
        }

        [Fact]
        public void Map___On_a_Nothing_with_SomethingFunc_and_NothingFunc___Returns_result_of_NothingFunc()
        {
            var nothing = default(Maybe<int>);

            var result = nothing.Map(value => value.ToString(), () => "42");

            result.Should().BeEquivalentTo(new Maybe<string>("42"));
        }

        #endregion

        #region Invoke

        [Fact]
        public void Invoke___On_a_Something_without_argument___Nothing_happens()
        {
            var something = new Maybe<int>(42);

            something.Invoke();
        }

        [Fact]
        public void Invoke___On_a_Something_with_SomethingAction___Invokes_SomethingAction()
        {
            var something = new Maybe<int>(42);
            var somethingAction = A.Fake<Action<int>>();

            something.Invoke(somethingAction);

            A.CallTo(somethingAction).MustHaveHappened(1, Times.Exactly);
        }

        [Fact]
        public void Invoke___On_a_Something_with_NothingAction___Nothing_happens()
        {
            var something = new Maybe<int>(42);
            var nothingAction = A.Fake<Action>();

            something.Invoke(actionIfNothing: nothingAction);

            A.CallTo(nothingAction).MustNotHaveHappened();
        }

        [Fact]
        public void Invoke___On_a_Something_with_SomethingAction_and_NothingAction___Invokes_SomethingAction()
        {
            var something = new Maybe<int>(42);
            var somethingAction = A.Fake<Action<int>>();
            var nothingAction = A.Fake<Action>();

            something.Invoke(somethingAction, nothingAction);

            A.CallTo(somethingAction).MustHaveHappened(1, Times.Exactly);
            A.CallTo(nothingAction).MustNotHaveHappened();
        }

        [Fact]
        public void Invoke___On_a_Nothing_without_arguments___Nothing_happens()
        {
            var nothing = default(Maybe<int>);

            nothing.Invoke();
        }

        [Fact]
        public void Invoke___On_a_Nothing_with_SomethingAction___Nothing_happens()
        {
            var nothing = default(Maybe<int>);
            var somethingAction = A.Fake<Action<int>>();

            nothing.Invoke(somethingAction);

            A.CallTo(somethingAction).MustNotHaveHappened();
        }

        [Fact]
        public void Invoke___On_a_Nothing_with_NothingAction___Invokes_NothingAction()
        {
            var nothing = default(Maybe<int>);
            var nothingAction = A.Fake<Action>();

            nothing.Invoke(actionIfNothing: nothingAction);

            A.CallTo(nothingAction).MustHaveHappened(1, Times.Exactly);
        }

        [Fact]
        public void Invoke___On_a_Nothing_with_SomethingAction_and_NothingAction___Invokes_NothingAction()
        {
            var nothing = default(Maybe<int>);
            var somethingAction = A.Fake<Action<int>>();
            var nothingAction = A.Fake<Action>();

            nothing.Invoke(somethingAction, nothingAction);

            A.CallTo(somethingAction).MustNotHaveHappened();
            A.CallTo(nothingAction).MustHaveHappened(1, Times.Exactly);
        }

        #endregion Invoke
    }
}