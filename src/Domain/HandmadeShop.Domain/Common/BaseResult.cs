namespace HandmadeShop.Domain.Common;

public class Result : Result<Result>
{
    protected Result(Result value) : base(value)
    {
    }

    protected Result(Error error) : base(error)
    {
    }

    private Result() : base()
    {
    }

    public static Result Success() => new();
    
    public static implicit operator Result (Error error) => new (error);
}