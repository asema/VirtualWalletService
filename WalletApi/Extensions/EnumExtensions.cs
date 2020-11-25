using System;
using System.ComponentModel;
using System.Linq;

namespace WalletApi.Extensions
{
    public static class EnumExtensions
    {
        private static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetField(name)
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .SingleOrDefault();
        }

        public static string GetDescription(this Enum value)
        {
            var description = value.GetAttribute<DescriptionAttribute>();
            return description != null ? description.Description : null;
        }
    }

}