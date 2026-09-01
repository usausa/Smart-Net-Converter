namespace Smart.Converter;

using System.Collections;

#pragma warning disable CA1002
public class WrapperCollection<T> : ICollection<T>
{
    protected List<T> List { get; }

    public WrapperCollection(IEnumerable<T> source)
    {
        List = source.ToList();
    }

    public IEnumerator<T> GetEnumerator() => List.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(T item) => List.Add(item);

    public void Clear() => List.Clear();

    public bool Contains(T item) => throw new NotSupportedException();

    public void CopyTo(T[] array, int arrayIndex) => List.CopyTo(array, arrayIndex);

    public bool Remove(T item) => throw new NotSupportedException();

    public int Count => List.Count;

    public bool IsReadOnly => false;
}
#pragma warning restore CA1002

public sealed class WrapperList<T> : WrapperCollection<T>, IList<T>
{
    public WrapperList(IEnumerable<T> source)
        : base(source)
    {
    }

    public int IndexOf(T item) => throw new NotSupportedException();

    public void Insert(int index, T item) => throw new NotSupportedException();

    public void RemoveAt(int index) => throw new NotSupportedException();

    public T this[int index]
    {
        get => List[index];
        set => List[index] = value;
    }
}
