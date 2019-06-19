using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter
{
    public static class EnumExtensions
    {
        public static ICollection<SystemParametersDescriptionDto> SystemParametersDescriptions<T>(this T enumerator)
        {
            var enumType = enumerator.GetType();
            var result = new List<SystemParametersDescriptionDto>();

            if (!enumType.IsEnum) return result;

            var names = Enum.GetNames(enumType);

            if (names.Length > 0)
            {
                var itemEnumType = enumType.Assembly.GetType(enumType.FullName);
                foreach (var item in names)
                {
                    var enumItem1 = itemEnumType.GetField(item);
                    var intValue = (int)enumItem1.GetValue(itemEnumType);
                    var itemInstance = (T)Enum.ToObject(enumType, intValue);
                    var systemParametersAttributes = itemInstance.GetType().GetField(item).GetCustomAttributes(typeof(SystemParametersDescriptionAttribute), false);

                    if (systemParametersAttributes.Any())
                    {
                        foreach (var subItem in systemParametersAttributes)
                        {
                            result.Add(((SystemParametersDescriptionAttribute)subItem).GetDto());
                        }
                    }
                }
            }

            return result;
        }
    }
}
