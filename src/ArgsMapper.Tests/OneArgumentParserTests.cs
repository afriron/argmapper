using System;
using Xunit;
using ArgsMapper.Services;

namespace ArgsMapper.Tests
{
    public class OneArgumentParserTests
    {
        const String longName = "testName";
        const String shortName = "t";
        const String testValue = "testValue";

        [Fact]
        public void ParseLongNameArgument()
        {
            String testArg = $"--{longName}={testValue}";
            ArgumentFlags okFlags = ArgumentFlags.Empty;
            Int32 lastPositionalArgumentIndex = -1, okIndex = -1;

            var parsedArg = new OneArgumentParser().Parse(testArg, ref lastPositionalArgumentIndex);

            Assert.Equal(parsedArg.Name, longName);
            Assert.Equal(parsedArg.Value, testValue);
            Assert.Equal(parsedArg.Index, okIndex);
            Assert.Equal(parsedArg.ArgumentFlags, okFlags);
        }

        [Fact]
        public void ParseShortNameArgument()
        {
            String testArg = $"-{shortName}={testValue}";
            ArgumentFlags okFlags = ArgumentFlags.HasShortFormat;
            Int32 lastPositionalArgumentIndex = -1, okIndex = -1;

            var parsedArg = new OneArgumentParser().Parse(testArg, ref lastPositionalArgumentIndex);

            Assert.Equal(parsedArg.Name, shortName);
            Assert.Equal(parsedArg.Value, testValue);
            Assert.Equal(parsedArg.Index, okIndex);
            Assert.Equal(parsedArg.ArgumentFlags, okFlags);
        }

        [Fact]
        public void ParseLongNameFlag()
        {
            String testArg = $"--{longName}";
            ArgumentFlags okFlags = ArgumentFlags.IsFlag;
            Int32 lastPositionalArgumentIndex = -1, okIndex = -1;

            var parsedArg = new OneArgumentParser().Parse(testArg, ref lastPositionalArgumentIndex);

            Assert.Equal(parsedArg.Name, longName);
            Assert.Equal(parsedArg.Value, String.Empty);
            Assert.Equal(parsedArg.Index, okIndex);
            Assert.Equal(parsedArg.ArgumentFlags, okFlags);
        }

        [Fact]
        public void ParseShortNameFlag()
        {
            String testArg = $"-{shortName}";
            ArgumentFlags okFlags = ArgumentFlags.IsFlag | ArgumentFlags.HasShortFormat;
            Int32 lastPositionalArgumentIndex = -1, okIndex = -1;

            var parsedArg = new OneArgumentParser().Parse(testArg, ref lastPositionalArgumentIndex);

            Assert.Equal(parsedArg.Name, shortName);
            Assert.Equal(parsedArg.Value, String.Empty);
            Assert.Equal(parsedArg.Index, okIndex);
            Assert.Equal(parsedArg.ArgumentFlags, okFlags);
        }

        [Fact]
        public void ArgsWithPositionByCount()
        {
            String testArg = $"{testValue}";
            ArgumentFlags okFlags = ArgumentFlags.IsPositionalArgument;
            Int32 lastPositionalArgumentIndex = -1, okIndex = 0;

            var parsedArg = new OneArgumentParser().Parse(testArg, ref lastPositionalArgumentIndex);

            Assert.Equal(parsedArg.Name, String.Empty);
            Assert.Equal(parsedArg.Value, testValue);
            Assert.Equal(parsedArg.Index, okIndex);
            Assert.Equal(parsedArg.ArgumentFlags, okFlags);
        }

        [Fact]
        public void ArgsWithTypeOfArgumentException()
        {
            String testArg = $"---{longName}={testValue}";
            Int32 lastPositionalArgumentIndex = -1;

            var parser = new OneArgumentParser();

            Assert.Throws<Errors.TypeOfArgumentException>(() => (parser.Parse(testArg, ref lastPositionalArgumentIndex)));
        }

        [Fact]
        public void ArgsWithInvalidArgumentNameException()
        {
            String testArg = $"-- {longName}";
            Int32 lastPositionalArgumentIndex = -1;

            var parser = new OneArgumentParser();

            Assert.Throws<Errors.InvalidArgumentNameException>(() => (parser.Parse(testArg, ref lastPositionalArgumentIndex)));
        }

        [Fact]
        public void ArgsWithInvalidArgumentValueException()
        {
            String testArg = $"--{longName}=";
            Int32 lastPositionalArgumentIndex = -1;

            var parser = new OneArgumentParser();

            Assert.Throws<Errors.InvalidArgumentValueException>(() => (parser.Parse(testArg, ref lastPositionalArgumentIndex)));
        }
    }
}