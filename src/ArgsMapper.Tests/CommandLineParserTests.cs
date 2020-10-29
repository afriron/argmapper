using System;
using System.Threading.Tasks;
using Xunit;

namespace ArgsMapper.Tests
{
    public class CommandLineParserTests
    {
        [Fact]
        public void GetFlagByFullName()
        {
            var testArg = new string[] { "--test" };
            var model = new TestCommandLineTemplate();

            var parser = new CommandLineParser();
            parser.LoadModelFromArg(testArg, ref model);

            Assert.True(model.FlagT);
        }

        [Fact]
        public void GetFlagByShortName()
        {
            var testArg = new string[] { "-b" };
            var model = new TestCommandLineTemplate();

            var parser = new CommandLineParser();
            parser.LoadModelFromArg(testArg, ref model);

            Assert.True(model.FlagB);
        }

        [Fact]
        public void GetPositionArg()
        {
            var okArgName = "bravo";
            var testArg = new string[] { "-b", okArgName };
            var model = new TestCommandLineTemplate();

            var parser = new CommandLineParser();
            parser.LoadModelFromArg(testArg, ref model);

            Assert.Equal(model.PosArg1, okArgName);
        }

        [Fact]
        public async void NotRequiredArg()
        {
            var testArg = new string[] { };
            var model = new TestCommandLineTemplateWithRequired();

            var parser = new CommandLineParser();

            await Assert.ThrowsAsync<Errors.NeedArgumentException>(() => Task.Run(() => { parser.LoadModelFromArg(testArg, ref model); }));
        }

        [Fact]
        public void FullModelCheck()
        {
            var okValueArg = "OkValue";
            var posArg1 = "bravo1";
            var posArg2 = "/bravo/2";

            var testArg = new string[] { "-b", $"--test1={okValueArg}", $"--reqTest={okValueArg}", "--test", posArg1, posArg2 };

            var model = new TestCommandLineTemplateWithRequired();

            var parser = new CommandLineParser();
            parser.LoadModelFromArg(testArg, ref model);

            Assert.True(model.FlagB);
            Assert.True(model.FlagT);
            Assert.False(model.FlagL);

            Assert.Equal(model.ArgT, okValueArg);
            Assert.Equal(model.ReqArg, okValueArg);

            Assert.Equal(model.PosArg1, posArg1);
            Assert.Equal(model.PosArg2, posArg2);
        }

        [Fact]
        public async void GetUnknownFlag()
        {
            var testArg = new string[] { "--badflag" };
            var model = new TestCommandLineTemplate();

            var parser = new CommandLineParser();
            await Assert.ThrowsAsync<Errors.MissingDefinitionException>(() => Task.Run(() => { parser.LoadModelFromArg(testArg, ref model); }));

        }
    }
}