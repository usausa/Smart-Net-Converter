namespace Smart.Converter.Converters;

using Smart.Converter.Types;

public sealed class EnumConverterFactoryWrapAroundTest
{
    [Fact]
    public void EnumToNumericWrapsAround()
    {
        var converter = new TestObjectConverter();
        Assert.Equal((byte)255, converter.Convert(Enum2Type.Minus, typeof(byte)));
        Assert.Equal((ushort)65535, converter.Convert(Enum2Type.Minus, typeof(ushort)));
        Assert.Equal(UInt32.MaxValue, converter.Convert(Enum2Type.Minus, typeof(uint)));
        Assert.True(converter.UsedOnly<EnumConverterFactory>());
    }
}
