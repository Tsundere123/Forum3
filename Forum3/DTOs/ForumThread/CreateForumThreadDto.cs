namespace Forum3.DTOs.ForumThread;

public class CreateForumThreadDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string UserName { get; set; } = null!;
}