namespace Smart.Converter.Converters;

#pragma warning disable CA2263
public sealed class EnumerableConverterFactoryToListTests
{
    [Fact]
    public void ArrayToSameElementList()
    {
        var converter = new TestObjectConverter();
        var source = new[] { 0, 1 };
        var destination = (List<int>)converter.Convert(source, typeof(List<int>));
        Assert.Equal(2, destination.Count);
        Assert.Equal(0, destination[0]);
        Assert.Equal(1, destination[1]);
        Assert.True(converter.UsedOnly<EnumerableConverterFactory>());
    }

    [Fact]
    public void ArrayToOtherElementList()
    {
        var converter = new TestObjectConverter();
        var source = new[] { 0, 1 };
        var destination = (List<string>)converter.Convert(source, typeof(List<string>));
        Assert.Equal(2, destination.Count);
        Assert.Equal("0", destination[0]);
        Assert.Equal("1", destination[1]);
        Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
    }

    [Fact]
    public void ListToSameElementList()
    {
        var converter = new TestObjectConverter();
        var source = new WrapperList<int>([0, 1]);
        var destination = (List<int>)converter.Convert(source, typeof(List<int>));
        Assert.Equal(2, destination.Count);
        Assert.Equal(0, destination[0]);
        Assert.Equal(1, destination[1]);
        Assert.True(converter.UsedOnly<EnumerableConverterFactory>());
    }

    [Fact]
    public void ListToOtherElementList()
    {
        var converter = new TestObjectConverter();
        var source = new WrapperList<int>([0, 1]);
        var destination = (List<string>)converter.Convert(source, typeof(List<string>));
        Assert.Equal(2, destination.Count);
        Assert.Equal("0", destination[0]);
        Assert.Equal("1", destination[1]);
        Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
    }

    [Fact]
    public void CollectionToSameElementList()
    {
        var converter = new TestObjectConverter();
        var source = new WrapperCollection<int>([0, 1]);
        var destination = (List<int>)converter.Convert(source, typeof(List<int>));
        Assert.Equal(2, destination.Count);
        Assert.Equal(0, destination[0]);
        Assert.Equal(1, destination[1]);
        Assert.True(converter.UsedOnly<EnumerableConverterFactory>());
    }

    [Fact]
    public void CollectionToOtherElementList()
    {
        var converter = new TestObjectConverter();
        var source = new WrapperCollection<int>([0, 1]);
        var destination = (List<string>)converter.Convert(source, typeof(List<string>));
        Assert.Equal(2, destination.Count);
        Assert.Equal("0", destination[0]);
        Assert.Equal("1", destination[1]);
        Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
    }

#pragma warning disable CA1861
    [Fact]
    public void EnumerableToSameElementList()
    {
        var converter = new TestObjectConverter();
        var source = new[] { 0, 1 }.Select(static x => x);
        var destination = (List<int>)converter.Convert(source, typeof(List<int>));
        Assert.Equal(2, destination.Count);
        Assert.Equal(0, destination[0]);
        Assert.Equal(1, destination[1]);
        Assert.True(converter.UsedOnly<EnumerableConverterFactory>());
    }
#pragma warning restore CA1861

#pragma warning disable CA1861
    [Fact]
    public void EnumerableToOtherElementList()
    {
        var converter = new TestObjectConverter();
        var source = new[] { 0, 1 }.Select(static x => x);
        var destination = (List<string>)converter.Convert(source, typeof(List<string>));
        Assert.Equal(2, destination.Count);
        Assert.Equal("0", destination[0]);
        Assert.Equal("1", destination[1]);
        Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
    }
#pragma warning restore CA1861
}
#pragma warning restore CA2263
