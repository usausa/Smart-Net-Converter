namespace Smart.Converter.Converters;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

public sealed class DateTimeConverterFactory : IConverterFactory
{
    private static readonly long DateTimeMaxTicks = DateTime.MaxValue.Ticks;

    private static readonly Dictionary<Type, Func<object, object?>> DateTimeToTickConverter = new()
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

    private static readonly Dictionary<Type, Func<object, object?>> DateTimeOffsetToTickConverter = new()
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

    private static readonly Dictionary<Type, Func<object, object?>> TimeSpanToTickConverter = new()
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

    // Numeric to DateTime: new DateTime(ticks) throws for ticks outside [0, DateTime.MaxValue.Ticks].
    // The range is pre-checked (out-of-range returns default) so the hot path avoids try/catch.
    private static readonly Dictionary<Type, Func<object, object?>> DateTimeFromTickConverter = new()
    {
        { typeof(byte), static x => DateTimeFromTicks((byte)x) },
        { typeof(sbyte), static x => DateTimeFromTicks((sbyte)x) },
        { typeof(short), static x => DateTimeFromTicks((short)x) },
        { typeof(ushort), static x => DateTimeFromTicks((ushort)x) },
        { typeof(int), static x => DateTimeFromTicks((int)x) },
        { typeof(uint), static x => DateTimeFromTicks((uint)x) },
        { typeof(long), static x => DateTimeFromTicks((long)x) },
        { typeof(ulong), static x => DateTimeFromTicks((long)(ulong)x) },
        { typeof(char), static x => DateTimeFromTicks((char)x) },
        { typeof(double), static x => DateTimeFromTicks((long)(double)x) },
        { typeof(float), static x => DateTimeFromTicks((long)(float)x) },
        { typeof(decimal), static x => DateTimeFromTicks((long)(decimal)x) }
    };

    private static readonly Dictionary<Type, Func<object, object?>> NullableDateTimeFromTickConverter = new()
    {
        { typeof(byte), static x => NullableDateTimeFromTicks((byte)x) },
        { typeof(sbyte), static x => NullableDateTimeFromTicks((sbyte)x) },
        { typeof(short), static x => NullableDateTimeFromTicks((short)x) },
        { typeof(ushort), static x => NullableDateTimeFromTicks((ushort)x) },
        { typeof(int), static x => NullableDateTimeFromTicks((int)x) },
        { typeof(uint), static x => NullableDateTimeFromTicks((uint)x) },
        { typeof(long), static x => NullableDateTimeFromTicks((long)x) },
        { typeof(ulong), static x => NullableDateTimeFromTicks((long)(ulong)x) },
        { typeof(char), static x => NullableDateTimeFromTicks((char)x) },
        { typeof(double), static x => NullableDateTimeFromTicks((long)(double)x) },
        { typeof(float), static x => NullableDateTimeFromTicks((long)(float)x) },
        { typeof(decimal), static x => NullableDateTimeFromTicks((long)(decimal)x) }
    };

    // Numeric to DateTimeOffset: the DateTime ticks are pre-checked to remove the bulk of the
    // out-of-range exceptions; a thin catch remains only for the local-offset boundary (see helper).
    private static readonly Dictionary<Type, Func<object, object?>> DateTimeOffsetFromTickConverter = new()
    {
        { typeof(byte), static x => DateTimeOffsetFromTicks((byte)x) },
        { typeof(sbyte), static x => DateTimeOffsetFromTicks((sbyte)x) },
        { typeof(short), static x => DateTimeOffsetFromTicks((short)x) },
        { typeof(ushort), static x => DateTimeOffsetFromTicks((ushort)x) },
        { typeof(int), static x => DateTimeOffsetFromTicks((int)x) },
        { typeof(uint), static x => DateTimeOffsetFromTicks((uint)x) },
        { typeof(long), static x => DateTimeOffsetFromTicks((long)x) },
        { typeof(ulong), static x => DateTimeOffsetFromTicks((long)(ulong)x) },
        { typeof(char), static x => DateTimeOffsetFromTicks((char)x) },
        { typeof(double), static x => DateTimeOffsetFromTicks((long)(double)x) },
        { typeof(float), static x => DateTimeOffsetFromTicks((long)(float)x) },
        { typeof(decimal), static x => DateTimeOffsetFromTicks((long)(decimal)x) }
    };

    private static readonly Dictionary<Type, Func<object, object?>> NullableDateTimeOffsetFromTickConverter = new()
    {
        { typeof(byte), static x => NullableDateTimeOffsetFromTicks((byte)x) },
        { typeof(sbyte), static x => NullableDateTimeOffsetFromTicks((sbyte)x) },
        { typeof(short), static x => NullableDateTimeOffsetFromTicks((short)x) },
        { typeof(ushort), static x => NullableDateTimeOffsetFromTicks((ushort)x) },
        { typeof(int), static x => NullableDateTimeOffsetFromTicks((int)x) },
        { typeof(uint), static x => NullableDateTimeOffsetFromTicks((uint)x) },
        { typeof(long), static x => NullableDateTimeOffsetFromTicks((long)x) },
        { typeof(ulong), static x => NullableDateTimeOffsetFromTicks((long)(ulong)x) },
        { typeof(char), static x => NullableDateTimeOffsetFromTicks((char)x) },
        { typeof(double), static x => NullableDateTimeOffsetFromTicks((long)(double)x) },
        { typeof(float), static x => NullableDateTimeOffsetFromTicks((long)(float)x) },
        { typeof(decimal), static x => NullableDateTimeOffsetFromTicks((long)(decimal)x) }
    };

    // Numeric to TimeSpan: new TimeSpan(long) accepts every long value and never throws, so the
    // previous try/catch(ArgumentOutOfRangeException) was unreachable dead code and is removed.
    private static readonly Dictionary<Type, Func<object, object?>> TimeSpanFromTickConverter = new()
    {
        { typeof(byte), static x => new TimeSpan((byte)x) },
        { typeof(sbyte), static x => new TimeSpan((sbyte)x) },
        { typeof(short), static x => new TimeSpan((short)x) },
        { typeof(ushort), static x => new TimeSpan((ushort)x) },
        { typeof(int), static x => new TimeSpan((int)x) },
        { typeof(uint), static x => new TimeSpan((uint)x) },
        { typeof(long), static x => new TimeSpan((long)x) },
        { typeof(ulong), static x => new TimeSpan((long)(ulong)x) },
        { typeof(char), static x => new TimeSpan((char)x) },
        { typeof(double), static x => new TimeSpan((long)(double)x) },
        { typeof(float), static x => new TimeSpan((long)(float)x) },
        { typeof(decimal), static x => new TimeSpan((long)(decimal)x) }
    };

    private static readonly Dictionary<Type, Func<object, object?>> NullableTimeSpanFromTickConverter = new()
    {
        { typeof(byte), static x => new TimeSpan((byte)x) },
        { typeof(sbyte), static x => new TimeSpan((sbyte)x) },
        { typeof(short), static x => new TimeSpan((short)x) },
        { typeof(ushort), static x => new TimeSpan((ushort)x) },
        { typeof(int), static x => new TimeSpan((int)x) },
        { typeof(uint), static x => new TimeSpan((uint)x) },
        { typeof(long), static x => new TimeSpan((long)x) },
        { typeof(ulong), static x => new TimeSpan((long)(ulong)x) },
        { typeof(char), static x => new TimeSpan((char)x) },
        { typeof(double), static x => new TimeSpan((long)(double)x) },
        { typeof(float), static x => new TimeSpan((long)(float)x) },
        { typeof(decimal), static x => new TimeSpan((long)(decimal)x) }
    };

    //--------------------------------------------------------------------------------
    // Helper
    //--------------------------------------------------------------------------------

    private static DateTime DateTimeFromTicks(long ticks) =>
        (ulong)ticks <= (ulong)DateTimeMaxTicks ? new DateTime(ticks) : default;

    private static DateTime? NullableDateTimeFromTicks(long ticks) =>
        (ulong)ticks <= (ulong)DateTimeMaxTicks ? new DateTime(ticks) : default(DateTime?);

    private static DateTimeOffset DateTimeOffsetFromTicks(long ticks)
    {
        if ((ulong)ticks > (ulong)DateTimeMaxTicks)
        {
            return default;
        }

        try
        {
            return new DateTimeOffset(new DateTime(ticks));
        }
        catch (ArgumentOutOfRangeException)
        {
            return default;
        }
    }

    private static DateTimeOffset? NullableDateTimeOffsetFromTicks(long ticks)
    {
        if ((ulong)ticks > (ulong)DateTimeMaxTicks)
        {
            return default;
        }

        try
        {
            return new DateTimeOffset(new DateTime(ticks));
        }
        catch (ArgumentOutOfRangeException)
        {
            return default;
        }
    }

    [RequiresDynamicCode("Converter factories use MakeGenericType/MakeGenericMethod at runtime.")]
    [RequiresUnreferencedCode("Converter factories use reflection to discover types at runtime.")]
    public Func<object, object?>? GetConverter(IObjectConverter context, Type sourceType, Type targetType)
    {
        // From DateTime
        if (sourceType == typeof(DateTime))
        {
            // DateTime to String
            if (targetType == typeof(string))
            {
                return static x => ((DateTime)x).ToString(CultureInfo.InvariantCulture);
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
            return DateTimeToTickConverter.GetValueOrDefault(underlyingTargetType!);
        }

        // From DateTimeOffset
        if (sourceType == typeof(DateTimeOffset))
        {
            // DateTimeOffset to String
            if (targetType == typeof(string))
            {
                return static x => ((DateTimeOffset)x).ToString(CultureInfo.InvariantCulture);
            }

            var underlyingTargetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;

            // DateTimeOffset to DateTime(Nullable)
            if (underlyingTargetType == typeof(DateTime))
            {
                return static x => ((DateTimeOffset)x).DateTime;
            }

            // DateTimeOffset to numeric
            return DateTimeOffsetToTickConverter.GetValueOrDefault(underlyingTargetType!);
        }

        // From TimeSpan
        if (sourceType == typeof(TimeSpan))
        {
            // TimeSpan to String
            if (targetType == typeof(string))
            {
                return static x => ((TimeSpan)x).ToString();
            }

            var underlyingTargetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType)! : targetType;

            // TimeSpan to numeric
            return TimeSpanToTickConverter.GetValueOrDefault(underlyingTargetType);
        }

        // From string
        if (sourceType == typeof(string))
        {
            var underlyingTargetType = targetType.IsNullableType() ? Nullable.GetUnderlyingType(targetType)! : targetType;

            // String to DateTime(Nullable)
            if (underlyingTargetType == typeof(DateTime))
            {
                var defaultValue = targetType.IsNullableType() ? null : (object)default(DateTime);
                return x => DateTime.TryParse((string)x, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result) ? result : defaultValue;
            }

            // String to DateTimeOffset(Nullable)
            if (underlyingTargetType == typeof(DateTimeOffset))
            {
                var defaultValue = targetType.IsNullableType() ? null : (object)default(DateTimeOffset);
                return x => DateTimeOffset.TryParse((string)x, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result) ? result : defaultValue;
            }

            // String to TimeSpan(Nullable)
            if (underlyingTargetType == typeof(TimeSpan))
            {
                var defaultValue = targetType.IsNullableType() ? null : (object)default(TimeSpan);
                return x => TimeSpan.TryParse((string)x, CultureInfo.InvariantCulture, out var result) ? result : defaultValue;
            }

            return null;
        }

        // From Numeric to DateTime
        if (targetType == typeof(DateTime))
        {
            return DateTimeFromTickConverter.GetValueOrDefault(sourceType);
        }

        // From Numeric to DateTime?
        if (targetType == typeof(DateTime?))
        {
            return NullableDateTimeFromTickConverter.GetValueOrDefault(sourceType);
        }

        // From Numeric to DateTimeOffset
        if (targetType == typeof(DateTimeOffset))
        {
            return DateTimeOffsetFromTickConverter.GetValueOrDefault(sourceType);
        }

        // From Numeric to DateTime?
        if (targetType == typeof(DateTimeOffset?))
        {
            return NullableDateTimeOffsetFromTickConverter.GetValueOrDefault(sourceType);
        }

        // From Numeric to TimeSpan
        if (targetType == typeof(TimeSpan))
        {
            return TimeSpanFromTickConverter.GetValueOrDefault(sourceType);
        }

        // From Numeric to TimeSpan?
        if (targetType == typeof(TimeSpan?))
        {
            return NullableTimeSpanFromTickConverter.GetValueOrDefault(sourceType);
        }

        return null;
    }
}
