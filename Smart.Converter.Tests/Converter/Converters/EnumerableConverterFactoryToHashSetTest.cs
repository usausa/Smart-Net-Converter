namespace Smart.Converter.Converters;

public sealed class EnumerableConverterFactoryToHashSetTest
{
    [Fact]
    public void ArrayToSameElementHashSet()
    {
        var converter = new TestObjectConverter();
        var source = new[] { 0, 1 };
        var destination = (HashSet<int>)converter.Convert(source, typeof(HashSet<int>));
        Assert.Equal(2, destination.Count);
        Assert.Contains(0, destination);
        Assert.Contains(1, destination);
        Assert.True(converter.UsedOnly<EnumerableConverterFactory>());
    }

    [Fact]
    public void ArrayToOtherElementHashSet()
    {
        var converter = new TestObjectConverter();
        var source = new[] { 0, 1 };
        var destination = (HashSet<string>)converter.Convert(source, typeof(HashSet<string>));
        Assert.Equal(2, destination.Count);
        Assert.Contains("0", destination);
        Assert.Contains("1", destination);
        Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
    }

    [Fact]
    public void ListToSameElementHashSet()
    {
        var converter = new TestObjectConverter();
        var source = new WrapperList<int>([0, 1]);
        var destination = (HashSet<int>)converter.Convert(source, typeof(HashSet<int>));
        Assert.Equal(2, destination.Count);
        Assert.Contains(0, destination);
        Assert.Contains(1, destination);
        Assert.True(converter.UsedOnly<EnumerableConverterFactory>());
    }

    [Fact]
    public void ListToOtherElementHashSet()
    {
        var converter = new TestObjectConverter();
        var source = new WrapperList<int>([0, 1]);
        var destination = (HashSet<string>)converter.Convert(source, typeof(HashSet<string>));
        Assert.Equal(2, destination.Count);
        Assert.Contains("0", destination);
        Assert.Contains("1", destination);
        Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
    }

    [Fact]
    public void CollectionToSameElementHashSet()
    {
        var converter = new TestObjectConverter();
        var source = new WrapperCollection<int>([0, 1]);
        var destination = (HashSet<int>)converter.Convert(source, typeof(HashSet<int>));
        Assert.Equal(2, destination.Count);
        Assert.Contains(0, destination);
        Assert.Contains(1, destination);
        Assert.True(converter.UsedOnly<EnumerableConverterFactory>());
    }

    [Fact]
    public void CollectionToOtherElementHashSet()
    {
        var converter = new TestObjectConverter();
        var source = new WrapperCollection<int>([0, 1]);
        var destination = (HashSet<string>)converter.Convert(source, typeof(HashSet<string>));
        Assert.Equal(2, destination.Count);
        Assert.Contains("0", destination);
        Assert.Contains("1", destination);
        Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
    }

    [Fact]
    public void EnumerableToSameElementHashSet()
    {
        var converter = new TestObjectConverter();
        var source = new[] { 0, 1 }.Select(static x => x);
        var destination = (HashSet<int>)converter.Convert(source, typeof(HashSet<int>));
        Assert.Equal(2, destination.Count);
        Assert.Contains(0, destination);
        Assert.Contains(1, destination);
        Assert.True(converter.UsedOnly<EnumerableConverterFactory>());
    }

    [Fact]
    public void EnumerableToOtherElementHashSet()
    {
        var converter = new TestObjectConverter();
        var source = new[] { 0, 1 }.Select(static x => x);
        var destination = (HashSet<string>)converter.Convert(source, typeof(HashSet<string>));
        Assert.Equal(2, destination.Count);
        Assert.Contains("0", destination);
        Assert.Contains("1", destination);
        Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
    }
}
