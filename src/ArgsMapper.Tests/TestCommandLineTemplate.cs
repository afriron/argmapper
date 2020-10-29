using System;
using ArgsMapper.Annotation;

namespace ArgsMapper.Tests
{
    class TestCommandLineTemplate
    {

        [ArgDefinition(ShortName = "b", IsFlag = true)]
        public Boolean FlagB { get; set; }

        [ArgDefinition(LongName = "lost", IsFlag = true)]
        public Boolean FlagL { get; set; }

        [ArgDefinition(LongName = "test1", About = "Print test")]
        public String ArgT { get; set; }

        [ArgDefinition(LongName = "test", IsFlag = true, About = "Print test")]
        public Boolean FlagT { get; set; }

        [ArgDefinition(Index = 0)]
        public String PosArg1 { get; set; }

        [ArgDefinition(Index = 1)]
        public String PosArg2 { get; set; }
    }

    class TestCommandLineTemplateWithRequired : TestCommandLineTemplate
    {
        [ArgDefinition(LongName = "reqTest", Required = true, About = "Is required argument")]
        public String ReqArg { get; set; }
    }
}