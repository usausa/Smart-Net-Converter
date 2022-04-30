#nullable disable
namespace Smart.Converter.Converters;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Ignore")]
public sealed class EnumConverterFactory : IConverterFactory
{
    private static readonly HashSet<Type> UnderlyingTypes = new()
    {
        typeof(byte),
        typeof(sbyte),
        typeof(short),
        typeof(ushort),
        typeof(int),
        typeof(uint),
        typeof(long),
        typeof(ulong),
        typeof(char)
    };

    private static readonly Dictionary<Tuple<Type, Type>, Func<object, object>> CastOperators = new()
    {
        // byte
        { Tuple.Create(typeof(byte), typeof(byte)), static x => (byte)x },
        { Tuple.Create(typeof(byte), typeof(sbyte)), static x => (sbyte)(byte)x },
        { Tuple.Create(typeof(byte), typeof(short)), static x => (short)(byte)x },
        { Tuple.Create(typeof(byte), typeof(ushort)), static x => (ushort)(byte)x },
        { Tuple.Create(typeof(byte), typeof(int)), static x => (int)(byte)x },
        { Tuple.Create(typeof(byte), typeof(uint)), static x => (uint)(byte)x },
        { Tuple.Create(typeof(byte), typeof(long)), static x => (long)(byte)x },
        { Tuple.Create(typeof(byte), typeof(ulong)), static x => (ulong)(byte)x },
        { Tuple.Create(typeof(byte), typeof(char)), static x => (char)(byte)x },
        // sbyte
        { Tuple.Create(typeof(sbyte), typeof(byte)), static x => (byte)(sbyte)x },
        { Tuple.Create(typeof(sbyte), typeof(sbyte)), static x => (sbyte)x },
        { Tuple.Create(typeof(sbyte), typeof(short)), static x => (short)(sbyte)x },
        { Tuple.Create(typeof(sbyte), typeof(ushort)), static x => (ushort)(sbyte)x },
        { Tuple.Create(typeof(sbyte), typeof(int)), static x => (int)(sbyte)x },
        { Tuple.Create(typeof(sbyte), typeof(uint)), static x => (uint)(sbyte)x },
        { Tuple.Create(typeof(sbyte), typeof(long)), static x => (long)(sbyte)x },
        { Tuple.Create(typeof(sbyte), typeof(ulong)), static x => (ulong)(sbyte)x },
        { Tuple.Create(typeof(sbyte), typeof(char)), static x => (char)(sbyte)x },
        // short
        { Tuple.Create(typeof(short), typeof(byte)), static x => (byte)(short)x },
        { Tuple.Create(typeof(short), typeof(sbyte)), static x => (sbyte)(short)x },
        { Tuple.Create(typeof(short), typeof(short)), static x => (short)x },
        { Tuple.Create(typeof(short), typeof(ushort)), static x => (ushort)(short)x },
        { Tuple.Create(typeof(short), typeof(int)), static x => (int)(short)x },
        { Tuple.Create(typeof(short), typeof(uint)), static x => (uint)(short)x },
        { Tuple.Create(typeof(short), typeof(long)), static x => (long)(short)x },
        { Tuple.Create(typeof(short), typeof(ulong)), static x => (ulong)(short)x },
        { Tuple.Create(typeof(short), typeof(char)), static x => (char)(short)x },
        // ushort
        { Tuple.Create(typeof(ushort), typeof(byte)), static x => (byte)(ushort)x },
        { Tuple.Create(typeof(ushort), typeof(sbyte)), static x => (sbyte)(ushort)x },
        { Tuple.Create(typeof(ushort), typeof(short)), static x => (short)(ushort)x },
        { Tuple.Create(typeof(ushort), typeof(ushort)), static x => (ushort)x },
        { Tuple.Create(typeof(ushort), typeof(int)), static x => (int)(ushort)x },
        { Tuple.Create(typeof(ushort), typeof(uint)), static x => (uint)(ushort)x },
        { Tuple.Create(typeof(ushort), typeof(long)), static x => (long)(ushort)x },
        { Tuple.Create(typeof(ushort), typeof(ulong)), static x => (ulong)(ushort)x },
        { Tuple.Create(typeof(ushort), typeof(char)), static x => (char)(ushort)x },
        // int
        { Tuple.Create(typeof(int), typeof(byte)), static x => (byte)(int)x },
        { Tuple.Create(typeof(int), typeof(sbyte)), static x => (sbyte)(int)x },
        { Tuple.Create(typeof(int), typeof(short)), static x => (short)(int)x },
        { Tuple.Create(typeof(int), typeof(ushort)), static x => (ushort)(int)x },
        { Tuple.Create(typeof(int), typeof(int)), static x => (int)x },
        { Tuple.Create(typeof(int), typeof(uint)), static x => (uint)(int)x },
        { Tuple.Create(typeof(int), typeof(long)), static x => (long)(int)x },
        { Tuple.Create(typeof(int), typeof(ulong)), static x => (ulong)(int)x },
        { Tuple.Create(typeof(int), typeof(char)), static x => (char)(int)x },
        // uint
        { Tuple.Create(typeof(uint), typeof(byte)), static x => (byte)(uint)x },
        { Tuple.Create(typeof(uint), typeof(sbyte)), static x => (sbyte)(uint)x },
        { Tuple.Create(typeof(uint), typeof(short)), static x => (short)(uint)x },
        { Tuple.Create(typeof(uint), typeof(ushort)), static x => (ushort)(uint)x },
        { Tuple.Create(typeof(uint), typeof(int)), static x => (int)(uint)x },
        { Tuple.Create(typeof(uint), typeof(uint)), static x => (uint)x },
        { Tuple.Create(typeof(uint), typeof(long)), static x => (long)(uint)x },
        { Tuple.Create(typeof(uint), typeof(ulong)), static x => (ulong)(uint)x },
        { Tuple.Create(typeof(uint), typeof(char)), static x => (char)(uint)x },
        // long
        { Tuple.Create(typeof(long), typeof(byte)), static x => (byte)(long)x },
        { Tuple.Create(typeof(long), typeof(sbyte)), static x => (sbyte)(long)x },
        { Tuple.Create(typeof(long), typeof(short)), static x => (short)(long)x },
        { Tuple.Create(typeof(long), typeof(ushort)), static x => (ushort)(long)x },
        { Tuple.Create(typeof(long), typeof(int)), static x => (int)(long)x },
        { Tuple.Create(typeof(long), typeof(uint)), static x => (uint)(long)x },
        { Tuple.Create(typeof(long), typeof(long)), static x => (long)x },
        { Tuple.Create(typeof(long), typeof(ulong)), static x => (ulong)(long)x },
        { Tuple.Create(typeof(long), typeof(char)), static x => (char)(long)x },
        // ulong
        { Tuple.Create(typeof(ulong), typeof(byte)), static x => (byte)(ulong)x },
        { Tuple.Create(typeof(ulong), typeof(sbyte)), static x => (sbyte)(ulong)x },
        { Tuple.Create(typeof(ulong), typeof(short)), static x => (short)(ulong)x },
        { Tuple.Create(typeof(ulong), typeof(ushort)), static x => (ushort)(ulong)x },
        { Tuple.Create(typeof(ulong), typeof(int)), static x => (int)(ulong)x },
        { Tuple.Create(typeof(ulong), typeof(uint)), static x => (uint)(ulong)x },
        { Tuple.Create(typeof(ulong), typeof(long)), static x => (long)(ulong)x },
        { Tuple.Create(typeof(ulong), typeof(ulong)), static x => (ulong)x },
        { Tuple.Create(typeof(ulong), typeof(char)), static x => (char)(ulong)x }
    };

    public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
    {
        var sourceEnumType = sourceType.GetEnumType();
        var targetEnumType = targetType.GetEnumType();

        if ((sourceEnumType is not null) && (targetEnumType is not null))
        {
            // Enum to Enum
            return source => Enum.ToObject(targetEnumType, source);
        }

        if (targetEnumType is not null)
        {
            // !Enum to Enum

            // String to Enum
            if (sourceType == typeof(string))
            {
                return ((IConverter)Activator.CreateInstance(typeof(StringToEnumConverter<>).MakeGenericType(targetEnumType))).Convert;
            }

            // Assignable
            if (UnderlyingTypes.Contains(sourceType))
            {
                var targetUnderlyingType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;
                return source => Enum.ToObject(targetUnderlyingType!, source);
            }

            return null;
        }

        if (sourceEnumType is not null)
        {
            // Enum to !Enum

            // Enum to String
            if (targetType == typeof(string))
            {
                return ((IConverter)Activator.CreateInstance(typeof(EnumToStringConverter<>).MakeGenericType(sourceEnumType))).Convert;
            }

            // Enum to Numeric
            var sourceUnderlyingType = Enum.GetUnderlyingType(sourceType);
            var targetUnderlyingType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;
            if (CastOperators.TryGetValue(Tuple.Create(sourceUnderlyingType, targetUnderlyingType), out var converter))
            {
                return converter;
            }

            return null;
        }

        return null;
    }

    private sealed class EnumToStringConverter<T> : IConverter
        where T : struct, Enum
    {
        public object Convert(object source)
        {
            return Enums<T>.GetName((T)source);
        }
    }

    private sealed class StringToEnumConverter<T> : IConverter
        where T : struct, Enum
    {
        public object Convert(object source)
        {
            return Enums<T>.TryParseValue((string)source, out var value) ? value : default;
        }
    }
}
