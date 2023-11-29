﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum3.Models;

public class ForumPost
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("Id")]
    public int ThreadId { get; set; }
    
    [Required]
    [ForeignKey("Id")]
    public string CreatorId { get; set; } = string.Empty;
    
    [Required]
    public string Content { get; set; } = string.Empty;
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;

    public DateTime EditedAt { get; set; } = DateTime.MinValue;

    [Required] 
    public bool IsSoftDeleted { get; set; }
    
    public string EditedBy { get; set; } = string.Empty;
    
    // Navigation Property
    public virtual ForumThread Thread { get; set; } = default!;
}