using System;
using System.Buffers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using Google.Protobuf;
using K4os.Compression.LZ4;
using MessagePack;
using Microsoft.Diagnostics.Runtime.Interop;
using Newtonsoft.Json;

namespace SerializationBenchmark
{
    internal static class Serializers
    {
        private static readonly BufferWriterPool<byte> _pool = new();

        private static readonly MessagePackSerializerOptions _options = MessagePackSerializerOptions.Standard
            .WithOmitAssemblyVersion(true);

        private static readonly MessagePackSerializerOptions _compressingOptions = MessagePackSerializerOptions.Standard
            .WithOmitAssemblyVersion(true)
            .WithCompression(MessagePackCompression.Lz4BlockArray);

        public static ArrayBufferWriter<byte> MessagePack_SerializePlain<T>(T data)
        {
            var br = _pool.Rent();
            MessagePackSerializer.Serialize(br, data);
            return br;
        }

        public static T MessagePack_DeserializePlain<T>(ReadOnlyMemory<byte> data)
        {
            return MessagePackSerializer.Deserialize<T>(data, _options);
        }

        public static ArrayBufferWriter<byte> MessagePack_SerializePickled<T>(T data)
        {
            var br = _pool.Rent();
            var br2 = _pool.Rent();
            MessagePackSerializer.Serialize(br, data, _options);
            LZ4Pickler.Pickle(br.WrittenSpan, br2);
            _pool.Return(br);

            return br2;
        }

        public static T MessagePack_DeserializePickled<T>(ReadOnlySpan<byte> data)
        {
            var br = _pool.Rent();
            LZ4Pickler.Unpickle(data, br);
            var result = MessagePackSerializer.Deserialize<T>(br.WrittenMemory, _options);
            _pool.Return(br);

            return result;
        }

        public static ArrayBufferWriter<byte> MessagePack_SerializeCompressed<T>(T data)
        {
            var br = _pool.Rent();
            MessagePackSerializer.Serialize(br, data, _compressingOptions);
            return br;
        }

        public static T MessagePack_DeserializeCompressed<T>(ReadOnlyMemory<byte> data)
        {
            return MessagePackSerializer.Deserialize<T>(data, _compressingOptions);
        }

        public static ArrayBufferWriter<byte> ProtobufNet_SerializePlain<T>(T data)
        {
            var br = _pool.Rent();
            ProtoBuf.Serializer.Serialize(br, data);
            return br;
        }

        public static T ProtobufNet_DeserializePlain<T>(ReadOnlySpan<byte> data)
        {
            return ProtoBuf.Serializer.Deserialize<T>(data);
        }

        public static ArrayBufferWriter<byte> ProtobufNet_SerializePickled<T>(T data)
        {
            var br = _pool.Rent();
            var br2 = _pool.Rent();
            ProtoBuf.Serializer.Serialize(br, data);
            LZ4Pickler.Pickle(br.WrittenSpan, br2);
            _pool.Return(br);

            return br2;
        }

        public static T ProtobufNet_DeserializePickled<T>(ReadOnlySpan<byte> data)
        {
            var br = _pool.Rent();
            LZ4Pickler.Unpickle(data, br);
            var result = ProtoBuf.Serializer.Deserialize<T>(br.WrittenMemory);
            _pool.Return(br);

            return result;
        }

        public static ArrayBufferWriter<byte> Protobuf_SerializePlain(IBufferMessage data)
        {
            var br = _pool.Rent();
            data.WriteTo(br);
            return br;
        }

        public static void Protobuf_DeserializePlain(byte[] data, IMessage target)
        {
            target.MergeFrom(data);
        }

        public static ArrayBufferWriter<byte> Protobuf_SerializePickled(IBufferMessage data)
        {
            var br = _pool.Rent();
            var br2 = _pool.Rent();
            data.WriteTo(br);
            LZ4Pickler.Pickle(br.WrittenSpan, br2);
            _pool.Return(br);

            return br2;
        }

        public static void Protobuf_DeserializePickled(ReadOnlySpan<byte> data, IMessage target)
        {
            target.MergeFrom(LZ4Pickler.Unpickle(data));
        }

        public static byte[] SystemTextJson_SerializePlain<T>(T data)
        {
            return System.Text.Json.JsonSerializer.SerializeToUtf8Bytes<T>(data);
        }

        public static T SystemTextJson_DeserializePlain<T>(ReadOnlySpan<byte> data)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(data)!;
        }

        public static ArrayBufferWriter<byte> SystemTextJson_SerializePickled<T>(T data)
        {
            var br = _pool.Rent();
            var br2 = _pool.Rent();
            System.Text.Json.JsonSerializer.Serialize(new Utf8JsonWriter(br), data);
            LZ4Pickler.Pickle(br.WrittenSpan, br2);
            _pool.Return(br);

            return br2;
        }

        public static T SystemTextJson_DeserializedPickled<T>(ReadOnlySpan<byte> data)
        {
            var br = _pool.Rent();
            LZ4Pickler.Unpickle(data, br);
            var result = System.Text.Json.JsonSerializer.Deserialize<T>(br.WrittenSpan)!;
            _pool.Return(br);

            return result;
        }

        public static ArrayBufferWriter<byte> Newtonsoft_SerializePlain<T>(T data)
        {
            var br = _pool.Rent();
            var ser = JsonConvert.SerializeObject(data);
            var count = UTF8Encoding.UTF8.GetByteCount(ser);
            var span = br.GetSpan(count);
            UTF8Encoding.UTF8.GetBytes(ser, span);
            br.Advance(count);

            return br;
        }

        public static T Newtonsoft_DeserializePlain<T>(ReadOnlySpan<byte> data)
        {
            return JsonConvert.DeserializeObject<T>(UTF8Encoding.UTF8.GetString(data));
        }

        public static ArrayBufferWriter<byte> Newtonsoft_SerializePickled<T>(T data)
        {
            var br = _pool.Rent();
            var br2 = _pool.Rent();
            var ser = JsonConvert.SerializeObject(data);
            int count = UTF8Encoding.UTF8.GetByteCount(ser);
            var span = br.GetSpan(count);
            UTF8Encoding.UTF8.GetBytes(ser, span);
            br.Advance(count);
            LZ4Pickler.Pickle(br.WrittenSpan, br2);
            _pool.Return(br);

            return br2;
        }

        public static T Newtonsoft_DeserializedPickled<T>(ReadOnlySpan<byte> data)
        {
            var br = _pool.Rent();
            LZ4Pickler.Unpickle(data, br);
            var result = JsonConvert.DeserializeObject<T>(UTF8Encoding.UTF8.GetString(br.WrittenSpan));
            _pool.Return(br);

            return result;
        }

        public static void Return(ArrayBufferWriter<byte> writer)
        {
            _pool.Return(writer);
        }
    }
}
