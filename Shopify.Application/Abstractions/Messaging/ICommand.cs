﻿using MediatR;
using Shopify.Domain.Abstraction;

namespace Shopify.Application.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result>, IBaseCommand
    {
    }

    public interface ICommand<TReponse> : IRequest<Result<TReponse>>, IBaseCommand
    {
    }

    public interface IBaseCommand
    {
    }
}
