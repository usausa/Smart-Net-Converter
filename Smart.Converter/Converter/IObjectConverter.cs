namespace Smart.Converter;

using System.Diagnostics.CodeAnalysis;

public interface IObjectConverter
{
    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    bool CanConvert<T>(object? value);

    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    bool CanConvert(object? value, Type targetType);

    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    bool CanConvert(Type sourceType, Type targetType);

    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    T? Convert<T>(object? value);

    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    object? Convert(object? value, Type targetType);

    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    Func<object?, object?>? CreateConverter(Type sourceType, Type targetType);
}
