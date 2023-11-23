#nullable disable
namespace Smart.Converter.Converters;

using System.Globalization;

public sealed class DecimalConverterFactory : IConverterFactory
{
    private static readonly Dictionary<Tuple<Type, Type>, Func<object, object>> Converters = new()
    {
        // From decimal
        { Tuple.Create(typeof(decimal), typeof(byte)), static x => { try { return Decimal.ToByte((decimal)x); } catch (OverflowException) { return default(byte); } } },
        { Tuple.Create(typeof(decimal), typeof(byte?)), static x => { try { return Decimal.ToByte((decimal)x); } catch (OverflowException) { return default(byte?); } } },
        { Tuple.Create(typeof(decimal), typeof(sbyte)), static x => { try { return Decimal.ToSByte((decimal)x); } catch (OverflowException) { return default(sbyte); } } },
        { Tuple.Create(typeof(decimal), typeof(sbyte?)), static x => { try { return Decimal.ToSByte((decimal)x); } catch (OverflowException) { return default(sbyte?); } } },
        { Tuple.Create(typeof(decimal), typeof(short)), static x => { try { return Decimal.ToInt16((decimal)x); } catch (OverflowException) { return default(short); } } },
        { Tuple.Create(typeof(decimal), typeof(short?)), static x => { try { return Decimal.ToInt16((decimal)x); } catch (OverflowException) { return default(short?); } } },
        { Tuple.Create(typeof(decimal), typeof(ushort)), static x => { try { return Decimal.ToUInt16((decimal)x); } catch (OverflowException) { return default(ushort); } } },
        { Tuple.Create(typeof(decimal), typeof(ushort?)), static x => { try { return Decimal.ToUInt16((decimal)x); } catch (OverflowException) { return default(ushort?); } } },
        { Tuple.Create(typeof(decimal), typeof(int)), static x => { try { return Decimal.ToInt32((decimal)x); } catch (OverflowException) { return default(int); } } },
        { Tuple.Create(typeof(decimal), typeof(int?)), static x => { try { return Decimal.ToInt32((decimal)x); } catch (OverflowException) { return default(int?); } } },
        { Tuple.Create(typeof(decimal), typeof(uint)), static x => { try { return Decimal.ToUInt32((decimal)x); } catch (OverflowException) { return default(uint); } } },
        { Tuple.Create(typeof(decimal), typeof(uint?)), static x => { try { return Decimal.ToUInt32((decimal)x); } catch (OverflowException) { return default(uint?); } } },
        { Tuple.Create(typeof(decimal), typeof(long)), static x => { try { return Decimal.ToInt64((decimal)x); } catch (OverflowException) { return default(long); } } },
        { Tuple.Create(typeof(decimal), typeof(long?)), static x => { try { return Decimal.ToInt64((decimal)x); } catch (OverflowException) { return default(long?); } } },
        { Tuple.Create(typeof(decimal), typeof(ulong)), static x => { try { return Decimal.ToUInt64((decimal)x); } catch (OverflowException) { return default(ulong); } } },
        { Tuple.Create(typeof(decimal), typeof(ulong?)), static x => { try { return Decimal.ToUInt64((decimal)x); } catch (OverflowException) { return default(ulong?); } } },
        { Tuple.Create(typeof(decimal), typeof(char)), static x => { try { return (char)Decimal.ToUInt16((decimal)x); } catch (OverflowException) { return default(char); } } },
        { Tuple.Create(typeof(decimal), typeof(char?)), static x => { try { return (char)Decimal.ToUInt16((decimal)x); } catch (OverflowException) { return default(char?); } } },
        { Tuple.Create(typeof(decimal), typeof(double)), static x => { try { return Decimal.ToDouble((decimal)x); } catch (OverflowException) { return default(double); } } },
        { Tuple.Create(typeof(decimal), typeof(double?)), static x => { try { return Decimal.ToDouble((decimal)x); } catch (OverflowException) { return default(double?); } } },
        { Tuple.Create(typeof(decimal), typeof(float)), static x => { try { return Decimal.ToSingle((decimal)x); } catch (OverflowException) { return default(float); } } },
        { Tuple.Create(typeof(decimal), typeof(float?)), static x => { try { return Decimal.ToSingle((decimal)x); } catch (OverflowException) { return default(float?); } } },
        { Tuple.Create(typeof(decimal), typeof(string)), static x => ((decimal)x).ToString(CultureInfo.CurrentCulture) },
        // To Decimal
        { Tuple.Create(typeof(byte), typeof(decimal)), static x => new decimal((byte)x) },
        { Tuple.Create(typeof(sbyte), typeof(decimal)), static x => new decimal((sbyte)x) },
        { Tuple.Create(typeof(short), typeof(decimal)), static x => new decimal((short)x) },
        { Tuple.Create(typeof(ushort), typeof(decimal)), static x => new decimal((ushort)x) },
        { Tuple.Create(typeof(int), typeof(decimal)), static x => new decimal((int)x) },
        { Tuple.Create(typeof(uint), typeof(decimal)), static x => new decimal((uint)x) },
        { Tuple.Create(typeof(long), typeof(decimal)), static x => new decimal((long)x) },
        { Tuple.Create(typeof(ulong), typeof(decimal)), static x => new decimal((ulong)x) },
        { Tuple.Create(typeof(char), typeof(decimal)), static x => new decimal((char)x) },
        { Tuple.Create(typeof(double), typeof(decimal)), static x => { try { return new decimal((double)x); } catch (OverflowException) { return default(decimal); } } },
        { Tuple.Create(typeof(float), typeof(decimal)), static x => { try { return new decimal((float)x); } catch (OverflowException) { return default(decimal); } } },
        { Tuple.Create(typeof(string), typeof(decimal)), static x => Decimal.TryParse((string)x, out var result) ? result : default },
        // To Decimal?
        { Tuple.Create(typeof(byte), typeof(decimal?)), static x => new decimal((byte)x) },
        { Tuple.Create(typeof(sbyte), typeof(decimal?)), static x => new decimal((sbyte)x) },
        { Tuple.Create(typeof(short), typeof(decimal?)), static x => new decimal((short)x) },
        { Tuple.Create(typeof(ushort), typeof(decimal?)), static x => new decimal((ushort)x) },
        { Tuple.Create(typeof(int), typeof(decimal?)), static x => new decimal((int)x) },
        { Tuple.Create(typeof(uint), typeof(decimal?)), static x => new decimal((uint)x) },
        { Tuple.Create(typeof(long), typeof(decimal?)), static x => new decimal((long)x) },
        { Tuple.Create(typeof(ulong), typeof(decimal?)), static x => new decimal((ulong)x) },
        { Tuple.Create(typeof(char), typeof(decimal?)), static x => new decimal((char)x) },
        { Tuple.Create(typeof(double), typeof(decimal?)), static x => { try { return new decimal((double)x); } catch (OverflowException) { return default(decimal?); } } },
        { Tuple.Create(typeof(float), typeof(decimal?)), static x => { try { return new decimal((float)x); } catch (OverflowException) { return default(decimal?); } } },
        { Tuple.Create(typeof(string), typeof(decimal?)), static x => Decimal.TryParse((string)x, out var result) ? result : default(decimal?) }
    };

    public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
    {
        var key = Tuple.Create(sourceType, targetType);
        return Converters.GetValueOrDefault(key);
    }
}
