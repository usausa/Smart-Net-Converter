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
    bool TryConvert(object? value, Type targetType, out object? result)
    {
        // Specialized null
        if (value is null)
        {
            result = null;
            return !targetType.IsValueType || targetType.IsNullableType();
        }

        // Specialized empty string for value type
        if ((value is string { Length: 0 }) && targetType.IsValueType)
        {
            result = null;
            return targetType.IsNullableType();
        }

        // Default implementation can't detect parse failure
        if (!CanConvert(value, targetType))
        {
            result = null;
            return false;
        }

        result = Convert(value, targetType);
        return true;
    }

    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    Func<object?, object?>? CreateConverter(Type sourceType, Type targetType);
}
