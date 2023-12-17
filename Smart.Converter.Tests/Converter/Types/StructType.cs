namespace Smart.Converter.Types;

#pragma warning disable CA1815
public readonly struct StructType
{
    public int X { get; init; }

    public int Y { get; init; }

    public override string ToString()
    {
        return $"({X},{Y})";
    }
}
#pragma warning restore CA1815
