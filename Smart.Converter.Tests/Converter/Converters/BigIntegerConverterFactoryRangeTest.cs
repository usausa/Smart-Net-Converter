namespace Smart.Converter.Converters;

using System.Numerics;

public sealed class BigIntegerConverterFactoryRangeTest
{
    //--------------------------------------------------------------------------------
    // BigInteger to integer (range boundary)
    //--------------------------------------------------------------------------------

    [Fact]
    public void BigIntegerToByteBoundary()
    {
        var converter = new TestObjectConverter();
        Assert.Equal(byte.MaxValue, converter.Convert(new BigInteger(byte.MaxValue), typeof(byte)));
        Assert.Equal(default(byte), converter.Convert(new BigInteger(byte.MaxValue) + 1, typeof(byte)));
        Assert.Equal(default(byte), converter.Convert(BigInteger.MinusOne, typeof(byte)));
        Assert.True(converter.UsedOnly<BigIntegerConverterFactory>());
    }

    [Fact]
    public void BigIntegerToIntBoundary()
    {
        var converter = new TestObjectConverter();
        Assert.Equal(int.MaxValue, converter.Convert(new BigInteger(int.MaxValue), typeof(int)));
        Assert.Equal(int.MinValue, converter.Convert(new BigInteger(int.MinValue), typeof(int)));
        Assert.Equal(default(int), converter.Convert(new BigInteger(int.MaxValue) + 1, typeof(int)));
        Assert.Equal(default(int), converter.Convert(new BigInteger(int.MinValue) - 1, typeof(int)));
        Assert.True(converter.UsedOnly<BigIntegerConverterFactory>());
    }

    [Fact]
    public void BigIntegerToULongBoundary()
    {
        var converter = new TestObjectConverter();
        Assert.Equal(ulong.MaxValue, converter.Convert(new BigInteger(ulong.MaxValue), typeof(ulong)));
        Assert.Equal(default(ulong), converter.Convert(new BigInteger(ulong.MaxValue) + 1, typeof(ulong)));
        Assert.Equal(default(ulong), converter.Convert(BigInteger.MinusOne, typeof(ulong)));
        Assert.True(converter.UsedOnly<BigIntegerConverterFactory>());
    }

    [Fact]
    public void BigIntegerToCharBoundary()
    {
        var converter = new TestObjectConverter();
        Assert.Equal(char.MaxValue, converter.Convert(new BigInteger(char.MaxValue), typeof(char)));
        Assert.Equal(default(char), converter.Convert(new BigInteger(char.MaxValue) + 1, typeof(char)));
        Assert.Equal(default(char), converter.Convert(BigInteger.MinusOne, typeof(char)));
        Assert.True(converter.UsedOnly<BigIntegerConverterFactory>());
    }

    [Fact]
    public void BigIntegerToDecimalBoundary()
    {
        var converter = new TestObjectConverter();
        Assert.Equal(decimal.MaxValue, converter.Convert(new BigInteger(decimal.MaxValue), typeof(decimal)));
        Assert.Equal(decimal.MinValue, converter.Convert(new BigInteger(decimal.MinValue), typeof(decimal)));
        Assert.Equal(default(decimal), converter.Convert(new BigInteger(decimal.MaxValue) + 1, typeof(decimal)));
        Assert.Equal(default(decimal), converter.Convert(new BigInteger(decimal.MinValue) - 1, typeof(decimal)));
        Assert.True(converter.UsedOnly<BigIntegerConverterFactory>());
    }

    //--------------------------------------------------------------------------------
    // double/float to BigInteger (NaN / Infinity => default via the IsFinite pre-check)
    //--------------------------------------------------------------------------------

    [Fact]
    public void DoubleNonFiniteToBigInteger()
    {
        var converter = new TestObjectConverter();
        Assert.Equal(default(BigInteger), converter.Convert(double.NaN, typeof(BigInteger)));
        Assert.Equal(default(BigInteger), converter.Convert(double.PositiveInfinity, typeof(BigInteger)));
        Assert.Equal(default(BigInteger), converter.Convert(double.NegativeInfinity, typeof(BigInteger)));
        Assert.True(converter.UsedOnly<BigIntegerConverterFactory>());
    }

    [Fact]
    public void DoubleNonFiniteToNullableBigInteger()
    {
        var converter = new TestObjectConverter();
        Assert.Null(converter.Convert(double.NaN, typeof(BigInteger?)));
        Assert.Null(converter.Convert(double.PositiveInfinity, typeof(BigInteger?)));
        Assert.Null(converter.Convert(double.NegativeInfinity, typeof(BigInteger?)));
        Assert.True(converter.UsedOnly<BigIntegerConverterFactory>());
    }

    [Fact]
    public void FloatNonFiniteToBigInteger()
    {
        var converter = new TestObjectConverter();
        Assert.Equal(default(BigInteger), converter.Convert(float.NaN, typeof(BigInteger)));
        Assert.Equal(default(BigInteger), converter.Convert(float.PositiveInfinity, typeof(BigInteger)));
        Assert.Equal(default(BigInteger), converter.Convert(float.NegativeInfinity, typeof(BigInteger)));
        Assert.True(converter.UsedOnly<BigIntegerConverterFactory>());
    }

    [Fact]
    public void FloatNonFiniteToNullableBigInteger()
    {
        var converter = new TestObjectConverter();
        Assert.Null(converter.Convert(float.NaN, typeof(BigInteger?)));
        Assert.Null(converter.Convert(float.PositiveInfinity, typeof(BigInteger?)));
        Assert.Null(converter.Convert(float.NegativeInfinity, typeof(BigInteger?)));
        Assert.True(converter.UsedOnly<BigIntegerConverterFactory>());
    }
}
