#nullable disable
namespace Smart.Converter.Converters;

using System.Numerics;

public sealed class BigIntegerConverterFactory : IConverterFactory
{
#pragma warning disable CA1305
    private static readonly Dictionary<(Type, Type), Func<object, object>> Converters = new()
    {
        // From decimal
        { (typeof(BigInteger), typeof(byte)), static x => { try { return (byte)(BigInteger)x; } catch (OverflowException) { return default(byte); } } },
        { (typeof(BigInteger), typeof(byte?)), static x => { try { return (byte)(BigInteger)x; } catch (OverflowException) { return default(byte?); } } },
        { (typeof(BigInteger), typeof(sbyte)), static x => { try { return (sbyte)(BigInteger)x; } catch (OverflowException) { return default(sbyte); } } },
        { (typeof(BigInteger), typeof(sbyte?)), static x => { try { return (sbyte)(BigInteger)x; } catch (OverflowException) { return default(sbyte?); } } },
        { (typeof(BigInteger), typeof(short)), static x => { try { return (short)(BigInteger)x; } catch (OverflowException) { return default(short); } } },
        { (typeof(BigInteger), typeof(short?)), static x => { try { return (short)(BigInteger)x; } catch (OverflowException) { return default(short?); } } },
        { (typeof(BigInteger), typeof(ushort)), static x => { try { return (ushort)(BigInteger)x; } catch (OverflowException) { return default(ushort); } } },
        { (typeof(BigInteger), typeof(ushort?)), static x => { try { return (ushort)(BigInteger)x; } catch (OverflowException) { return default(ushort?); } } },
        { (typeof(BigInteger), typeof(int)), static x => { try { return (int)(BigInteger)x; } catch (OverflowException) { return default(int); } } },
        { (typeof(BigInteger), typeof(int?)), static x => { try { return (int)(BigInteger)x; } catch (OverflowException) { return default(int?); } } },
        { (typeof(BigInteger), typeof(uint)), static x => { try { return (uint)(BigInteger)x; } catch (OverflowException) { return default(uint); } } },
        { (typeof(BigInteger), typeof(uint?)), static x => { try { return (uint)(BigInteger)x; } catch (OverflowException) { return default(uint?); } } },
        { (typeof(BigInteger), typeof(long)), static x => { try { return (long)(BigInteger)x; } catch (OverflowException) { return default(long); } } },
        { (typeof(BigInteger), typeof(long?)), static x => { try { return (long)(BigInteger)x; } catch (OverflowException) { return default(long?); } } },
        { (typeof(BigInteger), typeof(ulong)), static x => { try { return (ulong)(BigInteger)x; } catch (OverflowException) { return default(ulong); } } },
        { (typeof(BigInteger), typeof(ulong?)), static x => { try { return (ulong)(BigInteger)x; } catch (OverflowException) { return default(ulong?); } } },
        { (typeof(BigInteger), typeof(char)), static x => { try { return (char)(BigInteger)x; } catch (OverflowException) { return default(char); } } },
        { (typeof(BigInteger), typeof(char?)), static x => { try { return (char)(BigInteger)x; } catch (OverflowException) { return default(char?); } } },
        { (typeof(BigInteger), typeof(float)), static x => { try { return (float)(BigInteger)x; } catch (OverflowException) { return default(float); } } },
        { (typeof(BigInteger), typeof(float?)), static x => { try { return (float)(BigInteger)x; } catch (OverflowException) { return default(float?); } } },
        { (typeof(BigInteger), typeof(double)), static x => { try { return (double)(BigInteger)x; } catch (OverflowException) { return default(double); } } },
        { (typeof(BigInteger), typeof(double?)), static x => { try { return (double)(BigInteger)x; } catch (OverflowException) { return default(double?); } } },
        { (typeof(BigInteger), typeof(decimal)), static x => { try { return (decimal)(BigInteger)x; } catch (OverflowException) { return default(decimal); } } },
        { (typeof(BigInteger), typeof(decimal?)), static x => { try { return (decimal)(BigInteger)x; } catch (OverflowException) { return default(decimal?); } } },
        { (typeof(BigInteger), typeof(string)), static x => ((BigInteger)x).ToString() },
        // To BigInteger
        { (typeof(byte), typeof(BigInteger)), static x => new BigInteger((byte)x) },
        { (typeof(sbyte), typeof(BigInteger)), static x => new BigInteger((sbyte)x) },
        { (typeof(short), typeof(BigInteger)), static x => new BigInteger((short)x) },
        { (typeof(ushort), typeof(BigInteger)), static x => new BigInteger((ushort)x) },
        { (typeof(int), typeof(BigInteger)), static x => new BigInteger((int)x) },
        { (typeof(uint), typeof(BigInteger)), static x => new BigInteger((uint)x) },
        { (typeof(long), typeof(BigInteger)), static x => new BigInteger((long)x) },
        { (typeof(ulong), typeof(BigInteger)), static x => new BigInteger((ulong)x) },
        { (typeof(char), typeof(BigInteger)), static x => new BigInteger((char)x) },
        { (typeof(double), typeof(BigInteger)), static x => { try { return new BigInteger((double)x); } catch (OverflowException) { return default(BigInteger); } } },
        { (typeof(float), typeof(BigInteger)), static x => { try { return new BigInteger((float)x); } catch (OverflowException) { return default(BigInteger); } } },
        { (typeof(decimal), typeof(BigInteger)), static x => { try { return new BigInteger((decimal)x); } catch (OverflowException) { return default(BigInteger); } } },
        { (typeof(string), typeof(BigInteger)), static x => BigInteger.TryParse((string)x, out var result) ? result : default },
        // To BigInteger?
        { (typeof(byte), typeof(BigInteger?)), static x => new BigInteger((byte)x) },
        { (typeof(sbyte), typeof(BigInteger?)), static x => new BigInteger((sbyte)x) },
        { (typeof(short), typeof(BigInteger?)), static x => new BigInteger((short)x) },
        { (typeof(ushort), typeof(BigInteger?)), static x => new BigInteger((ushort)x) },
        { (typeof(int), typeof(BigInteger?)), static x => new BigInteger((int)x) },
        { (typeof(uint), typeof(BigInteger?)), static x => new BigInteger((uint)x) },
        { (typeof(long), typeof(BigInteger?)), static x => new BigInteger((long)x) },
        { (typeof(ulong), typeof(BigInteger?)), static x => new BigInteger((ulong)x) },
        { (typeof(char), typeof(BigInteger?)), static x => new BigInteger((char)x) },
        { (typeof(double), typeof(BigInteger?)), static x => { try { return new BigInteger((double)x); } catch (OverflowException) { return default(BigInteger?); } } },
        { (typeof(float), typeof(BigInteger?)), static x => { try { return new BigInteger((float)x); } catch (OverflowException) { return default(BigInteger?); } } },
        { (typeof(decimal), typeof(BigInteger?)), static x => { try { return new BigInteger((decimal)x); } catch (OverflowException) { return default(BigInteger?); } } },
        { (typeof(string), typeof(BigInteger?)), static x => BigInteger.TryParse((string)x, out var result) ? result : default(BigInteger?) }
    };
#pragma warning restore CA1305

    public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
    {
        return Converters.GetValueOrDefault((sourceType, targetType));
    }
}
