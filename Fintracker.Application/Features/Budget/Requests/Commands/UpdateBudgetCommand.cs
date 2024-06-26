﻿using Fintracker.Application.Contracts.Helpers;
using Fintracker.Application.DTO.Budget;
using Fintracker.Application.Responses.Commands_Responses;
using MediatR;

namespace Fintracker.Application.Features.Budget.Requests.Commands;

public class UpdateBudgetCommand : IRequest<UpdateCommandResponse<BudgetBaseDTO>>, INotGetRequest
{
    public UpdateBudgetDTO Budget { get; set; } = default!;
}