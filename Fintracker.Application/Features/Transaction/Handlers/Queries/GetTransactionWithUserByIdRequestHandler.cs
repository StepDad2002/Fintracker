﻿using AutoMapper;
using Fintracker.Application.Contracts.Persistence;
using Fintracker.Application.DTO.Transaction;
using Fintracker.Application.Exceptions;
using Fintracker.Application.Features.Transaction.Requests.Queries;
using MediatR;

namespace Fintracker.Application.Features.Transaction.Handlers.Queries;

public class
    GetTransactionWithUserByIdRequestHandler : IRequestHandler<GetTransactionWithUserByIdRequest,
    TransactionWithUserDTO>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetTransactionWithUserByIdRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<TransactionWithUserDTO> Handle(GetTransactionWithUserByIdRequest request,
        CancellationToken cancellationToken)
    {
        var transaction = await _unitOfWork.TransactionRepository.GetTransactionWithUserAsync(request.Id);

        if (transaction is null)
            throw new NotFoundException(new ExceptionDetails
            {
                ErrorMessage = $"Was not found by id [{request.Id}]",
                PropertyName = nameof(request.Id)
            },nameof(Domain.Entities.Transaction));

        return _mapper.Map<TransactionWithUserDTO>(transaction);
    }
}