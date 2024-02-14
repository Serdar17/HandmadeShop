public class MainSettings
{
    public const string SectionName = "Main";
    
    public string PublicUrl { get; private set; }
    public string InternalUrl { get; private set; }
    public string AllowedOrigins { get; private set; }
    public int UploadFileSizeLimit { get; private set; } = 20971520;
    public int UploadAvatarSizeLimit { get; private set; }
}