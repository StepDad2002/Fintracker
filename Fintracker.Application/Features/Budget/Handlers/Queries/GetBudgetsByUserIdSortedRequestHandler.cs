﻿using AutoMapper;
using Fintracker.Application.Contracts.Persistence;
using Fintracker.Application.DTO.Budget;
using Fintracker.Application.Exceptions;
using Fintracker.Application.Features.Budget.Requests.Queries;
using MediatR;

namespace Fintracker.Application.Features.Budget.Handlers.Queries;

public class
    GetBudgetsByUserIdSortedRequestHandler : IRequestHandler<GetBudgetsByUserIdSortedRequest,
    IReadOnlyList<BudgetBaseDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private List<string> _allowedSortColumns;

    public GetBudgetsByUserIdSortedRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _allowedSortColumns = new List<string>
        {
            nameof(Domain.Entities.Budget.Name).ToLowerInvariant(),
            nameof(Domain.Entities.Budget.StartDate).ToLowerInvariant(),
            nameof(Domain.Entities.Budget.Id).ToLowerInvariant(),
            nameof(Domain.Entities.Budget.EndDate).ToLowerInvariant(),
            nameof(Domain.Entities.Budget.Balance).ToLowerInvariant()
        };
    }

    public async Task<IReadOnlyList<BudgetBaseDTO>> Handle(GetBudgetsByUserIdSortedRequest request,
        CancellationToken cancellationToken)
    {
        if (!_allowedSortColumns.Contains(request.Params.SortBy))
            throw new BadRequestException(new ExceptionDetails
            {
                PropertyName = nameof(request.Params.SortBy),
                ErrorMessage = $"Allowed values for sort by are {string.Join(", ", _allowedSortColumns)}"
            });
        
        var budgetsByUserId =
            await _unitOfWork.BudgetRepository.GetByUserIdSortedAsync(request.UserId, request.Params);
        

        return _mapper.Map<List<BudgetBaseDTO>>(budgetsByUserId);
    }
}