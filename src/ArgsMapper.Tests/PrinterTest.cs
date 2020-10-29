using System.Reflection;
using Xunit;

[assembly: AssemblyDescriptionAttribute("This is test library")]

namespace ArgsMapper.Tests
{
    public class PrinterTest
    {
        [Fact]
        public void CommonPrinterTest()
        {
            var printer = new Services.Printer();
            printer.PrintHelp();
        }
    }
}