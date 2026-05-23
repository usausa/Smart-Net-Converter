namespace Smart.Converter.Converters;

using System.Diagnostics.CodeAnalysis;

public sealed class DBNullConverterFactory : IConverterFactory
{
    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    public Func<object, object?>? GetConverter(IObjectConverter context, Type sourceType, Type targetType)
    {
        if (sourceType == typeof(DBNull))
        {
            var defaultValue = targetType.GetDefaultValue();
            return _ => defaultValue;
        }

        return null;
    }
}
