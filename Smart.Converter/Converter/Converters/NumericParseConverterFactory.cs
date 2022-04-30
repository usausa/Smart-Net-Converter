#nullable disable
namespace Smart.Converter.Converters;

public sealed class NumericParseConverterFactory : IConverterFactory
{
    private static readonly Dictionary<Type, Func<object, object>> Converters = new()
    {
        { typeof(byte), static x => Byte.TryParse((string)x, out var result) ? result : default },
        { typeof(byte?), static x => Byte.TryParse((string)x, out var result) ? result : default(byte?) },
        { typeof(sbyte), static x => SByte.TryParse((string)x, out var result) ? result : default },
        { typeof(sbyte?), static x => SByte.TryParse((string)x, out var result) ? result : default(sbyte?) },
        { typeof(short), static x => Int16.TryParse((string)x, out var result) ? result : default },
        { typeof(short?), static x => Int16.TryParse((string)x, out var result) ? result : default(short?) },
        { typeof(ushort), static x => UInt16.TryParse((string)x, out var result) ? result : default },
        { typeof(ushort?), static x => UInt16.TryParse((string)x, out var result) ? result : default(ushort?) },
        { typeof(int), static x => Int32.TryParse((string)x, out var result) ? result : default },
        { typeof(int?), static x => Int32.TryParse((string)x, out var result) ? result : default(int?) },
        { typeof(uint), static x => UInt32.TryParse((string)x, out var result) ? result : default },
        { typeof(uint?), static x => UInt32.TryParse((string)x, out var result) ? result : default(uint?) },
        { typeof(long), static x => Int64.TryParse((string)x, out var result) ? result : default },
        { typeof(long?), static x => Int64.TryParse((string)x, out var result) ? result : default(long?) },
        { typeof(ulong), static x => UInt64.TryParse((string)x, out var result) ? result : default },
        { typeof(ulong?), static x => UInt64.TryParse((string)x, out var result) ? result : default(ulong?) },
        { typeof(char), static x => Char.TryParse((string)x, out var result) ? result : default },
        { typeof(char?), static x => Char.TryParse((string)x, out var result) ? result : default(char?) },
        { typeof(double), static x => Double.TryParse((string)x, out var result) ? result : default },
        { typeof(double?), static x => Double.TryParse((string)x, out var result) ? result : default(double?) },
        { typeof(float), static x => Single.TryParse((string)x, out var result) ? result : default },
        { typeof(float?), static x => Single.TryParse((string)x, out var result) ? result : default(float?) }
    };

    public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
    {
        if ((sourceType == typeof(string)) &&
            Converters.TryGetValue(targetType, out var converter))
        {
            return converter;
        }

        return null;
    }
}
