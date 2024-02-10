public class SwaggerSettings
{
    public const string SectionName = "Swagger";
    
    public bool Enabled { get; private set; } = false;

    public string OAuthClientId { get; private set; }
    public string OAuthClientSecret { get; private set; }

    public SwaggerSettings()
    {
        Enabled = false;
    }
}