namespace Forum3.DTOs.Lookup;

public class LookupPostDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public string ThreadTitle { get; set; }
    public int ThreadId { get; set; }
    public LookupUserDto Creator { get; set; }
    public bool IsSoftDeleted { get; set; }
}