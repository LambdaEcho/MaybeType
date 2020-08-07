using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using FluentAssertions;
using LambdaEcho.MaybeType;
using Xunit;

namespace MaybeType.Tests
{
    public partial class MaybeTests
    {
        [Theory]
        [MemberData(nameof(ISerialize___TestData))]
        public void ISerialize___Serialization_and_Deserialization___Work_Properly(object maybe, string expectedSerializedString)
        {
            // Arrange
            string serializedString;
            var binaryFormatter = new BinaryFormatter();

            using (var memoryStream = new MemoryStream())
            {
                // Act Serialization
                binaryFormatter.Serialize(memoryStream, maybe);

                // Assert Serialization
                memoryStream.Position = 0;
                using (var streamReader = new StreamReader(memoryStream))
                {
                    serializedString = streamReader.ReadToEnd();
                }
            }

            serializedString.Should().EndWithEquivalent(expectedSerializedString);

            // Act DeSerialization
            var byteArray = Encoding.ASCII.GetBytes(serializedString);
            using (var memoryStream = new MemoryStream(byteArray))
            {
                var reconstructedMaybe = binaryFormatter.Deserialize(memoryStream);

                // Assert DeSerialization
                maybe.Equals(reconstructedMaybe).Should().BeTrue();
            }
        }

        public static IEnumerable<object[]> ISerialize___TestData()
        {
            return new List<object[]>
            {
                new object[]
                {
                    Maybe.Create("Hello world!"), "\u0001h\u0001m\0\u0001\u0001\u0002\0\0\0\u0001\u0006\u0003\0\0\0\fHello world!\v"
                },
                new object[] { Maybe.Nothing<string>(), "\u0001h\u0001m\0\u0001\u0001\u0002\0\0\0\0\n\v" },
                new object[] { Maybe.Create(42), "\u0001h\u0001m\0\0\u0001\b\u0002\0\0\0\u0001*\0\0\0\v" },
                new object[] { Maybe.Nothing<int>(), "\u0001h\u0001m\0\0\u0001\b\u0002\0\0\0\0\0\0\0\0\v" }
            };
        }
    }
}