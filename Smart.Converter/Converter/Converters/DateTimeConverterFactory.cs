#nullable disable
namespace Smart.Converter.Converters;

using System.Globalization;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1305:SpecifyIFormatProvider", Justification = "Ignore")]
public sealed class DateTimeConverterFactory : IConverterFactory
{
    private static readonly Dictionary<Type, Func<object, object>> DateTimeToTickConverter = new()
    {
        { typeof(byte), static x => (byte)((DateTime)x).Ticks },
        { typeof(sbyte), static x => (sbyte)((DateTime)x).Ticks },
        { typeof(short), static x => (short)((DateTime)x).Ticks },
        { typeof(ushort), static x => (ushort)((DateTime)x).Ticks },
        { typeof(int), static x => (int)((DateTime)x).Ticks },
        { typeof(uint), static x => (uint)((DateTime)x).Ticks },
        { typeof(long), static x => ((DateTime)x).Ticks },
        { typeof(ulong), static x => (ulong)((DateTime)x).Ticks },
        { typeof(char), static x => (char)((DateTime)x).Ticks },
        { typeof(double), static x => (double)((DateTime)x).Ticks },
        { typeof(float), static x => (float)((DateTime)x).Ticks },
        { typeof(decimal), static x => (decimal)((DateTime)x).Ticks }
    };

    private static readonly Dictionary<Type, Func<object, object>> DateTimeOffsetToTickConverter = new()
    {
        { typeof(byte), static x => (byte)((DateTimeOffset)x).Ticks },
        { typeof(sbyte), static x => (sbyte)((DateTimeOffset)x).Ticks },
        { typeof(short), static x => (short)((DateTimeOffset)x).Ticks },
        { typeof(ushort), static x => (ushort)((DateTimeOffset)x).Ticks },
        { typeof(int), static x => (int)((DateTimeOffset)x).Ticks },
        { typeof(uint), static x => (uint)((DateTimeOffset)x).Ticks },
        { typeof(long), static x => ((DateTimeOffset)x).Ticks },
        { typeof(ulong), static x => (ulong)((DateTimeOffset)x).Ticks },
        { typeof(char), static x => (char)((DateTimeOffset)x).Ticks },
        { typeof(double), static x => (double)((DateTimeOffset)x).Ticks },
        { typeof(float), static x => (float)((DateTimeOffset)x).Ticks },
        { typeof(decimal), static x => (decimal)((DateTimeOffset)x).Ticks }
    };

    private static readonly Dictionary<Type, Func<object, object>> TimeSpanToTickConverter = new()
    {
        { typeof(byte), static x => (byte)((TimeSpan)x).Ticks },
        { typeof(sbyte), static x => (sbyte)((TimeSpan)x).Ticks },
        { typeof(short), static x => (short)((TimeSpan)x).Ticks },
        { typeof(ushort), static x => (ushort)((TimeSpan)x).Ticks },
        { typeof(int), static x => (int)((TimeSpan)x).Ticks },
        { typeof(uint), static x => (uint)((TimeSpan)x).Ticks },
        { typeof(long), static x => ((TimeSpan)x).Ticks },
        { typeof(ulong), static x => (ulong)((TimeSpan)x).Ticks },
        { typeof(char), static x => (char)((TimeSpan)x).Ticks },
        { typeof(double), static x => (double)((TimeSpan)x).Ticks },
        { typeof(float), static x => (float)((TimeSpan)x).Ticks },
        { typeof(decimal), static x => (decimal)((TimeSpan)x).Ticks }
    };

    private static readonly Dictionary<Type, Func<object, object>> DateTimeFromTickConverter = new()
    {
        { typeof(byte), static x => { try { return new DateTime((byte)x); } catch (ArgumentOutOfRangeException) { return default(DateTime); } } },
        { typeof(sbyte), static x => { try { return new DateTime((sbyte)x); } catch (ArgumentOutOfRangeException) { return default(DateTime); } } },
        { typeof(short), static x => { try { return new DateTime((short)x); } catch (ArgumentOutOfRangeException) { return default(DateTime); } } },
        { typeof(ushort), static x => { try { return new DateTime((ushort)x); } catch (ArgumentOutOfRangeException) { return default(DateTime); } } },
        { typeof(int), static x => { try { return new DateTime((int)x); } catch (ArgumentOutOfRangeException) { return default(DateTime); } } },
        { typeof(uint), static x => { try { return new DateTime((uint)x); } catch (ArgumentOutOfRangeException) { return default(DateTime); } } },
        { typeof(long), static x => { try { return new DateTime((long)x); } catch (ArgumentOutOfRangeException) { return default(DateTime); } } },
        { typeof(ulong), static x => { try { return new DateTime((long)(ulong)x); } catch (ArgumentOutOfRangeException) { return default(DateTime); } } },
        { typeof(char), static x => { try { return new DateTime((char)x); } catch (ArgumentOutOfRangeException) { return default(DateTime); } } },
        { typeof(double), static x => { try { return new DateTime((long)(double)x); } catch (ArgumentOutOfRangeException) { return default(DateTime); } } },
        { typeof(float), static x => { try { return new DateTime((long)(float)x); } catch (ArgumentOutOfRangeException) { return default(DateTime); } } },
        { typeof(decimal), static x => { try { return new DateTime((long)(decimal)x); } catch (ArgumentOutOfRangeException) { return default(DateTime); } } }
    };

    private static readonly Dictionary<Type, Func<object, object>> NullableDateTimeFromTickConverter = new()
    {
        { typeof(byte), static x => { try { return new DateTime((byte)x); } catch (ArgumentOutOfRangeException) { return default(DateTime?); } } },
        { typeof(sbyte), static x => { try { return new DateTime((sbyte)x); } catch (ArgumentOutOfRangeException) { return default(DateTime?); } } },
        { typeof(short), static x => { try { return new DateTime((short)x); } catch (ArgumentOutOfRangeException) { return default(DateTime?); } } },
        { typeof(ushort), static x => { try { return new DateTime((ushort)x); } catch (ArgumentOutOfRangeException) { return default(DateTime?); } } },
        { typeof(int), static x => { try { return new DateTime((int)x); } catch (ArgumentOutOfRangeException) { return default(DateTime?); } } },
        { typeof(uint), static x => { try { return new DateTime((uint)x); } catch (ArgumentOutOfRangeException) { return default(DateTime?); } } },
        { typeof(long), static x => { try { return new DateTime((long)x); } catch (ArgumentOutOfRangeException) { return default(DateTime?); } } },
        { typeof(ulong), static x => { try { return new DateTime((long)(ulong)x); } catch (ArgumentOutOfRangeException) { return default(DateTime?); } } },
        { typeof(char), static x => { try { return new DateTime((char)x); } catch (ArgumentOutOfRangeException) { return default(DateTime?); } } },
        { typeof(double), static x => { try { return new DateTime((long)(double)x); } catch (ArgumentOutOfRangeException) { return default(DateTime?); } } },
        { typeof(float), static x => { try { return new DateTime((long)(float)x); } catch (ArgumentOutOfRangeException) { return default(DateTime?); } } },
        { typeof(decimal), static x => { try { return new DateTime((long)(decimal)x); } catch (ArgumentOutOfRangeException) { return default(DateTime?); } } }
    };

    private static readonly Dictionary<Type, Func<object, object>> DateTimeOffsetFromTickConverter = new()
    {
        { typeof(byte), static x => { try { return new DateTimeOffset(new DateTime((byte)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset); } } },
        { typeof(sbyte), static x => { try { return new DateTimeOffset(new DateTime((sbyte)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset); } } },
        { typeof(short), static x => { try { return new DateTimeOffset(new DateTime((short)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset); } } },
        { typeof(ushort), static x => { try { return new DateTimeOffset(new DateTime((ushort)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset); } } },
        { typeof(int), static x => { try { return new DateTimeOffset(new DateTime((int)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset); } } },
        { typeof(uint), static x => { try { return new DateTimeOffset(new DateTime((uint)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset); } } },
        { typeof(long), static x => { try { return new DateTimeOffset(new DateTime((long)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset); } } },
        { typeof(ulong), static x => { try { return new DateTimeOffset(new DateTime((long)(ulong)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset); } } },
        { typeof(char), static x => { try { return new DateTimeOffset(new DateTime((char)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset); } } },
        { typeof(double), static x => { try { return new DateTimeOffset(new DateTime((long)(double)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset); } } },
        { typeof(float), static x => { try { return new DateTimeOffset(new DateTime((long)(float)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset); } } },
        { typeof(decimal), static x => { try { return new DateTimeOffset(new DateTime((long)(decimal)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset); } } }
    };

    private static readonly Dictionary<Type, Func<object, object>> NullableDateTimeOffsetFromTickConverter = new()
    {
        { typeof(byte), static x => { try { return new DateTimeOffset(new DateTime((byte)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset?); } } },
        { typeof(sbyte), static x => { try { return new DateTimeOffset(new DateTime((sbyte)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset?); } } },
        { typeof(short), static x => { try { return new DateTimeOffset(new DateTime((short)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset?); } } },
        { typeof(ushort), static x => { try { return new DateTimeOffset(new DateTime((ushort)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset?); } } },
        { typeof(int), static x => { try { return new DateTimeOffset(new DateTime((int)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset?); } } },
        { typeof(uint), static x => { try { return new DateTimeOffset(new DateTime((uint)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset?); } } },
        { typeof(long), static x => { try { return new DateTimeOffset(new DateTime((long)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset?); } } },
        { typeof(ulong), static x => { try { return new DateTimeOffset(new DateTime((long)(ulong)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset?); } } },
        { typeof(char), static x => { try { return new DateTimeOffset(new DateTime((char)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset?); } } },
        { typeof(double), static x => { try { return new DateTimeOffset(new DateTime((long)(double)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset?); } } },
        { typeof(float), static x => { try { return new DateTimeOffset(new DateTime((long)(float)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset?); } } },
        { typeof(decimal), static x => { try { return new DateTimeOffset(new DateTime((long)(decimal)x)); } catch (ArgumentOutOfRangeException) { return default(DateTimeOffset?); } } }
    };

    private static readonly Dictionary<Type, Func<object, object>> TimeSpanFromTickConverter = new()
    {
        { typeof(byte), static x => { try { return new TimeSpan((byte)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan); } } },
        { typeof(sbyte), static x => { try { return new TimeSpan((sbyte)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan); } } },
        { typeof(short), static x => { try { return new TimeSpan((short)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan); } } },
        { typeof(ushort), static x => { try { return new TimeSpan((ushort)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan); } } },
        { typeof(int), static x => { try { return new TimeSpan((int)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan); } } },
        { typeof(uint), static x => { try { return new TimeSpan((uint)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan); } } },
        { typeof(long), static x => { try { return new TimeSpan((long)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan); } } },
        { typeof(ulong), static x => { try { return new TimeSpan((long)(ulong)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan); } } },
        { typeof(char), static x => { try { return new TimeSpan((char)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan); } } },
        { typeof(double), static x => { try { return new TimeSpan((long)(double)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan); } } },
        { typeof(float), static x => { try { return new TimeSpan((long)(float)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan); } } },
        { typeof(decimal), static x => { try { return new TimeSpan((long)(decimal)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan); } } }
    };

    private static readonly Dictionary<Type, Func<object, object>> NullableTimeSpanFromTickConverter = new()
    {
        { typeof(byte), static x => { try { return new TimeSpan((byte)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan?); } } },
        { typeof(sbyte), static x => { try { return new TimeSpan((sbyte)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan?); } } },
        { typeof(short), static x => { try { return new TimeSpan((short)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan?); } } },
        { typeof(ushort), static x => { try { return new TimeSpan((ushort)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan?); } } },
        { typeof(int), static x => { try { return new TimeSpan((int)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan?); } } },
        { typeof(uint), static x => { try { return new TimeSpan((uint)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan?); } } },
        { typeof(long), static x => { try { return new TimeSpan((long)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan?); } } },
        { typeof(ulong), static x => { try { return new TimeSpan((long)(ulong)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan?); } } },
        { typeof(char), static x => { try { return new TimeSpan((char)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan?); } } },
        { typeof(double), static x => { try { return new TimeSpan((long)(double)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan?); } } },
        { typeof(float), static x => { try { return new TimeSpan((long)(float)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan?); } } },
        { typeof(decimal), static x => { try { return new TimeSpan((long)(decimal)x); } catch (ArgumentOutOfRangeException) { return default(TimeSpan?); } } }
    };

    public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
    {
        // From DateTime
        if (sourceType == typeof(DateTime))
        {
            // DateTime to String
            if (targetType == typeof(string))
            {
                return static x => ((DateTime)x).ToString(CultureInfo.CurrentCulture);
            }

            var underlyingTargetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;

            // DateTime to DateTimeOffset(Nullable)
            if (underlyingTargetType == typeof(DateTimeOffset))
            {
                var defaultValue = targetType.IsNullableType() ? null : (object)default(DateTimeOffset);
                return x =>
                {
                    try
                    {
                        return new DateTimeOffset((DateTime)x);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        return defaultValue;
                    }
                };
            }

            // DateTime to numeric
            if (DateTimeToTickConverter.TryGetValue(underlyingTargetType!, out var converter))
            {
                return converter;
            }

            return null;
        }

        // From DateTimeOffset
        if (sourceType == typeof(DateTimeOffset))
        {
            // DateTimeOffset to String
            if (targetType == typeof(string))
            {
                return static x => ((DateTimeOffset)x).ToString();
            }

            var underlyingTargetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;

            // DateTimeOffset to DateTime(Nullable)
            if (underlyingTargetType == typeof(DateTime))
            {
                return static x => ((DateTimeOffset)x).DateTime;
            }

            // DateTimeOffset to numeric
            if (DateTimeOffsetToTickConverter.TryGetValue(underlyingTargetType!, out var converter))
            {
                return converter;
            }

            return null;
        }

        // From TimeSpan
        if (sourceType == typeof(TimeSpan))
        {
            // TimeSpan to String
            if (targetType == typeof(string))
            {
                return static x => ((TimeSpan)x).ToString();
            }

            var underlyingTargetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;

            // TimeSpan to numeric
            if (TimeSpanToTickConverter.TryGetValue(underlyingTargetType!, out var converter))
            {
                return converter;
            }

            return null;
        }

        // From string
        if (sourceType == typeof(string))
        {
            var underlyingTargetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;

            // String to DateTime(Nullable)
            if (underlyingTargetType == typeof(DateTime))
            {
                var defaultValue = targetType.IsNullableType() ? null : (object)default(DateTime);
                return x => DateTime.TryParse((string)x, out var result) ? result : defaultValue;
            }

            // String to DateTimeOffset(Nullable)
            if (underlyingTargetType == typeof(DateTimeOffset))
            {
                var defaultValue = targetType.IsNullableType() ? null : (object)default(DateTimeOffset);
                return x => DateTimeOffset.TryParse((string)x, out var result) ? result : defaultValue;
            }

            // String to TimeSpan(Nullable)
            if (underlyingTargetType == typeof(TimeSpan))
            {
                var defaultValue = targetType.IsNullableType() ? null : (object)default(TimeSpan);
                return x => TimeSpan.TryParse((string)x, out var result) ? result : defaultValue;
            }

            return null;
        }

        // From Numeric to DateTime
        if (targetType == typeof(DateTime))
        {
            return DateTimeFromTickConverter.TryGetValue(sourceType, out var converter) ? converter : null;
        }

        // From Numeric to DateTime?
        if (targetType == typeof(DateTime?))
        {
            return NullableDateTimeFromTickConverter.TryGetValue(sourceType, out var converter) ? converter : null;
        }

        // From Numeric to DateTimeOffset
        if (targetType == typeof(DateTimeOffset))
        {
            return DateTimeOffsetFromTickConverter.TryGetValue(sourceType, out var converter) ? converter : null;
        }

        // From Numeric to DateTime?
        if (targetType == typeof(DateTimeOffset?))
        {
            return NullableDateTimeOffsetFromTickConverter.TryGetValue(sourceType, out var converter) ? converter : null;
        }

        // From Numeric to TimeSpan
        if (targetType == typeof(TimeSpan))
        {
            return TimeSpanFromTickConverter.TryGetValue(sourceType, out var converter) ? converter : null;
        }

        // From Numeric to TimeSpan?
        if (targetType == typeof(TimeSpan?))
        {
            return NullableTimeSpanFromTickConverter.TryGetValue(sourceType, out var converter) ? converter : null;
        }

        return null;
    }
}
