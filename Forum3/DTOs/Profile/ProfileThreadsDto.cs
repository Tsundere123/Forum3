namespace Forum3.DTOs.Profile;

public class ProfileThreadsDto
{
    public LookupUserDto User { get; set; } = default!;
    public List<LookupThreadDto>? Threads { get; set; } = new();
}