using System;

namespace ArgsMapper.Services
{
    ///<summary>
    /// Parses one command line argument
    /// Argument spreads to Name and Value. Also set flags in ArgumentFlags if this argument has short format, is flag or this is poristion argument
    ///</summary>
    internal sealed class OneArgumentParser
    {
        #region Constants ARG_PREFIX and NAME_VALUE_SPLITTER
        const char ARG_PREFIX = '-';
        const char NAME_VALUE_SPLITTER = '=';
        #endregion

        #region Fields
        private string name = "";
        private string value = "";
        private int index = -1;
        private ArgumentFlags argumentFlags = 0;
        #endregion

        #region Constructors
        ///<summary>
        /// Parses one command line argument
        ///</summary>
        public OneArgumentParser()
        {
            index = -1;
        }
        #endregion

        #region Public method
        ///<summary>
        /// Parses one command line argument
        ///</summary>
        public OneArgumentParser Parse(string argument, ref int lastPositionalArgumentIndex)
        {
            string argName = "", argValue = "";
            if (TryGetNotPositionalArgOrFlag(argument, ref argName, ref argValue, ref argumentFlags))
            {
                name = argName;
                value = argValue;
                return this;
            }

            // If this is not argument and flag, then this is positional argument
            value = argument;
            index = ++lastPositionalArgumentIndex;
            argumentFlags = argumentFlags | ArgumentFlags.IsPositionalArgument;
            return this;
        }

        #endregion

        #region Public property

        // Get name of argument
        public string Name { get => name; }

        // Get value of argument
        public string Value { get => value; }

        // Get index of positional argument
        public int Index { get => index; }

        // Get additional info about this argument as ArgumentFlags
        public ArgumentFlags ArgumentFlags { get => argumentFlags; }
        #endregion

        #region Private method
        private bool TryGetNotPositionalArgOrFlag(string argument, ref string argName, ref string argValue, ref ArgumentFlags flags)
        {
            int prefixCount = GetPrefixCount(argument);

            switch (prefixCount)
            {
                case 0:
                    return false;

                case 1:
                    flags = flags | ArgumentFlags.HasShortFormat;
                    break;

                case 2:
                    break;

                default:
                    throw new Errors.TypeOfArgumentException(argument);
            }

            var cleanArg = argument.Remove(0, prefixCount);
            ExtractNameAndValueFromArg(cleanArg, ref argName, ref argValue, ref flags);

            return true;
        }

        private int GetPrefixCount(string arg)
        {
            return arg.Length - arg.TrimStart(ARG_PREFIX).Length;
        }

        private void ExtractNameAndValueFromArg(string arg, ref string argName, ref string argValue, ref ArgumentFlags flags)
        {
            if (string.IsNullOrWhiteSpace(arg) || char.IsWhiteSpace(arg[0]))
            {
                throw new Errors.InvalidArgumentNameException(arg);
            }

            var nameValue = arg.Split(NAME_VALUE_SPLITTER, 2);

            argName = nameValue[0];

            // If argument does`n value, then it is a flag
            if (nameValue.Length == 1)
            {
                flags = flags | ArgumentFlags.IsFlag;
                return;
            }

            argValue = nameValue[1];

            // If argument contains "=", but don`t contain
            if (nameValue.Length > 1 && string.IsNullOrWhiteSpace(argValue))
            {
                throw new Errors.InvalidArgumentValueException(arg);
            }
        }
    }
    #endregion
}