namespace HandmadeShop.Domain.Exceptions;

public class OrderingDomainException : Exception
{
    /// <summary>
    ///Error code
    /// </summary>
    public string? Code { get; }

    /// <summary>
    /// Error name
    /// </summary>
    public string? Name { get; }

    #region Constructors

    public OrderingDomainException()
    {
    }

    public OrderingDomainException(string message) : base(message)
    {
    }

    public OrderingDomainException(Exception inner) : base(inner.Message, inner)
    {
    }

    public OrderingDomainException(string message, Exception inner) : base(message, inner)
    {
    }

    public OrderingDomainException(string code, string message) : base(message)
    {
        Code = code;
    }

    public OrderingDomainException(string code, string message, Exception inner) : base(message, inner)
    {
        Code = code;
    }

    #endregion

    public static void ThrowIf(Func<bool> predicate, string message)
    {
        if (predicate.Invoke())
            throw new OrderingDomainException(message);
    }
}