namespace Forum3.DTOs.Profile;

public class ProfilePostsDto
{
    public LookupUserDto User { get; set; } = default!;
    public List<LookupPostDto>? Posts { get; set; } = new();
}