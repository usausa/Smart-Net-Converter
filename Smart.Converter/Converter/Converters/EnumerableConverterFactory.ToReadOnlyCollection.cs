#nullable disable
namespace Smart.Converter.Converters;

using System.Collections.ObjectModel;

public sealed partial class EnumerableConverterFactory
{
    private sealed class SameTypeReadOnlyCollectionProvider : IEnumerableConverterProvider
    {
        public static IEnumerableConverterProvider Default { get; } = new SameTypeReadOnlyCollectionProvider();

        public Type GetConverterType(SourceEnumerableType sourceEnumerableType)
        {
            return sourceEnumerableType switch
            {
                SourceEnumerableType.Array => typeof(SameTypeReadOnlyCollectionFromListConverter<>),
                SourceEnumerableType.List => typeof(SameTypeReadOnlyCollectionFromListConverter<>),
                _ => typeof(SameTypeReadOnlyCollectionFromEnumerableConverter<>)
            };
        }
    }

    private sealed class OtherTypeReadOnlyCollectionProvider : IEnumerableConverterProvider
    {
        public static IEnumerableConverterProvider Default { get; } = new OtherTypeReadOnlyCollectionProvider();

        public Type GetConverterType(SourceEnumerableType sourceEnumerableType)
        {
            return sourceEnumerableType switch
            {
                SourceEnumerableType.Array => typeof(OtherTypeReadOnlyCollectionFromArrayConverter<,>),
                SourceEnumerableType.List => typeof(OtherTypeReadOnlyCollectionFromListConverter<,>),
                SourceEnumerableType.Collection => typeof(OtherTypeReadOnlyCollectionFromCollectionConverter<,>),
                _ => typeof(OtherTypeReadOnlyCollectionFromEnumerableConverter<,>)
            };
        }
    }

    //--------------------------------------------------------------------------------
    // Same type
    //--------------------------------------------------------------------------------

#pragma warning disable CA1812
    private sealed class SameTypeReadOnlyCollectionFromListConverter<TDestination> : IConverter
    {
        public object Convert(object source)
        {
            return new ReadOnlyCollection<TDestination>((IList<TDestination>)source);
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class SameTypeReadOnlyCollectionFromEnumerableConverter<TDestination> : IConverter
    {
        public object Convert(object source)
        {
            return new ReadOnlyCollection<TDestination>(((IEnumerable<TDestination>)source).ToList());
        }
    }
#pragma warning restore CA1812

    //--------------------------------------------------------------------------------
    // Other type
    //--------------------------------------------------------------------------------

#pragma warning disable CA1812
    private sealed class OtherTypeReadOnlyCollectionFromArrayConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object> converter;

        public OtherTypeReadOnlyCollectionFromArrayConverter(Func<object, object> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            return new ReadOnlyCollection<TDestination>(new ArrayConvertList<TSource, TDestination>((TSource[])source, converter));
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class OtherTypeReadOnlyCollectionFromListConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object> converter;

        public OtherTypeReadOnlyCollectionFromListConverter(Func<object, object> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            return new ReadOnlyCollection<TDestination>(new ListConvertList<TSource, TDestination>((IList<TSource>)source, converter));
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class OtherTypeReadOnlyCollectionFromCollectionConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object> converter;

        public OtherTypeReadOnlyCollectionFromCollectionConverter(Func<object, object> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            var sourceCollection = (ICollection<TSource>)source;
            var list = new List<TDestination>(sourceCollection.Count);
            foreach (var value in sourceCollection)
            {
                list.Add((TDestination)converter(value));
            }

            return new ReadOnlyCollection<TDestination>(list);
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class OtherTypeReadOnlyCollectionFromEnumerableConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object> converter;

        public OtherTypeReadOnlyCollectionFromEnumerableConverter(Func<object, object> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            var list = new List<TDestination>();
            foreach (var value in (IEnumerable<TSource>)source)
            {
                list.Add((TDestination)converter(value));
            }

            return new ReadOnlyCollection<TDestination>(list);
        }
    }
#pragma warning restore CA1812
}
