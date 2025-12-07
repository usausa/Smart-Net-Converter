#nullable disable
namespace Smart.Converter.Converters;

public sealed class EnumConverterFactory : IConverterFactory
{
    private static readonly HashSet<Type> UnderlyingTypes =
    [
        typeof(byte),
        typeof(sbyte),
        typeof(short),
        typeof(ushort),
        typeof(int),
        typeof(uint),
        typeof(long),
        typeof(ulong),
        typeof(char)
    ];

    private static readonly Dictionary<(Type, Type), Func<object, object>> CastOperators = new()
    {
        // byte
        { (typeof(byte), typeof(byte)), static x => (byte)x },
        { (typeof(byte), typeof(sbyte)), static x => (sbyte)(byte)x },
        { (typeof(byte), typeof(short)), static x => (short)(byte)x },
        { (typeof(byte), typeof(ushort)), static x => (ushort)(byte)x },
        { (typeof(byte), typeof(int)), static x => (int)(byte)x },
        { (typeof(byte), typeof(uint)), static x => (uint)(byte)x },
        { (typeof(byte), typeof(long)), static x => (long)(byte)x },
        { (typeof(byte), typeof(ulong)), static x => (ulong)(byte)x },
        { (typeof(byte), typeof(char)), static x => (char)(byte)x },
        // sbyte
        { (typeof(sbyte), typeof(byte)), static x => (byte)(sbyte)x },
        { (typeof(sbyte), typeof(sbyte)), static x => (sbyte)x },
        { (typeof(sbyte), typeof(short)), static x => (short)(sbyte)x },
        { (typeof(sbyte), typeof(ushort)), static x => (ushort)(sbyte)x },
        { (typeof(sbyte), typeof(int)), static x => (int)(sbyte)x },
        { (typeof(sbyte), typeof(uint)), static x => (uint)(sbyte)x },
        { (typeof(sbyte), typeof(long)), static x => (long)(sbyte)x },
        { (typeof(sbyte), typeof(ulong)), static x => (ulong)(sbyte)x },
        { (typeof(sbyte), typeof(char)), static x => (char)(sbyte)x },
        // short
        { (typeof(short), typeof(byte)), static x => (byte)(short)x },
        { (typeof(short), typeof(sbyte)), static x => (sbyte)(short)x },
        { (typeof(short), typeof(short)), static x => (short)x },
        { (typeof(short), typeof(ushort)), static x => (ushort)(short)x },
        { (typeof(short), typeof(int)), static x => (int)(short)x },
        { (typeof(short), typeof(uint)), static x => (uint)(short)x },
        { (typeof(short), typeof(long)), static x => (long)(short)x },
        { (typeof(short), typeof(ulong)), static x => (ulong)(short)x },
        { (typeof(short), typeof(char)), static x => (char)(short)x },
        // ushort
        { (typeof(ushort), typeof(byte)), static x => (byte)(ushort)x },
        { (typeof(ushort), typeof(sbyte)), static x => (sbyte)(ushort)x },
        { (typeof(ushort), typeof(short)), static x => (short)(ushort)x },
        { (typeof(ushort), typeof(ushort)), static x => (ushort)x },
        { (typeof(ushort), typeof(int)), static x => (int)(ushort)x },
        { (typeof(ushort), typeof(uint)), static x => (uint)(ushort)x },
        { (typeof(ushort), typeof(long)), static x => (long)(ushort)x },
        { (typeof(ushort), typeof(ulong)), static x => (ulong)(ushort)x },
        { (typeof(ushort), typeof(char)), static x => (char)(ushort)x },
        // int
        { (typeof(int), typeof(byte)), static x => (byte)(int)x },
        { (typeof(int), typeof(sbyte)), static x => (sbyte)(int)x },
        { (typeof(int), typeof(short)), static x => (short)(int)x },
        { (typeof(int), typeof(ushort)), static x => (ushort)(int)x },
        { (typeof(int), typeof(int)), static x => (int)x },
        { (typeof(int), typeof(uint)), static x => (uint)(int)x },
        { (typeof(int), typeof(long)), static x => (long)(int)x },
        { (typeof(int), typeof(ulong)), static x => (ulong)(int)x },
        { (typeof(int), typeof(char)), static x => (char)(int)x },
        // uint
        { (typeof(uint), typeof(byte)), static x => (byte)(uint)x },
        { (typeof(uint), typeof(sbyte)), static x => (sbyte)(uint)x },
        { (typeof(uint), typeof(short)), static x => (short)(uint)x },
        { (typeof(uint), typeof(ushort)), static x => (ushort)(uint)x },
        { (typeof(uint), typeof(int)), static x => (int)(uint)x },
        { (typeof(uint), typeof(uint)), static x => (uint)x },
        { (typeof(uint), typeof(long)), static x => (long)(uint)x },
        { (typeof(uint), typeof(ulong)), static x => (ulong)(uint)x },
        { (typeof(uint), typeof(char)), static x => (char)(uint)x },
        // long
        { (typeof(long), typeof(byte)), static x => (byte)(long)x },
        { (typeof(long), typeof(sbyte)), static x => (sbyte)(long)x },
        { (typeof(long), typeof(short)), static x => (short)(long)x },
        { (typeof(long), typeof(ushort)), static x => (ushort)(long)x },
        { (typeof(long), typeof(int)), static x => (int)(long)x },
        { (typeof(long), typeof(uint)), static x => (uint)(long)x },
        { (typeof(long), typeof(long)), static x => (long)x },
        { (typeof(long), typeof(ulong)), static x => (ulong)(long)x },
        { (typeof(long), typeof(char)), static x => (char)(long)x },
        // ulong
        { (typeof(ulong), typeof(byte)), static x => (byte)(ulong)x },
        { (typeof(ulong), typeof(sbyte)), static x => (sbyte)(ulong)x },
        { (typeof(ulong), typeof(short)), static x => (short)(ulong)x },
        { (typeof(ulong), typeof(ushort)), static x => (ushort)(ulong)x },
        { (typeof(ulong), typeof(int)), static x => (int)(ulong)x },
        { (typeof(ulong), typeof(uint)), static x => (uint)(ulong)x },
        { (typeof(ulong), typeof(long)), static x => (long)(ulong)x },
        { (typeof(ulong), typeof(ulong)), static x => (ulong)x },
        { (typeof(ulong), typeof(char)), static x => (char)(ulong)x }
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
            return CastOperators.GetValueOrDefault((sourceUnderlyingType, targetUnderlyingType));
        }

        return null;
    }

#pragma warning disable CA1812
    private sealed class EnumToStringConverter<T> : IConverter
        where T : struct, Enum
    {
        public object Convert(object source)
        {
            return Enums<T>.GetName((T)source);
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class StringToEnumConverter<T> : IConverter
        where T : struct, Enum
    {
        public object Convert(object source)
        {
            return Enums<T>.TryParseValue((string)source, out var value) ? value : default;
        }
    }
#pragma warning restore CA1812
}
