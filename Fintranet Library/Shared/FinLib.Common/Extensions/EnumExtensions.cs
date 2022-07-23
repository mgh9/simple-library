using FinLib.Common.Helpers;
using System;

namespace FinLib.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<TEnum>(this TEnum @enum, string enumItem)
        {
            return EnumHelper.GetDescription<TEnum>(enumItem);
        }

        public static string GetDescription(this Enum @enum, string enumItem)
        {
            return EnumHelper.GetDescription(@enum, enumItem);
        }

        public static string GetDescription(this Enum @enum)
        {
            return EnumHelper.GetDescription(@enum);
        }
    }
}
