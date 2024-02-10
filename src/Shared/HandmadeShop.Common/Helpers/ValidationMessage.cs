namespace HandmadeShop.Common.Helpers;

public static class ValidationMessage
{
    public static string GetRequired(string filedName)
    {
        return $"{filedName} is required";
    }
}