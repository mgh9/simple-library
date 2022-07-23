using FinLib.Common.Helpers;
using System;

namespace FinLib.Common.Extensions
{
    public static class TypeExtensions
    {
        public static string GetDescription(this Type type, bool returnClassNameIfNoDescription = false)
        {
            return TypeHelper.GetDescription(type, returnClassNameIfNoDescription);
        }

        public static string GetDescription(this object @object, bool returnClassNameIfNoDescription = false)
        {
            return TypeHelper.GetDescription(@object, returnClassNameIfNoDescription);
        }

        public static T Clone<T>(this T source)
        {
            return TypeHelper.Clone(source);
        }

        /// <summary>
        /// اگه ابجکت، مقدار داشته باشه، خود اون مقدار رو برمیگردونه، اگه نداشته باشه، دیفالت مقدار اون ابجکت (بر اساس تایپ اون) رو برمیگردونه
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="throwExceptionIfNull">if True, then if the object is null throw exception</param>
        /// <returns></returns>
        public static T GetValueOrDefault<T>(this object value, bool throwExceptionIfNull = false)
        {
            if (throwExceptionIfNull)
                value.ThrowIfNull();

            if (value is not null)
            {
                return TypeHelper.Cast(value, typeof(T));
            }

            return default;
        }

        public static T To<T>(this object value)
        {
            //value.ThrowIfNull();
            if (value is not null)
            {
                return TypeHelper.Cast(value, typeof(T));
            }

            return default;
        }
    }
}
