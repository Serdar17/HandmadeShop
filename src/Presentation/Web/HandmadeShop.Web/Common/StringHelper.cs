namespace HandmadeShop.Web.Common;

public static class StringHelper
{
    public static string Decline(string word, int num)
    {
        if (num % 100 >= 11 && num % 100 <= 19)
        {
            return $"{word}ов";
        }

        var r = num % 10;

        return (r % 10) switch
        {
            1 => word,
            > 1 and < 5 => $"{word}a",
            _ => $"{word}ов"
        };
    } 
}