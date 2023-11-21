﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum3.Models;

public class ForumThread
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [ForeignKey("ForumCategoryId")]
    public int CategoryId { get; set; }

    public string CreatorId { get; set; } = string.Empty;
    
    [Required]
    [DefaultValue(typeof(DateTime), "DateTime.UtcNow")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;
    
    public DateTime EditedAt { get; set; } = DateTime.MinValue;

    public string EditedBy { get; set; } = string.Empty;

    [Required] 
    public bool IsSoftDeleted { get; set; }
    
    public bool IsPinned { get; set; }
    
    public bool IsLocked { get; set; }
    
    // //Navigation Property
    public virtual ForumCategory? Category { get; set; }
    
    //Navigation Property
    public virtual List<ForumPost>? Posts { get; set; }
}