/*
* This file contains class that describing the model of expected command line arguments.
*/

using System;

namespace ArgsMapper.Annotation
{
    /// <summary>
    /// The ArgDefinitionAttribute class contains arguments to the model class of the command-line argument.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ArgDefinitionAttribute : Attribute
    {
        private String longName;
        private String shortName;
        private Boolean isFlag;
        private Int32 index;
        private Boolean required;
        private String about;
        public ArgDefinitionAttribute()
        {
            isFlag = false;
            index = -1;
            required = false;
        }

        /// <summary>
        /// Gets or sets a full name of argument
        /// </summary>
        public String LongName { get => longName; set => longName = value; }

        /// <summary>
        ///  Gets or sets a short name of argument
        /// </summary>
        public String ShortName { get => shortName; set => shortName = value; }

        ///<summary>
        /// Gets or sets a short name of argument
        ///</summary>
        public Boolean IsFlag { get => isFlag; set => isFlag = value; }

        ///<summary>
        /// Gets or sets a index of positional argument
        ///</summary>
        public Int32 Index { get => index; set => index = value; }

        ///<summary>
        /// Gets or sets a whether the element is required
        ///</summary>
        public Boolean Required { get => required; set => required = value; }

        /// <summary>
        ///  Gets or sets a info about argument
        /// </summary>
        public String About { get => about; set => about = value; }
    }
}