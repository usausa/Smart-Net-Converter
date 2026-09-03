namespace Smart.Converter.Converters;

#pragma warning disable CA2263
public sealed class EnumerableConverterFactoryNullElementTests
{
    //--------------------------------------------------------------------------------
    // Array
    //--------------------------------------------------------------------------------

#pragma warning disable CA1861
    [Fact]
    public void ArrayWithNullElementToValueElementArray()
    {
        var converter = new TestObjectConverter();
        var source = new[] { "1", null, "3" };
        var destination = (int[])converter.Convert(source, typeof(int[]));
        Assert.Equal(new[] { 1, 0, 3 }, destination);
    }
#pragma warning restore CA1861

    [Fact]
    public void ArrayWithNullElementToNullableElementArray()
    {
        var converter = new TestObjectConverter();
        var source = new[] { "1", null, "3" };
        var destination = (int?[])converter.Convert(source, typeof(int?[]));
        Assert.Equal<int?>([1, null, 3], destination);
    }

    //--------------------------------------------------------------------------------
    // List
    //--------------------------------------------------------------------------------

#pragma warning disable CA1861
    [Fact]
    public void ListWithNullElementToValueElementArray()
    {
        var converter = new TestObjectConverter();
        var source = new List<string?> { "1", null, "3" };
        var destination = (int[])converter.Convert(source, typeof(int[]));
        Assert.Equal(new[] { 1, 0, 3 }, destination);
    }
#pragma warning restore CA1861

    [Fact]
    public void ListWithNullElementToNullableElementArray()
    {
        var converter = new TestObjectConverter();
        var source = new List<string?> { "1", null, "3" };
        var destination = (int?[])converter.Convert(source, typeof(int?[]));
        Assert.Equal<int?>([1, null, 3], destination);
    }

    //--------------------------------------------------------------------------------
    // Enumerable
    //--------------------------------------------------------------------------------

#pragma warning disable CA1861
    [Fact]
    public void EnumerableWithNullElementToValueElementList()
    {
        var converter = new TestObjectConverter();
        var source = new[] { "1", null, "3" }.Select(static x => x);
        var destination = (List<int>)converter.Convert(source, typeof(List<int>));
        Assert.Equal(new[] { 1, 0, 3 }, destination);
    }
#pragma warning restore CA1861

    [Fact]
    public void EnumerableWithNullElementToNullableElementArray()
    {
        var converter = new TestObjectConverter();
        var source = new[] { "1", null, "3" }.Select(static x => x);
        var destination = (int?[])converter.Convert(source, typeof(int?[]));
        Assert.Equal<int?>([1, null, 3], destination);
    }
}
#pragma warning restore CA2263
