namespace Smart.Converter.Converters;

using System.Diagnostics.CodeAnalysis;

public static class DefaultObjectFactories
{
    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    public static IConverterFactory[] Create()
    {
        return
        [
            new DBNullConverterFactory(),               // DBNull
            new AssignableConverterFactory(),           // IsAssignableFrom
            new BooleanConverterFactory(),              // Boolean
            new DateTimeConverterFactory(),             // DateTime/DateTimeOffset
            new GuidConverterFactory(),                 // Guid
            new DecimalConverterFactory(),              // Decimal
            new BigIntegerConverterFactory(),           // BigInteger
            new NumericCastConverterFactory(),          // Numeric cast
            new NumericParseConverterFactory(),         // Numeric parse
            new EnumConverterFactory(),                 // Enum to Enum, String to Enum, Assignable to Enum, Enum to Assignable
            new EnumerableConverterFactory(),           // Enumerable
            new ValueHolderConverterFactory(),          // ValueHolder
            new ConversionOperatorConverterFactory(),   // Implicit/Explicit operator
            new ToStringConverterFactory(),             // ToString finally
            new ConstructorConverterFactory()           // Constructor
        ];
    }
}
