namespace HandmadeShop.Web.Common;

public class Result<TValue>
{
    public TValue? Value { get; }

    protected Result()
    {
        IsSuccess = true;
    }
    protected Result(TValue value)
    {
        Value = value;
        IsSuccess = true;
        Error = Error.None;
    }
    protected Result(Error error)
    {
        IsSuccess = false;
        Error = error;
    }
    
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    
    public Error Error { get; }
    
    public static Result<TValue> Success(TValue value) => new(value);

    public static Result<TValue> Failure(Error error) => new(error);
    
    public static implicit operator Result<TValue>(TValue value) => new (value);
    
    public static implicit operator Result<TValue> (Error error) => new (error);
}