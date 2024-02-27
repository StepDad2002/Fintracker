﻿using Fintracker.Application.DTO.Common;

namespace Fintracker.Application.DTO.Wallet;

public class UpdateWalletDTO : IBaseDto, IWalletDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public decimal Balance { get; set; }

    public Guid CurrencyId { get; set; }
    
}