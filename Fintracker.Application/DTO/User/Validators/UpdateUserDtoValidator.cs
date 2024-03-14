﻿using Fintracker.Application.Contracts.Identity;
using Fintracker.Application.Features.User.Requests.Commands;
using Fintracker.Application.Helpers;
using FluentValidation;
using Microsoft.Extensions.Options;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract

namespace Fintracker.Application.DTO.User.Validators;

public class UpdateUserDtoValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserDtoValidator(IUserRepository userRepository, IOptions<AppSettings> options)
    {
        RuleFor(x => x.User.Id)
            .ApplyCommonRules()
            .OverridePropertyName(nameof(UpdateUserCommand.User.Id))
            .MustAsync(async (id, _) => await userRepository.ExistsAsync(id))
            .WithMessage(x => $"{nameof(Domain.Entities.User)} with id does not exist [{x.User.Id}]");

        RuleFor(x => x.User.Email)
            .ApplyCommonRules(x => x.User.Email is not null)
            .ApplyEmail()
            .OverridePropertyName(nameof(UpdateUserCommand.User.Email))
            .MustAsync(async (dto, email, _) =>
            {
                var existingUser = await userRepository.GetAsNoTrackingAsync(email);

                if (existingUser is null)
                    return true;
                if (existingUser.Email == dto.User.Email && existingUser.Id == dto.User.Id)
                    return true;
                return false;
            })
            .WithMessage(x => $"Invalid email [{x.User.Email}]");

        RuleFor(x => x.User.UserDetails)
            .SetValidator(new UserDetailsValidator(options))
            .OverridePropertyName(string.Empty);
    }
}