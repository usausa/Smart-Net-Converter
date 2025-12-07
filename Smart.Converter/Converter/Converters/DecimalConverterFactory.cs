#nullable disable
namespace Smart.Converter.Converters;

using System.Globalization;

public sealed class DecimalConverterFactory : IConverterFactory
{
    private static readonly Dictionary<(Type, Type), Func<object, object>> Converters = new()
    {
        // From decimal
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
        { (typeof(decimal), typeof(double)), static x => { try { return Decimal.ToDouble((decimal)x); } catch (OverflowException) { return default(double); } } },
        { (typeof(decimal), typeof(double?)), static x => { try { return Decimal.ToDouble((decimal)x); } catch (OverflowException) { return default(double?); } } },
        { (typeof(decimal), typeof(float)), static x => { try { return Decimal.ToSingle((decimal)x); } catch (OverflowException) { return default(float); } } },
        { (typeof(decimal), typeof(float?)), static x => { try { return Decimal.ToSingle((decimal)x); } catch (OverflowException) { return default(float?); } } },
        { (typeof(decimal), typeof(string)), static x => ((decimal)x).ToString(CultureInfo.CurrentCulture) },
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
        { (typeof(double), typeof(decimal)), static x => { try { return new decimal((double)x); } catch (OverflowException) { return default(decimal); } } },
        { (typeof(float), typeof(decimal)), static x => { try { return new decimal((float)x); } catch (OverflowException) { return default(decimal); } } },
        { (typeof(string), typeof(decimal)), static x => Decimal.TryParse((string)x, out var result) ? result : default },
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
        { (typeof(string), typeof(decimal?)), static x => Decimal.TryParse((string)x, out var result) ? result : default(decimal?) }
    };

    public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
    {
        var key = (sourceType, targetType);
        return Converters.GetValueOrDefault(key);
    }
}
