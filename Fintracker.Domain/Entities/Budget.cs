﻿using Fintracker.Domain.Common;

namespace Fintracker.Domain.Entities;

public class Budget : FinancialEntity
{
    public Budget()
    {
        Categories = new HashSet<Category>();
        Transactions = new HashSet<Transaction>();
    }

    public ICollection<Category> Categories { get; set; }
    
    public ICollection<Transaction> Transactions { get; set; }
    public User Owner { get; set; } = default!;

    public Guid OwnerId { get; set; }

    public ICollection<User> Members { get; set; }

    public Wallet Wallet { get; set; } = default!;

    public Guid WalletId { get; set; }
    
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsPublic { get; set; }
    
}