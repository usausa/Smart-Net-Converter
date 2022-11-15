``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22621.755)
AMD Ryzen 9 5900X, 1 CPU, 24 logical and 12 physical cores
.NET SDK=7.0.100
  [Host]    : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2
  MediumRun : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2

Job=MediumRun  IterationCount=15  LaunchCount=2  
WarmupCount=10  

```
|                 Method |       Mean |     Error |    StdDev |     Median |       Min |        Max |        P90 |   Gen0 | Allocated |
|----------------------- |-----------:|----------:|----------:|-----------:|----------:|-----------:|-----------:|-------:|----------:|
|         ValueByPointer |  0.3405 ns | 0.0052 ns | 0.0076 ns |  0.3390 ns | 0.3304 ns |  0.3598 ns |  0.3492 ns |      - |         - |
|       ValueByTypedFunc |  0.7529 ns | 0.0095 ns | 0.0137 ns |  0.7525 ns | 0.7352 ns |  0.7853 ns |  0.7711 ns |      - |         - |
|    ValueByCachePointer |  1.0658 ns | 0.0431 ns | 0.0631 ns |  1.0761 ns | 0.8741 ns |  1.1424 ns |  1.1273 ns |      - |         - |
|       ValueByCacheFunc |  0.5749 ns | 0.0075 ns | 0.0110 ns |  0.5750 ns | 0.5564 ns |  0.5940 ns |  0.5906 ns |      - |         - |
|   ObjectValueByPointer | 10.2498 ns | 1.6292 ns | 2.4385 ns | 10.8138 ns | 4.8718 ns | 13.3111 ns | 12.9444 ns | 0.0014 |      24 B |
| ObjectValueByTypedFunc |  5.8232 ns | 0.5102 ns | 0.7636 ns |  5.8964 ns | 4.7240 ns |  6.9598 ns |  6.7431 ns | 0.0014 |      24 B |
|      ObjectValueByFunc | 11.1047 ns | 1.6940 ns | 2.5355 ns |  9.3085 ns | 8.2578 ns | 14.9639 ns | 14.2565 ns | 0.0029 |      48 B |
