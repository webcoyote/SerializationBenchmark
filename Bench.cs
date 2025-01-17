﻿#define SKIP_JSON

using System;
using System.Buffers;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace SerializationBenchmark
{
    [MemoryDiagnoser]
    public class Bench
    {
        const int Iterations = 10000;

        private static readonly BufferWriter<byte> _msgpack_plain;
        private static readonly BufferWriter<byte> _msgpack_comp;
        private static readonly BufferWriter<byte> _msgpack_pickled;
        private static readonly BufferWriter<byte> _protobufnet_plain;
        private static readonly BufferWriter<byte> _protobufnet_pickled;
        private static readonly BufferWriter<byte> _protobuf_plain;
        private static readonly BufferWriter<byte> _protobuf_pickled;

#if !SKIP_JSON
        private static readonly byte[] _systemtextjson_plain;
        private static readonly BufferWriter<byte> _systemtextjson_pickled;
        private static readonly BufferWriter<byte> _newtonsoft_plain;
        private static readonly BufferWriter<byte> _newtonsoft_pickled;
#endif

        static Bench()
        {
            _msgpack_plain = Serializers.MessagePack_SerializePlain(TestData.TestValue);
            _msgpack_comp = Serializers.MessagePack_SerializeCompressed(TestData.TestValue);
            _msgpack_pickled = Serializers.MessagePack_SerializePickled(TestData.TestValue);
            _protobufnet_plain = Serializers.ProtobufNet_SerializePlain(TestData.TestValue);
            _protobufnet_pickled = Serializers.ProtobufNet_SerializePickled(TestData.TestValue);
            _protobuf_plain = Serializers.Protobuf_SerializePlain(TestData.TestValuePB);
            _protobuf_pickled = Serializers.Protobuf_SerializePickled(TestData.TestValuePB);
#if !SKIP_JSON
            _systemtextjson_plain = Serializers.SystemTextJson_SerializePlain(TestData.TestValue);
            _systemtextjson_pickled = Serializers.SystemTextJson_SerializePickled(TestData.TestValue);
            _newtonsoft_plain = Serializers.Newtonsoft_SerializePlain(TestData.TestValue);
            _newtonsoft_pickled = Serializers.Newtonsoft_SerializePickled(TestData.TestValue);
#endif

            Console.WriteLine($"_msgpack_plain         : {_msgpack_plain.WrittenCount} bytes");
            Console.WriteLine($"_msgpack_comp          : {_msgpack_comp.WrittenCount} bytes");
            Console.WriteLine($"_msgpack_pickled       : {_msgpack_pickled.WrittenCount} bytes");
            Console.WriteLine($"_protobufnet_plain     : {_protobufnet_plain.WrittenCount} bytes");
            Console.WriteLine($"_protobufnet_pickled   : {_protobufnet_pickled.WrittenCount} bytes");
            Console.WriteLine($"_protobuf_plain        : {_protobuf_plain.WrittenCount} bytes");
            Console.WriteLine($"_protobuf_pickled      : {_protobuf_pickled.WrittenCount} bytes");
#if !SKIP_JSON
            Console.WriteLine($"_systemtextjson_plain  : {_systemtextjson_plain.Length} bytes");
            Console.WriteLine($"_systemtextjson_pickled: {_systemtextjson_pickled.WrittenCount} bytes");
            Console.WriteLine($"_newtonsoft_plain      : {_newtonsoft_plain.WrittenCount} bytes");
            Console.WriteLine($"_newtonsoft_pickled    : {_newtonsoft_pickled.WrittenCount} bytes");
#endif
        }

        [Benchmark]
        public void MessagePack_SerializePlain()
        {
            for (int i = 0; i < Iterations; i++)
            {
                var br = Serializers.MessagePack_SerializePlain(TestData.TestValue);
                Serializers.Return(br);
            }
        }

        [Benchmark]
        public void MessagePack_SerializeCompressed()
        {
            for (int i = 0; i < Iterations; i++)
            {
                var br = Serializers.MessagePack_SerializeCompressed(TestData.TestValue);
                Serializers.Return(br);
            }
        }

        [Benchmark]
        public void MessagePack_SerializePickled()
        {
            for (int i = 0; i < Iterations; i++)
            {
                var br = Serializers.MessagePack_SerializePickled(TestData.TestValue);
                Serializers.Return(br);
            }
        }

        [Benchmark]
        public void ProtobufNet_SerializePlain()
        {
            for (int i = 0; i < Iterations; i++)
            {
                var br = Serializers.ProtobufNet_SerializePlain(TestData.TestValue);
                Serializers.Return(br);
            }
        }

        [Benchmark]
        public void ProtobufNet_SerializePickled()
        {
            for (int i = 0; i < Iterations; i++)
            {
                var br = Serializers.ProtobufNet_SerializePickled(TestData.TestValue);
                Serializers.Return(br);
            }
        }

        [Benchmark]
        public void Protobuf_SerializePlain()
        {
            for (int i = 0; i < Iterations; i++)
            {
                var br = Serializers.Protobuf_SerializePlain(TestData.TestValuePB);
                Serializers.Return(br);
            }
        }

        [Benchmark]
        public void Protobuf_SerializePickled()
        {
            for (int i = 0; i < Iterations; i++)
            {
                var br = Serializers.Protobuf_SerializePickled(TestData.TestValuePB);
                Serializers.Return(br);
            }
        }

#if !SKIP_JSON
        [Benchmark]
        public void SystemTextJson_SerializePlain()
        {
            for (int i = 0; i < Iterations; i++)
            {
                _ = Serializers.SystemTextJson_SerializePlain(TestData.TestValue);
            }
        }

        [Benchmark]
        public void SystemTextJson_SerializePickled()
        {
            for (int i = 0; i < Iterations; i++)
            {
                var br = Serializers.SystemTextJson_SerializePickled(TestData.TestValue);
                Serializers.Return(br);
            }
        }

        [Benchmark]
        public void Newtonsoft_SerializePlain()
        {
            for (int i = 0; i < Iterations; i++)
            {
                _ = Serializers.Newtonsoft_SerializePlain(TestData.TestValue);
            }
        }

        [Benchmark]
        public void Newtonsoft_SerializePickled()
        {
            for (int i = 0; i < Iterations; i++)
            {
                var br = Serializers.Newtonsoft_SerializePickled(TestData.TestValue);
                Serializers.Return(br);
            }
        }
#endif

        [Benchmark]
        public void MessagePack_DeserializePlain()
        {
            for (int i = 0; i < Iterations; i++)
            {
                _ = Serializers.MessagePack_DeserializePlain<UserLicensesResponse>(_msgpack_plain.WrittenMemory);
            }
        }

        [Benchmark]
        public void MessagePack_DeserializeCompressed()
        {
            for (int i = 0; i < Iterations; i++)
            {
                _ = Serializers.MessagePack_DeserializeCompressed<UserLicensesResponse>(_msgpack_comp.WrittenMemory);
            }
        }

        [Benchmark]
        public void MessagePack_DeserializePickled()
        {
            for (int i = 0; i < Iterations; i++)
            {
                _ = Serializers.MessagePack_DeserializePickled<UserLicensesResponse>(_msgpack_pickled.WrittenSpan);
            }
        }

        [Benchmark]
        public void ProtobufNet_DeserializePlain()
        {
            for (int i = 0; i < Iterations; i++)
            {
                _ = Serializers.ProtobufNet_DeserializePlain<UserLicensesResponse>(_protobufnet_plain.WrittenSpan);
            }
        }

        [Benchmark]
        public void ProtobufNet_DeserializePickled()
        {
            for (int i = 0; i < Iterations; i++)
            {
                _ = Serializers.ProtobufNet_DeserializePickled<UserLicensesResponse>(_protobufnet_pickled.WrittenSpan);
            }
        }

        [Benchmark]
        public void Protobuf_DeserializePlain()
        {
            for (int i = 0; i < Iterations; i++)
            {
                var target = new UserLicensesResponsePB();
                _ = Serializers.Protobuf_DeserializePlain<UserLicensesResponsePB>(_protobuf_plain.WrittenMemory, target);
            }
        }

        [Benchmark]
        public void Protobuf_DeserializePickled()
        {
            for (int i = 0; i < Iterations; i++)
            {
                var target = new UserLicensesResponsePB();
                _ = Serializers.Protobuf_DeserializePickled<UserLicensesResponsePB>(_protobuf_pickled.WrittenSpan, target);
            }
        }

#if !SKIP_JSON
        [Benchmark]
        public void SystemTextJson_DeserializePlain()
        {
            for (int i = 0; i < Iterations; i++)
            {
                _ = Serializers.SystemTextJson_DeserializePlain<UserLicensesResponse>(_systemtextjson_plain);
            }
        }

        [Benchmark]
        public void SystemTextJson_DeserializePickled()
        {
            for (int i = 0; i < Iterations; i++)
            {
                _ = Serializers.SystemTextJson_DeserializedPickled<UserLicensesResponse>(_systemtextjson_pickled.WrittenSpan);
            }
        }

        [Benchmark]
        public void Newtonsoft_DeserializePlain()
        {
            for (int i = 0; i < Iterations; i++)
            {
                _ = Serializers.Newtonsoft_DeserializePlain<UserLicensesResponse>(_newtonsoft_plain.WrittenSpan);
            }
        }

        [Benchmark]
        public void Newtonsoft_DeserializePickled()
        {
            for (int i = 0; i < Iterations; i++)
            {
                _ = Serializers.Newtonsoft_DeserializedPickled<UserLicensesResponse>(_newtonsoft_pickled.WrittenSpan);
            }
        }
#endif

        static void Main(string[] args)
        {
            _ = BenchmarkRunner.Run<Bench>();
        }
    }
}
