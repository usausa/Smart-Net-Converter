namespace Smart.Converter.Types;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Ignore")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "Ignore")]
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

[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Ignore")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "Ignore")]
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
