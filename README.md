# SerializationBenchmark

Compare serialization to a byte buffer for different packages and options.

|                            Method |       Mean |    Error |   StdDev |     Median |       Gen 0 |      Gen 1 | Gen 2 |  Allocated |
|---------------------------------- |-----------:|---------:|---------:|-----------:|------------:|-----------:|------:|-----------:|
|        MessagePack_SerializePlain |   314.6 ms |  9.62 ms | 28.23 ms |   300.8 ms |   2000.0000 |          - |     - |    9.61 MB |
|   MessagePack_SerializeCompressed |   483.1 ms |  6.76 ms |  6.00 ms |   482.5 ms |   2000.0000 |          - |     - |    9.61 MB |
|      MessagePack_SerializePickled |   715.3 ms | 13.81 ms | 18.90 ms |   711.7 ms |   2000.0000 |          - |     - |    9.61 MB |
|        ProtobufNet_SerializePlain | 1,041.0 ms | 13.51 ms | 12.64 ms | 1,041.2 ms |  11000.0000 |          - |     - |   44.25 MB |
|      ProtobufNet_SerializePickled | 1,462.1 ms |  9.75 ms |  8.15 ms | 1,462.8 ms |  11000.0000 |          - |     - |   44.25 MB |
|           Protobuf_SerializePlain |   424.9 ms |  6.37 ms |  5.32 ms |   424.6 ms |   3000.0000 |          - |     - |   13.73 MB |
|         Protobuf_SerializePickled |   906.9 ms |  8.65 ms |  7.22 ms |   905.1 ms |   3000.0000 |          - |     - |   13.73 MB |
|     SystemTextJson_SerializePlain |   760.2 ms | 15.08 ms | 13.37 ms |   755.8 ms |  56000.0000 |          - |     - |  226.97 MB |
|   SystemTextJson_SerializePickled | 1,360.8 ms | 25.24 ms | 24.79 ms | 1,366.6 ms |   4000.0000 |          - |     - |   19.53 MB |
|         Newtonsoft_SerializePlain | 1,592.5 ms | 31.41 ms | 34.91 ms | 1,577.8 ms | 285000.0000 | 47000.0000 |     - | 1153.26 MB |
|       Newtonsoft_SerializePickled | 2,187.5 ms | 38.19 ms | 35.73 ms | 2,191.8 ms | 235000.0000 |          - |     - |  945.74 MB |
|      MessagePack_DeserializePlain |   526.6 ms |  5.65 ms |  5.00 ms |   524.4 ms | 113000.0000 |  2000.0000 |     - |  452.12 MB |
| MessagePack_DeserializeCompressed |   566.0 ms |  9.74 ms |  9.11 ms |   565.0 ms | 113000.0000 |  2000.0000 |     - |  452.12 MB |
|    MessagePack_DeserializePickled |   608.7 ms | 11.86 ms | 11.65 ms |   607.3 ms | 113000.0000 |  2000.0000 |     - |  452.12 MB |
|      ProtobufNet_DeserializePlain |   729.4 ms | 14.40 ms | 19.70 ms |   721.9 ms | 121000.0000 |          - |     - |  482.79 MB |
|    ProtobufNet_DeserializePickled |   744.1 ms |  6.44 ms |  6.02 ms |   744.4 ms | 121000.0000 |          - |     - |  482.79 MB |
|         Protobuf_DeserializePlain |   488.1 ms |  4.75 ms |  4.21 ms |   486.5 ms | 129000.0000 |  1000.0000 |     - |  518.34 MB |
|       Protobuf_DeserializePickled |   561.3 ms | 11.02 ms | 13.54 ms |   561.2 ms | 154000.0000 | 22000.0000 |     - |  617.76 MB |
|   SystemTextJson_DeserializePlain | 1,546.7 ms |  9.80 ms |  8.69 ms | 1,547.0 ms | 125000.0000 | 16000.0000 |     - |  498.96 MB |
| SystemTextJson_DeserializePickled | 1,715.3 ms | 22.92 ms | 21.44 ms | 1,727.5 ms | 125000.0000 | 16000.0000 |     - |  498.96 MB |
|       Newtonsoft_DeserializePlain | 2,757.0 ms | 40.80 ms | 36.17 ms | 2,754.1 ms | 255000.0000 |  3000.0000 |     - | 1021.19 MB |
|     Newtonsoft_DeserializePickled | 2,885.2 ms | 47.46 ms | 44.40 ms | 2,901.8 ms | 255000.0000 | 54000.0000 |     - | 1021.35 MB |

Serialized data sizes for the different options:

_msgpack_plain         : 11012 bytes
_msgpack_comp          : 7382 bytes
_msgpack_pickled       : 6996 bytes
_protobufnet_plain     : 11610 bytes
_protobufnet_pickled   : 7346 bytes
_protobuf_plain        : 10393 bytes
_protobuf_pickled      : 7164 bytes
_systemtextjson_plain  : 21686 bytes
_systemtextjson_pickled: 8360 bytes
_newtonsoft_plain      : 21671 bytes
_newtonsoft_pickled    : 8349 bytes

The *Plain varients use the respective technology directly in their best light, using reusable buffers
when possible.

The *Compressed variants use built-in compression functionality when available.

The *Pickled vriants use an external LZ4 compression library.
