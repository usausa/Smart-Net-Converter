#nullable disable
namespace Smart.Converter.Converters;

public sealed class AssignableConverterFactory : IConverterFactory
{
    private static readonly Func<object, object> Converter = static x => x;

    public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
    {
        return targetType.IsAssignableFrom(sourceType) ? Converter : null;
    }
}
