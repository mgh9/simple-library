using FinLib.Common.Exceptions.Infra;
using FinLib.Common.Extensions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace FinLib.Common.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription<TEnum>(string enumItem)
        {
            Type type = typeof(TEnum);
            return getDescription(type, enumItem);
        }

        public static string GetDescription(Enum @enum, string enumItem)
        {
            @enum.ThrowIfNull();

            Type type = @enum.GetType();
            return getDescription(type, enumItem);
        }

        public static string GetDescription(Enum @enum)
        {
            @enum.ThrowIfNull();

            Type type = @enum.GetType();

            MemberInfo[] memInfo = type.GetMember(@enum.ToString());

            if (memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(
                                              typeof(DescriptionAttribute),
                                              false);

                if (attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return @enum.ToString();
        }

        public static string GetEnumDescriptionValue(Enum enumType)
        {
            var theAttribute = enumType.GetType().GetMember(enumType.ToString())
                           .First()
                           .GetCustomAttribute<DescriptionAttribute>();

            if (theAttribute == null)
                return null;

            return theAttribute.Description;
        }

        private static string getDescription(Type enumType, string enumItem)
        {
            var name = Enum.GetNames(enumType).Where(f => f.Equals(enumItem, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }

            var field = enumType.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }

        /// <summary>
        /// Type e enum ro (dar Assembly haaye load shode) az ruye esm e enum, barmigardune
        /// </summary>
        /// <param name="enumName"></param>
        /// <returns></returns>
        public static Type GetEnumType(string enumName)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var type = assembly.GetType(enumName);

                if (Nullable.GetUnderlyingType(type) != null)
                {
                    type = Nullable.GetUnderlyingType(type);
                }

                if (type == null)
                    continue;

                if (type.IsEnum)
                    return type;
            }

            return null;
        }

        /// <summary>
        /// meghdar e Enum ro az ruye Value (text) un barmigardune (Parse mkone b T)
        /// </summary>
        /// <typeparam name="T">Type of the returning Enum</typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, false);
        }
    }
}
