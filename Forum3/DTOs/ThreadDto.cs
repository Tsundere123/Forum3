﻿using Forum3.Models;

namespace Forum3.DTOs;

public class ThreadDto
{
    public int? Id { get; set; }
    
    public string? Title { get; set; }
    
    public LookupPostDto? LatestPost { get; set; }
    
    public LookupUserDto? Creator { get; set; }
    
    public int? PostCount { get; set; }
    
    public bool IsPinned { get; set; }
    
    public bool IsLocked { get; set; }
    
    public bool IsSoftDeleted { get; set; }
    
    public DateTime CreatedAt { get; set; }
    

}