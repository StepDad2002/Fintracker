﻿using Fintracker.Domain.Common;

namespace Fintracker.Domain.Entities;

public class Transaction : IEntity<Guid>
{
    public Transaction()
    {
        Budgets = new HashSet<Budget>();
    }
    
    public Guid Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public string CreatedBy { get; set; } = default!;
    
    public DateTime ModifiedAt { get; set; }
    
    public string ModifiedBy { get; set; } = default!;

    public User User { get; set; } = default!;
    
    public Guid UserId { get; set; }

    public Guid WalletId { get; set; }

    public Wallet Wallet { get; set; } = default!;


    public ICollection<Budget> Budgets { get; set; }

    public Guid CategoryId { get; set; }

    public Category Category { get; set; } = default!;
    
    public Guid CurrencyId { get; set; }

    public Currency Currency { get; set; } = default!;

    public decimal Amount { get; set; }
    
    public decimal AmountInWalletCurrency { get; set; }

    public string? Note { get; set; }
    
    public DateTime Date { get; set; }
    public string? Label { get; set; }

    public bool IsBankTransaction { get; set; }
}