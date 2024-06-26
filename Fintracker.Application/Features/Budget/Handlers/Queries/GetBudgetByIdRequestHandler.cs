﻿using AutoMapper;
using Fintracker.Application.Contracts.Persistence;
using Fintracker.Application.DTO.Budget;
using Fintracker.Application.Exceptions;
using Fintracker.Application.Features.Budget.Requests.Queries;
using MediatR;

namespace Fintracker.Application.Features.Budget.Handlers.Queries;

public class GetBudgetByIdRequestHandler : IRequestHandler<GetBudgetByIdRequest, BudgetBaseDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBudgetByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BudgetBaseDTO> Handle(GetBudgetByIdRequest request, CancellationToken cancellationToken)
    {
        var budget = await _unitOfWork.BudgetRepository.GetBudgetByIdAsync(request.Id);

        if (budget is null)
            throw new NotFoundException(new ExceptionDetails
            {
                ErrorMessage = $"Was not found by id [{request.Id}]",
                PropertyName = nameof(request.Id)
            }, nameof(Domain.Entities.Budget));

        return _mapper.Map<BudgetBaseDTO>(budget);
    }
}
