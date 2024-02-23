namespace HandmadeShop.Services.Settings.Settings;

public class CacheSettings
{
    public const string SectionName = "Cache";
    
    public string Uri { get; private set; } = string.Empty;
    
    public int CacheLifeTime { get; set; } = 1;
}