﻿using AutoMapper;
using Fintracker.Application.Contracts.Identity;
using Fintracker.Application.DTO.User;
using Fintracker.Application.Exceptions;
using Fintracker.Application.Features.User.Requests.Commands;
using Fintracker.Application.Responses.Commands_Responses;
using MediatR;

namespace Fintracker.Application.Features.User.Handlers.Commands;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteCommandResponse<UserBaseDTO>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<DeleteCommandResponse<UserBaseDTO>> Handle(DeleteUserCommand request,
        CancellationToken cancellationToken)
    {
        var response = new DeleteCommandResponse<UserBaseDTO>();

        var user = await _userRepository.GetAsync(request.Id);

        if (user is null)
            throw new NotFoundException(new ExceptionDetails
            {
                ErrorMessage = $"Was not found by id [{request.Id}]",
                PropertyName = nameof(request.Id)
            },nameof(Domain.Entities.User));

        var deletedObj = _mapper.Map<UserBaseDTO>(user);
        await _userRepository.DeleteAsync(user);

        response.Success = true;
        response.Message = "Deleted successfully";
        response.DeletedObj = deletedObj;
        response.Id = deletedObj.Id;

        return response;
    }
}