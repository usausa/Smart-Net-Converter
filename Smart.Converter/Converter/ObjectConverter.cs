namespace Smart.Converter;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using Smart.Converter.Converters;

#pragma warning disable CA1034
public sealed class ObjectConverter : IObjectConverter
{
    [UnconditionalSuppressMessage("AOT", "IL3050", Justification = "Default instance is created intentionally with reflection-based factories.")]
    [UnconditionalSuppressMessage("Trimming", "IL2026", Justification = "Default instance is created intentionally with reflection-based factories.")]
    public static ObjectConverter Default { get; } = new();

    private readonly TypePairHashArray converterCache = new();

    private readonly TypePairHashArray tryConverterCache = new();

    private IConverterFactory[] factories;

    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    public ObjectConverter()
    {
        factories = DefaultObjectFactories.Create();
    }

    public ObjectConverter(IEnumerable<IConverterFactory> converterFactories)
    {
        factories = converterFactories.ToArray();
    }

    public void SetFactories(IEnumerable<IConverterFactory> converterFactories)
    {
        factories = converterFactories.ToArray();
        converterCache.Clear();
        tryConverterCache.Clear();
    }

    //--------------------------------------------------------------------------------
    // Converter
    //--------------------------------------------------------------------------------

    public void Reset()
    {
        converterCache.Clear();
        tryConverterCache.Clear();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    private Func<object, object?>? FindConverter(Type sourceType, Type targetType)
    {
        var factoriesLocal = factories;
        // ReSharper disable once ForCanBeConvertedToForeach
        for (var i = 0; i < factoriesLocal.Length; i++)
        {
            var converter = factoriesLocal[i].GetConverter(this, sourceType, targetType);
            if (converter is not null)
            {
                return converter;
            }
        }

        return null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    private Func<object, object?>? GetConverter(Type sourceType, Type targetType)
    {
        if (!converterCache.TryGetValue(sourceType, targetType, out var converter))
        {
            converter = converterCache.AddIfNotExist(sourceType, targetType, FindConverter);
        }

        return converter;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    private Func<object, object?>? FindTryConverter(Type sourceType, Type targetType)
    {
        var factoriesLocal = factories;
        // ReSharper disable once ForCanBeConvertedToForeach
        for (var i = 0; i < factoriesLocal.Length; i++)
        {
            var factory = factoriesLocal[i];
            var converter = factory.GetTryConverter(this, sourceType, targetType) ?? factory.GetConverter(this, sourceType, targetType);
            if (converter is not null)
            {
                return converter;
            }
        }

        return null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    private Func<object, object?>? GetTryConverter(Type sourceType, Type targetType)
    {
        if (!tryConverterCache.TryGetValue(sourceType, targetType, out var converter))
        {
            converter = tryConverterCache.AddIfNotExist(sourceType, targetType, FindTryConverter);
        }

        return converter;
    }

    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    public bool CanConvert<T>(object? value)
    {
        return CanConvert(value, typeof(T));
    }

    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    public bool CanConvert(object? value, Type targetType)
    {
        if (value is null)
        {
            return true;
        }

        var sourceType = value.GetType();
        if (sourceType == (targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType))
        {
            return true;
        }

        return GetConverter(sourceType, targetType) is not null;
    }

    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    public bool CanConvert(Type sourceType, Type targetType)
    {
        return GetConverter(sourceType.IsNullableType() ? Nullable.GetUnderlyingType(sourceType)! : sourceType, targetType) is not null;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    public T? Convert<T>(object? value)
    {
        return (T?)Convert(value, typeof(T));
    }

    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    public object? Convert(object? value, Type targetType)
    {
        // Specialized null
        if (value is null)
        {
            return targetType.GetDefaultValue();
        }

        // Specialized same type for performance (Nullable is excluded because operation is slow)
        var sourceType = value.GetType();
        if (sourceType == (targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType))
        {
            return value;
        }

        var converter = GetConverter(sourceType, targetType);
        if (converter is null)
        {
            throw new ObjectConverterException($"Type {sourceType} can't convert to {targetType}");
        }

        return converter(value);
    }

    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    public bool TryConvert(object? value, Type targetType, out object? result)
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

        // Specialized same type for performance (Nullable is excluded because operation is slow)
        var sourceType = value.GetType();
        if (sourceType == (targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType))
        {
            result = value;
            return true;
        }

        var converter = GetTryConverter(sourceType, targetType);
        if (converter is null)
        {
            result = null;
            return false;
        }

        var converted = converter(value);
        if (converted == ConvertFailure.Value)
        {
            result = null;
            return false;
        }

        result = converted;
        return true;
    }

    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    public Func<object?, object?>? CreateConverter(Type sourceType, Type targetType)
    {
        var converter = GetConverter(sourceType.IsNullableType() ? Nullable.GetUnderlyingType(sourceType)! : sourceType, targetType);
        if (converter is null)
        {
            return null;
        }

        return CreateConverter(
            targetType.GetDefaultValue(),
            targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType)! : targetType,
            converter);
    }

    private static Func<object?, object?> CreateConverter(object? defaultValue, Type targetType, Func<object, object?> converter)
    {
        return value => value is null
            ? defaultValue
            : value.GetType() == targetType
                ? value
                : converter(value);
    }

    //--------------------------------------------------------------------------------
    // Diagnostics
    //--------------------------------------------------------------------------------

    public sealed class DiagnosticsInfo
    {
        public int CacheCount { get; }

        public int CacheWidth { get; }

        public int CacheDepth { get; }

        public DiagnosticsInfo(
            int cacheCount,
            int cacheWidth,
            int cacheDepth)
        {
            CacheCount = cacheCount;
            CacheWidth = cacheWidth;
            CacheDepth = cacheDepth;
        }
    }

    public DiagnosticsInfo Diagnostics
    {
        get
        {
            var cacheDiagnostics = converterCache.Diagnostics;
            return new DiagnosticsInfo(
                cacheDiagnostics.Count,
                cacheDiagnostics.Width,
                cacheDiagnostics.Depth);
        }
    }
}
#pragma warning restore CA1034
