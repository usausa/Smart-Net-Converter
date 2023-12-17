namespace Smart.Converter.Types;

#pragma warning disable CA1815
#pragma warning disable CA2225
public readonly struct ExplicitType
{
    public int Value { get; init; }

    public static explicit operator int(ExplicitType value)
    {
        return value.Value;
    }

    public static explicit operator ExplicitType(int value)
    {
        return new() { Value = value };
    }
}
#pragma warning restore CA2225
#pragma warning restore CA1815

#pragma warning disable CA1815
#pragma warning disable CA2225
public readonly struct NullableExplicitType
{
    public int? Value { get; init; }

    public static explicit operator int?(NullableExplicitType value)
    {
        return value.Value;
    }

    public static explicit operator NullableExplicitType(int? value)
    {
        return new() { Value = value };
    }
}
#pragma warning restore CA2225
#pragma warning restore CA1815
