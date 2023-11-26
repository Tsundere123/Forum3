namespace Forum3.DTOs;

public class LookupPostDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public string ThreadTitle { get; set; }
    public int ThreadId { get; set; }
    public LookupUserDto Creator { get; set; }
}