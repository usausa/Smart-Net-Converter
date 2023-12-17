#nullable disable
namespace Smart.Converter.Converters;

using Smart.ComponentModel;

public sealed class ValueHolderConverterFactory : IConverterFactory
{
    public Func<object, object> GetConverter(IObjectConverter context, Type sourceType, Type targetType)
    {
        var isSourceValueType = ValueHolderHelper.IsValueHolderType(sourceType);
        if (isSourceValueType)
        {
            var sourceValueType = ValueHolderHelper.GetValueTypeProperty(sourceType).PropertyType;
            var type = sourceType.IsNullableType() ? Nullable.GetUnderlyingType(targetType) : targetType;
            if (sourceValueType == type)
            {
                return ((IConverter)Activator.CreateInstance(typeof(ValueHolderConverter<>).MakeGenericType(sourceValueType))).Convert;
            }

            var converter = context.CreateConverter(sourceValueType, targetType);
            if (converter != null)
            {
                return ((IConverter)Activator.CreateInstance(typeof(ValueHolderWithConvertConverter<>).MakeGenericType(sourceValueType), converter)).Convert;
            }
        }

        return null;
    }

#pragma warning disable CA1812
    private sealed class ValueHolderConverter<T> : IConverter
    {
        public object Convert(object source)
        {
            return ((IValueHolder<T>)source).Value;
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class ValueHolderWithConvertConverter<T> : IConverter
    {
        private readonly Func<object, object> converter;

        public ValueHolderWithConvertConverter(Func<object, object> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            return converter(((IValueHolder<T>)source).Value);
        }
    }
#pragma warning restore CA1812
}
