using System;
using System.Reflection;

namespace ArgsMapper.Services
{
    public class Printer
    {
        public virtual void PrintHelp()
        {
            var entryAssembly = Assembly.GetEntryAssembly();

            var assemblyTitle = entryAssembly.GetCustomAttribute<AssemblyTitleAttribute>().Title;
            var version = entryAssembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;


            Console.WriteLine($"{assemblyTitle} {version}");

            var description = entryAssembly.GetCustomAttribute<AssemblyDescriptionAttribute>();
            if (description != null)
            {
                Console.WriteLine(description.Description);
            }
        }
    }
}