﻿using Fintracker.Application.DTO.Transaction;
using Fintracker.Application.Models;
using MediatR;

namespace Fintracker.Application.Features.Transaction.Requests.Queries;

public class GetTransactionsByCategoryIdSortedRequest : IRequest<IReadOnlyList<TransactionBaseDTO>>
{
    public Guid CategoryId { get; set; }
    public Guid UserId { get; set; }

    public TransactionQueryParams Params { get; set; } = default!;
}