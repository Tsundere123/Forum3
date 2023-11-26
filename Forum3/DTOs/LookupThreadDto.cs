namespace Forum3.DTOs;

public class LookupThreadDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Category { get; set; }
    public LookupUserDto Creator { get; set; }
}