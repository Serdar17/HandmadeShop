namespace HandmadeShop.Services.Settings.Settings;

public class EmailSettings
{
    public const string SectionName = "EmailConfiguration";

    public string From { get; private set; }
    public string SmtpServer { get; private set; }
    public bool EnableSsl { get; private set; }
    public int Port { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; }
    
}