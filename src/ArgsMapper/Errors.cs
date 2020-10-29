using System;

namespace ArgsMapper.Errors
{
    ///<summary>
    /// Occurs when the argument cannot be parsed.
    ///</summary>
    public class TypeOfArgumentException : Exception
    {
        public TypeOfArgumentException(string arg) : base($"Error while parsing the argument {arg}")
        {
        }
    }


    ///<summary>
    /// Occurs when name of argument cannot be received.
    ///</summary>
    public class InvalidArgumentNameException : Exception
    {
        public InvalidArgumentNameException(string arg) : base($"Error getting the name of the argument {arg}")
        {

        }
    }

    ///<summary>
    /// Occurs when value of argument cannot be received.
    ///</summary>
    public class InvalidArgumentValueException : Exception
    {
        public InvalidArgumentValueException(string arg) : base($"Error getting the value of the argument {arg}")
        {

        }
    }

    ///<summary>
    /// Occurs when an argument is passed in the console that is not in the model.
    ///</summary>
    public class MissingDefinitionException : Exception
    {
        public MissingDefinitionException(string arg) : base($"In the model does`t exist definition for argument {arg}")
        {

        }
    }

    ///<summary>
    /// Occurs when model of arguments contains doublicate of arguments definition.
    ///</summary>
    public class DuplicateDefinitionException : Exception
    {
        public DuplicateDefinitionException(string property) : base($"There is doublicate definition in the model of property {property}")
        {

        }
    }

    ///<summary>
    /// Occurs when model has non Boolean type of argument with Flag attribute
    ///</summary>
    public class InvalidModelTypeForFlagException : Exception
    {
        public InvalidModelTypeForFlagException(string property) : base($"There is non Boolean type for property {property}")
        {

        }
    }

    public class MissingAttributeException : Exception
    {
        public MissingAttributeException(string property) : base($"Property {property} does`t contain an annotation ArgDefinitionAttribute")
        {

        }
    }

    ///<summary>
    /// Некорректный формат аргументе
    ///</summary>
    public class IncorrectFormatException : Exception
    {
        public IncorrectFormatException(string name) : base($"Incorrect format of argument {name}")
        {

        }
    }

    ///<summary>
    /// Required argument does`t exist
    ///</summary>
    public class NeedArgumentException : Exception
    {
        public NeedArgumentException(string arg) : base($"Argument {arg} is required, but does`t exist")
        {

        }
    }

}