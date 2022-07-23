using FinLib.Common.Exceptions.Infra;
using FinLib.Common.Extensions;
using System;
using System.Reflection;
using System.Text;

namespace FinLib.Common.Helpers
{
    public static class ReflectionHelper
    {
        public static bool IsMethodExists(string assemblyName, string fullMethodName)
        {
            assemblyName.ThrowIfNull();
            fullMethodName.ThrowIfNull();

            var parts = fullMethodName.Split('.');
            if (parts.Length == 0)
            {
                throw new InvalidModelException(nameof(fullMethodName) + " is not a valid methodname");
            }
            
            StringBuilder className = new StringBuilder();
            for (int i = 0; i < parts.Length - 1; i++)
            {
                className.Append(parts[i]);
                if (i != parts.Length - 2)
                    className.Append('.');
            }

            var methodName = parts[^1];

            var theAssembly = Assembly.Load(assemblyName);
            if (theAssembly == null)
                return false;

            Type theType = theAssembly.GetType(className.ToString());
            if (theType == null)
                return false;

            var theMethod = theType.GetMethod(methodName);

            return theMethod != null;
        }

        public static bool HasMethod(object objectToCheck, string methodName)
        {
            objectToCheck.ThrowIfNull();
            methodName.ThrowIfNull();

            var type = objectToCheck.GetType();
            return type.GetMethod(methodName) != null;
        }

        public static bool HasProperty(Type obj, string propertyName)
        {
            obj.ThrowIfNull();
            propertyName.ThrowIfNull();
            
            return obj.GetProperty(propertyName) != null;
        }

        /// <summary>
        /// age Enum bashe ya NullableEnum baash ~> true, else ~> false
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsEnum(Type type)
        {
            type.ThrowIfNull();

            // age khodesh Enum hast k ~> true
            if (type.IsEnum)
                return true;

            // else, check kon shayad NullableEnum bashe
            Type itsUndrlyingType = Nullable.GetUnderlyingType(type);
            return (itsUndrlyingType != null) && itsUndrlyingType.IsEnum;
        }
    }
}
