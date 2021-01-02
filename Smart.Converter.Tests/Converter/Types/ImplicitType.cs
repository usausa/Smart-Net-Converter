namespace Smart.Converter.Types
{
    public struct ImplicitType
    {
        public int Value { get; init; }

        public static implicit operator int(ImplicitType value)
        {
            return value.Value;
        }

        public static implicit operator ImplicitType(int value)
        {
            return new() { Value = value };
        }
    }

    public struct NullableImplicitType
    {
        public int? Value { get; init; }

        public static implicit operator int? (NullableImplicitType value)
        {
            return value.Value;
        }

        public static implicit operator NullableImplicitType(int? value)
        {
            return new() { Value = value };
        }
    }
}
