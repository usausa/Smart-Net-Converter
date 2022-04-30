#nullable disable
namespace Smart.Converter.Converters;

public sealed class BooleanConverterFactory : IConverterFactory
{
    private static readonly object BoolFalse = false;
    private static readonly object BoolTrue = true;

    private static readonly object ByteFalse = (byte)0;
    private static readonly object ByteTrue = (byte)1;

    private static readonly object SByteFalse = (sbyte)0;
    private static readonly object SByteTrue = (sbyte)1;

    private static readonly object Int16False = (short)0;
    private static readonly object Int16True = (short)1;

    private static readonly object UInt16False = (ushort)0;
    private static readonly object UInt16True = (ushort)1;

    private static readonly object Int32False = 0;
    private static readonly object Int32True = 1;

    private static readonly object UInt32False = 0U;
    private static readonly object UInt32True = 1U;

    private static readonly object Int64False = 0L;
    private static readonly object Int64True = 1L;

    private static readonly object UInt64False = 0UL;
    private static readonly object UInt64True = 1UL;

    private static readonly object CharFalse = (char)0;
    private static readonly object CharTrue = (char)1;

    private static readonly object DoubleFalse = 0.0d;
    private static readonly object DoubleTrue = 1.0d;

    private static readonly object FloatFalse = 0.0f;
    private static readonly object FloatTrue = 1.0f;

    private static readonly Dictionary<Type, Func<object, object>> FromBooleanConverters = new()
    {
        { typeof(byte), static x => (bool)x ? ByteTrue : ByteFalse },
        { typeof(sbyte), static x => (bool)x ? SByteTrue : SByteFalse },
        { typeof(short), static x => (bool)x ? Int16True : Int16False },
        { typeof(ushort), static x => (bool)x ? UInt16True : UInt16False },
        { typeof(int), static x => (bool)x ? Int32True : Int32False },
        { typeof(uint), static x => (bool)x ? UInt32True : UInt32False },
        { typeof(long), static x => (bool)x ? Int64True : Int64False },
        { typeof(ulong), static x => (bool)x ? UInt64True : UInt64False },
        { typeof(char), static x => (bool)x ? CharTrue : CharFalse },
        { typeof(double), static x => (bool)x ? DoubleTrue : DoubleFalse },
        { typeof(float), static x => (bool)x ? FloatTrue : FloatFalse },
        { typeof(decimal), static x => (bool)x ? Decimal.One : Decimal.Zero }
    };

    private static readonly Dictionary<Type, Func<object, object>> ToBooleanConverters = new()
    {
        { typeof(byte), static x => (byte)x != default ? BoolTrue : BoolFalse },
        { typeof(sbyte), static x => (sbyte)x != default ? BoolTrue : BoolFalse },
        { typeof(short), static x => (short)x != default ? BoolTrue : BoolFalse },
        { typeof(ushort), static x => (ushort)x != default ? BoolTrue : BoolFalse },
        { typeof(int), static x => (int)x != default ? BoolTrue : BoolFalse },
        { typeof(uint), static x => (uint)x != default ? BoolTrue : BoolFalse },
        { typeof(long), static x => (long)x != default ? BoolTrue : BoolFalse },
        { typeof(ulong), static x => (ulong)x != default ? BoolTrue : BoolFalse },
        { typeof(char), static x => (char)x != default ? BoolTrue : BoolFalse },
        // ReSharper disable CompareOfFloatsByEqualityOperator
        { typeof(double), static x => (double)x != default ? BoolTrue : BoolFalse },
        { typeof(float), static x => (float)x != default ? BoolTrue : BoolFalse },
        // ReSharper restore CompareOfFloatsByEqualityOperator
        { typeof(decimal), static x => (decimal)x != default ? BoolTrue : BoolFalse }
    };

    public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
    {
        if (sourceType == typeof(bool))
        {
            if (targetType.IsValueType)
            {
                var type = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;
                if (FromBooleanConverters.TryGetValue(type!, out var converter))
                {
                    return converter;
                }
            }

            if (targetType == typeof(string))
            {
                return static x => ((bool)x).ToString();
            }
        }
        else if ((targetType == typeof(bool)) || (targetType == typeof(bool?)))
        {
            if (sourceType.IsValueType)
            {
                var type = sourceType.IsNullableType() ? Nullable.GetUnderlyingType(sourceType) : sourceType;
                if (ToBooleanConverters.TryGetValue(type!, out var converter))
                {
                    return converter;
                }
            }

            if (sourceType == typeof(string))
            {
                return static x => Boolean.TryParse((string)x, out var result) ? result : null;
            }
        }

        return null;
    }
}
