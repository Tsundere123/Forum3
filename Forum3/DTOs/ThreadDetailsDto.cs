namespace Forum3.DTOs;

public class ThreadDetailsDto
{
    public int? Id { get; set; }
    public string Title { get; set; }
    public string Creator { get; set; }
    public bool IsPinned { get; set; }
    public bool IsSoftDeleted { get; set; }
    public bool IsLocked { get; set; }
    
}