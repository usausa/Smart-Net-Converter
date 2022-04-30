#nullable disable
namespace Smart.Converter.Converters;

public sealed class ToStringConverterFactory : IConverterFactory
{
    private static readonly Func<object, object> Converter = static x => x.ToString();

    public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
    {
        return targetType == typeof(string) ? Converter : null;
    }
}
