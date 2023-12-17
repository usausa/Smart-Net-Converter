#nullable disable
namespace Smart.Converter.Converters;

public sealed partial class EnumerableConverterFactory
{
#pragma warning disable CA1812
    private sealed class SameTypeHashSetProvider : IEnumerableConverterProvider
    {
        public static IEnumerableConverterProvider Default { get; } = new SameTypeHashSetProvider();

        public Type GetConverterType(SourceEnumerableType sourceEnumerableType)
        {
            return typeof(SameTypeHashSetFromEnumerableConverter<>);
        }
    }
#pragma warning restore CA1812

#pragma warning disable CA1812
    private sealed class OtherTypeHashSetProvider : IEnumerableConverterProvider
    {
        public static IEnumerableConverterProvider Default { get; } = new OtherTypeHashSetProvider();

        public Type GetConverterType(SourceEnumerableType sourceEnumerableType)
        {
            return typeof(OtherTypeHashSetFromEnumerableConverter<,>);
        }
    }
#pragma warning restore CA1812

    //--------------------------------------------------------------------------------
    // Same type
    //--------------------------------------------------------------------------------

#pragma warning disable CA1812
    private sealed class SameTypeHashSetFromEnumerableConverter<TDestination> : IConverter
    {
        public object Convert(object source)
        {
            return new HashSet<TDestination>((IEnumerable<TDestination>)source);
        }
    }
#pragma warning restore CA1812

    //--------------------------------------------------------------------------------
    // Other type
    //--------------------------------------------------------------------------------

#pragma warning disable CA1812
    private sealed class OtherTypeHashSetFromEnumerableConverter<TSource, TDestination> : IConverter
    {
        private readonly Func<object, object> converter;

        public OtherTypeHashSetFromEnumerableConverter(Func<object, object> converter)
        {
            this.converter = converter;
        }

        public object Convert(object source)
        {
            var collection = new HashSet<TDestination>();
            foreach (var value in (IEnumerable<TSource>)source)
            {
                collection.Add((TDestination)converter(value));
            }

            return collection;
        }
    }
#pragma warning restore CA1812
}
