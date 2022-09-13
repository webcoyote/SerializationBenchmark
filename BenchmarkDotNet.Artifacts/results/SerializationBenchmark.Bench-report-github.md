``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.856/21H2)
AMD Ryzen 7 3800X, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.302
  [Host]     : .NET 5.0.13 (5.0.1321.56516), X64 RyuJIT AVX2
  DefaultJob : .NET 5.0.13 (5.0.1321.56516), X64 RyuJIT AVX2


```
|                            Method |       Mean |    Error |   StdDev |        Gen0 |       Gen1 |    Allocated |
|---------------------------------- |-----------:|---------:|---------:|------------:|-----------:|-------------:|
|        MessagePack_SerializePlain |   240.8 ms |  1.64 ms |  1.53 ms |   1000.0000 |          - |   10080339 B |
|   MessagePack_SerializeCompressed |   387.4 ms |  6.93 ms |  6.14 ms |   1000.0000 |          - |   10080288 B |
|      MessagePack_SerializePickled |   562.7 ms |  2.94 ms |  2.61 ms |   1000.0000 |          - |   10080760 B |
|        ProtobufNet_SerializePlain |   949.4 ms |  6.86 ms |  6.42 ms |   5000.0000 |          - |   46401736 B |
|      ProtobufNet_SerializePickled | 1,280.5 ms | 14.83 ms | 13.87 ms |   5000.0000 |          - |   46401576 B |
|           Protobuf_SerializePlain |   316.7 ms |  2.84 ms |  2.66 ms |           - |          - |        144 B |
|         Protobuf_SerializePickled |   663.2 ms |  3.02 ms |  2.36 ms |           - |          - |        288 B |
|     SystemTextJson_SerializePlain |   618.2 ms |  4.90 ms |  4.35 ms |  28000.0000 |          - |  238081576 B |
|   SystemTextJson_SerializePickled | 1,052.4 ms |  8.89 ms |  8.32 ms |   2000.0000 |          - |   20481576 B |
|         Newtonsoft_SerializePlain | 1,431.2 ms |  8.25 ms |  7.71 ms | 144000.0000 | 24000.0000 | 1209361736 B |
|       Newtonsoft_SerializePickled | 1,849.9 ms | 11.96 ms | 11.19 ms | 118000.0000 | 14000.0000 |  991841576 B |
|      MessagePack_DeserializePlain |   436.1 ms |  5.58 ms |  5.22 ms |  56000.0000 |  7000.0000 |  474081576 B |
| MessagePack_DeserializeCompressed |   479.9 ms |  4.51 ms |  4.22 ms |  56000.0000 |  7000.0000 |  474081864 B |
|    MessagePack_DeserializePickled |   480.9 ms |  4.47 ms |  4.18 ms |  56000.0000 |  7000.0000 |  474082120 B |
|      ProtobufNet_DeserializePlain |   622.7 ms |  2.62 ms |  2.45 ms |  60000.0000 |  7000.0000 |  506241152 B |
|    ProtobufNet_DeserializePickled |   667.9 ms |  5.71 ms |  5.34 ms |  60000.0000 |  7000.0000 |  506242440 B |
|         Protobuf_DeserializePlain |   427.2 ms |  4.71 ms |  4.41 ms |  64000.0000 |  8000.0000 |  535304624 B |
|       Protobuf_DeserializePickled |   455.9 ms |  4.09 ms |  3.62 ms |  64000.0000 |  8000.0000 |  535304912 B |
|   SystemTextJson_DeserializePlain | 1,345.9 ms | 10.26 ms |  9.60 ms |  62000.0000 |  8000.0000 |  523201576 B |
| SystemTextJson_DeserializePickled | 1,357.5 ms |  8.56 ms |  8.00 ms |  62000.0000 |  8000.0000 |  523201576 B |
|       Newtonsoft_DeserializePlain | 2,824.2 ms | 11.06 ms | 10.34 ms | 127000.0000 | 31000.0000 | 1071041648 B |
|     Newtonsoft_DeserializePickled | 2,829.8 ms |  8.75 ms |  7.75 ms | 127000.0000 | 31000.0000 | 1071121648 B |

Serialized data sizes for the different options:

_msgpack_plain         : 11012 bytes
_msgpack_comp          : 7367 bytes
_msgpack_pickled       : 7005 bytes
_protobufnet_plain     : 11621 bytes
_protobufnet_pickled   : 7351 bytes
_protobuf_plain        : 10404 bytes
_protobuf_pickled      : 7170 bytes
_systemtextjson_plain  : 21702 bytes
_systemtextjson_pickled: 8381 bytes
_newtonsoft_plain      : 21687 bytes
_newtonsoft_pickled    : 8370 bytes

