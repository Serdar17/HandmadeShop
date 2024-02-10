using HandmadeShop.Domain.Common;
using MediatR;

namespace HandmadeShop.Application.Abstraction.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}