using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FluentAssertions;
using LambdaEcho.MaybeType;
using LambdaEcho.MaybeType.Extensions;
using Xunit;

namespace MaybeType.Tests.Extensions
{
    public class IEnumerableExtensionsTests
    {
        [Fact]
        public void InvokeEach___On_IEnumerable_of_Maybe___All_Maybe_invoked()
        {
            var array = new[] { Maybe.Create(42), Maybe.Nothing<int>(), Maybe.Create(17) };

            var results = new Collection<string>();
            array.InvokeEach(m => results.Add(m.ToString()), () => results.Add("Nothing"));

            results.Count.Should().Be(3);
            results[0].Should().BeEquivalentTo("42");
            results[1].Should().BeEquivalentTo("Nothing");
            results[2].Should().BeEquivalentTo("17");
        }

        [Fact]
        public void InvokeEach___On_IEnumerable_of_Null___Throws_ArgumentNullException()
        {
            var maybes = (IEnumerable<Maybe<int>>) null;

            Action act = () =>
            {
                var results = new Collection<string>();
                maybes.InvokeEach(m => results.Add(m.ToString()), () => results.Add("Nothing"));
            };

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void MapEach___On_IEnumerable_of_Maybe___All_Maybe_mapped()
        {
            var array = new[] { Maybe.Create(42), Maybe.Nothing<int>(), Maybe.Create(17) };

            var results = array.MapEach(m => m.ToString(), () => "Nothing");

            var list = results.ToList();
            list.Count.Should().Be(3);
            list[0].Should().BeEquivalentTo(Maybe.Create("42"));
            list[1].Should().BeEquivalentTo(Maybe.Create("Nothing"));
            list[2].Should().BeEquivalentTo(Maybe.Create("17"));
        }

        [Fact]
        public void MapEach___On_IEnumerable_of_Null___Throws_ArgumentNullException()
        {
            var maybes = (IEnumerable<Maybe<int>>) null;

            Action act = () =>
            {
                var _ = maybes.MapEach(m => m.ToString(), () => "Nothing").ToList();
            };

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void MapEach_Maybe___On_IEnumerable_of_Maybe___All_Maybe_mapped()
        {
            var array = new[] { Maybe.Create(42), Maybe.Nothing<int>(), Maybe.Create(17) };

            var results = array.MapEach(m => Maybe.Create(m.ToString()), () => Maybe.Create("Nothing"));

            var list = results.ToList();
            list.Count.Should().Be(3);
            list[0].Should().BeEquivalentTo(Maybe.Create("42"));
            list[1].Should().BeEquivalentTo(Maybe.Create("Nothing"));
            list[2].Should().BeEquivalentTo(Maybe.Create("17"));
        }

        [Fact]
        public void MapEach_Maybe___On_IEnumerable_of_Null___Throws_ArgumentNullException()
        {
            var maybes = (IEnumerable<Maybe<int>>) null;

            Action act = () =>
            {
                var _ = maybes.MapEach(m => Maybe.Create(m.ToString()), () => Maybe.Create("Nothing")).ToList();
            };

            act.Should().Throw<ArgumentNullException>();
        }
    }
}