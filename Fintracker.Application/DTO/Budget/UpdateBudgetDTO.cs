﻿using Fintracker.Application.DTO.Common;

namespace Fintracker.Application.DTO.Budget;

public class UpdateBudgetDTO :  IBaseDto, IBudgetDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = default!;

    
    public decimal StartBalance { get; set; }
    
    public Guid CurrencyId { get; set; }
    
    public Guid WalletId { get; set; }
    
    public ICollection<Guid> CategoryIds { get; set; } = default!;
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public bool IsPublic { get; set; }
}