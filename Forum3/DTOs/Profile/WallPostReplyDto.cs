namespace Forum3.DTOs.Profile;

public class WallPostReplyDto
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public LookupUserDto Author { get; set; } = default!;
}