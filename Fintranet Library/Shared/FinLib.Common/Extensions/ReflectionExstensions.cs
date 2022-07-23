using System;
using FinLib.Common.Helpers;

namespace FinLib.Common.Extensions
{
    public static class ReflectionExstensions
    {
        public static bool IsEnum(this Type type)
        {
            return Helpers.ReflectionHelper.IsEnum(type);
        }

        public static bool HasProperty(this Type theType, string property)
        {
            return ReflectionHelper.HasProperty(theType, property);
        }

        public static bool HasProperty(this object obj, string property)
        {
            return ReflectionHelper.HasProperty(obj?.GetType(), property);
        }
    }
}
