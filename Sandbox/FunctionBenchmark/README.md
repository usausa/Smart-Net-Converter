``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
AMD Ryzen 9 5900X, 1 CPU, 24 logical and 12 physical cores
.NET Core SDK=5.0.101
  [Host]    : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT
  MediumRun : .NET Core 5.0.1 (CoreCLR 5.0.120.57516, CoreFX 5.0.120.57516), X64 RyuJIT

Job=MediumRun  IterationCount=15  LaunchCount=2  
WarmupCount=10  

```
|                 Method |      Mean |     Error |    StdDev |     Median |       Min |        Max |        P90 |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------- |----------:|----------:|----------:|-----------:|----------:|-----------:|-----------:|-------:|------:|------:|----------:|
|         ValueByPointer | 0.3682 ns | 0.0058 ns | 0.0083 ns |  0.3658 ns | 0.3555 ns |  0.3919 ns |  0.3789 ns |      - |     - |     - |         - |
|       ValueByTypedFunc | 0.5648 ns | 0.0024 ns | 0.0035 ns |  0.5644 ns | 0.5597 ns |  0.5710 ns |  0.5690 ns |      - |     - |     - |         - |
|    ValueByCachePointer | 0.7682 ns | 0.0035 ns | 0.0053 ns |  0.7671 ns | 0.7604 ns |  0.7799 ns |  0.7766 ns |      - |     - |     - |         - |
|       ValueByCacheFunc | 0.6978 ns | 0.0614 ns | 0.0900 ns |  0.7763 ns | 0.5967 ns |  0.7912 ns |  0.7866 ns |      - |     - |     - |         - |
|   ObjectValueByPointer | 5.4053 ns | 0.5838 ns | 0.8184 ns |  5.3001 ns | 3.9882 ns |  7.3063 ns |  6.6473 ns | 0.0014 |     - |     - |      24 B |
| ObjectValueByTypedFunc | 7.4237 ns | 1.0588 ns | 1.5848 ns |  7.3618 ns | 4.9461 ns | 10.0906 ns |  9.6416 ns | 0.0014 |     - |     - |      24 B |
|      ObjectValueByFunc | 9.9599 ns | 1.2423 ns | 1.8593 ns | 10.0910 ns | 7.5402 ns | 12.3811 ns | 11.9766 ns | 0.0029 |     - |     - |      48 B |
