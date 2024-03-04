﻿using Fintracker.Application.Contracts.Persistence;
using Fintracker.Domain.Entities;
using Moq;
using System.Linq.Dynamic.Core;

namespace Fintracker.TEST.Repositories;

public class MockWalletRepository
{
    public static Mock<IWalletRepository> GetWalletRepository()
    {
        var wallets = new List<Wallet>()
        {
            new()
            {
                Id = new Guid("BA5D310A-4CE3-41EA-AC27-C212AB5652A0"),
                Balance = 1000,
                Name = "Wallet 1",
                OwnerId = new Guid("A98A21C7-E794-4A65-B618-FA6D8A5F63D9")
            },
            new()
            {
                Id = new Guid("16E4E7F0-48EE-46E6-9543-ABA7975FBE71"),
                Balance = 2000,
                Name = "Wallet 2",
                OwnerId = new Guid("A98A21C7-E794-4A65-B618-FA6D8A5F63D9")
            },
            new()
            {
                Id = new Guid("83E1C69F-8B85-46D4-8AB7-2DBE7D66038C"),
                Balance = 3000,
                Name = "Wallet 3",
            },
            new()
            {
                Id = new Guid("95E0ECF9-0647-450B-9495-B2A709D166B5"),
                Balance = 500,
                Name = "With Owner",
                Owner = new User() { Id = new Guid("A98A21C7-E794-4A65-B618-FA6D8A5F63D9"), Email = "owner@gmail.com" },
            },
            new()
            {
                Id = new Guid("83D7946B-3CCD-401E-8EF4-62BCA04FD528"),
                Balance = 2000,
                Name = "With Members",
                Users = new List<User>()
                {
                    new()
                    {
                        Id = new Guid("2045DCCE-ED9E-4880-ABF8-1710C678BA3F"),
                        Email = "member1@gmail.com"
                    },
                    new()
                    {
                        Id = new Guid("99E84605-35FB-491A-ADC5-523516612B41"),
                        Email = "member2@gmail.com"
                    }
                }
            },
            new()
            {
                Id = new Guid("8ED1883D-1833-47CB-8E12-27AC26F5E6A7"),
                Name = "With Transactions",
                Transactions = new List<Transaction>()
                {
                    new()
                    {
                        Id = new Guid("0F47F18E-5DE9-429B-9EF9-4CF74F338EE3"),
                        Note = "transaction 1"
                    },
                    new()
                    {
                        Id = new Guid("DABB1876-F428-4C8C-B04A-52F94582DFCF"),
                        Note = "transaction 2"
                    }
                }
            },
            new()
            {
                Id = new Guid("32A22A34-F772-4F65-B806-51B2E8528D6E"),
                Name = "With Budgets",
                Budgets = new List<Budget>()
                {
                    new()
                    {
                        Id = new Guid("438A3485-E4F0-4C79-971C-DC07FB92BAD8"),
                        Name = "Budget 1",
                        Categories = null
                    },
                    new()
                    {
                        Id = new Guid("B036C34F-FD3F-484C-9CA2-7E603E5E076A"),
                        Name = "Budget 2",
                        Categories = null
                    }
                }
            }
        };

        var mock = new Mock<IWalletRepository>();

        //GenericRepository

        mock.Setup(x => x.AddAsync(It.IsAny<Wallet>()))
            .Returns(async (Wallet b) =>
            {
                wallets.Add(b);
                return b;
            });

        mock.Setup(x => x.UpdateAsync(It.IsAny<Wallet>()))
            .Returns(async (Wallet b) =>
            {
                if (wallets.Find(x => x.Id == b.Id) != null)
                {
                    int index = wallets.FindIndex(x => x.Id == b.Id);
                    wallets[index] = b;
                }
            });

        mock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .Returns(async (Guid id) => { return wallets.Find(x => x.Id == id); });

        mock.Setup(x => x.GetAsyncNoTracking(It.IsAny<Guid>()))
            .Returns(async (Guid id) => { return wallets.Find(x => x.Id == id); });

        mock.Setup(x => x.GetAllAsync())
            .ReturnsAsync(wallets);

        mock.Setup(x => x.GetAllAsyncNoTracking())
            .ReturnsAsync(wallets);

        mock.Setup(x => x.DeleteAsync(It.IsAny<Wallet>()))
            .Returns(async (Wallet b) =>
            {
                if (wallets.Find(x => x.Id == b.Id) != null)
                {
                    int index = wallets.FindIndex(x => x.Id == b.Id);
                    wallets.RemoveAt(index);
                }
            });

        mock.Setup(x => x.ExistsAsync(It.IsAny<Guid>()))
            .Returns(async (Guid id) => { return wallets.Find(x => x.Id == id) != null; });

        //WalletRepository

        mock.Setup(x => x.GetWalletWithOwnerAsync(It.IsAny<Guid>()))
            .Returns((Guid id) => Task.FromResult(wallets.Find(x => x.Id == id)));

        mock.Setup(x => x.GetWalletWithMembersAsync(It.IsAny<Guid>()))
            .Returns((Guid id) => Task.FromResult(wallets.Find(x => x.Id == id)));

        mock.Setup(x => x.GetWalletWithTransactionsAsync(It.IsAny<Guid>()))
            .Returns((Guid id) => Task.FromResult(wallets.Find(x => x.Id == id)));

        mock.Setup(x => x.GetWalletWithBudgetsAsync(It.IsAny<Guid>()))
            .Returns((Guid id) => Task.FromResult(wallets.Find(x => x.Id == id)));

        mock.Setup(x => x.GetByOwnerIdAsync(It.IsAny<Guid>()))
            .Returns((Guid id) =>
                Task.FromResult((IReadOnlyList<Wallet>)wallets.Where(x => x.OwnerId == id).ToList()));

        mock.Setup(x => x.GetByOwnerIdSortedAsync(It.IsAny<Guid>(), It.IsAny<string>()))
            .Returns((Guid id, string sortBy) =>
                Task.FromResult((IReadOnlyList<Wallet>)wallets
                    .Where(x => x.OwnerId == id)
                    .AsQueryable()
                    .OrderBy(sortBy)
                    .ToList()));

        return mock;
    }
}