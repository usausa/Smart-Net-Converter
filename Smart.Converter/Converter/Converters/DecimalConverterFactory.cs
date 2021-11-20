#nullable disable
namespace Smart.Converter.Converters;

using System;
using System.Collections.Generic;
using System.Globalization;

public sealed class DecimalConverterFactory : IConverterFactory
{
    private static readonly Dictionary<Tuple<Type, Type>, Func<object, object>> Converters = new()
    {
        // From decimal
        { Tuple.Create(typeof(decimal), typeof(byte)), x => { try { return Decimal.ToByte((decimal)x); } catch (OverflowException) { return default(byte); } } },
        { Tuple.Create(typeof(decimal), typeof(byte?)), x => { try { return Decimal.ToByte((decimal)x); } catch (OverflowException) { return default(byte?); } } },
        { Tuple.Create(typeof(decimal), typeof(sbyte)), x => { try { return Decimal.ToSByte((decimal)x); } catch (OverflowException) { return default(sbyte); } } },
        { Tuple.Create(typeof(decimal), typeof(sbyte?)), x => { try { return Decimal.ToSByte((decimal)x); } catch (OverflowException) { return default(sbyte?); } } },
        { Tuple.Create(typeof(decimal), typeof(short)), x => { try { return Decimal.ToInt16((decimal)x); } catch (OverflowException) { return default(short); } } },
        { Tuple.Create(typeof(decimal), typeof(short?)), x => { try { return Decimal.ToInt16((decimal)x); } catch (OverflowException) { return default(short?); } } },
        { Tuple.Create(typeof(decimal), typeof(ushort)), x => { try { return Decimal.ToUInt16((decimal)x); } catch (OverflowException) { return default(ushort); } } },
        { Tuple.Create(typeof(decimal), typeof(ushort?)), x => { try { return Decimal.ToUInt16((decimal)x); } catch (OverflowException) { return default(ushort?); } } },
        { Tuple.Create(typeof(decimal), typeof(int)), x => { try { return Decimal.ToInt32((decimal)x); } catch (OverflowException) { return default(int); } } },
        { Tuple.Create(typeof(decimal), typeof(int?)), x => { try { return Decimal.ToInt32((decimal)x); } catch (OverflowException) { return default(int?); } } },
        { Tuple.Create(typeof(decimal), typeof(uint)), x => { try { return Decimal.ToUInt32((decimal)x); } catch (OverflowException) { return default(uint); } } },
        { Tuple.Create(typeof(decimal), typeof(uint?)), x => { try { return Decimal.ToUInt32((decimal)x); } catch (OverflowException) { return default(uint?); } } },
        { Tuple.Create(typeof(decimal), typeof(long)), x => { try { return Decimal.ToInt64((decimal)x); } catch (OverflowException) { return default(long); } } },
        { Tuple.Create(typeof(decimal), typeof(long?)), x => { try { return Decimal.ToInt64((decimal)x); } catch (OverflowException) { return default(long?); } } },
        { Tuple.Create(typeof(decimal), typeof(ulong)), x => { try { return Decimal.ToUInt64((decimal)x); } catch (OverflowException) { return default(ulong); } } },
        { Tuple.Create(typeof(decimal), typeof(ulong?)), x => { try { return Decimal.ToUInt64((decimal)x); } catch (OverflowException) { return default(ulong?); } } },
        { Tuple.Create(typeof(decimal), typeof(char)), x => { try { return (char)Decimal.ToUInt16((decimal)x); } catch (OverflowException) { return default(char); } } },
        { Tuple.Create(typeof(decimal), typeof(char?)), x => { try { return (char)Decimal.ToUInt16((decimal)x); } catch (OverflowException) { return default(char?); } } },
        { Tuple.Create(typeof(decimal), typeof(double)), x => { try { return Decimal.ToDouble((decimal)x); } catch (OverflowException) { return default(double); } } },
        { Tuple.Create(typeof(decimal), typeof(double?)), x => { try { return Decimal.ToDouble((decimal)x); } catch (OverflowException) { return default(double?); } } },
        { Tuple.Create(typeof(decimal), typeof(float)), x => { try { return Decimal.ToSingle((decimal)x); } catch (OverflowException) { return default(float); } } },
        { Tuple.Create(typeof(decimal), typeof(float?)), x => { try { return Decimal.ToSingle((decimal)x); } catch (OverflowException) { return default(float?); } } },
        { Tuple.Create(typeof(decimal), typeof(string)), x => ((decimal)x).ToString(CultureInfo.CurrentCulture) },
        // To Decimal
        { Tuple.Create(typeof(byte), typeof(decimal)), x => new decimal((byte)x) },
        { Tuple.Create(typeof(sbyte), typeof(decimal)), x => new decimal((sbyte)x) },
        { Tuple.Create(typeof(short), typeof(decimal)), x => new decimal((short)x) },
        { Tuple.Create(typeof(ushort), typeof(decimal)), x => new decimal((ushort)x) },
        { Tuple.Create(typeof(int), typeof(decimal)), x => new decimal((int)x) },
        { Tuple.Create(typeof(uint), typeof(decimal)), x => new decimal((uint)x) },
        { Tuple.Create(typeof(long), typeof(decimal)), x => new decimal((long)x) },
        { Tuple.Create(typeof(ulong), typeof(decimal)), x => new decimal((ulong)x) },
        { Tuple.Create(typeof(char), typeof(decimal)), x => new decimal((char)x) },
        { Tuple.Create(typeof(double), typeof(decimal)), x => { try { return new decimal((double)x); } catch (OverflowException) { return default(decimal); } } },
        { Tuple.Create(typeof(float), typeof(decimal)), x => { try { return new decimal((float)x); } catch (OverflowException) { return default(decimal); } } },
        { Tuple.Create(typeof(string), typeof(decimal)), x => Decimal.TryParse((string)x, out var result) ? result : default },
        // To Decimal?
        { Tuple.Create(typeof(byte), typeof(decimal?)), x => new decimal((byte)x) },
        { Tuple.Create(typeof(sbyte), typeof(decimal?)), x => new decimal((sbyte)x) },
        { Tuple.Create(typeof(short), typeof(decimal?)), x => new decimal((short)x) },
        { Tuple.Create(typeof(ushort), typeof(decimal?)), x => new decimal((ushort)x) },
        { Tuple.Create(typeof(int), typeof(decimal?)), x => new decimal((int)x) },
        { Tuple.Create(typeof(uint), typeof(decimal?)), x => new decimal((uint)x) },
        { Tuple.Create(typeof(long), typeof(decimal?)), x => new decimal((long)x) },
        { Tuple.Create(typeof(ulong), typeof(decimal?)), x => new decimal((ulong)x) },
        { Tuple.Create(typeof(char), typeof(decimal?)), x => new decimal((char)x) },
        { Tuple.Create(typeof(double), typeof(decimal?)), x => { try { return new decimal((double)x); } catch (OverflowException) { return default(decimal?); } } },
        { Tuple.Create(typeof(float), typeof(decimal?)), x => { try { return new decimal((float)x); } catch (OverflowException) { return default(decimal?); } } },
        { Tuple.Create(typeof(string), typeof(decimal?)), x => Decimal.TryParse((string)x, out var result) ? result : default(decimal?) }
    };

    public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
    {
        var key = Tuple.Create(sourceType, targetType);
        if (Converters.TryGetValue(key, out var converter))
        {
            return converter;
        }

        return null;
    }
}
