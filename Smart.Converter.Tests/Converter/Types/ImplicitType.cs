namespace Smart.Converter.Types;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Ignore")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "Ignore")]
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

[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Ignore")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "Ignore")]
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
