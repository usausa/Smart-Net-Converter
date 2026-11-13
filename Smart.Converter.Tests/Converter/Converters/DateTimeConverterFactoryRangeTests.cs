namespace Smart.Converter.Converters;

#pragma warning disable CA2263
public sealed class DateTimeConverterFactoryRangeTests
{
    //--------------------------------------------------------------------------------
    // Numeric to DateTime (range boundary)
    //--------------------------------------------------------------------------------

    [Fact]
    public void LongToDateTimeBoundary()
    {
        var converter = new TestObjectConverter();
        Assert.Equal(DateTime.MinValue, converter.Convert(0L, typeof(DateTime)));
        Assert.Equal(DateTime.MaxValue, converter.Convert(DateTime.MaxValue.Ticks, typeof(DateTime)));
        Assert.Equal(default(DateTime), converter.Convert(DateTime.MaxValue.Ticks + 1, typeof(DateTime)));
        Assert.Equal(default(DateTime), converter.Convert(long.MaxValue, typeof(DateTime)));
        Assert.Equal(default(DateTime), converter.Convert(long.MinValue, typeof(DateTime)));
        Assert.Equal(default(DateTime), converter.Convert(-1L, typeof(DateTime)));
        Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
    }

    [Fact]
    public void LongToNullableDateTimeBoundary()
    {
        var converter = new TestObjectConverter();
        Assert.Equal(DateTime.MaxValue, converter.Convert(DateTime.MaxValue.Ticks, typeof(DateTime?)));
        Assert.Null(converter.Convert(DateTime.MaxValue.Ticks + 1, typeof(DateTime?)));
        Assert.Null(converter.Convert(long.MaxValue, typeof(DateTime?)));
        Assert.Null(converter.Convert(long.MinValue, typeof(DateTime?)));
        Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
    }

    //--------------------------------------------------------------------------------
    // DateTime/Numeric to DateTimeOffset (range boundary)
    //--------------------------------------------------------------------------------

    [Fact]
    public void LongToDateTimeOffsetBoundary()
    {
        var converter = new TestObjectConverter();
        Assert.Equal(new DateTimeOffset(new DateTime(2000, 1, 1)), converter.Convert(new DateTime(2000, 1, 1).Ticks, typeof(DateTimeOffset)));
        Assert.Equal(default(DateTimeOffset), converter.Convert(DateTime.MaxValue.Ticks + 1, typeof(DateTimeOffset)));
        Assert.Equal(default(DateTimeOffset), converter.Convert(long.MaxValue, typeof(DateTimeOffset)));
        Assert.Equal(default(DateTimeOffset), converter.Convert(long.MinValue, typeof(DateTimeOffset)));
        Assert.Equal(default(DateTimeOffset), converter.Convert(-1L, typeof(DateTimeOffset)));
        Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
    }

    [Fact]
    public void LongToNullableDateTimeOffsetBoundary()
    {
        var converter = new TestObjectConverter();
        Assert.Equal(new DateTimeOffset(new DateTime(2000, 1, 1)), converter.Convert(new DateTime(2000, 1, 1).Ticks, typeof(DateTimeOffset?)));
        Assert.Null(converter.Convert(DateTime.MaxValue.Ticks + 1, typeof(DateTimeOffset?)));
        Assert.Null(converter.Convert(long.MaxValue, typeof(DateTimeOffset?)));
        Assert.Null(converter.Convert(long.MinValue, typeof(DateTimeOffset?)));
        Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
    }

    [Fact]
    public void DateTimeToDateTimeOffsetAroundLocalOffsetBoundary()
    {
        var converter = new TestObjectConverter();
        foreach (var value in LocalOffsetBoundaryValues())
        {
            Assert.Equal(CreateOrDefault(value), converter.Convert(value, typeof(DateTimeOffset)));
            Assert.Equal(CreateOrNull(value), converter.Convert(value, typeof(DateTimeOffset?)));
        }

        Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
    }

    [Fact]
    public void LongToDateTimeOffsetAroundLocalOffsetBoundary()
    {
        var converter = new TestObjectConverter();
        foreach (var value in LocalOffsetBoundaryValues().Where(static x => x.Kind == DateTimeKind.Unspecified))
        {
            Assert.Equal(CreateOrDefault(value), converter.Convert(value.Ticks, typeof(DateTimeOffset)));
            Assert.Equal(CreateOrNull(value), converter.Convert(value.Ticks, typeof(DateTimeOffset?)));
        }

        Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
    }

    //--------------------------------------------------------------------------------
    // Numeric to TimeSpan (every long is accepted; new TimeSpan(long) never throws)
    //--------------------------------------------------------------------------------

    [Fact]
    public void LongToTimeSpanAcceptsAllValues()
    {
        var converter = new TestObjectConverter();
        Assert.Equal(new TimeSpan(-1L), converter.Convert(-1L, typeof(TimeSpan)));
        Assert.Equal(TimeSpan.MaxValue, converter.Convert(long.MaxValue, typeof(TimeSpan)));
        Assert.Equal(TimeSpan.MinValue, converter.Convert(long.MinValue, typeof(TimeSpan)));
        Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
    }

    [Fact]
    public void LongToNullableTimeSpanAcceptsAllValues()
    {
        var converter = new TestObjectConverter();
        Assert.Equal(new TimeSpan(-1L), converter.Convert(-1L, typeof(TimeSpan?)));
        Assert.Equal(TimeSpan.MaxValue, converter.Convert(long.MaxValue, typeof(TimeSpan?)));
        Assert.Equal(TimeSpan.MinValue, converter.Convert(long.MinValue, typeof(TimeSpan?)));
        Assert.True(converter.UsedOnly<DateTimeConverterFactory>());
    }

    private static IEnumerable<DateTime> LocalOffsetBoundaryValues()
    {
        const long margin = TimeSpan.TicksPerHour * 14;
        var max = DateTime.MaxValue.Ticks;
        var low = TimeZoneInfo.Local.GetUtcOffset(DateTime.MinValue).Ticks;
        var high = max + TimeZoneInfo.Local.GetUtcOffset(DateTime.MaxValue).Ticks;
        long[] candidates = [0, 1, low - 1, low, low + 1, margin - 1, margin, max - margin, max - margin + 1, high - 1, high, high + 1, max - 1, max];
        foreach (var kind in (DateTimeKind[])[DateTimeKind.Unspecified, DateTimeKind.Local, DateTimeKind.Utc])
        {
            foreach (var ticks in candidates.Where(x => (x >= 0) && (x <= max)))
            {
                yield return new DateTime(ticks, kind);
            }
        }
    }

    private static DateTimeOffset CreateOrDefault(DateTime value) => CreateOrNull(value) ?? default;

    private static DateTimeOffset? CreateOrNull(DateTime value)
    {
        try
        {
            return new DateTimeOffset(value);
        }
        catch (ArgumentOutOfRangeException)
        {
            return null;
        }
    }
}
#pragma warning restore CA2263
