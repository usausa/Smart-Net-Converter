namespace Smart.Converter.Converters;

public sealed class NumericCastConverterFactoryWrapAroundTest
{
    [Fact]
    public void IntToByteWrapsAround()
    {
        var converter = new TestObjectConverter();
        Assert.Equal((byte)0, converter.Convert(256, typeof(byte)));
        Assert.Equal((byte)1, converter.Convert(257, typeof(byte)));
        Assert.Equal((byte)255, converter.Convert(-1, typeof(byte)));
        Assert.True(converter.UsedOnly<NumericCastConverterFactory>());
    }

    [Fact]
    public void NegativeIntToUnsignedWrapsAround()
    {
        var converter = new TestObjectConverter();
        Assert.Equal((ushort)65535, converter.Convert(-1, typeof(ushort)));
        Assert.Equal(UInt32.MaxValue, converter.Convert(-1, typeof(uint)));
        Assert.Equal(UInt64.MaxValue, converter.Convert(-1L, typeof(ulong)));
        Assert.True(converter.UsedOnly<NumericCastConverterFactory>());
    }

    [Fact]
    public void LongToIntTruncatesHighBits()
    {
        var converter = new TestObjectConverter();
        Assert.Equal(0, converter.Convert(0x1_0000_0000L, typeof(int)));
        Assert.Equal(1, converter.Convert(0x1_0000_0001L, typeof(int)));
        Assert.Equal(-1, converter.Convert(0xFFFF_FFFFL, typeof(int)));
        Assert.True(converter.UsedOnly<NumericCastConverterFactory>());
    }
}
