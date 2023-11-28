namespace Forum3.DTOs;

public class PostDto
{
    public int? Id { get; set; }
    public string? Content { get; set; }
    public bool? IsSoftDeleted { get; set; }
    public DateTime? CreatedAt { get; set; }
    public LookupUserDto? Creator { get; set; }
    public LookupUserDto? EditedBy { get; set; }
    public DateTime? EditedAt { get; set; }
}