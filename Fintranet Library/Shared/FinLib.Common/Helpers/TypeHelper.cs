using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace FinLib.Common.Helpers
{
    public static class TypeHelper
    {
        public static dynamic Cast(dynamic pObjectValue, Type pCastTo)
        {
            return Convert.ChangeType(pObjectValue, pCastTo);
        }

        public static IEnumerable<Type> FindDerivedTypes(Assembly assembly, Type baseType)
        {
            return assembly.GetTypes().Where(t => baseType.IsAssignableFrom(t));
        }

        public static IEnumerable<Type> FindDerivedTypesExcludeItSelf(Assembly assembly, Type baseType)
        {
            return assembly.GetTypes().Where(t => t != baseType &&
                                                  baseType.IsAssignableFrom(t));
        }

        public static IEnumerable<Type> GetAllTypesImplementingOpenGenericType(Assembly assembly, Type openGenericType)
        {
            return from x in assembly.GetTypes()
                   from z in x.GetInterfaces()
                   let y = x.BaseType
                   where
                   (y != null && y.IsGenericType &&
                   openGenericType.IsAssignableFrom(y.GetGenericTypeDefinition())) ||
                   (z.IsGenericType &&
                   openGenericType.IsAssignableFrom(z.GetGenericTypeDefinition()))
                   select x;
        }

        /// <summary>
        /// افزودن یک پراپرتی به همراه مقدارش، به ابجکتِ داینامیک
        /// </summary>
        /// <param name="expandoObject"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        public static void AddProperty(ExpandoObject expandoObject, string propertyName, object propertyValue)
        {
            var exDict = expandoObject as IDictionary<string, object>;
            if (exDict.ContainsKey(propertyName))
                exDict[propertyName] = propertyValue;
            else
                exDict.Add(propertyName, propertyValue);
        }

        public static string GetDescription(Type type, bool returnClassNameIfNoDescription = false)
        {
            var descriptions = (DescriptionAttribute[])type.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (descriptions.Length == 0)
            {
                return returnClassNameIfNoDescription ? type.Name : null;
            }

            return descriptions[0].Description;
        }

        public static string GetDescription(object @object, bool returnClassNameIfNoDescription = false)
        {
            return GetDescription(@object.GetType(), returnClassNameIfNoDescription);
        }

        public static T Clone<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}
