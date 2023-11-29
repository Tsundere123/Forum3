namespace Forum3.DTOs.Lookup;

public class LookupPostDto
{
    public int Id { get; set; }
    public string Content { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public string ThreadTitle { get; set; } = default!;
    public int ThreadId { get; set; }
    public LookupUserDto Creator { get; set; } = default!;
    public bool IsSoftDeleted { get; set; }
}