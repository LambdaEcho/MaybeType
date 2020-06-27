using FluentAssertions;
using LambdaEcho.MaybeType;
using Xunit;

namespace MaybeType.Tests
{
    public class MaybeFactoryTests
    {
        [Fact]
        public void Create___Invoked_with_a_String_value___Returns_a_Something()
        {
            var something = Maybe.Create("Hello World!");

            something.HasValue.Should().BeTrue();
            something.Value.Should().BeEquivalentTo("Hello World!");
        }

        [Fact]
        public void Create___Invoked_with_Null_String___Returns_a_Nothing()
        {
            var nothing = Maybe.Create((string) null);

            nothing.HasValue.Should().BeFalse();
        }

        [Fact]
        public void Nothing___Invoked___Returns_a_Nothing()
        {
            var nothing = Maybe.Nothing<float>();

            nothing.HasValue.Should().BeFalse();
        }
    }
}