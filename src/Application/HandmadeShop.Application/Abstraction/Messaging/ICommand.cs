﻿using HandmadeShop.Domain.Common;
using MediatR;

namespace HandmadeShop.Application.Abstraction.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}