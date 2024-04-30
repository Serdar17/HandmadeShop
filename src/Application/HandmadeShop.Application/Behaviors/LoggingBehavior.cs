using MediatR;
using Microsoft.Extensions.Logging;

namespace HandmadeShop.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest
    : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        // var userId = _service.UserId;
        // _logger.LogInformation("Entity from request: {Name} {@UserId} {@Request}",
        //     requestName, userId, request);

        var response = await next();
    
        logger.LogInformation("Handled {Name} object {@Response}", typeof(TResponse).Name, typeof(TResponse));
                
        return response;
    }
}