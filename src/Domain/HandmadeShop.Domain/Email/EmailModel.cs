namespace HandmadeShop.Domain.Email;

public class EmailModel
{
    public string Subject { get; }
    public string Body { get; }
    public string DestinationEmail { get; set; }

    public EmailModel(string subject, string body, string destinationEmail)
    {
        Subject = subject;
        Body = body;
        DestinationEmail = destinationEmail;
    }
}