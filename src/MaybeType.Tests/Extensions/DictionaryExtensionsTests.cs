using System;
using System.Collections.Generic;
using FluentAssertions;
using LambdaEcho.MaybeType;
using LambdaEcho.MaybeType.Extensions;
using Xunit;

namespace MaybeType.Tests.Extensions
{
    public class DictionaryExtensionsTests
    {
        [Fact]
        public void Dictionary_TryGetValueMaybe___For_existing_Key___Returns_a_Something()
        {
            var dictionary = new Dictionary<int?, bool>
            {
                { 42, true }
            };

            var maybe = dictionary.TryGetValueMaybe(42);

            maybe.Value.Should().BeTrue();
        }

        [Fact]
        public void Dictionary_TryGetValueMaybe___For_not_existing_Key___Returns_a_Nothing()
        {
            var dictionary = new Dictionary<int?, bool>
            {
                { 42, true }
            };

            var maybe = dictionary.TryGetValueMaybe(0);

            maybe.HasValue.Should().BeFalse();
        }

        [Fact]
        public void Dictionary_TryGetValueMaybe___For_NULL_Key___Throws_ArgumentNullException()
        {
            var dictionary = new Dictionary<int?, bool>
            {
                { 43, true }
            };

            Action act = () =>
            {
                var _ = dictionary.TryGetValueMaybe(null);
            };

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Dictionary_TryGetValueMaybe___On_NULL_dictionary___Throws_ArgumentNullException()
        {
            const int searchedKey = 42;
            Dictionary<int, int> dictionary = null;

            Action act = () =>
            {
                // ReSharper disable once ExpressionIsAlwaysNull
                var _ = dictionary.TryGetValueMaybe(searchedKey);
            };

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void DictionaryWithMaybeValue_TryGetValueMaybe___For_existing_Key___Returns_a_Something()
        {
            var dictionary = new Dictionary<int?, Maybe<bool>>
            {
                { 42, new Maybe<bool>(true) }
            };

            var maybe = dictionary.TryGetValueMaybe(42);

            maybe.Value.Should().BeTrue();
        }

        [Fact]
        public void DictionaryWithMaybeValue_TryGetValueMaybe___For_not_existing_Key___Returns_a_Nothing()
        {
            var dictionary = new Dictionary<int?, Maybe<bool>>
            {
                { 42, new Maybe<bool>(true) }
            };

            var maybe = dictionary.TryGetValueMaybe(0);

            maybe.HasValue.Should().BeFalse();
        }

        [Fact]
        public void DictionaryWithMaybeValue_TryGetValueMaybe___For_NULL_Key___Throws_ArgumentNullException()
        {
            var dictionary = new Dictionary<int?, Maybe<bool>>
            {
                { 43, new Maybe<bool>(true) }
            };

            Action act = () =>
            {
                var _ = dictionary.TryGetValueMaybe(null);
            };

            act.Should().Throw<ArgumentNullException>();
        }


        [Fact]
        public void DictionaryWithMaybeValue_TryGetValueMaybe___On_NULL_dictionary___Throws_ArgumentNullException()
        {
            const int searchedKey = 42;
            Dictionary<int, Maybe<int>> dictionary = null;

            Action act = () =>
            {
                // ReSharper disable once ExpressionIsAlwaysNull
                var _ = dictionary.TryGetValueMaybe(searchedKey);
            };

            act.Should().Throw<ArgumentNullException>();
        }
    }
}