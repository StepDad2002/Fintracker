﻿using Fintracker.Application.DTO.Common;

namespace Fintracker.Application.DTO.Category;

public class UpdateCategoryDTO : IBaseDto, ICategoryDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = default!;
    
    public string Image { get; set; } = default!;

    public string IconColour { get; set; } = default!;
}