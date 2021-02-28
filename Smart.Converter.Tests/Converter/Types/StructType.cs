namespace Smart.Converter.Types
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Ignore")]
    public struct StructType
    {
        public int X { get; init; }

        public int Y { get; init; }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}
