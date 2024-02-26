﻿using Fintracker.Domain.Common;

namespace Fintracker.Domain.Entities;

public class Currency : IEntity<Guid>
{
    public Guid Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public string CreatedBy { get; set; }
    
    public DateTime ModifiedAt { get; set; }
    
    public string ModifiedBy { get; set; }

    public string Name { get; set; }
    
    public string Symbol { get; set; }
}