using HandmadeShop.Common.Constants;
using HandmadeShop.Domain.Email;
using HandmadeShop.Domain.EmailSender;
using HandmadeShop.Services.Logger.Logger;
using HandmadeShop.Services.RabbitMq.RabbitMq;

namespace HandmadeShop.Worker;

public class TaskExecutor : ITaskExecutor
{
    private readonly IAppLogger _logger;
    private readonly IRabbitMq _rabbitMq;
    private readonly IServiceProvider _serviceProvider;

    public TaskExecutor(IAppLogger logger, IRabbitMq rabbitMq, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _rabbitMq = rabbitMq;
        _serviceProvider = serviceProvider;
    }
    
    public void Start()
    {
        _rabbitMq.Subscribe<EmailModel>(RabbitMqTaskQueueNames.SendEmail, async data
            => await Execute<IEmailSender>(async service =>
            {
                _logger.Information($"RABBITMQ::: {RabbitMqTaskQueueNames.SendEmail}:");
                await service.Send(data);
            }));
    }
    
    private async Task Execute<T>(Func<T, Task> action)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();

            var service = scope.ServiceProvider.GetService<T>();
            if (service != null)
                await action(service);
            else
                _logger.Information($"Error: {action} wasn`t resolved");
        }
        catch (Exception e)
        {
            _logger.Information($"Error: {RabbitMqTaskQueueNames.SendEmail}: {e.Message}");
            throw;
        }
    }
}