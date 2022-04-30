#nullable disable
namespace Smart.Converter.Converters;

public sealed class GuidConverterFactory : IConverterFactory
{
    public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
    {
        if ((sourceType == typeof(Guid)) && (targetType == typeof(string)))
        {
            return static source => ((Guid)source).ToString();
        }

        if (sourceType == typeof(string))
        {
            if (targetType == typeof(Guid))
            {
                return static source => Guid.TryParse((string)source, out var result) ? result : default;
            }

            if (targetType == typeof(Guid?))
            {
                return static source => Guid.TryParse((string)source, out var result) ? result : null;
            }
        }

        return null;
    }
}
