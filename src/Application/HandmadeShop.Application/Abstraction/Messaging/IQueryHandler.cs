using HandmadeShop.Domain.Common;
using MediatR;

namespace HandmadeShop.Application.Abstraction.Messaging;

public interface IQueryHandler<TQuery, TResponse> 
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}