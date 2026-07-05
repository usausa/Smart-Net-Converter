namespace Smart.Converter.Converters;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

public sealed class ToStringConverterFactory : IConverterFactory
{
    private static readonly Func<object, object?> Converter = static x => x is IFormattable formattable
        ? formattable.ToString(null, CultureInfo.InvariantCulture)
        : x.ToString();

    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    public Func<object, object?>? GetConverter(IObjectConverter context, Type sourceType, Type targetType)
    {
        return targetType == typeof(string) ? Converter : null;
    }
}
