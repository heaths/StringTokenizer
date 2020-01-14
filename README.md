This is a port of [StringTokenizer](https://github.com/dotnet/extensions/blob/master/src/Primitives/src/StringTokenizer.cs) for benchmarking variations and for eventual inclusion as source rather than redistributing an entire assembly.

# Benchmarks

The benchmark below is from a Surface Laptop 2, but primarily meant to show relative performance of `string.Split`, and `StringTokenizer` using both safe and unsafe methods.

|                     Method |            Options |                 Text |        Mean |     Error |      StdDev |      Median |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------------- |------------------- |--------------------- |------------:|----------:|------------:|------------:|-------:|------:|------:|----------:|
|                UsingString |               None |  Lore(...)qua. [123] |    922.8 ns |  18.10 ns |    35.74 ns |    916.3 ns | 0.2136 |     - |     - |     896 B |
|       UsingStringTokenizer |               None |  Lore(...)qua. [123] |    781.3 ns |  16.38 ns |    41.98 ns |    773.7 ns |      - |     - |     - |         - |
| UsingUnsafeStringTokenizer |               None |  Lore(...)qua. [123] |    827.4 ns |  17.33 ns |    49.16 ns |    814.1 ns |      - |     - |     - |         - |
|                UsingString |               None |  Lore(...)rum. [445] |  3,322.5 ns |  73.65 ns |    90.45 ns |  3,299.0 ns | 0.7477 |     - |     - |    3128 B |
|       UsingStringTokenizer |               None |  Lore(...)rum. [445] |  2,786.4 ns |  55.30 ns |   148.55 ns |  2,734.3 ns |      - |     - |     - |         - |
| UsingUnsafeStringTokenizer |               None |  Lore(...)rum. [445] |  3,092.4 ns |  86.92 ns |   250.79 ns |  3,028.6 ns |      - |     - |     - |         - |
|                UsingString |               None | Lore(...)mst. [1334] | 12,894.3 ns | 633.20 ns | 1,816.77 ns | 12,279.8 ns | 2.1973 |     - |     - |    9240 B |
|       UsingStringTokenizer |               None | Lore(...)mst. [1334] | 11,737.3 ns | 404.87 ns | 1,187.43 ns | 11,565.5 ns |      - |     - |     - |         - |
| UsingUnsafeStringTokenizer |               None | Lore(...)mst. [1334] | 10,909.2 ns | 502.56 ns | 1,481.80 ns | 10,628.8 ns |      - |     - |     - |         - |
|                UsingString | RemoveEmptyEntries |  Lore(...)qua. [123] |  1,045.3 ns |  37.00 ns |   109.10 ns |  1,031.0 ns | 0.2556 |     - |     - |    1072 B |
|       UsingStringTokenizer | RemoveEmptyEntries |  Lore(...)qua. [123] |    935.6 ns |  19.89 ns |    17.64 ns |    936.9 ns |      - |     - |     - |         - |
| UsingUnsafeStringTokenizer | RemoveEmptyEntries |  Lore(...)qua. [123] |    982.7 ns |  20.49 ns |    36.95 ns |    975.1 ns |      - |     - |     - |         - |
|                UsingString | RemoveEmptyEntries |  Lore(...)rum. [445] |  3,866.8 ns | 137.39 ns |   398.60 ns |  3,823.6 ns | 0.8850 |     - |     - |    3704 B |
|       UsingStringTokenizer | RemoveEmptyEntries |  Lore(...)rum. [445] |  3,689.0 ns | 150.97 ns |   430.72 ns |  3,638.9 ns |      - |     - |     - |         - |
| UsingUnsafeStringTokenizer | RemoveEmptyEntries |  Lore(...)rum. [445] |  4,935.7 ns | 221.83 ns |   650.60 ns |  4,896.7 ns |      - |     - |     - |         - |
|                UsingString | RemoveEmptyEntries | Lore(...)mst. [1334] | 14,652.2 ns | 391.33 ns | 1,153.83 ns | 14,617.8 ns | 2.5940 |     - |     - |   10872 B |
|       UsingStringTokenizer | RemoveEmptyEntries | Lore(...)mst. [1334] | 12,600.7 ns | 344.43 ns | 1,015.57 ns | 12,399.5 ns |      - |     - |     - |         - |
| UsingUnsafeStringTokenizer | RemoveEmptyEntries | Lore(...)mst. [1334] | 13,756.7 ns | 346.14 ns |   981.94 ns | 13,654.1 ns |      - |     - |     - |         - |
