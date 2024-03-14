﻿using MediatR;

namespace Fintracker.Application.Features.User.Requests.Commands;

public class SentResetPasswordCommand : IRequest<Unit>
{
    public string Email { get; set; } = default!;

    public string UrlCallback { get; set; } = default!;
}