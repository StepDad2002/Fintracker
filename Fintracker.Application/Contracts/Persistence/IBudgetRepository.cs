﻿using Fintracker.Domain.Entities;

namespace Fintracker.Application.Contracts.Persistence;

public interface IBudgetRepository : IGenericRepository<Budget>
{
    Task<Budget?> GetBudgetWithWalletAsync(Guid id);
    Task<Budget?> GetBudgetAsync(Guid id);
    Task<Budget?> GetBudgetWithUserAsync(Guid id);

    Task<IReadOnlyList<Budget>> GetBudgetsByCategoryId(Guid categoryId);
    Task<IReadOnlyList<Budget>> GetByUserIdAsync(Guid userId);
    Task<IReadOnlyList<Budget>> GetByWalletIdAsync(Guid walletId);

    Task<IReadOnlyList<Budget>> GetByUserIdSortedAsync(Guid userId, string sortBy, bool isDescending);
    Task<IReadOnlyList<Budget>> GetByWalletIdSortedAsync(Guid walletId, string sortBy, bool isDescending);
}