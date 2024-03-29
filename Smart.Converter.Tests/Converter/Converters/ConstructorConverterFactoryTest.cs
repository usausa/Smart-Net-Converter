namespace Smart.Converter.Converters;

using Smart.ComponentModel;

public sealed class ConstructorConverterFactoryTest
{
    [Fact]
    public void IntToTypeHasSameTypeConstructor()
    {
        var converter = new TestObjectConverter();
        Assert.Equal(1, ((NotificationValue<int>)converter.Convert(1, typeof(NotificationValue<int>))).Value);
        Assert.True(converter.UsedOnly<ConstructorConverterFactory>());
    }

    [Fact]
    public void IntToTypeHasSameNullableTypeConstructor()
    {
        var converter = new TestObjectConverter();
        Assert.Equal(1, ((NotificationValue<int?>)converter.Convert(1, typeof(NotificationValue<int?>))).Value);
        Assert.True(converter.UsedOnly<ConstructorConverterFactory>());
    }

    [Fact]
    public void IntToTypeHasDifferentTypeConstructor()
    {
        var converter = new TestObjectConverter();
        Assert.Equal("1", ((NotificationValue<string>)converter.Convert(1, typeof(NotificationValue<string>))).Value);
        Assert.True(converter.UsedIn(typeof(ConstructorConverterFactory), typeof(ToStringConverterFactory)));
    }
}
