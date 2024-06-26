﻿using AutoMapper;
using Fintracker.Application.Contracts.Persistence;
using Fintracker.Application.DTO.Currency;
using Fintracker.Application.Exceptions;
using Fintracker.Application.Features.Currency.Requests.Queries;
using MediatR;

namespace Fintracker.Application.Features.Currency.Handlers.Queries;

public class GetCurrenciesSortedRequestHandler : IRequestHandler<GetCurrenciesSortedRequest, IReadOnlyList<CurrencyDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly List<string> _allowedSortColumns;

    public GetCurrenciesSortedRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _allowedSortColumns = new()
        {
            nameof(Domain.Entities.Currency.Name).ToLowerInvariant(),
            nameof(Domain.Entities.Currency.Symbol).ToLowerInvariant()
        };
    }

    public async Task<IReadOnlyList<CurrencyDTO>> Handle(GetCurrenciesSortedRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Params.SortBy == "Id" || !_allowedSortColumns.Contains(request.Params.SortBy))
            throw new BadRequestException(
                new ExceptionDetails
                {
                    PropertyName = nameof(request.Params.SortBy),
                    ErrorMessage = $"Allowed values for sort by are {string.Join(", ", _allowedSortColumns)}"
                });

        var currencies =
            await _unitOfWork.CurrencyRepository.GetCurrenciesSorted(request.Params);

        return _mapper.Map<List<CurrencyDTO>>(currencies);
    }
}