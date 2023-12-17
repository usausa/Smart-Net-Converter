namespace Smart.Converter;

[Serializable]
public sealed class ObjectConverterException : Exception
{
    public ObjectConverterException()
    {
    }

    public ObjectConverterException(string message)
        : base(message)
    {
    }

    public ObjectConverterException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
