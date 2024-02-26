﻿
using Fintracker.Application.DTO.Common;

namespace Fintracker.Application.DTO.Category;

public class CreateCategoryDTO : IBaseDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public CategoryTypeEnum Type { get; set; }

    public string Image { get; set; }

    public string IconColour { get; set; }
}