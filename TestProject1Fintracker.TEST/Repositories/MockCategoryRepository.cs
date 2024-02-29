﻿using Fintracker.Application.Contracts.Persistence;
using Fintracker.Domain.Entities;
using Fintracker.Domain.Enums;
using Moq;

namespace TestProject1Fintracker.TEST.Repositories;

public class MockCategoryRepository
{
    public static Mock<ICategoryRepository> GetCategoryRepository()
    {
        var categories = new List<Category>()
        {
            new()
            {
                Id = new Guid("77326B96-DF2B-4CC8-93A3-D11A276433D6"),
                Type = CategoryType.INCOME,
                Name = "Category 1",
                Image = "Glory",
                IconColour = "pink"
            },
            new()
            {
                Id = new Guid("D670263B-92CF-48C8-923A-EB09188F6077"),
                Type = CategoryType.EXPENSE,
                Name = "Category 2",
                Image = "frog",
                IconColour = "green"
            }
        };
        var mock = new Mock<ICategoryRepository>();

        mock.Setup(x => x.ExistsAsync(It.IsAny<Guid>()))
            .Returns(async (Guid id) =>
            {
                return categories.Find(c => c.Id == id) != null;
            });
        
        mock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .Returns(async (Guid id) => { return categories.Find(x => x.Id == id); });

        mock.Setup(x => x.GetAllAsyncNoTracking())
            .ReturnsAsync(categories);
        
        mock.Setup(x => x.GetAllAsync())
            .ReturnsAsync(categories);

        mock.Setup(x => x.AddAsync(It.IsAny<Category>()))
            .Returns(async (Category c) =>
            {
                categories.Add(c);
                return c;
            });

        mock.Setup(x => x.UpdateAsync(It.IsAny<Category>()))
            .Returns(async (Category c) =>
            {
                if (categories.Find(x => x.Id == c.Id) != null)
                {
                    int index = categories.FindIndex(x => x.Id == c.Id);
                    categories[index] = c;
                }
            });
        
        mock.Setup(x => x.DeleteAsync(It.IsAny<Category>()))
            .Returns(async (Category c) =>
            {
                if (categories.Find(x => x.Id == c.Id) != null)
                {
                    int index = categories.FindIndex(x => x.Id == c.Id);
                    categories.RemoveAt(index);
                }
            });
        
        return mock;
    }
}