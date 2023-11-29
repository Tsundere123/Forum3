using Forum3.DTOs.Lookup;

namespace Forum3.DTOs.ForumPost;

public class ForumPostDto
{
    public int? Id { get; set; }
    public string? Content { get; set; }
    public bool? IsSoftDeleted { get; set; }
    public DateTime? CreatedAt { get; set; }
    public LookupUserDto? Creator { get; set; }
    public LookupUserDto? EditedBy { get; set; }
    public DateTime? EditedAt { get; set; }
}