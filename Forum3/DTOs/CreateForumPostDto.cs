﻿namespace Forum3.DTOs;

public class CreateForumPostDto
{
    public int ThreadId { get; set; }
    public string Content { get; set; } = null!;
    public string UserName { get; set; } = null!;
}