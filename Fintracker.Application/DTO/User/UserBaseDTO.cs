﻿using Fintracker.Application.DTO.Common;

namespace Fintracker.Application.DTO.User;

public class UserBaseDTO : IBaseDto
{
    public Guid Id { get; set; }
    
    public string Email { get; set; }

    public UserDetailsDTO UserDetails { get; set; }
}