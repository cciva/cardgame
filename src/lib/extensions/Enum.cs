using System;
using System.Reflection;
using System.ComponentModel;

namespace Game.Lib
{
    public static class EnumExtensions
    {
        public static string PrettyPrint<T>(this T @enum) where T : struct, System.Enum
        {
            Type type = @enum.GetType();
            if(!type.IsEnum)
            {
                throw new InvalidOperationException("must be enum");
            }

            var m = type.GetMember(@enum.ToString());

            if(m.Length > 0)
            {
                var attrs = m[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if(attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return @enum.ToString();
        }
    }
}