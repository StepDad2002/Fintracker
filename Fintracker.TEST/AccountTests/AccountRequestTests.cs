﻿using Fintracker.Application.Models.Identity;
using Fintracker.TEST.Repositories;
using FluentAssertions;

namespace Fintracker.TEST.AccountTests;

public class AccountRequestTests
{
    [Fact]
    public async Task Test_Register_With_Valid_Params_Should_Not_Throw_Exception()
    {
        var mockAccount = MockAccountService.GetAccountService().Object;
        var registerRequest = new RegisterRequest
        {
            UserName = "username",
            Email = "test@mail.com",
            Password = "Password123!",
            ConfirmPassword = "Password123!"
        };
        var expectedUserId = new Guid("A6F29D61-2014-43D1-9EAE-0042781DD703");
        var regResponse = await mockAccount.Register(registerRequest);

        regResponse.UserId.Should().Be(expectedUserId);

    }
    
    
    [Fact]
    public async Task Test_Login_With_Valid_Credentials_Should_not_throw_Exception()
    {
        var mockAccount = MockAccountService.GetAccountService().Object;
        var loginRequest = new LoginRequest
        {
            Email = "test@mail.com",
            Password = "Password123!",
        };
        var expectedLoginResponse = new LoginResponse
        {
            UserId = new Guid("A6F29D61-2014-43D1-9EAE-0042781DD703"),
            UserEmail = "test@email.com",
            Token = "Some token here as well"
        };
        var actualLoginResponse = await mockAccount.Login(loginRequest);

        actualLoginResponse.UserId.Should().Be(expectedLoginResponse.UserId);
        actualLoginResponse.UserEmail.Should().Be(expectedLoginResponse.UserEmail);
        actualLoginResponse.Token.Should().Be(expectedLoginResponse.Token);

    }
}