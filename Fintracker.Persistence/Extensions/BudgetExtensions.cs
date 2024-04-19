﻿using System.Linq.Expressions;
using Fintracker.Application.Models;
using Fintracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fintracker.Persistence.Extensions;

public static class BudgetExtensions
{
    public static async Task<IReadOnlyList<Budget>> GetByUserIdSortedAsync(
        this DbSet<Budget> budgets,
        Guid userId,
        QueryParams queryParams,
        bool? isPublic)
    {
        // Create a parameter expression for the entity type
        var parameter = Expression.Parameter(typeof(Budget), "x");

        // Create a property access expression for the specified sort column
        var property = Expression.Property(parameter, queryParams.SortBy);

        // Create a lambda expression for the OrderBy method
        var converted = Expression.Convert(property, typeof(object));
        var lambda = Expression.Lambda<Func<Budget, object>>(converted, parameter);

        // Apply the sorting to the query
        var query = budgets
            .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .Include(x => x.Categories)
            .Include(x => x.Currency)
            .Include(x => x.Wallet)
            .ThenInclude(x => x.Owner)
            .Include(x => x.User)
            .ThenInclude(x => x.UserDetails)
            .AsSplitQuery()
            .Where(x => x.UserId == userId);

        if (isPublic.HasValue)
        {
            query = query.Where(x => x.IsPublic == isPublic.Value);
        }

        query = queryParams.IsDescending
            ? query.OrderByDescending(lambda)
            : query.OrderBy(lambda);

        return await query.ToListAsync();
    }

    public static async Task<IReadOnlyList<Budget>> GetByWalletIdSortedAsync(
        this DbSet<Budget> budgets,
        Guid walletId,
        QueryParams queryParams,
        bool? isPublic)
    {
        // Create a parameter expression for the entity type
        var parameter = Expression.Parameter(typeof(Budget), "x");

        // Create a property access expression for the specified sort column
        var property = Expression.Property(parameter, queryParams.SortBy);

        // Create a lambda expression for the OrderBy method
        var converted = Expression.Convert(property, typeof(object));
        var lambda = Expression.Lambda<Func<Budget, object>>(converted, parameter);

        // Apply the sorting to the query
        var query = budgets
            .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .Include(x => x.Categories)
            .Include(x => x.Currency)
            .Include(x => x.Wallet)
            .ThenInclude(x => x.Owner)
            .Include(x => x.User)
            .ThenInclude(x => x.UserDetails)
            .AsSplitQuery()
            .Where(x => x.WalletId == walletId);

        if (isPublic.HasValue)
        {
            query = query.Where(x => x.IsPublic == isPublic.Value);
        }

        query = queryParams.IsDescending
            ? query.OrderByDescending(lambda)
            : query.OrderBy(lambda);

        return await query.ToListAsync();
    }
}