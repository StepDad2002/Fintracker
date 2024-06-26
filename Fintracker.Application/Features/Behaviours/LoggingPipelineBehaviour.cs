﻿using MediatR;
using Serilog;

namespace Fintracker.Application.Features.Behaviours;

public class LoggingPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        Log.Information("Handling {Request}.", requestName);
        var response = await next();
        string responseName = typeof(TResponse).Name;
        Log.Information("Handled {Response}.", responseName);
        return response;
    }
}