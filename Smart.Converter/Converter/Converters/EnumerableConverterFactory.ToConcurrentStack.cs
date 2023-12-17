#nullable disable
namespace Smart.Converter.Converters;

using System.Collections.Concurrent;

public sealed partial class EnumerableConverterFactory
{
    private sealed class SameTypeConcurrentStackProvider : IEnumerableConverterProvider
    {
        public static IEnumerableConverterProvider Default { get; } = new SameTypeConcurrentStackProvider();

        public Type GetConverterType(SourceEnumerableType sourceEnumerableType)
        {
            return typeof(SameTypeConcurrentStackFromEnumerableConverter<>);
        }
    }

    private sealed class OtherTypeConcurrentStackProvider : IEnumerableConverterProvider
    {
        public static IEnumerableConverterProvider Default { get; } = new OtherTypeConcurrentStackProvider();

        public Type GetConverterType(SourceEnumerableType sourceEnumerableType)
        {
            return sourceEnumerableType switch
            {
                SourceEnumerableType.Array => typeof(OtherTypeConcurrentStackFromArrayConverter<,>),
                SourceEnumerableType.List => typeof(OtherTypeConcurrentStackFromListConverter<,>),
                SourceEnumerableType.Collection => typeof(OtherTypeConcurrentStackFromCollectionConverter<,>),
                _ => typeof(OtherTypeConcurrentStackFromEnumerableConverter<,>)
            };
        }
    }

    //--------------------------------------------------------------------------------
    // Same type
    //--------------------------------------------------------------------------------

#pragma warning disable CA1812
    private sealed class SameTypeConcurrentStackFromEnumerableConverter<TDestination> : IConverter
    {
        public object Convert(object source)
        {
            return new ConcurrentStack<TDestination>((IEnumerable<TDestination>)source);
        }
    }
#pragma warning restore CA1812

    //--------------------------------------------------------------------------------
    // Other type
    //--------------------------------------------------------------------------------

#pragma warning disable CA1812
    private sealed class OtherTypeConcurrentStackFromArrayConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object> converter;

        public OtherTypeConcurrentStackFromArrayConverter(Func<object, object> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            return new ConcurrentStack<TDestination>(new ArrayConvertList<TSource, TDestination>((TSource[])source, converter));
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class OtherTypeConcurrentStackFromListConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object> converter;

        public OtherTypeConcurrentStackFromListConverter(Func<object, object> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            return new ConcurrentStack<TDestination>(new ListConvertList<TSource, TDestination>((IList<TSource>)source, converter));
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class OtherTypeConcurrentStackFromCollectionConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object> converter;

        public OtherTypeConcurrentStackFromCollectionConverter(Func<object, object> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            return new ConcurrentStack<TDestination>(new CollectionConvertCollection<TSource, TDestination>((ICollection<TSource>)source, converter));
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class OtherTypeConcurrentStackFromEnumerableConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object> converter;

        public OtherTypeConcurrentStackFromEnumerableConverter(Func<object, object> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            return new ConcurrentStack<TDestination>(new EnumerableConvertEnumerable<TSource, TDestination>((IEnumerable<TSource>)source, converter));
        }
    }
#pragma warning restore CA1812
}
