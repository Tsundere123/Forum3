using Forum3.DAL;
using Forum3.DTOs;
using Forum3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum3.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ForumPostController : Controller
{
    private readonly IForumThreadRepository _forumThreadRepository;
    private readonly IForumPostRepository _forumPostRepository;
    private readonly UserManager<ApplicationUser> _userManager;   
    
    public ForumPostController(IForumThreadRepository forumThreadRepository,IForumPostRepository forumPostRepository, UserManager<ApplicationUser> userManager)
    {
        _forumThreadRepository = forumThreadRepository;
        _forumPostRepository = forumPostRepository;
        _userManager = userManager;
    }
    
    private const int PageSize = 10;

    [HttpGet("{forumThreadId}/{page?}")]
    public async Task<IActionResult> ForumPostView(int forumThreadId, int? page)
    {
        var forumPosts = await _forumPostRepository.GetAllForumPostsByThreadId(forumThreadId);
        return Ok(forumPosts);
    }
    
    [HttpPost("Create/{threadId}")]
    public async Task<IActionResult> CreatePost(int threadId, [FromBody] CreateForumPostDto createForumForumThreadDto)
    {
        var forumThread = await _forumThreadRepository.GetForumThreadById(threadId);
        if (forumThread == null) return NotFound();
        
        var user = await _userManager.FindByNameAsync(createForumForumThreadDto.UserName);
        if (user == null) return NotFound();
        
        var forumPost = new ForumPost
        {
            Content = createForumForumThreadDto.Content,
            CreatorId = user.Id,
            ThreadId = forumThread.Id,
            IsSoftDeleted = false,
            CreatedAt = DateTime.Now
        };
        var resultPost = await _forumPostRepository.CreateNewForumPost(forumPost);
        
        if (!resultPost) return BadRequest();
        
        return Ok();
    }
}