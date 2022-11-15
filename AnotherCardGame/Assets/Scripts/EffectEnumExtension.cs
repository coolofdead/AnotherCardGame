using System;
using System.Linq;

public static class EffectEnumExtension
{
    public static T[] GetFlags<T>(this T flagsEnumValue) where T : Enum
    {
        return Enum
            .GetValues(typeof(T))
            .Cast<T>()
            .Where(e => flagsEnumValue.HasFlag(e))
            .ToArray();
    }
}
