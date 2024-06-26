﻿using Fintracker.Application.DTO.Common;

namespace Fintracker.Application.DTO.Category;

public class CategoryDTO : IBaseDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = default!;

    public CategoryTypeEnum Type { get; set; }

    public string Image { get; set; } = default!;

    public string IconColour { get; set; } = default!;
    
    public bool IsSystemCategory { get; set; }
    
    public int TransactionCount { get; set; }

    public int BudgetCount { get; set; }
}