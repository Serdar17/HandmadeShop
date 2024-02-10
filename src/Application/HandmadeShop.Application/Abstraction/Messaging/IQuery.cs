using MediatR;

namespace HandmadeShop.Application.Abstraction.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}