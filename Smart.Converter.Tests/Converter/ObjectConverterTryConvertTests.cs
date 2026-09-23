namespace Smart.Converter;

using System.Numerics;

using Smart.Converter.Types;

#pragma warning disable CA2263
public sealed class ObjectConverterTryConvertTests
{
    //--------------------------------------------------------------------------------
    // Null
    //--------------------------------------------------------------------------------

    [Fact]
    public void NullToValueTypeFails()
    {
        var converter = new ObjectConverter();
        Assert.False(converter.TryConvert(null, typeof(int), out var result));
        Assert.Null(result);
    }

    [Fact]
    public void NullToNullableSucceeds()
    {
        var converter = new ObjectConverter();
        Assert.True(converter.TryConvert(null, typeof(int?), out var result));
        Assert.Null(result);
    }

    [Fact]
    public void NullToReferenceTypeSucceeds()
    {
        var converter = new ObjectConverter();
        Assert.True(converter.TryConvert(null, typeof(string), out var result));
        Assert.Null(result);
    }

    //--------------------------------------------------------------------------------
    // Empty
    //--------------------------------------------------------------------------------

    [Fact]
    public void EmptyToValueTypeFails()
    {
        var converter = new ObjectConverter();
        Assert.False(converter.TryConvert(string.Empty, typeof(int), out var result));
        Assert.Null(result);
    }

    [Fact]
    public void EmptyToNullableSucceeds()
    {
        var converter = new ObjectConverter();
        Assert.True(converter.TryConvert(string.Empty, typeof(int?), out var result));
        Assert.Null(result);
    }

    [Fact]
    public void EmptyToStringSucceeds()
    {
        var converter = new ObjectConverter();
        Assert.True(converter.TryConvert(string.Empty, typeof(string), out var result));
        Assert.Equal(string.Empty, result);
    }

    //--------------------------------------------------------------------------------
    // Parse
    //--------------------------------------------------------------------------------

    [Theory]
    [InlineData(typeof(byte))]
    [InlineData(typeof(sbyte))]
    [InlineData(typeof(short))]
    [InlineData(typeof(ushort))]
    [InlineData(typeof(int))]
    [InlineData(typeof(uint))]
    [InlineData(typeof(long))]
    [InlineData(typeof(ulong))]
    [InlineData(typeof(char))]
    [InlineData(typeof(double))]
    [InlineData(typeof(float))]
    [InlineData(typeof(decimal))]
    [InlineData(typeof(BigInteger))]
    [InlineData(typeof(bool))]
    [InlineData(typeof(DateTime))]
    [InlineData(typeof(DateTimeOffset))]
    [InlineData(typeof(TimeSpan))]
    [InlineData(typeof(Guid))]
    [InlineData(typeof(Enum1Type))]
    public void InvalidStringFails(Type targetType)
    {
        var converter = new ObjectConverter();
        Assert.False(converter.TryConvert("invalid", targetType, out var result));
        Assert.Null(result);
    }

    [Theory]
    [InlineData(typeof(byte?))]
    [InlineData(typeof(sbyte?))]
    [InlineData(typeof(short?))]
    [InlineData(typeof(ushort?))]
    [InlineData(typeof(int?))]
    [InlineData(typeof(uint?))]
    [InlineData(typeof(long?))]
    [InlineData(typeof(ulong?))]
    [InlineData(typeof(char?))]
    [InlineData(typeof(double?))]
    [InlineData(typeof(float?))]
    [InlineData(typeof(decimal?))]
    [InlineData(typeof(BigInteger?))]
    [InlineData(typeof(bool?))]
    [InlineData(typeof(DateTime?))]
    [InlineData(typeof(DateTimeOffset?))]
    [InlineData(typeof(TimeSpan?))]
    [InlineData(typeof(Guid?))]
    [InlineData(typeof(Enum1Type?))]
    public void InvalidStringToNullableFails(Type targetType)
    {
        var converter = new ObjectConverter();
        Assert.False(converter.TryConvert("invalid", targetType, out var result));
        Assert.Null(result);
    }

    [Theory]
    [InlineData("1", typeof(byte))]
    [InlineData("-1", typeof(sbyte))]
    [InlineData("-1", typeof(short))]
    [InlineData("1", typeof(ushort))]
    [InlineData("-1", typeof(int))]
    [InlineData("1", typeof(uint))]
    [InlineData("-1", typeof(long))]
    [InlineData("1", typeof(ulong))]
    [InlineData("a", typeof(char))]
    [InlineData("1.5", typeof(double))]
    [InlineData("1.5", typeof(float))]
    [InlineData("1.5", typeof(decimal))]
    [InlineData("1", typeof(BigInteger))]
    [InlineData("True", typeof(bool))]
    [InlineData("2000-01-02", typeof(DateTime))]
    [InlineData("2000-01-02", typeof(DateTimeOffset))]
    [InlineData("01:02:03", typeof(TimeSpan))]
    [InlineData("3f2504e0-4f89-11d3-9a0c-0305e82c3301", typeof(Guid))]
    [InlineData("One", typeof(Enum1Type))]
    [InlineData("1", typeof(int?))]
    [InlineData("True", typeof(bool?))]
    [InlineData("One", typeof(Enum1Type?))]
    public void ValidStringSucceeds(string value, Type targetType)
    {
        var converter = new ObjectConverter();
        Assert.True(converter.TryConvert(value, targetType, out var result));
        Assert.NotNull(result);
        Assert.Equal(converter.Convert(value, targetType), result);
    }

    [Fact]
    public void ConvertResultOnFailureIsNotChanged()
    {
        var converter = new ObjectConverter();
        Assert.Equal(0, converter.Convert("invalid", typeof(int)));
        Assert.Null(converter.Convert("invalid", typeof(int?)));
        Assert.Null(converter.Convert("invalid", typeof(bool)));
        Assert.Equal(Enum1Type.Zero, converter.Convert("invalid", typeof(Enum1Type?)));
    }

    //--------------------------------------------------------------------------------
    // Other
    //--------------------------------------------------------------------------------

    [Fact]
    public void SameTypeSucceeds()
    {
        var converter = new ObjectConverter();
        Assert.True(converter.TryConvert(1, typeof(int?), out var result));
        Assert.Equal(1, result);
    }

    [Fact]
    public void ConverterWithoutParseSucceeds()
    {
        var converter = new ObjectConverter();
        Assert.True(converter.TryConvert(1, typeof(long), out var result));
        Assert.Equal(1L, result);
        Assert.True(converter.TryConvert(1, typeof(string), out result));
        Assert.Equal("1", result);
    }

    [Fact]
    public void NoConverterFails()
    {
        var converter = new ObjectConverter();
        Assert.False(converter.TryConvert(new object(), typeof(int), out var result));
        Assert.Null(result);
    }

    [Fact]
    public void SetFactoriesClearsCache()
    {
        var converter = new ObjectConverter();
        Assert.True(converter.TryConvert("1", typeof(int), out _));

        converter.SetFactories([]);

        Assert.False(converter.TryConvert("1", typeof(int), out _));
    }

    //--------------------------------------------------------------------------------
    // Default implementation
    //--------------------------------------------------------------------------------

    [Fact]
    public void DefaultImplementationHandlesNullAndEmpty()
    {
        IObjectConverter converter = new TestObjectConverter();
        Assert.False(converter.TryConvert(null, typeof(int), out _));
        Assert.True(converter.TryConvert(null, typeof(int?), out var result));
        Assert.Null(result);
        Assert.False(converter.TryConvert(string.Empty, typeof(int), out _));
        Assert.True(converter.TryConvert(string.Empty, typeof(int?), out result));
        Assert.Null(result);
    }

    [Fact]
    public void DefaultImplementationUsesConvert()
    {
        IObjectConverter converter = new TestObjectConverter();
        Assert.True(converter.TryConvert("1", typeof(int), out var result));
        Assert.Equal(1, result);
        Assert.False(converter.TryConvert(new object(), typeof(int), out result));
        Assert.Null(result);
    }
}
