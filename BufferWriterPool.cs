using System.Collections.Concurrent;

namespace SerializationBenchmark
{
    public sealed class BufferWriterPool<T>
    {
        private readonly ConcurrentBag<BufferWriter<T>> _writers = new();

        public BufferWriter<T> Rent()
        {
            if (_writers.TryTake(out var result))
            {
                return result;
            }

            return new BufferWriter<T>();
        }

        public void Return(BufferWriter<T> writer)
        {
            writer.Reset();
            _writers.Add(writer);
        }
    }
}
