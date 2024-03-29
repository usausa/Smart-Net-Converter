#nullable disable
namespace Smart.Converter;

public interface IConverterFactory
{
    Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType);
}
