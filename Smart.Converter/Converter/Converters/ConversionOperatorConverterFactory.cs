namespace Smart.Converter.Converters;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

public sealed class ConversionOperatorConverterFactory : IConverterFactory
{
#pragma warning disable IL2026, IL3050
    private static readonly MethodInfo CreateMethod = typeof(ConversionOperatorConverterFactory).GetMethod(nameof(CreateConverter), BindingFlags.NonPublic | BindingFlags.Static)!;
#pragma warning restore IL2026, IL3050

    [RequiresDynamicCode("ConversionOperatorConverterFactory uses MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("ConversionOperatorConverterFactory uses reflection to find conversion operators at runtime.")]
    public Func<object, object?>? GetConverter(IObjectConverter context, Type sourceType, Type targetType)
    {
        var underlyingTargetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType)! : targetType;

        var methodInfo = GetImplicitConversionOperator(sourceType, underlyingTargetType);
        if (methodInfo is not null)
        {
            return BuildConverter(methodInfo);
        }

        if (underlyingTargetType != targetType)
        {
            methodInfo = GetImplicitConversionOperator(sourceType, targetType);
            if (methodInfo is not null)
            {
                return BuildConverter(methodInfo);
            }
        }

        methodInfo = GetExplicitConversionOperator(sourceType, underlyingTargetType);
        if (methodInfo is not null)
        {
            return BuildConverter(methodInfo);
        }

        if (underlyingTargetType != targetType)
        {
            methodInfo = GetExplicitConversionOperator(sourceType, targetType);
            if (methodInfo is not null)
            {
                return BuildConverter(methodInfo);
            }
        }

        return null;
    }

    [RequiresUnreferencedCode("ConversionOperatorConverterFactory uses reflection to find conversion operators at runtime.")]
    private static MethodInfo? GetImplicitConversionOperator(Type sourceType, Type targetType)
    {
        var sourceTypeMethod = sourceType
            .GetMethods()
            .FirstOrDefault(mi =>
                mi.IsPublic &&
                mi.IsStatic &&
                mi.Name == "op_Implicit" &&
                mi.ReturnType == targetType);
        return sourceTypeMethod ?? targetType
                   .GetMethods()
                   .FirstOrDefault(mi =>
                       mi.IsPublic &&
                       mi.IsStatic &&
                       mi.Name == "op_Implicit" &&
                       mi.GetParameters().Length == 1 &&
                       IsMatchParameterType(mi.GetParameters()[0].ParameterType, sourceType));
    }

    [RequiresUnreferencedCode("ConversionOperatorConverterFactory uses reflection to find conversion operators at runtime.")]
    private static MethodInfo? GetExplicitConversionOperator(Type sourceType, Type targetType)
    {
        var sourceTypeMethod = sourceType
            .GetMethods()
            .FirstOrDefault(mi =>
                mi.IsPublic &&
                mi.IsStatic &&
                mi.Name == "op_Explicit" &&
                mi.ReturnType == targetType);
        return sourceTypeMethod ?? targetType
            .GetMethods()
            .FirstOrDefault(mi =>
                       mi.IsPublic &&
                       mi.IsStatic &&
                       mi.Name == "op_Explicit" &&
                       mi.GetParameters().Length == 1 &&
                       IsMatchParameterType(mi.GetParameters()[0].ParameterType, sourceType));
    }

    private static bool IsMatchParameterType(Type parameterType, Type sourceType)
    {
        return parameterType.IsNullableType()
            ? Nullable.GetUnderlyingType(parameterType) == sourceType
            : parameterType == sourceType;
    }

    [RequiresDynamicCode("ConversionOperatorConverterFactory uses MakeGenericMethod at runtime.")]
    private static Func<object, object?> BuildConverter(MethodInfo mi)
    {
#pragma warning disable IL2060
        var method = CreateMethod.MakeGenericMethod(mi.ReturnType);
#pragma warning restore IL2060
        return (Func<object, object?>)method.Invoke(null, [mi])!;
    }

    private static Func<object, object?> CreateConverter<TDestination>(MethodInfo mi)
    {
        return source =>
        {
#pragma warning disable CA1031
            try
            {
                return (TDestination?)mi.Invoke(null, [source]);
            }
            catch
            {
                return default(TDestination);
            }
#pragma warning restore CA1031
        };
    }
}
