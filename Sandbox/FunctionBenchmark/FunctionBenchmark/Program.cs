namespace FunctionBenchmark
{
    using System;

    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Columns;
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Diagnosers;
    using BenchmarkDotNet.Exporters;
    using BenchmarkDotNet.Jobs;
    using BenchmarkDotNet.Running;

    public static class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmark>();
        }
    }

    public class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            AddExporter(MarkdownExporter.Default, MarkdownExporter.GitHub);
            AddColumn(
                StatisticColumn.Mean,
                StatisticColumn.Min,
                StatisticColumn.Max,
                StatisticColumn.P90,
                StatisticColumn.Error,
                StatisticColumn.StdDev);
            AddDiagnoser(MemoryDiagnoser.Default);
            AddJob(Job.MediumRun);
        }
    }

    [Config(typeof(BenchmarkConfig))]
    public class Benchmark
    {
        private readonly Converter converter = new();

        private readonly int value = 1;

        private readonly object objectValue = 1;

        private readonly Func<int, short> cachedPointerConverter = new Converter().GetCachedWrapPointerConverter();

        private readonly Func<int, short> cachedFuncConverter = new Converter().GetCachedFuncConverter();

        [Benchmark]
        public short ValueByPointer() => converter.ConvertByPointer(value);

        [Benchmark]
        public short ValueByTypedFunc() => converter.ConvertByTypedFunc(value);

        [Benchmark]
        public short ValueByCachePointer() => cachedPointerConverter(value);

        [Benchmark]
        public short ValueByCacheFunc() => cachedFuncConverter(value);

        [Benchmark]
        public object ObjectValueByPointer() => converter.ConvertByPointer(objectValue);

        [Benchmark]
        public object ObjectValueByTypedFunc() => converter.ConvertByTypedFunc(objectValue);

        [Benchmark]
        public object ObjectValueByFunc() => converter.ConvertByFunc(objectValue);
    }

    public unsafe class Converter
    {
        private readonly delegate*<int, short> pointerTypedConverter;

        private readonly Func<int, short> funcTypedConverter;

        private readonly Func<int, short> cachedConverter;

        private readonly Func<object, object> funcConverter;

        public Converter()
        {
            pointerTypedConverter = &Convert;
            funcTypedConverter = Convert;
            cachedConverter = x => pointerTypedConverter(x);
            funcConverter = ObjectConvert;
        }

        public Func<int, short> GetCachedWrapPointerConverter() => cachedConverter;

        public Func<int, short> GetCachedFuncConverter() => funcTypedConverter;

        public short ConvertByPointer(int value) => pointerTypedConverter(value);

        public short ConvertByTypedFunc(int value) => funcTypedConverter(value);

        public object ConvertByPointer(object value) => pointerTypedConverter((int)value);

        public object ConvertByTypedFunc(object value) => funcTypedConverter((int)value);

        public object ConvertByFunc(object value) => funcConverter((int)value);

        // Inner

        public static short Convert(int source) => (short)source;

        public static object ObjectConvert(object source) => (short)(int)source;
    }
}
