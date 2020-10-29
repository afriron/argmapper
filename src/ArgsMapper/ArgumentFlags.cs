using System;

namespace ArgsMapper
{
    ///<summary>
    /// Contains flags that define the type of command-line argument: short syntax, positional argument, and flag.
    ///</summary>
    [Flags]
    internal enum ArgumentFlags
    {
        Empty = 0,
        IsFlag = 1,
        IsPositionalArgument = 2,
        HasShortFormat = 4
    }
}