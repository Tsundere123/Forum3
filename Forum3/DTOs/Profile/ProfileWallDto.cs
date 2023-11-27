namespace Forum3.DTOs.Profile;

public class ProfileWallDto
{
    public LookupUserDto User { get; set; } = default!;
    public List<WallPostDto>? WallPosts { get; set; } = new();
}