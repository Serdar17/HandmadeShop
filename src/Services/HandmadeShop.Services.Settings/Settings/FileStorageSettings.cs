namespace HandmadeShop.Services.Settings.Settings;

public class FileStorageSettings
{
    public const string SectionName = "YandexDisk";

    public string OAuthToken { get; private set; } = string.Empty;
}