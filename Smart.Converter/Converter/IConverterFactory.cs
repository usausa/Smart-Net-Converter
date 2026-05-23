namespace Smart.Converter;

using System.Diagnostics.CodeAnalysis;

public interface IConverterFactory
{
    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    Func<object, object?>? GetConverter(IObjectConverter context, Type sourceType, Type targetType);
}
