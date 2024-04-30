using HandmadeShop.Common.Constants;
using HandmadeShop.Domain.Email;
using HandmadeShop.Infrastructure.Abstractions.EmailSender;
using HandmadeShop.Services.Logger.Logger;
using HandmadeShop.Services.RabbitMq.RabbitMq;

namespace HandmadeShop.Worker;

public class TaskExecutor(IAppLogger logger, IRabbitMq rabbitMq, IServiceProvider serviceProvider)
    : ITaskExecutor
{
    public void Start()
    {
        rabbitMq.Subscribe<EmailModel>(RabbitMqTaskQueueNames.SendEmail, async data
            => await Execute<IEmailSender>(async service =>
            {
                logger.Information($"RABBITMQ::: {RabbitMqTaskQueueNames.SendEmail}:");
                await service.Send(data);
            }));
    }
    
    private async Task Execute<T>(Func<T, Task> action)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();

            var service = scope.ServiceProvider.GetService<T>();
            if (service != null)
                await action(service);
            else
                logger.Information($"Error: {action} wasn`t resolved");
        }
        catch (Exception e)
        {
            logger.Information($"Error: {RabbitMqTaskQueueNames.SendEmail}: {e.Message}");
            throw;
        }
    }
}