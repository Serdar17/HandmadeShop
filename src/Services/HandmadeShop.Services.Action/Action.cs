using HandmadeShop.Common.Constants;
using HandmadeShop.Domain.Email;
using HandmadeShop.Infrastructure.Abstractions.Actions;
using HandmadeShop.Services.RabbitMq.RabbitMq;
using Microsoft.Extensions.Logging;

namespace HandmadeShop.Services.Action;

public class Action : IAction
{
    private readonly IRabbitMq _rabbitMq;
    private readonly ILogger<Action> _logger;

    public Action(IRabbitMq rabbitMq, 
        ILogger<Action> logger)
    {
        _rabbitMq = rabbitMq;
        _logger = logger;
    }

    public async Task SendEmail(EmailModel email)
    {
        _logger.LogInformation("Сообщение было отправлено {@email}", email);
        await _rabbitMq.PushAsync(RabbitMqTaskQueueNames.SendEmail, email);
    }
}