namespace Smart.Converter.Converters;

public sealed class DateTimeConverterFactoryRangeTest
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
    // Numeric to DateTimeOffset (range boundary; the local-offset boundary near Min/MaxValue is
    // time-zone dependent, so only the tick pre-check (clearly out-of-range) is asserted here)
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
}
