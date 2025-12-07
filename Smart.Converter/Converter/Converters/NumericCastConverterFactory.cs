#nullable disable
namespace Smart.Converter.Converters;

public sealed class NumericCastConverterFactory : IConverterFactory
{
    private static readonly Dictionary<(Type, Type), Func<object, object>> Converters = new()
    {
        // byte
        { (typeof(byte), typeof(sbyte)), static x => (sbyte)(byte)x },
        { (typeof(byte), typeof(short)), static x => (short)(byte)x },
        { (typeof(byte), typeof(ushort)), static x => (ushort)(byte)x },
        { (typeof(byte), typeof(int)), static x => (int)(byte)x },
        { (typeof(byte), typeof(uint)), static x => (uint)(byte)x },
        { (typeof(byte), typeof(long)), static x => (long)(byte)x },
        { (typeof(byte), typeof(ulong)), static x => (ulong)(byte)x },
        { (typeof(byte), typeof(char)), static x => (char)(byte)x },
        { (typeof(byte), typeof(double)), static x => (double)(byte)x },
        { (typeof(byte), typeof(float)), static x => (float)(byte)x },
        // sbyte
        { (typeof(sbyte), typeof(byte)), static x => (byte)(sbyte)x },
        { (typeof(sbyte), typeof(short)), static x => (short)(sbyte)x },
        { (typeof(sbyte), typeof(ushort)), static x => (ushort)(sbyte)x },
        { (typeof(sbyte), typeof(int)), static x => (int)(sbyte)x },
        { (typeof(sbyte), typeof(uint)), static x => (uint)(sbyte)x },
        { (typeof(sbyte), typeof(long)), static x => (long)(sbyte)x },
        { (typeof(sbyte), typeof(ulong)), static x => (ulong)(sbyte)x },
        { (typeof(sbyte), typeof(char)), static x => (char)(sbyte)x },
        { (typeof(sbyte), typeof(double)), static x => (double)(sbyte)x },
        { (typeof(sbyte), typeof(float)), static x => (float)(sbyte)x },
        // short
        { (typeof(short), typeof(byte)), static x => (byte)(short)x },
        { (typeof(short), typeof(sbyte)), static x => (sbyte)(short)x },
        { (typeof(short), typeof(ushort)), static x => (ushort)(short)x },
        { (typeof(short), typeof(int)), static x => (int)(short)x },
        { (typeof(short), typeof(uint)), static x => (uint)(short)x },
        { (typeof(short), typeof(long)), static x => (long)(short)x },
        { (typeof(short), typeof(ulong)), static x => (ulong)(short)x },
        { (typeof(short), typeof(char)), static x => (char)(short)x },
        { (typeof(short), typeof(double)), static x => (double)(short)x },
        { (typeof(short), typeof(float)), static x => (float)(short)x },
        // ushort
        { (typeof(ushort), typeof(byte)), static x => (byte)(ushort)x },
        { (typeof(ushort), typeof(sbyte)), static x => (sbyte)(ushort)x },
        { (typeof(ushort), typeof(short)), static x => (short)(ushort)x },
        { (typeof(ushort), typeof(int)), static x => (int)(ushort)x },
        { (typeof(ushort), typeof(uint)), static x => (uint)(ushort)x },
        { (typeof(ushort), typeof(long)), static x => (long)(ushort)x },
        { (typeof(ushort), typeof(ulong)), static x => (ulong)(ushort)x },
        { (typeof(ushort), typeof(char)), static x => (char)(ushort)x },
        { (typeof(ushort), typeof(double)), static x => (double)(ushort)x },
        { (typeof(ushort), typeof(float)), static x => (float)(ushort)x },
        // int
        { (typeof(int), typeof(byte)), static x => (byte)(int)x },
        { (typeof(int), typeof(sbyte)), static x => (sbyte)(int)x },
        { (typeof(int), typeof(short)), static x => (short)(int)x },
        { (typeof(int), typeof(ushort)), static x => (ushort)(int)x },
        { (typeof(int), typeof(uint)), static x => (uint)(int)x },
        { (typeof(int), typeof(long)), static x => (long)(int)x },
        { (typeof(int), typeof(ulong)), static x => (ulong)(int)x },
        { (typeof(int), typeof(char)), static x => (char)(int)x },
        { (typeof(int), typeof(double)), static x => (double)(int)x },
        { (typeof(int), typeof(float)), static x => (float)(int)x },
        // uint
        { (typeof(uint), typeof(byte)), static x => (byte)(uint)x },
        { (typeof(uint), typeof(sbyte)), static x => (sbyte)(uint)x },
        { (typeof(uint), typeof(short)), static x => (short)(uint)x },
        { (typeof(uint), typeof(ushort)), static x => (ushort)(uint)x },
        { (typeof(uint), typeof(int)), static x => (int)(uint)x },
        { (typeof(uint), typeof(long)), static x => (long)(uint)x },
        { (typeof(uint), typeof(ulong)), static x => (ulong)(uint)x },
        { (typeof(uint), typeof(char)), static x => (char)(uint)x },
        { (typeof(uint), typeof(double)), static x => (double)(uint)x },
        { (typeof(uint), typeof(float)), static x => (float)(uint)x },
        // long
        { (typeof(long), typeof(byte)), static x => (byte)(long)x },
        { (typeof(long), typeof(sbyte)), static x => (sbyte)(long)x },
        { (typeof(long), typeof(short)), static x => (short)(long)x },
        { (typeof(long), typeof(ushort)), static x => (ushort)(long)x },
        { (typeof(long), typeof(int)), static x => (int)(long)x },
        { (typeof(long), typeof(uint)), static x => (uint)(long)x },
        { (typeof(long), typeof(ulong)), static x => (ulong)(long)x },
        { (typeof(long), typeof(char)), static x => (char)(long)x },
        { (typeof(long), typeof(double)), static x => (double)(long)x },
        { (typeof(long), typeof(float)), static x => (float)(long)x },
        // ulong
        { (typeof(ulong), typeof(byte)), static x => (byte)(ulong)x },
        { (typeof(ulong), typeof(sbyte)), static x => (sbyte)(ulong)x },
        { (typeof(ulong), typeof(short)), static x => (short)(ulong)x },
        { (typeof(ulong), typeof(ushort)), static x => (ushort)(ulong)x },
        { (typeof(ulong), typeof(int)), static x => (int)(ulong)x },
        { (typeof(ulong), typeof(uint)), static x => (uint)(ulong)x },
        { (typeof(ulong), typeof(long)), static x => (long)(ulong)x },
        { (typeof(ulong), typeof(char)), static x => (char)(ulong)x },
        { (typeof(ulong), typeof(double)), static x => (double)(ulong)x },
        { (typeof(ulong), typeof(float)), static x => (float)(ulong)x },
        // char
        { (typeof(char), typeof(byte)), static x => (byte)(char)x },
        { (typeof(char), typeof(sbyte)), static x => (sbyte)(char)x },
        { (typeof(char), typeof(short)), static x => (short)(char)x },
        { (typeof(char), typeof(ushort)), static x => (ushort)(char)x },
        { (typeof(char), typeof(int)), static x => (int)(char)x },
        { (typeof(char), typeof(uint)), static x => (uint)(char)x },
        { (typeof(char), typeof(long)), static x => (long)(char)x },
        { (typeof(char), typeof(ulong)), static x => (ulong)(char)x },
        { (typeof(char), typeof(double)), static x => (double)(char)x },
        { (typeof(char), typeof(float)), static x => (float)(char)x },
        // double
        { (typeof(double), typeof(byte)), static x => (byte)(double)x },
        { (typeof(double), typeof(sbyte)), static x => (sbyte)(double)x },
        { (typeof(double), typeof(short)), static x => (short)(double)x },
        { (typeof(double), typeof(ushort)), static x => (ushort)(double)x },
        { (typeof(double), typeof(int)), static x => (int)(double)x },
        { (typeof(double), typeof(uint)), static x => (uint)(double)x },
        { (typeof(double), typeof(long)), static x => (long)(double)x },
        { (typeof(double), typeof(ulong)), static x => (ulong)(double)x },
        { (typeof(double), typeof(char)), static x => (char)(double)x },
        { (typeof(double), typeof(float)), static x => (float)(double)x },
        // float
        { (typeof(float), typeof(byte)), static x => (byte)(float)x },
        { (typeof(float), typeof(sbyte)), static x => (sbyte)(float)x },
        { (typeof(float), typeof(short)), static x => (short)(float)x },
        { (typeof(float), typeof(ushort)), static x => (ushort)(float)x },
        { (typeof(float), typeof(int)), static x => (int)(float)x },
        { (typeof(float), typeof(uint)), static x => (uint)(float)x },
        { (typeof(float), typeof(long)), static x => (long)(float)x },
        { (typeof(float), typeof(ulong)), static x => (ulong)(float)x },
        { (typeof(float), typeof(char)), static x => (char)(float)x },
        { (typeof(float), typeof(double)), static x => (double)(float)x }
    };

    public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
    {
        if (sourceType.IsValueType && targetType.IsValueType)
        {
            var key = (sourceType, targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType);
            if (Converters.TryGetValue(key, out var converter))
            {
                return converter;
            }
        }

        return null;
    }
}
