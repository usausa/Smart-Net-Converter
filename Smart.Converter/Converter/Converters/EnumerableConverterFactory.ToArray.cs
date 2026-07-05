namespace Smart.Converter.Converters;

using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public sealed partial class EnumerableConverterFactory
{
    private sealed class SameTypeArrayProvider : IEnumerableConverterProvider
    {
        public static SameTypeArrayProvider Default { get; } = new();

        public Type GetConverterType(SourceEnumerableType sourceEnumerableType)
        {
            return sourceEnumerableType switch
            {
                SourceEnumerableType.Array => throw new NotSupportedException(),
                SourceEnumerableType.List => typeof(SameTypeArrayFromCollectionConverter<>),
                SourceEnumerableType.Collection => typeof(SameTypeArrayFromCollectionConverter<>),
                _ => typeof(SameTypeArrayFromEnumerableConverter<>)
            };
        }
    }

    private sealed class OtherTypeArrayProvider : IEnumerableConverterProvider
    {
        public static OtherTypeArrayProvider Default { get; } = new();

        public Type GetConverterType(SourceEnumerableType sourceEnumerableType)
        {
            return sourceEnumerableType switch
            {
                SourceEnumerableType.Array => typeof(OtherTypeArrayFromArrayConverter<,>),
                SourceEnumerableType.List => typeof(OtherTypeArrayFromListConverter<,>),
                SourceEnumerableType.Collection => typeof(OtherTypeArrayFromCollectionConverter<,>),
                _ => typeof(OtherTypeArrayFromEnumerableConverter<,>)
            };
        }
    }

    //--------------------------------------------------------------------------------
    // Same type
    //--------------------------------------------------------------------------------

#pragma warning disable CA1812
    private sealed class SameTypeArrayFromCollectionConverter<TDestination> : IConverter
    {
        public object Convert(object source)
        {
            var sourceCollection = (ICollection<TDestination>)source;
            var array = new TDestination[sourceCollection.Count];
            sourceCollection.CopyTo(array, 0);

            return array;
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class SameTypeArrayFromEnumerableConverter<TDestination> : IConverter
    {
        public object Convert(object source)
        {
            if (source is ICollection<TDestination> sourceCollection)
            {
                var array = new TDestination[sourceCollection.Count];
                sourceCollection.CopyTo(array, 0);
                return array;
            }

            using var buffer = new ArrayBuffer<TDestination>(0);
            foreach (var value in (IEnumerable<TDestination>)source)
            {
                buffer.Add(value);
            }
            return buffer.ToArray();
        }
    }
#pragma warning restore CA1812

    //--------------------------------------------------------------------------------
    // Builder to other type Array from Collection
    //--------------------------------------------------------------------------------

#pragma warning disable CA1812
    private sealed class OtherTypeArrayFromArrayConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object?> converter;

        public OtherTypeArrayFromArrayConverter(Func<object, object?> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            var sourceArray = (TSource[])source;
            var array = new TDestination[sourceArray.Length];
            ref var sourceReference = ref MemoryMarshal.GetArrayDataReference(sourceArray);
            ref var destinationReference = ref MemoryMarshal.GetArrayDataReference(array);
            for (var i = 0; i < sourceArray.Length; i++)
            {
                Unsafe.Add(ref destinationReference, i) = ConvertValue<TSource, TDestination>(converter, Unsafe.Add(ref sourceReference, i));
            }

            return array;
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class OtherTypeArrayFromListConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object?> converter;

        public OtherTypeArrayFromListConverter(Func<object, object?> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            var sourceList = (IList<TSource>)source;
            var count = sourceList.Count;
            var array = new TDestination[count];
            for (var i = 0; i < count; i++)
            {
                array[i] = ConvertValue<TSource, TDestination>(converter, sourceList[i]);
            }

            return array;
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class OtherTypeArrayFromCollectionConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object?> converter;

        public OtherTypeArrayFromCollectionConverter(Func<object, object?> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            var sourceCollection = (ICollection<TSource>)source;
            var count = sourceCollection.Count;
            var array = new TDestination[count];

            var buffer = ArrayPool<TSource>.Shared.Rent(count);
            try
            {
                sourceCollection.CopyTo(buffer, 0);
                for (var i = 0; i < count; i++)
                {
                    array[i] = ConvertValue<TSource, TDestination>(converter, buffer[i]);
                }
            }
            finally
            {
                ArrayPool<TSource>.Shared.Return(buffer, RuntimeHelpers.IsReferenceOrContainsReferences<TSource>());
            }

            return array;
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class OtherTypeArrayFromEnumerableConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object?> converter;

        public OtherTypeArrayFromEnumerableConverter(Func<object, object?> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            using var buffer = new ArrayBuffer<TDestination>(0);
            foreach (var value in (IEnumerable<TSource>)source)
            {
                buffer.Add(ConvertValue<TSource, TDestination>(converter, value));
            }
            return buffer.ToArray();
        }
    }
#pragma warning restore CA1812
}
