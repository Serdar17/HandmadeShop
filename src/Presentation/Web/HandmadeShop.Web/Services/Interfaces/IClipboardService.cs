namespace HandmadeShop.Web.Services;

public interface IClipboardService
{
    Task CopyToClipboard(string text);
}