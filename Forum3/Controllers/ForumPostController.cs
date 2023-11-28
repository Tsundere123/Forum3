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
    public async Task<IActionResult> CreatePost(int threadId, [FromBody] CreateForumPostDto createForumForumDto)
    {
        var forumThread = await _forumThreadRepository.GetForumThreadById(threadId);
        if (forumThread == null) return NotFound();
        
        var user = await _userManager.FindByNameAsync(createForumForumDto.UserName);
        if (user == null) return NotFound();
        
        var forumPost = new ForumPost
        {
            Content = createForumForumDto.Content,
            CreatorId = user.Id,
            ThreadId = forumThread.Id,
            IsSoftDeleted = false,
            CreatedAt = DateTime.Now
        };
        var resultPost = await _forumPostRepository.CreateNewForumPost(forumPost);
        
        if (!resultPost) return BadRequest();
        
        return Ok();
    }

    [HttpPost("Edit/{postId}")]
    public async Task<IActionResult> EditPost(int postId, [FromBody] CreateForumPostDto editForumPostDto)
    {
        var forumPost = await _forumPostRepository.GetForumPostById(postId);
        if (forumPost == null) return NotFound();

        var user = await _userManager.FindByNameAsync(editForumPostDto.UserName);
        if (user == null) return NotFound();

        //Sets content
        forumPost.Content = editForumPostDto.Content;
        forumPost.EditedAt = DateTime.Now;
        forumPost.EditedBy = user.Id;
        
        var result = await _forumPostRepository.UpdateForumPost(forumPost);
        if (result) return Ok();
        return BadRequest();
    }
}