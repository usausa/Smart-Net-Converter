#nullable disable
namespace Smart.Converter.Converters;

public sealed class NumericCastConverterFactory : IConverterFactory
{
    private static readonly Dictionary<Tuple<Type, Type>, Func<object, object>> Converters = new()
    {
        // byte
        { Tuple.Create(typeof(byte), typeof(sbyte)), static x => (sbyte)(byte)x },
        { Tuple.Create(typeof(byte), typeof(short)), static x => (short)(byte)x },
        { Tuple.Create(typeof(byte), typeof(ushort)), static x => (ushort)(byte)x },
        { Tuple.Create(typeof(byte), typeof(int)), static x => (int)(byte)x },
        { Tuple.Create(typeof(byte), typeof(uint)), static x => (uint)(byte)x },
        { Tuple.Create(typeof(byte), typeof(long)), static x => (long)(byte)x },
        { Tuple.Create(typeof(byte), typeof(ulong)), static x => (ulong)(byte)x },
        { Tuple.Create(typeof(byte), typeof(char)), static x => (char)(byte)x },
        { Tuple.Create(typeof(byte), typeof(double)), static x => (double)(byte)x },
        { Tuple.Create(typeof(byte), typeof(float)), static x => (float)(byte)x },
        // sbyte
        { Tuple.Create(typeof(sbyte), typeof(byte)), static x => (byte)(sbyte)x },
        { Tuple.Create(typeof(sbyte), typeof(short)), static x => (short)(sbyte)x },
        { Tuple.Create(typeof(sbyte), typeof(ushort)), static x => (ushort)(sbyte)x },
        { Tuple.Create(typeof(sbyte), typeof(int)), static x => (int)(sbyte)x },
        { Tuple.Create(typeof(sbyte), typeof(uint)), static x => (uint)(sbyte)x },
        { Tuple.Create(typeof(sbyte), typeof(long)), static x => (long)(sbyte)x },
        { Tuple.Create(typeof(sbyte), typeof(ulong)), static x => (ulong)(sbyte)x },
        { Tuple.Create(typeof(sbyte), typeof(char)), static x => (char)(sbyte)x },
        { Tuple.Create(typeof(sbyte), typeof(double)), static x => (double)(sbyte)x },
        { Tuple.Create(typeof(sbyte), typeof(float)), static x => (float)(sbyte)x },
        // short
        { Tuple.Create(typeof(short), typeof(byte)), static x => (byte)(short)x },
        { Tuple.Create(typeof(short), typeof(sbyte)), static x => (sbyte)(short)x },
        { Tuple.Create(typeof(short), typeof(ushort)), static x => (ushort)(short)x },
        { Tuple.Create(typeof(short), typeof(int)), static x => (int)(short)x },
        { Tuple.Create(typeof(short), typeof(uint)), static x => (uint)(short)x },
        { Tuple.Create(typeof(short), typeof(long)), static x => (long)(short)x },
        { Tuple.Create(typeof(short), typeof(ulong)), static x => (ulong)(short)x },
        { Tuple.Create(typeof(short), typeof(char)), static x => (char)(short)x },
        { Tuple.Create(typeof(short), typeof(double)), static x => (double)(short)x },
        { Tuple.Create(typeof(short), typeof(float)), static x => (float)(short)x },
        // ushort
        { Tuple.Create(typeof(ushort), typeof(byte)), static x => (byte)(ushort)x },
        { Tuple.Create(typeof(ushort), typeof(sbyte)), static x => (sbyte)(ushort)x },
        { Tuple.Create(typeof(ushort), typeof(short)), static x => (short)(ushort)x },
        { Tuple.Create(typeof(ushort), typeof(int)), static x => (int)(ushort)x },
        { Tuple.Create(typeof(ushort), typeof(uint)), static x => (uint)(ushort)x },
        { Tuple.Create(typeof(ushort), typeof(long)), static x => (long)(ushort)x },
        { Tuple.Create(typeof(ushort), typeof(ulong)), static x => (ulong)(ushort)x },
        { Tuple.Create(typeof(ushort), typeof(char)), static x => (char)(ushort)x },
        { Tuple.Create(typeof(ushort), typeof(double)), static x => (double)(ushort)x },
        { Tuple.Create(typeof(ushort), typeof(float)), static x => (float)(ushort)x },
        // int
        { Tuple.Create(typeof(int), typeof(byte)), static x => (byte)(int)x },
        { Tuple.Create(typeof(int), typeof(sbyte)), static x => (sbyte)(int)x },
        { Tuple.Create(typeof(int), typeof(short)), static x => (short)(int)x },
        { Tuple.Create(typeof(int), typeof(ushort)), static x => (ushort)(int)x },
        { Tuple.Create(typeof(int), typeof(uint)), static x => (uint)(int)x },
        { Tuple.Create(typeof(int), typeof(long)), static x => (long)(int)x },
        { Tuple.Create(typeof(int), typeof(ulong)), static x => (ulong)(int)x },
        { Tuple.Create(typeof(int), typeof(char)), static x => (char)(int)x },
        { Tuple.Create(typeof(int), typeof(double)), static x => (double)(int)x },
        { Tuple.Create(typeof(int), typeof(float)), static x => (float)(int)x },
        // uint
        { Tuple.Create(typeof(uint), typeof(byte)), static x => (byte)(uint)x },
        { Tuple.Create(typeof(uint), typeof(sbyte)), static x => (sbyte)(uint)x },
        { Tuple.Create(typeof(uint), typeof(short)), static x => (short)(uint)x },
        { Tuple.Create(typeof(uint), typeof(ushort)), static x => (ushort)(uint)x },
        { Tuple.Create(typeof(uint), typeof(int)), static x => (int)(uint)x },
        { Tuple.Create(typeof(uint), typeof(long)), static x => (long)(uint)x },
        { Tuple.Create(typeof(uint), typeof(ulong)), static x => (ulong)(uint)x },
        { Tuple.Create(typeof(uint), typeof(char)), static x => (char)(uint)x },
        { Tuple.Create(typeof(uint), typeof(double)), static x => (double)(uint)x },
        { Tuple.Create(typeof(uint), typeof(float)), static x => (float)(uint)x },
        // long
        { Tuple.Create(typeof(long), typeof(byte)), static x => (byte)(long)x },
        { Tuple.Create(typeof(long), typeof(sbyte)), static x => (sbyte)(long)x },
        { Tuple.Create(typeof(long), typeof(short)), static x => (short)(long)x },
        { Tuple.Create(typeof(long), typeof(ushort)), static x => (ushort)(long)x },
        { Tuple.Create(typeof(long), typeof(int)), static x => (int)(long)x },
        { Tuple.Create(typeof(long), typeof(uint)), static x => (uint)(long)x },
        { Tuple.Create(typeof(long), typeof(ulong)), static x => (ulong)(long)x },
        { Tuple.Create(typeof(long), typeof(char)), static x => (char)(long)x },
        { Tuple.Create(typeof(long), typeof(double)), static x => (double)(long)x },
        { Tuple.Create(typeof(long), typeof(float)), static x => (float)(long)x },
        // ulong
        { Tuple.Create(typeof(ulong), typeof(byte)), static x => (byte)(ulong)x },
        { Tuple.Create(typeof(ulong), typeof(sbyte)), static x => (sbyte)(ulong)x },
        { Tuple.Create(typeof(ulong), typeof(short)), static x => (short)(ulong)x },
        { Tuple.Create(typeof(ulong), typeof(ushort)), static x => (ushort)(ulong)x },
        { Tuple.Create(typeof(ulong), typeof(int)), static x => (int)(ulong)x },
        { Tuple.Create(typeof(ulong), typeof(uint)), static x => (uint)(ulong)x },
        { Tuple.Create(typeof(ulong), typeof(long)), static x => (long)(ulong)x },
        { Tuple.Create(typeof(ulong), typeof(char)), static x => (char)(ulong)x },
        { Tuple.Create(typeof(ulong), typeof(double)), static x => (double)(ulong)x },
        { Tuple.Create(typeof(ulong), typeof(float)), static x => (float)(ulong)x },
        // char
        { Tuple.Create(typeof(char), typeof(byte)), static x => (byte)(char)x },
        { Tuple.Create(typeof(char), typeof(sbyte)), static x => (sbyte)(char)x },
        { Tuple.Create(typeof(char), typeof(short)), static x => (short)(char)x },
        { Tuple.Create(typeof(char), typeof(ushort)), static x => (ushort)(char)x },
        { Tuple.Create(typeof(char), typeof(int)), static x => (int)(char)x },
        { Tuple.Create(typeof(char), typeof(uint)), static x => (uint)(char)x },
        { Tuple.Create(typeof(char), typeof(long)), static x => (long)(char)x },
        { Tuple.Create(typeof(char), typeof(ulong)), static x => (ulong)(char)x },
        { Tuple.Create(typeof(char), typeof(double)), static x => (double)(char)x },
        { Tuple.Create(typeof(char), typeof(float)), static x => (float)(char)x },
        // double
        { Tuple.Create(typeof(double), typeof(byte)), static x => (byte)(double)x },
        { Tuple.Create(typeof(double), typeof(sbyte)), static x => (sbyte)(double)x },
        { Tuple.Create(typeof(double), typeof(short)), static x => (short)(double)x },
        { Tuple.Create(typeof(double), typeof(ushort)), static x => (ushort)(double)x },
        { Tuple.Create(typeof(double), typeof(int)), static x => (int)(double)x },
        { Tuple.Create(typeof(double), typeof(uint)), static x => (uint)(double)x },
        { Tuple.Create(typeof(double), typeof(long)), static x => (long)(double)x },
        { Tuple.Create(typeof(double), typeof(ulong)), static x => (ulong)(double)x },
        { Tuple.Create(typeof(double), typeof(char)), static x => (char)(double)x },
        { Tuple.Create(typeof(double), typeof(float)), static x => (float)(double)x },
        // float
        { Tuple.Create(typeof(float), typeof(byte)), static x => (byte)(float)x },
        { Tuple.Create(typeof(float), typeof(sbyte)), static x => (sbyte)(float)x },
        { Tuple.Create(typeof(float), typeof(short)), static x => (short)(float)x },
        { Tuple.Create(typeof(float), typeof(ushort)), static x => (ushort)(float)x },
        { Tuple.Create(typeof(float), typeof(int)), static x => (int)(float)x },
        { Tuple.Create(typeof(float), typeof(uint)), static x => (uint)(float)x },
        { Tuple.Create(typeof(float), typeof(long)), static x => (long)(float)x },
        { Tuple.Create(typeof(float), typeof(ulong)), static x => (ulong)(float)x },
        { Tuple.Create(typeof(float), typeof(char)), static x => (char)(float)x },
        { Tuple.Create(typeof(float), typeof(double)), static x => (double)(float)x }
    };

    public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
    {
        if (sourceType.IsValueType && targetType.IsValueType)
        {
            var key = Tuple.Create(sourceType, targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType);
            if (Converters.TryGetValue(key, out var converter))
            {
                return converter;
            }
        }

        return null;
    }
}
