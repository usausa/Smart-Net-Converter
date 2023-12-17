#nullable disable
namespace Smart.Converter.Converters;

public sealed partial class EnumerableConverterFactory
{
#pragma warning disable CA1812
    private sealed class SameTypeStackProvider : IEnumerableConverterProvider
    {
        public static IEnumerableConverterProvider Default { get; } = new SameTypeStackProvider();

        public Type GetConverterType(SourceEnumerableType sourceEnumerableType)
        {
            return typeof(SameTypeStackFromEnumerableConverter<>);
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class OtherTypeStackProvider : IEnumerableConverterProvider
    {
        public static IEnumerableConverterProvider Default { get; } = new OtherTypeStackProvider();

        public Type GetConverterType(SourceEnumerableType sourceEnumerableType)
        {
            return sourceEnumerableType switch
            {
                SourceEnumerableType.Array => typeof(OtherTypeStackFromArrayConverter<,>),
                SourceEnumerableType.List => typeof(OtherTypeStackFromListConverter<,>),
                SourceEnumerableType.Collection => typeof(OtherTypeStackFromCollectionConverter<,>),
                _ => typeof(OtherTypeStackFromEnumerableConverter<,>)
            };
        }
    }
#pragma warning restore CA1812

    //--------------------------------------------------------------------------------
    // Same type
    //--------------------------------------------------------------------------------

#pragma warning disable CA1812
    private sealed class SameTypeStackFromEnumerableConverter<TDestination> : IConverter
    {
        public object Convert(object source)
        {
            return new Stack<TDestination>((IEnumerable<TDestination>)source);
        }
    }
#pragma warning restore CA1812

    //--------------------------------------------------------------------------------
    // Other type
    //--------------------------------------------------------------------------------

#pragma warning disable CA1812
    private sealed class OtherTypeStackFromArrayConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object> converter;

        public OtherTypeStackFromArrayConverter(Func<object, object> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            return new Stack<TDestination>(new ArrayConvertList<TSource, TDestination>((TSource[])source, converter));
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class OtherTypeStackFromListConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object> converter;

        public OtherTypeStackFromListConverter(Func<object, object> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            return new Stack<TDestination>(new ListConvertList<TSource, TDestination>((IList<TSource>)source, converter));
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class OtherTypeStackFromCollectionConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object> converter;

        public OtherTypeStackFromCollectionConverter(Func<object, object> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            return new Stack<TDestination>(new CollectionConvertCollection<TSource, TDestination>((ICollection<TSource>)source, converter));
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class OtherTypeStackFromEnumerableConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object> converter;

        public OtherTypeStackFromEnumerableConverter(Func<object, object> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            var collection = new Stack<TDestination>();
            foreach (var value in (IEnumerable<TSource>)source)
            {
                collection.Push((TDestination)converter(value));
            }

            return collection;
        }
    }
#pragma warning restore CA1812
}
