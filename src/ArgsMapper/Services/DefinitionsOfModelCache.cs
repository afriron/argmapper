using System;
using System.Collections.Generic;
using System.Reflection;
using ArgsMapper.Annotation;
using System.Linq;

namespace ArgsMapper.Services
{
    ///<summary>
    /// Cache of arguments definition from arguments model
    ///</summary>
    internal sealed class DefinitionsOfModelCache
    {
        private Dictionary<ArgDefinitionAttribute, PropertyInfo> storage = new Dictionary<ArgDefinitionAttribute, PropertyInfo>();

        ///<summary>
        /// Loads attributes of their model class into the dictionary
        ///</summary>
        public void CreateFor(Type modelType)
        {
            PropertyInfo[] modelProperties = modelType.GetProperties();

            foreach (var property in modelProperties)
            {
                ArgDefinitionAttribute attrOfField = property.GetCustomAttribute<ArgDefinitionAttribute>();
                if (attrOfField == null)
                {
                    throw new Errors.MissingAttributeException(property.Name);
                }

                if (string.IsNullOrEmpty(attrOfField.LongName + attrOfField.ShortName))
                {
                    attrOfField.LongName = property.Name;
                }

                storage.Add(attrOfField, property);
            }
        }

        public PropertyInfo GetProperty(int index)
        {
            return storage.FirstOrDefault(t => t.Key.Index == index).Value;
        }

        public PropertyInfo GetProperty(string name, bool hasShortFormat, bool isFlag)
        {
            return storage.FirstOrDefault(t => name == (hasShortFormat ? t.Key.ShortName : t.Key.LongName) && isFlag == t.Key.IsFlag).Value;
        }
        public IEnumerable<PropertyInfo> GetRequiredProperties()
        {
            return storage.Where(t => t.Key.Required == true).Select(t => t.Value);
        }
    }
}