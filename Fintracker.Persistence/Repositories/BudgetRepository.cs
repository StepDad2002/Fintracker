﻿using Fintracker.Application.Contracts.Persistence;
using Fintracker.Domain.Entities;
using Fintracker.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Fintracker.Persistence.Repositories;

public class BudgetRepository : GenericRepository<Budget>, IBudgetRepository
{
    private readonly AppDbContext _db;

    public BudgetRepository(AppDbContext context) : base(context)
    {
        _db = context;
    }

    public async Task<Budget?> GetBudgetWithWalletAsync(Guid id)
    {
        return await _db.Budgets
            .Include(x => x.Wallet)
            .Include(x => x.Categories)
            .Include(x => x.Currency)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Budget?> GetBudgetAsync(Guid id)
    {
        return await _db.Budgets
            .Include(x => x.Categories)
            .Include(x => x.Currency)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<Budget?> GetBudgetWithUserAsync(Guid id)
    {
        return await _db.Budgets
            .Include(x => x.User)
            .ThenInclude(x => x.UserDetails)
            .Include(x => x.Categories)
            .Include(x => x.Currency)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<Budget>> GetBudgetsByCategoryId(Guid categoryId)
    {
        return await _db.Budgets
            .Include(x => x.Categories)
            .Include(x => x.Currency)
            .Where(x => x.Categories
                .Any(x => x.Id == categoryId))
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Budget>> GetByUserIdAsync(Guid userId, bool isPublic)
    {
        return await _db.Budgets
            .Include(x => x.Categories)
            .Include(x => x.Currency)
            .Where(x => x.UserId == userId && x.IsPublic == isPublic)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Budget>> GetByWalletIdAsync(Guid walletId, bool isPublic)
    {
        return await _db.Budgets
            .Include(x => x.Categories)
            .Include(x => x.Currency)
            .Where(x => x.WalletId == walletId && x.IsPublic == isPublic)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Budget>> GetByUserIdSortedAsync(Guid userId, string sortBy, bool isDescending, bool isPublic)
    {
        return await _db.Budgets.GetByUserIdSortedAsync(userId, sortBy, isDescending, isPublic);
    }

    public async Task<IReadOnlyList<Budget>> GetByWalletIdSortedAsync(Guid walletId, string sortBy, bool isDescending, bool isPublic)
    {
        return await _db.Budgets.GetByWalletIdSortedAsync(walletId, sortBy, isDescending, isPublic);
    }
}