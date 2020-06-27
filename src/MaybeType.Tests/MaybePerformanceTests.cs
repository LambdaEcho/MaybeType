using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using LambdaEcho.MaybeType;
using Xunit;
using Xunit.Abstractions;

namespace MaybeType.Tests
{
    public class MaybePerformanceTests
    {
        private const int Loops = 10_000_000;

        private readonly ITestOutputHelper _testOutputHelper;

        public MaybePerformanceTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Memory___Maybe_vs_Nullable()
        {
            const double value = 3.1415d;
            var something = new Maybe<double>(value);
            var nullable = new double?(value);

            _testOutputHelper.WriteLine($"IntPtr.Size: {IntPtr.Size}");
            _testOutputHelper.WriteLine($"size(double): {sizeof(double)}");
            _testOutputHelper.WriteLine($"size(Maybe<double>): {Marshal.SizeOf(something)}");
            _testOutputHelper.WriteLine($"size(Nullable<double>): {Marshal.SizeOf(nullable)}");
            _testOutputHelper.WriteLine($"size(Maybe<int>): {Marshal.SizeOf(default(Maybe<int>))}");
            _testOutputHelper.WriteLine($"size(bool): {sizeof(bool)}");
        }

        [Fact]
        public void Performance_Constructor___Maybe_of_String_vs_String()
        {
            var list1 = new List<Maybe<string>>();
            var stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            for (var i = 0; i < Loops; i++)
            {
                list1.Add(new Maybe<string>(i.ToString()));
            }

            stopwatch1.Stop();

            var list2 = new List<string>();
            var stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            for (var i = 0; i < Loops; i++)
            {
                list2.Add(i.ToString());
            }

            stopwatch2.Stop();


            _testOutputHelper.WriteLine($"Maybe<string>: {stopwatch1.ElapsedMilliseconds}");
            _testOutputHelper.WriteLine($"string: {stopwatch2.ElapsedMilliseconds}");
        }

        [Fact]
        public void Performance_Constructor___Maybe_of_Value_Type_vs_Value_Type_vs_Nullable_of_Value_Type()
        {
            var list1 = new List<Maybe<int>>();
            var stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            for (var i = 0; i < Loops; i++)
            {
                list1.Add(new Maybe<int>(i));
            }

            stopwatch1.Stop();

            var list2 = new List<int>();
            var stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            for (var i = 0; i < Loops; i++)
            {
                list2.Add(i);
            }

            stopwatch2.Stop();

            var list3 = new List<int?>();
            var stopwatch3 = new Stopwatch();
            stopwatch3.Start();
            for (var i = 0; i < Loops; i++)
            {
                list3.Add(i);
            }

            stopwatch3.Stop();

            _testOutputHelper.WriteLine($"Maybe<int>: {stopwatch1.ElapsedMilliseconds}");
            _testOutputHelper.WriteLine($"int: {stopwatch2.ElapsedMilliseconds}");
            _testOutputHelper.WriteLine($"Nullable<int>: {stopwatch3.ElapsedMilliseconds}");
        }

        [Fact]
        public void Performance_Value___Maybe_of_Value_Type_vs_Value_Type_vs_Nullable_of_Value_Type()
        {
            var value = 23;
            var something = new Maybe<int>(42);
            var nullable = new int?(17);

            var stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            for (var i = 0; i < Loops; i++)
            {
                var x = value;
            }

            stopwatch1.Stop();

            var stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            for (var i = 0; i < Loops; i++)
            {
                var x = something.Value;
            }

            stopwatch2.Stop();

            var stopwatch3 = new Stopwatch();
            stopwatch3.Start();
            for (var i = 0; i < Loops; i++)
            {
                var x = nullable.Value;
            }

            stopwatch3.Stop();

            _testOutputHelper.WriteLine($"Maybe<int>: {stopwatch1.ElapsedMilliseconds}");
            _testOutputHelper.WriteLine($"int: {stopwatch2.ElapsedMilliseconds}");
            _testOutputHelper.WriteLine($"Nullable<int>: {stopwatch3.ElapsedMilliseconds}");
        }
    }
}