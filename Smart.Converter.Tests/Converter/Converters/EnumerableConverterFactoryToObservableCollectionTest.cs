namespace Smart.Converter.Converters;

using System.Collections.ObjectModel;

#pragma warning disable CA2263
public sealed class EnumerableConverterFactoryToObservableCollectionTest
{
    [Fact]
    public void ArrayToSameElementObservableCollection()
    {
        var converter = new TestObjectConverter();
        var source = new[] { 0, 1 };
        var destination = (ObservableCollection<int>)converter.Convert(source, typeof(ObservableCollection<int>));
        Assert.Equal(2, destination.Count);
        Assert.Contains(0, destination);
        Assert.Contains(1, destination);
        Assert.True(converter.UsedOnly<EnumerableConverterFactory>());
    }

    [Fact]
    public void ArrayToOtherElementObservableCollection()
    {
        var converter = new TestObjectConverter();
        var source = new[] { 0, 1 };
        var destination = (ObservableCollection<string>)converter.Convert(source, typeof(ObservableCollection<string>));
        Assert.Equal(2, destination.Count);
        Assert.Contains("0", destination);
        Assert.Contains("1", destination);
        Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
    }

    [Fact]
    public void ListToSameElementObservableCollection()
    {
        var converter = new TestObjectConverter();
        var source = new WrapperList<int>([0, 1]);
        var destination = (ObservableCollection<int>)converter.Convert(source, typeof(ObservableCollection<int>));
        Assert.Equal(2, destination.Count);
        Assert.Contains(0, destination);
        Assert.Contains(1, destination);
        Assert.True(converter.UsedOnly<EnumerableConverterFactory>());
    }

    [Fact]
    public void ListToOtherElementObservableCollection()
    {
        var converter = new TestObjectConverter();
        var source = new WrapperList<int>([0, 1]);
        var destination = (ObservableCollection<string>)converter.Convert(source, typeof(ObservableCollection<string>));
        Assert.Equal(2, destination.Count);
        Assert.Contains("0", destination);
        Assert.Contains("1", destination);
        Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
    }

    [Fact]
    public void CollectionToSameElementObservableCollection()
    {
        var converter = new TestObjectConverter();
        var source = new WrapperCollection<int>([0, 1]);
        var destination = (ObservableCollection<int>)converter.Convert(source, typeof(ObservableCollection<int>));
        Assert.Equal(2, destination.Count);
        Assert.Contains(0, destination);
        Assert.Contains(1, destination);
        Assert.True(converter.UsedOnly<EnumerableConverterFactory>());
    }

    [Fact]
    public void CollectionToOtherElementObservableCollection()
    {
        var converter = new TestObjectConverter();
        var source = new WrapperCollection<int>([0, 1]);
        var destination = (ObservableCollection<string>)converter.Convert(source, typeof(ObservableCollection<string>));
        Assert.Equal(2, destination.Count);
        Assert.Contains("0", destination);
        Assert.Contains("1", destination);
        Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
    }

#pragma warning disable CA1861
    [Fact]
    public void EnumerableToSameElementObservableCollection()
    {
        var converter = new TestObjectConverter();
        var source = new[] { 0, 1 }.Select(static x => x);
        var destination = (ObservableCollection<int>)converter.Convert(source, typeof(ObservableCollection<int>));
        Assert.Equal(2, destination.Count);
        Assert.Contains(0, destination);
        Assert.Contains(1, destination);
        Assert.True(converter.UsedOnly<EnumerableConverterFactory>());
    }
#pragma warning restore CA1861

#pragma warning disable CA1861
    [Fact]
    public void EnumerableToOtherElementObservableCollection()
    {
        var converter = new TestObjectConverter();
        var source = new[] { 0, 1 }.Select(static x => x);
        var destination = (ObservableCollection<string>)converter.Convert(source, typeof(ObservableCollection<string>));
        Assert.Equal(2, destination.Count);
        Assert.Contains("0", destination);
        Assert.Contains("1", destination);
        Assert.True(converter.UsedIn(typeof(EnumerableConverterFactory), typeof(ToStringConverterFactory)));
    }
#pragma warning restore CA1861
}
#pragma warning restore CA2263
