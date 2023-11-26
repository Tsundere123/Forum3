namespace Forum3.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public LookupThreadDto? LatestThread { get; set; }
    public int ThreadCount { get; set; }
    public int PostCount { get; set; }
}