namespace HandmadeShop.Domain.Email;

public class EmailModel(string subject, string body, string destinationEmail)
{
    public string Subject { get; } = subject;
    public string Body { get; } = body;
    public string DestinationEmail { get; set; } = destinationEmail;
}