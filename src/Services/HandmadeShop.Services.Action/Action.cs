using HandmadeShop.Common.Constants;
using HandmadeShop.Domain.Email;
using HandmadeShop.Infrastructure.Abstractions.Actions;
using HandmadeShop.Services.RabbitMq.RabbitMq;
using Microsoft.Extensions.Logging;

namespace HandmadeShop.Services.Action;

public class Action(
    IRabbitMq rabbitMq,
    ILogger<Action> logger) : IAction
{
    public async Task SendEmail(EmailModel email)
    {
        logger.LogInformation("The message {@email} was sent to the queue", email);
        await rabbitMq.PushAsync(RabbitMqTaskQueueNames.SendEmail, email);
    }
}