namespace Smart.Converter.Converters;

using System.Diagnostics.CodeAnalysis;
using System.Numerics;

public sealed class BigIntegerConverterFactory : IConverterFactory
{
    private static readonly BigInteger DecimalMinValue = new(Decimal.MinValue);

    private static readonly BigInteger DecimalMaxValue = new(Decimal.MaxValue);

#pragma warning disable CA1305
    private static readonly Dictionary<(Type, Type), Func<object, object?>> Converters = new()
    {
        // From BigInteger to integer (range pre-checked; out-of-range returns default instead of catching OverflowException)
        { (typeof(BigInteger), typeof(byte)), static x => x is BigInteger b && (b >= byte.MinValue) && (b <= byte.MaxValue) ? (byte)b : default },
        { (typeof(BigInteger), typeof(byte?)), static x => x is BigInteger b && (b >= byte.MinValue) && (b <= byte.MaxValue) ? (byte)b : default(byte?) },
        { (typeof(BigInteger), typeof(sbyte)), static x => x is BigInteger b && (b >= sbyte.MinValue) && (b <= sbyte.MaxValue) ? (sbyte)b : default },
        { (typeof(BigInteger), typeof(sbyte?)), static x => x is BigInteger b && (b >= sbyte.MinValue) && (b <= sbyte.MaxValue) ? (sbyte)b : default(sbyte?) },
        { (typeof(BigInteger), typeof(short)), static x => x is BigInteger b && (b >= short.MinValue) && (b <= short.MaxValue) ? (short)b : default },
        { (typeof(BigInteger), typeof(short?)), static x => x is BigInteger b && (b >= short.MinValue) && (b <= short.MaxValue) ? (short)b : default(short?) },
        { (typeof(BigInteger), typeof(ushort)), static x => x is BigInteger b && (b >= ushort.MinValue) && (b <= ushort.MaxValue) ? (ushort)b : default },
        { (typeof(BigInteger), typeof(ushort?)), static x => x is BigInteger b && (b >= ushort.MinValue) && (b <= ushort.MaxValue) ? (ushort)b : default(ushort?) },
        { (typeof(BigInteger), typeof(int)), static x => x is BigInteger b && (b >= int.MinValue) && (b <= int.MaxValue) ? (int)b : default },
        { (typeof(BigInteger), typeof(int?)), static x => x is BigInteger b && (b >= int.MinValue) && (b <= int.MaxValue) ? (int)b : default(int?) },
        { (typeof(BigInteger), typeof(uint)), static x => x is BigInteger b && (b >= uint.MinValue) && (b <= uint.MaxValue) ? (uint)b : default },
        { (typeof(BigInteger), typeof(uint?)), static x => x is BigInteger b && (b >= uint.MinValue) && (b <= uint.MaxValue) ? (uint)b : default(uint?) },
        { (typeof(BigInteger), typeof(long)), static x => x is BigInteger b && (b >= long.MinValue) && (b <= long.MaxValue) ? (long)b : default },
        { (typeof(BigInteger), typeof(long?)), static x => x is BigInteger b && (b >= long.MinValue) && (b <= long.MaxValue) ? (long)b : default(long?) },
        { (typeof(BigInteger), typeof(ulong)), static x => x is BigInteger b && (b >= ulong.MinValue) && (b <= ulong.MaxValue) ? (ulong)b : default },
        { (typeof(BigInteger), typeof(ulong?)), static x => x is BigInteger b && (b >= ulong.MinValue) && (b <= ulong.MaxValue) ? (ulong)b : default(ulong?) },
        { (typeof(BigInteger), typeof(char)), static x => x is BigInteger b && (b >= ushort.MinValue) && (b <= ushort.MaxValue) ? (char)b : default },
        { (typeof(BigInteger), typeof(char?)), static x => x is BigInteger b && (b >= ushort.MinValue) && (b <= ushort.MaxValue) ? (char)b : default(char?) },
        // From BigInteger to floating point ((float)/(double) return +/-Infinity for out-of-range and never throw)
        { (typeof(BigInteger), typeof(float)), static x => (float)(BigInteger)x },
        { (typeof(BigInteger), typeof(float?)), static x => (float)(BigInteger)x },
        { (typeof(BigInteger), typeof(double)), static x => (double)(BigInteger)x },
        { (typeof(BigInteger), typeof(double?)), static x => (double)(BigInteger)x },
        // From BigInteger to decimal (range pre-checked against the cached decimal bounds)
        { (typeof(BigInteger), typeof(decimal)), static x => x is BigInteger b && (b >= DecimalMinValue) && (b <= DecimalMaxValue) ? (decimal)b : default },
        { (typeof(BigInteger), typeof(decimal?)), static x => x is BigInteger b && (b >= DecimalMinValue) && (b <= DecimalMaxValue) ? (decimal)b : default(decimal?) },
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
        // double/float to BigInteger (NaN/Infinity pre-checked with IsFinite instead of catching OverflowException)
        { (typeof(double), typeof(BigInteger)), static x => Double.IsFinite((double)x) ? new BigInteger((double)x) : default },
        { (typeof(float), typeof(BigInteger)), static x => Single.IsFinite((float)x) ? new BigInteger((float)x) : default },
        // decimal to BigInteger (new BigInteger(decimal) never throws, so no catch is needed)
        { (typeof(decimal), typeof(BigInteger)), static x => new BigInteger((decimal)x) },
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
        { (typeof(double), typeof(BigInteger?)), static x => Double.IsFinite((double)x) ? new BigInteger((double)x) : default(BigInteger?) },
        { (typeof(float), typeof(BigInteger?)), static x => Single.IsFinite((float)x) ? new BigInteger((float)x) : default(BigInteger?) },
        { (typeof(decimal), typeof(BigInteger?)), static x => new BigInteger((decimal)x) },
        { (typeof(string), typeof(BigInteger?)), static x => BigInteger.TryParse((string)x, out var result) ? result : default(BigInteger?) }
    };
#pragma warning restore CA1305

    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    public Func<object, object?>? GetConverter(IObjectConverter context, Type sourceType, Type targetType)
    {
        return Converters.GetValueOrDefault((sourceType, targetType));
    }
}
