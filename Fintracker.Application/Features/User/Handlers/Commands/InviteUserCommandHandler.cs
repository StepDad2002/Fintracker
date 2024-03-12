﻿using System.Text;
using Fintracker.Application.Contracts.Identity;
using Fintracker.Application.Contracts.Infrastructure;
using Fintracker.Application.Contracts.Persistence;
using Fintracker.Application.DTO.Invite.Validators;
using Fintracker.Application.Exceptions;
using Fintracker.Application.Features.User.Requests.Commands;
using Fintracker.Application.Models.Mail;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Fintracker.Application.Features.User.Handlers.Commands;

public class InviteUserCommandHandler : IRequestHandler<InviteUserCommand, Unit>
{
    private readonly IEmailSender _emailSender;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly AppSettings _appSettings;
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly string _tempPass;

    public InviteUserCommandHandler(IEmailSender emailSender, IUserRepository userRepository, IUnitOfWork unitOfWork,
        ITokenService tokenService, IOptions<AppSettings> options, UserManager<Domain.Entities.User> userManager)
    {
        _emailSender = emailSender;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _userManager = userManager;
        _appSettings = options.Value;
        _tempPass = GenerateTempPassword();
    }

    public async Task<Unit> Handle(InviteUserCommand request, CancellationToken cancellationToken)
    {
        var validator = new InviteUseValidator(_userRepository, _unitOfWork);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.IsValid)
        {
            var inviteEmailModel = new InviteEmailModel()
            {
                WhoInvited = request.WhoInvited ?? "User",
                TempPass = _tempPass
            };
            
            var isUserExists = await _userRepository.ExistsAsync(request.UserEmail);
            var user = new Domain.Entities.User();
            
            if (!isUserExists)
            {
                user = await _userRepository.RegisterUserWithTemporaryPassword(request.UserEmail, Guid.NewGuid(), _tempPass);
                var token = await _tokenService.CreateToken(user);
                inviteEmailModel.Ref =
                    $"{_appSettings.BaseUrl}/{request.UrlCallback}?token={token}&walletId={request.WalletId}";
            }
            else
            {
                var token =
                    await _tokenService.CreateToken(
                        (await _userRepository.GetAsNoTrackingAsync(request.UserEmail))!);

                inviteEmailModel.Ref = $"{_appSettings.BaseUrl}/{request.UrlCallback}?token={token}&walletId={request.WalletId}";
            }
            
            var isEmailSent = await _emailSender.SendEmail(new EmailModel()
            {
                Email = request.UserEmail,
                Subject = "Invitation to a new wallet",
                HtmlPath = isUserExists ? "inviteForm.html" : "inviteFormWithNewUser.html",
                Name = "",
                PlainMessage = ""
            }, inviteEmailModel);

            if (!isEmailSent)
            {
                await _userManager.DeleteAsync(user);
                throw new BadRequestException($"Email to {request.UserEmail} was not sent!");
            }

            return Unit.Value;
        }

        throw new BadRequestException(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
    }
    

    private string GenerateTempPassword()
    {
        string chars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
        string signs = "!@#$%^&*(){}:'~`";
        var sb = new StringBuilder();
        for (int i = 0; i < 8; i++)
        {
            sb.Append(chars[Random.Shared.Next(0, chars.Length)]);
        }

        sb.Append(chars[Random.Shared.Next(26, chars.Length)]);
        sb.Append(signs[Random.Shared.Next(0, signs.Length)]);

        for (int i = 0; i < 3; i++)
        {
            sb.Append(Random.Shared.Next(0, 9));
        }

        return sb.ToString();
    }
}