namespace Smart.Converter.Converters;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

public sealed class DecimalConverterFactory : IConverterFactory
{
    private static readonly Dictionary<(Type, Type), Func<object, object?>> Converters = new()
    {
        // From decimal to integer (kept as try/catch: Decimal.ToXxx rounds and throws at the range
        { (typeof(decimal), typeof(byte)), static x => { try { return Decimal.ToByte((decimal)x); } catch (OverflowException) { return default(byte); } } },
        { (typeof(decimal), typeof(byte?)), static x => { try { return Decimal.ToByte((decimal)x); } catch (OverflowException) { return default(byte?); } } },
        { (typeof(decimal), typeof(sbyte)), static x => { try { return Decimal.ToSByte((decimal)x); } catch (OverflowException) { return default(sbyte); } } },
        { (typeof(decimal), typeof(sbyte?)), static x => { try { return Decimal.ToSByte((decimal)x); } catch (OverflowException) { return default(sbyte?); } } },
        { (typeof(decimal), typeof(short)), static x => { try { return Decimal.ToInt16((decimal)x); } catch (OverflowException) { return default(short); } } },
        { (typeof(decimal), typeof(short?)), static x => { try { return Decimal.ToInt16((decimal)x); } catch (OverflowException) { return default(short?); } } },
        { (typeof(decimal), typeof(ushort)), static x => { try { return Decimal.ToUInt16((decimal)x); } catch (OverflowException) { return default(ushort); } } },
        { (typeof(decimal), typeof(ushort?)), static x => { try { return Decimal.ToUInt16((decimal)x); } catch (OverflowException) { return default(ushort?); } } },
        { (typeof(decimal), typeof(int)), static x => { try { return Decimal.ToInt32((decimal)x); } catch (OverflowException) { return default(int); } } },
        { (typeof(decimal), typeof(int?)), static x => { try { return Decimal.ToInt32((decimal)x); } catch (OverflowException) { return default(int?); } } },
        { (typeof(decimal), typeof(uint)), static x => { try { return Decimal.ToUInt32((decimal)x); } catch (OverflowException) { return default(uint); } } },
        { (typeof(decimal), typeof(uint?)), static x => { try { return Decimal.ToUInt32((decimal)x); } catch (OverflowException) { return default(uint?); } } },
        { (typeof(decimal), typeof(long)), static x => { try { return Decimal.ToInt64((decimal)x); } catch (OverflowException) { return default(long); } } },
        { (typeof(decimal), typeof(long?)), static x => { try { return Decimal.ToInt64((decimal)x); } catch (OverflowException) { return default(long?); } } },
        { (typeof(decimal), typeof(ulong)), static x => { try { return Decimal.ToUInt64((decimal)x); } catch (OverflowException) { return default(ulong); } } },
        { (typeof(decimal), typeof(ulong?)), static x => { try { return Decimal.ToUInt64((decimal)x); } catch (OverflowException) { return default(ulong?); } } },
        { (typeof(decimal), typeof(char)), static x => { try { return (char)Decimal.ToUInt16((decimal)x); } catch (OverflowException) { return default(char); } } },
        { (typeof(decimal), typeof(char?)), static x => { try { return (char)Decimal.ToUInt16((decimal)x); } catch (OverflowException) { return default(char?); } } },
        // decimal to double/float: double/float have a wider range than decimal, so Decimal.ToDouble/ToSingle never overflow (no catch needed)
        { (typeof(decimal), typeof(double)), static x => Decimal.ToDouble((decimal)x) },
        { (typeof(decimal), typeof(double?)), static x => Decimal.ToDouble((decimal)x) },
        { (typeof(decimal), typeof(float)), static x => Decimal.ToSingle((decimal)x) },
        { (typeof(decimal), typeof(float?)), static x => Decimal.ToSingle((decimal)x) },
        { (typeof(decimal), typeof(string)), static x => ((decimal)x).ToString(CultureInfo.InvariantCulture) },
        // To Decimal
        { (typeof(byte), typeof(decimal)), static x => new decimal((byte)x) },
        { (typeof(sbyte), typeof(decimal)), static x => new decimal((sbyte)x) },
        { (typeof(short), typeof(decimal)), static x => new decimal((short)x) },
        { (typeof(ushort), typeof(decimal)), static x => new decimal((ushort)x) },
        { (typeof(int), typeof(decimal)), static x => new decimal((int)x) },
        { (typeof(uint), typeof(decimal)), static x => new decimal((uint)x) },
        { (typeof(long), typeof(decimal)), static x => new decimal((long)x) },
        { (typeof(ulong), typeof(decimal)), static x => new decimal((ulong)x) },
        { (typeof(char), typeof(decimal)), static x => new decimal((char)x) },
        // double/float to decimal (kept as try/catch: new decimal(double/float) throws when out of range
        { (typeof(double), typeof(decimal)), static x => { try { return new decimal((double)x); } catch (OverflowException) { return default(decimal); } } },
        { (typeof(float), typeof(decimal)), static x => { try { return new decimal((float)x); } catch (OverflowException) { return default(decimal); } } },
        { (typeof(string), typeof(decimal)), static x => Decimal.TryParse((string)x, NumberStyles.Number, CultureInfo.InvariantCulture, out var result) ? result : default },
        // To Decimal?
        { (typeof(byte), typeof(decimal?)), static x => new decimal((byte)x) },
        { (typeof(sbyte), typeof(decimal?)), static x => new decimal((sbyte)x) },
        { (typeof(short), typeof(decimal?)), static x => new decimal((short)x) },
        { (typeof(ushort), typeof(decimal?)), static x => new decimal((ushort)x) },
        { (typeof(int), typeof(decimal?)), static x => new decimal((int)x) },
        { (typeof(uint), typeof(decimal?)), static x => new decimal((uint)x) },
        { (typeof(long), typeof(decimal?)), static x => new decimal((long)x) },
        { (typeof(ulong), typeof(decimal?)), static x => new decimal((ulong)x) },
        { (typeof(char), typeof(decimal?)), static x => new decimal((char)x) },
        { (typeof(double), typeof(decimal?)), static x => { try { return new decimal((double)x); } catch (OverflowException) { return default(decimal?); } } },
        { (typeof(float), typeof(decimal?)), static x => { try { return new decimal((float)x); } catch (OverflowException) { return default(decimal?); } } },
        { (typeof(string), typeof(decimal?)), static x => Decimal.TryParse((string)x, NumberStyles.Number, CultureInfo.InvariantCulture, out var result) ? result : default(decimal?) }
    };

    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    public Func<object, object?>? GetConverter(IObjectConverter context, Type sourceType, Type targetType)
    {
        var key = (sourceType, targetType);
        return Converters.GetValueOrDefault(key);
    }
}
