namespace HandmadeShop.Domain.Email;

public class EmailModel
{
    public string Subject { get; }
    public string Body { get; }

    public EmailModel(string subject, string body)
    {
        Subject = subject;
        Body = body;
    }
}