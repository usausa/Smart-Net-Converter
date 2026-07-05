namespace Smart.Converter.Converters;

using System.Buffers;
using System.Runtime.CompilerServices;

internal struct ArrayBuffer<T> : IDisposable
{
    private T[] buffer;

    private int size;

    public ArrayBuffer(int initialSize)
    {
        buffer = initialSize > 0 ? ArrayPool<T>.Shared.Rent(initialSize) : [];
        size = 0;
    }

    public void Add(T value)
    {
        if (size >= buffer.Length)
        {
            var newBuffer = ArrayPool<T>.Shared.Rent(size == 0 ? 4 : size << 1);
            buffer.AsSpan(0, size).CopyTo(newBuffer);
            if (buffer.Length > 0)
            {
                ArrayPool<T>.Shared.Return(buffer, RuntimeHelpers.IsReferenceOrContainsReferences<T>());
            }

            buffer = newBuffer;
        }

        buffer[size++] = value;
    }

    public readonly T[] ToArray()
    {
        if (size == 0)
        {
            return [];
        }

        var array = new T[size];
        buffer.AsSpan(0, size).CopyTo(array);
        return array;
    }

    public void Dispose()
    {
        if (buffer.Length > 0)
        {
            ArrayPool<T>.Shared.Return(buffer, RuntimeHelpers.IsReferenceOrContainsReferences<T>());
        }

        buffer = [];
        size = 0;
    }
}
