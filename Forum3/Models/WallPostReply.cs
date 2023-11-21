﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum3.Models;

public class WallPostReply
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string AuthorId { get; set; } = string.Empty;
    
    [Required]
    public string Content { get; set; } = string.Empty;
    
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [Required]
    [ForeignKey("Id")]
    public int WallPostId { get; set; }
    
    //Navigational property
    public virtual WallPost WallPost { get; set; } = default!;
}