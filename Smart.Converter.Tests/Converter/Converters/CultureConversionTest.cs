namespace Smart.Converter.Converters;

using System.Globalization;
using System.Numerics;

public sealed class CultureConversionTest
{
    private sealed class CultureScope : IDisposable
    {
        private readonly CultureInfo previous;

        public CultureScope(string name)
        {
            previous = CultureInfo.CurrentCulture;
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo(name);
        }

        public void Dispose() => CultureInfo.CurrentCulture = previous;
    }

    //--------------------------------------------------------------------------------
    // string -> value
    //--------------------------------------------------------------------------------

    [Theory]
    [InlineData("ja-JP")]
    [InlineData("en-US")]
    [InlineData("fr-FR")]
    public void StringToNumericIsCultureIndependent(string culture)
    {
        using var scope = new CultureScope(culture);
        var converter = new TestObjectConverter();
        Assert.Equal(1.5d, converter.Convert("1.5", typeof(double)));
        Assert.Equal(1.5f, converter.Convert("1.5", typeof(float)));
        Assert.Equal(1.5m, converter.Convert("1.5", typeof(decimal)));
        Assert.Equal(-1234, converter.Convert("-1234", typeof(int)));
        Assert.Equal(new BigInteger(1234), converter.Convert("1234", typeof(BigInteger)));
    }

    //--------------------------------------------------------------------------------
    // value -> string
    //--------------------------------------------------------------------------------

    [Theory]
    [InlineData("ja-JP")]
    [InlineData("en-US")]
    [InlineData("fr-FR")]
    public void NumericToStringIsCultureIndependent(string culture)
    {
        using var scope = new CultureScope(culture);
        var converter = new TestObjectConverter();
        Assert.Equal("1.5", converter.Convert(1.5d, typeof(string)));
        Assert.Equal("1.5", converter.Convert(1.5f, typeof(string)));
        Assert.Equal("1.5", converter.Convert(1.5m, typeof(string)));
        Assert.Equal("-1234", converter.Convert(-1234, typeof(string)));
        Assert.Equal("1234", converter.Convert(new BigInteger(1234), typeof(string)));
    }

    //--------------------------------------------------------------------------------
    // DateTime parse / format
    //--------------------------------------------------------------------------------

    [Theory]
    [InlineData("ja-JP")]
    [InlineData("en-US")]
    [InlineData("fr-FR")]
    public void DateTimeIsCultureIndependent(string culture)
    {
        using var scope = new CultureScope(culture);
        var converter = new TestObjectConverter();
        var value = new DateTime(2000, 1, 23, 4, 5, 6);
        Assert.Equal(value, converter.Convert("2000-01-23T04:05:06", typeof(DateTime)));
        Assert.Equal(value.ToString(CultureInfo.InvariantCulture), converter.Convert(value, typeof(string)));
    }
}
