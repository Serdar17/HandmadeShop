namespace HandmadeShop.Common.Validator;

public interface IModelValidator<in T> where T : class
{
    void Check(T model);
    Task CheckAsync(T model);
}