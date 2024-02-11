namespace HandmadeShop.Services.RabbitMq.Settings;

public class RabbitMqSettings
{
    public const string SectionName = "RabbitMq";
    
    public string Uri { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; }
}
