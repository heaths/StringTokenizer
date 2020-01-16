This is a port of [StringTokenizer](https://github.com/dotnet/extensions/blob/master/src/Primitives/src/StringTokenizer.cs) for benchmarking variations and for eventual inclusion as source rather than redistributing an entire assembly.

# Benchmarks

```
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-8650U CPU 1.90GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.1.101
  [Host]     : .NET Core 3.1.1 (CoreCLR 4.700.19.60701, CoreFX 4.700.19.60801), X64 RyuJIT
  DefaultJob : .NET Core 3.1.1 (CoreCLR 4.700.19.60701, CoreFX 4.700.19.60801), X64 RyuJIT
```

|                     Method |            Options |                 Text |        Mean |       Error |      StdDev |      Median |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------------- |------------------- |--------------------- |------------:|------------:|------------:|------------:|-------:|------:|------:|----------:|
|                **UsingString** |               **None** |  **Lore(...)qua. [123]** |    **924.6 ns** |    **18.08 ns** |    **22.86 ns** |    **918.6 ns** | **0.2136** |     **-** |     **-** |     **896 B** |
|       UsingStringTokenizer |               None |  Lore(...)qua. [123] |    822.9 ns |    16.12 ns |    20.96 ns |    824.2 ns |      - |     - |     - |         - |
| UsingUnsafeStringTokenizer |               None |  Lore(...)qua. [123] |    864.8 ns |    17.31 ns |    25.38 ns |    869.8 ns |      - |     - |     - |         - |
|  UsingStringTokenizerSpans |               None |  Lore(...)qua. [123] |    728.5 ns |    14.14 ns |    17.36 ns |    728.0 ns |      - |     - |     - |         - |
|                **UsingString** |               **None** |  **Lore(...)rum. [445]** |  **3,698.3 ns** |    **65.82 ns** |    **67.60 ns** |  **3,683.5 ns** | **0.7477** |     **-** |     **-** |    **3128 B** |
|       UsingStringTokenizer |               None |  Lore(...)rum. [445] |  2,921.9 ns |    58.03 ns |    73.39 ns |  2,929.3 ns |      - |     - |     - |         - |
| UsingUnsafeStringTokenizer |               None |  Lore(...)rum. [445] |  3,006.6 ns |    49.09 ns |    43.52 ns |  3,007.0 ns |      - |     - |     - |         - |
|  UsingStringTokenizerSpans |               None |  Lore(...)rum. [445] |  2,264.4 ns |    44.46 ns |    69.21 ns |  2,282.5 ns |      - |     - |     - |         - |
|                **UsingString** |               **None** | **Lore(...)mst. [1334]** | **10,406.0 ns** |   **206.23 ns** |   **295.77 ns** | **10,391.5 ns** | **2.1973** |     **-** |     **-** |    **9240 B** |
|       UsingStringTokenizer |               None | Lore(...)mst. [1334] | 22,141.9 ns | 2,949.27 ns | 8,696.00 ns | 24,170.7 ns |      - |     - |     - |         - |
| UsingUnsafeStringTokenizer |               None | Lore(...)mst. [1334] | 23,224.3 ns |   923.15 ns | 2,692.88 ns | 22,827.1 ns |      - |     - |     - |         - |
|  UsingStringTokenizerSpans |               None | Lore(...)mst. [1334] | 17,523.6 ns |   352.03 ns |   686.61 ns | 17,242.6 ns |      - |     - |     - |         - |
|                **UsingString** | **RemoveEmptyEntries** |  **Lore(...)qua. [123]** |  **2,046.4 ns** |    **39.53 ns** |    **33.01 ns** |  **2,052.7 ns** | **0.2556** |     **-** |     **-** |    **1072 B** |
|       UsingStringTokenizer | RemoveEmptyEntries |  Lore(...)qua. [123] |  1,962.5 ns |    36.26 ns |    33.91 ns |  1,962.2 ns |      - |     - |     - |         - |
| UsingUnsafeStringTokenizer | RemoveEmptyEntries |  Lore(...)qua. [123] |  2,071.6 ns |    49.91 ns |    59.41 ns |  2,056.8 ns |      - |     - |     - |         - |
|  UsingStringTokenizerSpans | RemoveEmptyEntries |  Lore(...)qua. [123] |    720.3 ns |    14.44 ns |    39.05 ns |    711.6 ns |      - |     - |     - |         - |
|                **UsingString** | **RemoveEmptyEntries** |  **Lore(...)rum. [445]** |  **3,496.8 ns** |   **109.91 ns** |   **324.07 ns** |  **3,367.9 ns** | **0.8850** |     **-** |     **-** |    **3704 B** |
|       UsingStringTokenizer | RemoveEmptyEntries |  Lore(...)rum. [445] |  3,297.3 ns |    65.90 ns |   120.51 ns |  3,299.2 ns |      - |     - |     - |         - |
| UsingUnsafeStringTokenizer | RemoveEmptyEntries |  Lore(...)rum. [445] |  3,431.3 ns |    66.41 ns |   101.42 ns |  3,412.1 ns |      - |     - |     - |         - |
|  UsingStringTokenizerSpans | RemoveEmptyEntries |  Lore(...)rum. [445] |  2,473.3 ns |    48.76 ns |    68.35 ns |  2,475.5 ns |      - |     - |     - |         - |
|                **UsingString** | **RemoveEmptyEntries** | **Lore(...)mst. [1334]** | **10,154.6 ns** |   **198.07 ns** |   **203.41 ns** | **10,147.3 ns** | **2.5940** |     **-** |     **-** |   **10872 B** |
|       UsingStringTokenizer | RemoveEmptyEntries | Lore(...)mst. [1334] | 10,211.3 ns |   211.21 ns |   216.90 ns | 10,194.7 ns |      - |     - |     - |         - |
| UsingUnsafeStringTokenizer | RemoveEmptyEntries | Lore(...)mst. [1334] | 11,040.8 ns |   184.67 ns |   163.71 ns | 11,041.2 ns |      - |     - |     - |         - |
|  UsingStringTokenizerSpans | RemoveEmptyEntries | Lore(...)mst. [1334] |  7,918.3 ns |   157.10 ns |   204.28 ns |  7,955.9 ns |      - |     - |     - |         - |
