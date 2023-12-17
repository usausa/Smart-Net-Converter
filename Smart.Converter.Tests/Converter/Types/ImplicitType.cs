namespace Smart.Converter.Types;

#pragma warning disable CA1815
#pragma warning disable CA2225
public readonly struct ImplicitType
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
#pragma warning restore CA2225
#pragma warning restore CA1815

#pragma warning disable CA1815
#pragma warning disable CA2225
public readonly struct NullableImplicitType
{
    public int? Value { get; init; }

    public static implicit operator int?(NullableImplicitType value)
    {
        return value.Value;
    }

    public static implicit operator NullableImplicitType(int? value)
    {
        return new() { Value = value };
    }
}
#pragma warning restore CA2225
#pragma warning restore CA1815
