namespace Forum3.DTOs;

public class EditForumPostDto
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public string UserName { get; set; } = null!;
}