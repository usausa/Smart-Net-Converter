namespace Smart.Converter.Converters;

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public sealed partial class EnumerableConverterFactory
{
    private sealed class SameTypeListProvider : IEnumerableConverterProvider
    {
        public static IEnumerableConverterProvider Default { get; } = new SameTypeListProvider();

        public Type GetConverterType(SourceEnumerableType sourceEnumerableType)
        {
            return typeof(SameTypeListFromEnumerableConverter<>);
        }
    }

    private sealed class OtherTypeListProvider : IEnumerableConverterProvider
    {
        public static IEnumerableConverterProvider Default { get; } = new OtherTypeListProvider();

        public Type GetConverterType(SourceEnumerableType sourceEnumerableType)
        {
            return sourceEnumerableType switch
            {
                SourceEnumerableType.Array => typeof(OtherTypeListFromArrayConverter<,>),
                SourceEnumerableType.List => typeof(OtherTypeListFromListConverter<,>),
                SourceEnumerableType.Collection => typeof(OtherTypeListFromCollectionConverter<,>),
                _ => typeof(OtherTypeListFromEnumerableConverter<,>)
            };
        }
    }

    //--------------------------------------------------------------------------------
    // Same type
    //--------------------------------------------------------------------------------

#pragma warning disable CA1812
    private sealed class SameTypeListFromEnumerableConverter<TDestination> : IConverter
    {
        public object Convert(object source)
        {
            return new List<TDestination>((IEnumerable<TDestination>)source);
        }
    }
#pragma warning restore CA1812

    //--------------------------------------------------------------------------------
    // Other type
    //--------------------------------------------------------------------------------

#pragma warning disable CA1812
    private sealed class OtherTypeListFromArrayConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object?> converter;

        public OtherTypeListFromArrayConverter(Func<object, object?> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            var sourceArray = (TSource[])source;
            var list = new List<TDestination>(sourceArray.Length);
            ref var sourceReference = ref MemoryMarshal.GetArrayDataReference(sourceArray);
            for (var i = 0; i < sourceArray.Length; i++)
            {
                list.Add(ConvertValue<TSource, TDestination>(converter, Unsafe.Add(ref sourceReference, i)));
            }

            return list;
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class OtherTypeListFromListConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object?> converter;

        public OtherTypeListFromListConverter(Func<object, object?> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            var sourceList = (IList<TSource>)source;
            var count = sourceList.Count;
            var list = new List<TDestination>(count);
            for (var i = 0; i < count; i++)
            {
                list.Add(ConvertValue<TSource, TDestination>(converter, sourceList[i]));
            }

            return list;
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class OtherTypeListFromCollectionConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object?> converter;

        public OtherTypeListFromCollectionConverter(Func<object, object?> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            return new List<TDestination>(new CollectionConvertCollection<TSource, TDestination>((ICollection<TSource>)source, converter));
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class OtherTypeListFromEnumerableConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object?> converter;

        public OtherTypeListFromEnumerableConverter(Func<object, object?> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            var collection = new List<TDestination>();
            foreach (var value in (IEnumerable<TSource>)source)
            {
                collection.Add(ConvertValue<TSource, TDestination>(converter, value));
            }

            return collection;
        }
    }
#pragma warning restore CA1812
}
