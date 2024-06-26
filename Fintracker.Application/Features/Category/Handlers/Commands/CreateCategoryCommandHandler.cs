﻿using AutoMapper;
using Fintracker.Application.Contracts.Persistence;
using Fintracker.Application.DTO.Category;
using Fintracker.Application.Features.Category.Requests.Commands;
using Fintracker.Application.Responses.Commands_Responses;
using MediatR;

namespace Fintracker.Application.Features.Category.Handlers.Commands;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCommandResponse<CategoryDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateCommandResponse<CategoryDTO>> Handle(CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var response = new CreateCommandResponse<CategoryDTO>();

        var categoryEntity = _mapper.Map<Domain.Entities.Category>(request.Category);
        categoryEntity.UserId = request.UserId;
        await _unitOfWork.CategoryRepository.AddAsync(categoryEntity);
        var categoryDto = _mapper.Map<CategoryDTO>(categoryEntity);

        response.Success = true;
        response.Message = "Created successfully";
        response.Id = categoryEntity.Id;
        response.CreatedObject = categoryDto;

        await _unitOfWork.SaveAsync();


        return response;
    }
}