﻿using Fintracker.Application.DTO.Transaction;
using MediatR;

namespace Fintracker.Application.Features.Transaction.Requests.Queries;

public class GetTransactionsByUserIdRequest : IRequest<IReadOnlyList<TransactionBaseDTO>>
{
    public Guid UserId { get; set; }
}