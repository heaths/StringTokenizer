This is a port of [StringTokenizer](https://github.com/dotnet/extensions/blob/master/src/Primitives/src/StringTokenizer.cs) for benchmarking variations and for eventual inclusion as source rather than redistributing an entire assembly.

# Benchmarks

```
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.19042
Intel Core i7-8650U CPU 1.90GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 3.1.11 (CoreCLR 4.700.20.56602, CoreFX 4.700.20.56604), X64 RyuJIT
  Job-MQCWDB : .NET Core 3.1.11 (CoreCLR 4.700.20.56602, CoreFX 4.700.20.56604), X64 RyuJIT

Runtime=.NET Core 3.1  Toolchain=netcoreapp3.1  
```

|                     Method |            Options |                 Text |        Mean |       Error |      StdDev |      Median | Ratio | RatioSD |   Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------------- |------------------- |--------------------- |------------:|------------:|------------:|------------:|------:|--------:|--------:|------:|------:|----------:|
|                UsingString |               None |  Lore(...)qua. [123] |    930.1 ns |    52.12 ns |   152.04 ns |    847.0 ns |  1.00 |    0.00 |  0.2136 |     - |     - |     896 B |
|       UsingStringTokenizer |               None |  Lore(...)qua. [123] |    688.1 ns |    17.39 ns |    49.33 ns |    666.4 ns |  0.75 |    0.11 |       - |     - |     - |         - |
| UsingUnsafeStringTokenizer |               None |  Lore(...)qua. [123] |    659.4 ns |    12.90 ns |    15.36 ns |    659.2 ns |  0.61 |    0.04 |       - |     - |     - |         - |
|  UsingStringTokenizerSpans |               None |  Lore(...)qua. [123] |    541.2 ns |    10.23 ns |    10.04 ns |    541.5 ns |  0.51 |    0.03 |       - |     - |     - |         - |
|    UsingRegularExpressions |               None |  Lore(...)qua. [123] |  5,562.2 ns |   121.19 ns |   157.58 ns |  5,551.1 ns |  5.13 |    0.32 |  1.6022 |     - |     - |    6712 B |
|                            |                    |                      |             |             |             |             |       |         |         |       |       |           |
|                UsingString |               None |  Lore(...)rum. [445] |  2,790.2 ns |    51.11 ns |    45.31 ns |  2,784.1 ns |  1.00 |    0.00 |  0.7477 |     - |     - |    3128 B |
|       UsingStringTokenizer |               None |  Lore(...)rum. [445] |  2,272.3 ns |    36.86 ns |    34.48 ns |  2,272.6 ns |  0.82 |    0.01 |       - |     - |     - |         - |
| UsingUnsafeStringTokenizer |               None |  Lore(...)rum. [445] |  2,429.9 ns |    46.97 ns |    43.94 ns |  2,427.2 ns |  0.87 |    0.03 |       - |     - |     - |         - |
|  UsingStringTokenizerSpans |               None |  Lore(...)rum. [445] |  2,835.0 ns |   125.87 ns |   359.13 ns |  2,860.3 ns |  0.90 |    0.19 |       - |     - |     - |         - |
|    UsingRegularExpressions |               None |  Lore(...)rum. [445] | 28,199.3 ns |   382.81 ns |   358.08 ns | 28,127.8 ns | 10.12 |    0.20 |  5.7678 |     - |     - |   24168 B |
|                            |                    |                      |             |             |             |             |       |         |         |       |       |           |
|                UsingString |               None | Lore(...)mst. [1334] | 13,865.4 ns |   252.36 ns |   236.06 ns | 13,859.7 ns |  1.00 |    0.00 |  2.1973 |     - |     - |    9240 B |
|       UsingStringTokenizer |               None | Lore(...)mst. [1334] | 11,404.0 ns |   319.27 ns |   403.78 ns | 11,277.2 ns |  0.83 |    0.03 |       - |     - |     - |         - |
| UsingUnsafeStringTokenizer |               None | Lore(...)mst. [1334] | 11,732.9 ns |   231.77 ns |   266.91 ns | 11,729.9 ns |  0.85 |    0.02 |       - |     - |     - |         - |
|  UsingStringTokenizerSpans |               None | Lore(...)mst. [1334] |  7,443.1 ns |   489.99 ns | 1,437.05 ns |  6,620.8 ns |  0.67 |    0.08 |       - |     - |     - |         - |
|    UsingRegularExpressions |               None | Lore(...)mst. [1334] | 63,548.1 ns | 1,259.20 ns | 1,293.10 ns | 63,199.8 ns |  4.58 |    0.13 | 17.8223 |     - |     - |   75009 B |
|                            |                    |                      |             |             |             |             |       |         |         |       |       |           |
|                UsingString | RemoveEmptyEntries |  Lore(...)qua. [123] |    807.8 ns |    18.20 ns |    17.03 ns |    802.0 ns |  1.00 |    0.00 |  0.2556 |     - |     - |    1072 B |
|       UsingStringTokenizer | RemoveEmptyEntries |  Lore(...)qua. [123] |    707.7 ns |    13.93 ns |    14.91 ns |    702.2 ns |  0.88 |    0.02 |       - |     - |     - |         - |
| UsingUnsafeStringTokenizer | RemoveEmptyEntries |  Lore(...)qua. [123] |    735.3 ns |    14.22 ns |    16.92 ns |    733.1 ns |  0.91 |    0.03 |       - |     - |     - |         - |
|  UsingStringTokenizerSpans | RemoveEmptyEntries |  Lore(...)qua. [123] |    622.2 ns |    11.68 ns |    10.92 ns |    618.6 ns |  0.77 |    0.02 |       - |     - |     - |         - |
|    UsingRegularExpressions | RemoveEmptyEntries |  Lore(...)qua. [123] |  5,543.9 ns |   105.71 ns |   129.82 ns |  5,489.2 ns |  6.92 |    0.28 |  1.4801 |     - |     - |    6200 B |
|                            |                    |                      |             |             |             |             |       |         |         |       |       |           |
|                UsingString | RemoveEmptyEntries |  Lore(...)rum. [445] |  2,952.7 ns |    57.36 ns |    56.34 ns |  2,948.5 ns |  1.00 |    0.00 |  0.8850 |     - |     - |    3704 B |
|       UsingStringTokenizer | RemoveEmptyEntries |  Lore(...)rum. [445] |  2,915.9 ns |    71.50 ns |    87.81 ns |  2,886.2 ns |  0.99 |    0.03 |       - |     - |     - |         - |
| UsingUnsafeStringTokenizer | RemoveEmptyEntries |  Lore(...)rum. [445] |  2,689.1 ns |    42.84 ns |    37.97 ns |  2,681.1 ns |  0.91 |    0.03 |       - |     - |     - |         - |
|  UsingStringTokenizerSpans | RemoveEmptyEntries |  Lore(...)rum. [445] |  2,275.7 ns |    70.39 ns |   204.22 ns |  2,185.7 ns |  0.85 |    0.05 |       - |     - |     - |         - |
|    UsingRegularExpressions | RemoveEmptyEntries |  Lore(...)rum. [445] | 19,532.9 ns |   389.84 ns |   571.42 ns | 19,436.0 ns |  6.64 |    0.25 |  5.3406 |     - |     - |   22376 B |
|                            |                    |                      |             |             |             |             |       |         |         |       |       |           |
|                UsingString | RemoveEmptyEntries | Lore(...)mst. [1334] |  9,178.4 ns |   183.25 ns |   196.07 ns |  9,155.4 ns |  1.00 |    0.00 |  2.5940 |     - |     - |   10872 B |
|       UsingStringTokenizer | RemoveEmptyEntries | Lore(...)mst. [1334] |  9,116.8 ns |   219.40 ns |   194.49 ns |  9,080.3 ns |  0.99 |    0.03 |       - |     - |     - |         - |
| UsingUnsafeStringTokenizer | RemoveEmptyEntries | Lore(...)mst. [1334] |  8,866.3 ns |   139.75 ns |   130.72 ns |  8,862.6 ns |  0.97 |    0.02 |       - |     - |     - |         - |
|  UsingStringTokenizerSpans | RemoveEmptyEntries | Lore(...)mst. [1334] |  6,736.4 ns |   126.22 ns |   123.97 ns |  6,734.0 ns |  0.73 |    0.02 |       - |     - |     - |         - |
|    UsingRegularExpressions | RemoveEmptyEntries | Lore(...)mst. [1334] | 55,075.4 ns |   461.29 ns |   408.93 ns | 54,973.2 ns |  5.99 |    0.13 | 15.0146 |     - |     - |   62976 B |
