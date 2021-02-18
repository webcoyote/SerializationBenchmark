# SerializationBenchmark

Compare serialization to a byte buffer for different packages and options.

|                            Method |       Mean |     Error |    StdDev |     Median |       Gen 0 |      Gen 1 | Gen 2 |    Allocated |
|---------------------------------- |-----------:|----------:|----------:|-----------:|------------:|-----------:|------:|-------------:|
|        MessagePack_SerializePlain |   296.5 ms |   9.26 ms |  27.30 ms |   283.5 ms |   2000.0000 |          - |     - |   10080288 B |
|   MessagePack_SerializeCompressed |   487.4 ms |   8.67 ms |   7.68 ms |   487.4 ms |   2000.0000 |          - |     - |   10080288 B |
|      MessagePack_SerializePickled |   821.4 ms |  14.20 ms |  13.28 ms |   818.7 ms |   2000.0000 |          - |     - |   10080288 B |
|        ProtobufNet_SerializePlain | 1,059.6 ms |  20.60 ms |  36.08 ms | 1,050.7 ms |  11000.0000 |          - |     - |   46400288 B |
|      ProtobufNet_SerializePickled | 1,624.0 ms |  31.03 ms |  31.87 ms | 1,625.3 ms |  11000.0000 |          - |     - |   46401664 B |
|           Protobuf_SerializePlain |   381.6 ms |   7.12 ms |   6.31 ms |   384.0 ms |           - |          - |     - |        288 B |
|         Protobuf_SerializePickled |   939.5 ms |  14.72 ms |  13.77 ms |   937.3 ms |           - |          - |     - |        288 B |
|     SystemTextJson_SerializePlain |   726.4 ms |  14.48 ms |  15.50 ms |   726.7 ms |  56000.0000 |          - |     - |  238081536 B |
|   SystemTextJson_SerializePickled | 1,660.1 ms |  32.79 ms |  81.06 ms | 1,641.7 ms |   4000.0000 |          - |     - |   20480288 B |
|         Newtonsoft_SerializePlain | 1,694.9 ms |  20.97 ms |  19.61 ms | 1,690.5 ms | 288000.0000 |          - |     - | 1209520288 B |
|       Newtonsoft_SerializePickled | 2,372.9 ms |  25.39 ms |  21.20 ms | 2,361.0 ms | 235000.0000 |  1000.0000 |     - |  991840288 B |
|      MessagePack_DeserializePlain |   539.8 ms |   3.52 ms |   2.94 ms |   539.3 ms | 113000.0000 |  2000.0000 |     - |  474088912 B |
| MessagePack_DeserializeCompressed |   584.2 ms |   9.75 ms |   9.12 ms |   580.8 ms | 113000.0000 |  2000.0000 |     - |  474080576 B |
|    MessagePack_DeserializePickled |   600.4 ms |  12.00 ms |  13.82 ms |   593.9 ms | 113000.0000 |  2000.0000 |     - |  474081536 B |
|      ProtobufNet_DeserializePlain |   712.4 ms |  12.01 ms |  10.03 ms |   709.0 ms | 121000.0000 |          - |     - |  506241152 B |
|    ProtobufNet_DeserializePickled |   797.6 ms |  15.85 ms |  15.56 ms |   801.1 ms | 121000.0000 |          - |     - |  506241960 B |
|         Protobuf_DeserializePlain |   514.5 ms |   4.26 ms |   3.56 ms |   513.6 ms | 128000.0000 |  3000.0000 |     - |  535327640 B |
|       Protobuf_DeserializePickled |   588.2 ms |  11.74 ms |  18.27 ms |   588.6 ms | 128000.0000 |  3000.0000 |     - |  535326368 B |
|   SystemTextJson_DeserializePlain | 2,623.4 ms | 207.36 ms | 584.87 ms | 2,483.0 ms | 125000.0000 | 16000.0000 |     - |  523200288 B |
| SystemTextJson_DeserializePickled | 2,138.7 ms |  86.99 ms | 256.50 ms | 2,064.3 ms | 125000.0000 | 16000.0000 |     - |  523200288 B |
|       Newtonsoft_DeserializePlain | 3,463.8 ms | 105.98 ms | 300.66 ms | 3,362.9 ms | 255000.0000 |  2000.0000 |     - | 1071200288 B |
|     Newtonsoft_DeserializePickled | 3,082.5 ms |  60.75 ms | 109.54 ms | 3,052.7 ms | 255000.0000 | 54000.0000 |     - | 1070880288 B |

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

The *Plain variants use the respective technology directly in their best light, using reusable buffers
when possible.

The *Compressed variants use built-in compression functionality when available.

The *Pickled variants use an external LZ4 compression library.
