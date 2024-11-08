using System.ComponentModel;

namespace Domain.Extensions;

public static class EnumExtensions
{
    public static string GetDescription<T>(this T enumValue) where T : Enum
    {
        if (!typeof(T).IsEnum) return null;

        var description = enumValue.ToString();
        var fieldInfo = enumValue.GetType().GetField(description);
        var attrs = fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), true);
        if (attrs is { Length: > 0 })
        {
            description = ((DescriptionAttribute)attrs[0]).Description;
        }

        return description;
    }
}