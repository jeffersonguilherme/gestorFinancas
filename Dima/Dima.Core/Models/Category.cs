﻿namespace Dima.Core.Models;

public class Category
{
    
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}
