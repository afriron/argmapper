using System;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using ArgsMapper.Services;

namespace ArgsMapper
{
    ///<summary>
    /// Type CommandLineParser contains methods of create model for accessing command line parameters
    ///</summary>
    public class CommandLineParser
    {
        ///<summary>
        /// Creates a model for accessing command line parameters
        ///</summary>
        public void LoadModelFromArg<T>(IList<String> args, ref T model)
        {
            DefinitionsOfModelCache modelCache = new DefinitionsOfModelCache();
            modelCache.CreateFor(typeof(T));

            BindValuesToModel(args, ref modelCache, ref model);

            CheckBindedModelAndThrowException(ref model, ref modelCache);
        }

        private void BindValuesToModel<T>(IList<String> args, ref DefinitionsOfModelCache modelCache, ref T model)
        {
            Int32 lastPositionalArgumentIndex = -1;

            foreach (var arg in args)
            {
                var parser = new Services.OneArgumentParser();
                var parsedArgument = parser.Parse(arg, ref lastPositionalArgumentIndex);

                PropertyInfo property = null;

                if (parser.ArgumentFlags.HasFlag(ArgumentFlags.IsPositionalArgument))
                {
                    property = modelCache.GetProperty(parser.Index);
                    if (property == null)
                        throw new Errors.MissingDefinitionException(arg);

                    property.SetValue(model, parser.Value);
                    Debug.WriteLine($"Set value of positional argument {parser.Index}: {parser.Value}");
                }
                else
                {
                    property = modelCache.GetProperty(parser.Name, parser.ArgumentFlags.HasFlag(ArgumentFlags.HasShortFormat), parser.ArgumentFlags.HasFlag(ArgumentFlags.IsFlag));
                    if (property == null)
                        throw new Errors.MissingDefinitionException(arg);

                    if (parser.ArgumentFlags.HasFlag(ArgumentFlags.IsFlag))
                    {
                        property.SetValue(model, true);
                        Debug.WriteLine($"Set flag {parser.Name}");
                    }
                    else
                    {
                        property.SetValue(model, parser.Value);
                        Debug.WriteLine($"Set value of argument {parser.Name}: {parser.Value}");
                    }
                }
            }
        }

        private void CheckBindedModelAndThrowException<T>(ref T model, ref DefinitionsOfModelCache modelCache)
        {
            var requredProperties = modelCache.GetRequiredProperties();
            foreach (var property in requredProperties)
            {
                if (string.IsNullOrEmpty((property.GetValue(model) as String)))
                {
                    throw new Errors.NeedArgumentException(property.Name);
                }
            }
        }
    }
}