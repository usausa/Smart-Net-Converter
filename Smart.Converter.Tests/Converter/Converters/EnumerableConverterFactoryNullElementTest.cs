namespace Smart.Converter.Converters;

public sealed class EnumerableConverterFactoryNullElementTest
{
    //--------------------------------------------------------------------------------
    // Array
    //--------------------------------------------------------------------------------

    [Fact]
    public void ArrayWithNullElementToValueElementArray()
    {
        var converter = new TestObjectConverter();
        var source = new[] { "1", null, "3" };
        var destination = (int[])converter.Convert(source, typeof(int[]));
        Assert.Equal(new[] { 1, 0, 3 }, destination);
    }

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

    [Fact]
    public void ListWithNullElementToValueElementArray()
    {
        var converter = new TestObjectConverter();
        var source = new List<string?> { "1", null, "3" };
        var destination = (int[])converter.Convert(source, typeof(int[]));
        Assert.Equal(new[] { 1, 0, 3 }, destination);
    }

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

    [Fact]
    public void EnumerableWithNullElementToValueElementList()
    {
        var converter = new TestObjectConverter();
        var source = new[] { "1", null, "3" }.Select(static x => x);
        var destination = (List<int>)converter.Convert(source, typeof(List<int>));
        Assert.Equal(new[] { 1, 0, 3 }, destination);
    }

    [Fact]
    public void EnumerableWithNullElementToNullableElementArray()
    {
        var converter = new TestObjectConverter();
        var source = new[] { "1", null, "3" }.Select(static x => x);
        var destination = (int?[])converter.Convert(source, typeof(int?[]));
        Assert.Equal<int?>([1, null, 3], destination);
    }
}
