﻿namespace Fintracker.Application.DTO.Account;

public class LoginResponse
{
    public Guid UserId { get; set; }

    public string Token { get; set; }

    public string UserEmail { get; set; }
}