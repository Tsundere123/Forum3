using Duende.IdentityServer.Extensions;
using Forum3.DAL;
using Forum3.DTOs.ForumPost;
using Forum3.DTOs.Lookup;
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
    
    public ForumPostController(
        IForumThreadRepository forumThreadRepository,
        IForumPostRepository forumPostRepository, 
        UserManager<ApplicationUser> userManager)
    {
        _forumThreadRepository = forumThreadRepository;
        _forumPostRepository = forumPostRepository;
        _userManager = userManager;
    }
    
    [HttpGet("{forumThreadId}")]
    public async Task<IActionResult> ForumPostView(int forumThreadId)
    {
        // Posts may return empty, which is fine.
        // A thread may exist without any posts.
        
        var forumPosts = await _forumPostRepository.GetAllForumPostsByThreadId(forumThreadId);
        var forumPostsList = forumPosts.ToList();
        
        var forumPostsDto = forumPostsList.Select(forumPost => new ForumPostDto
        {
            Id = forumPost.Id,
            Content = forumPost.Content,
            IsSoftDeleted = forumPost.IsSoftDeleted,
            CreatedAt = forumPost.CreatedAt,
            Creator = GetUserDto(_userManager.FindByIdAsync(forumPost.CreatorId).Result),
            EditedBy = forumPost.EditedBy != string.Empty ? GetUserDto(_userManager.FindByIdAsync(forumPost.EditedBy).Result) : null,
            EditedAt = forumPost.EditedAt != DateTime.MinValue ? forumPost.EditedAt : null
        }).ToList();
        
        return Ok(forumPostsDto);
    }
    
    [HttpPost("Create/{threadId}")]
    public async Task<IActionResult> CreatePost(int threadId, [FromBody] CreateForumPostDto createForumForumDto)
    {
        var forumThread = await _forumThreadRepository.GetForumThreadById(threadId);
        if (forumThread == null) return NotFound();
        
        if (createForumForumDto.Content.IsNullOrEmpty()) return BadRequest();
        if (createForumForumDto.UserName.IsNullOrEmpty()) return BadRequest();
        
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

    [HttpDelete("PermaDelete/{postId}")]
    public async Task<IActionResult> PermaDeleteSelectedForumPost(int postId)
    {
        var forumPost = await _forumPostRepository.GetForumPostById(postId);
        if (forumPost != null)
        {
            var result = await _forumPostRepository.DeleteForumPost(forumPost.Id);
            if (result) return Ok();
        }
        return BadRequest();
    }

    [HttpDelete("SoftDelete/{postId}")]
    public async Task<IActionResult> SoftDeleteSelectedForumPost(int postId)
    {
        var forumPost = await _forumPostRepository.GetForumPostById(postId);
        if (forumPost == null) return BadRequest();

        forumPost.IsSoftDeleted = true;
        await _forumPostRepository.UpdateForumPost(forumPost);
        return Ok();
    }
    
    
    [HttpDelete("UnSoftDelete/{postId}")]
    public async Task<IActionResult> UnSoftDeleteSelectedForumPost(int postId)
    {
        var forumPost = await _forumPostRepository.GetForumPostById(postId);
        if (forumPost == null) return BadRequest();

        forumPost.IsSoftDeleted = false;
        await _forumPostRepository.UpdateForumPost(forumPost);
        return Ok();
    }
    
    private LookupUserDto GetUserDto(ApplicationUser user)
    {
        return new LookupUserDto
        {
            UserName = user.UserName,
            Avatar = user.Avatar,
            CreatedAt = user.CreatedAt
        };
    }
}