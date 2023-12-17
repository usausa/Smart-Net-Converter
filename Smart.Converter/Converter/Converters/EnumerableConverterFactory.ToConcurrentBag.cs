#nullable disable
namespace Smart.Converter.Converters;

using System.Collections.Concurrent;

public sealed partial class EnumerableConverterFactory
{
    private sealed class SameTypeConcurrentBagProvider : IEnumerableConverterProvider
    {
        public static IEnumerableConverterProvider Default { get; } = new SameTypeConcurrentBagProvider();

        public Type GetConverterType(SourceEnumerableType sourceEnumerableType)
        {
            return typeof(SameTypeConcurrentBagFromEnumerableConverter<>);
        }
    }

    private sealed class OtherTypeConcurrentBagProvider : IEnumerableConverterProvider
    {
        public static IEnumerableConverterProvider Default { get; } = new OtherTypeConcurrentBagProvider();

        public Type GetConverterType(SourceEnumerableType sourceEnumerableType)
        {
            return sourceEnumerableType switch
            {
                SourceEnumerableType.Array => typeof(OtherTypeConcurrentBagFromArrayConverter<,>),
                SourceEnumerableType.List => typeof(OtherTypeConcurrentBagFromListConverter<,>),
                SourceEnumerableType.Collection => typeof(OtherTypeConcurrentBagFromCollectionConverter<,>),
                _ => typeof(OtherTypeConcurrentBagFromEnumerableConverter<,>)
            };
        }
    }

    //--------------------------------------------------------------------------------
    // Same type
    //--------------------------------------------------------------------------------

#pragma warning disable CA1812
    private sealed class SameTypeConcurrentBagFromEnumerableConverter<TDestination> : IConverter
    {
        public object Convert(object source)
        {
            return new ConcurrentBag<TDestination>((IEnumerable<TDestination>)source);
        }
    }
#pragma warning restore CA1812

    //--------------------------------------------------------------------------------
    // Other type
    //--------------------------------------------------------------------------------

#pragma warning disable CA1812
    private sealed class OtherTypeConcurrentBagFromArrayConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object> converter;

        public OtherTypeConcurrentBagFromArrayConverter(Func<object, object> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            return new ConcurrentBag<TDestination>(new ArrayConvertList<TSource, TDestination>((TSource[])source, converter));
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class OtherTypeConcurrentBagFromListConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object> converter;

        public OtherTypeConcurrentBagFromListConverter(Func<object, object> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            return new ConcurrentBag<TDestination>(new ListConvertList<TSource, TDestination>((IList<TSource>)source, converter));
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class OtherTypeConcurrentBagFromCollectionConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object> converter;

        public OtherTypeConcurrentBagFromCollectionConverter(Func<object, object> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            return new ConcurrentBag<TDestination>(new CollectionConvertCollection<TSource, TDestination>((ICollection<TSource>)source, converter));
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class OtherTypeConcurrentBagFromEnumerableConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object> converter;

        public OtherTypeConcurrentBagFromEnumerableConverter(Func<object, object> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            return new ConcurrentBag<TDestination>(new EnumerableConvertEnumerable<TSource, TDestination>((IEnumerable<TSource>)source, converter));
        }
    }
#pragma warning restore CA1812
}
