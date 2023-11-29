using Forum3.DAL;
using Forum3.DTOs;
using Forum3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ForumThreadController : Controller
{
    private readonly IForumCategoryRepository _forumCategoryRepository;
    private readonly IForumThreadRepository _forumThreadRepository;
    private readonly IForumPostRepository _forumPostRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public ForumThreadController(IForumCategoryRepository forumCategoryRepository, IForumThreadRepository forumThreadRepository,
        IForumPostRepository forumPostRepository, UserManager<ApplicationUser> userManager)
    {
        _forumCategoryRepository = forumCategoryRepository;
        _forumThreadRepository = forumThreadRepository;
        _userManager = userManager;
        _forumPostRepository = forumPostRepository;
    }

    [HttpGet("{forumCategoryId}/{page?}")]
    public async Task<IActionResult> ForumThreadsOfCategory(int forumCategoryId, int? page)
    {
        var threads = await _forumThreadRepository.GetForumThreadsByCategoryId(forumCategoryId);
        if (threads == null) return NotFound();
        
        var threadsList = threads.ToList();
        
        // Sort threads by last post (or created at if no posts)
        var sortedThreads = threadsList.Select(t => new
            {
                ForumThread = t,
                LastPost = t.Posts!.Any() ? t.Posts!.Max(p => p.CreatedAt) : t.CreatedAt
            })
            .OrderByDescending(t => t.LastPost)
            .Select(t => t.ForumThread);
        
        var result = sortedThreads.Select(t => new ThreadDto()
        {
            Id = t.Id,
            Title = t.Title,
            PostCount = t.Posts.Count,
            IsPinned = t.IsPinned,
            IsLocked = t.IsLocked,
            IsSoftDeleted = t.IsSoftDeleted,
            CreatedAt = t.CreatedAt,
            LatestPost = t.Posts.Any() ? new LookupPostDto()
            {
                Id = t.Posts.LastOrDefault()!.Id,
                Content = t.Posts.LastOrDefault()!.Content,
                CreatedAt = t.Posts.LastOrDefault()!.CreatedAt,
                ThreadTitle = t.Title,
                ThreadId = t.Id,
                Creator = new LookupUserDto()
                {
                    UserName = _userManager.Users.FirstOrDefault(u => u.Id == t.Posts.LastOrDefault()!.CreatorId)?.UserName,
                    Avatar = _userManager.Users.FirstOrDefault(u => u.Id == t.Posts.LastOrDefault()!.CreatorId)?.Avatar,
                    CreatedAt = _userManager.Users.FirstOrDefault(u => u.Id == t.Posts.LastOrDefault()!.CreatorId)?.CreatedAt
                },
                IsSoftDeleted = t.Posts.LastOrDefault()!.IsSoftDeleted
            } : null,
            Creator = new LookupUserDto()
            {
                UserName = _userManager.Users.FirstOrDefault(u => u.Id == t.CreatorId)?.UserName,
                Avatar = _userManager.Users.FirstOrDefault(u => u.Id == t.CreatorId)?.Avatar,
                CreatedAt = _userManager.Users.FirstOrDefault(u => u.Id == t.CreatorId)?.CreatedAt
            }
            
        }).ToList();
        return Ok(result);
    }

    [HttpGet("CategoryDetails/{categoryId}")]
    public async Task<IActionResult> GetCategoryDetails(int categoryId)
    {
        var forumCategory = await _forumCategoryRepository.GetForumCategoryById(categoryId);
        
        if (forumCategory == null) return NotFound();
        
        var result = new CategoryDetailsDto()
        {
            Id = forumCategory.Id,
            Name = forumCategory.Name,
            Description = forumCategory.Description
        };
        return Ok(result);
    }

    [HttpGet("ThreadDetails/{threadId}")]
    public async Task<IActionResult> GetThreadDetails(int threadId)
    {
        var forumThread = await _forumThreadRepository.GetForumThreadById(threadId);
        if (forumThread == null) return NotFound();

        var result = new ThreadDetailsDto()
        {
            Id = forumThread.Id,
            Title = forumThread.Title,
            Creator = _userManager.FindByIdAsync(forumThread.CreatorId).Result.UserName,
            IsPinned = forumThread.IsPinned,
            IsLocked = forumThread.IsLocked,
            IsSoftDeleted = forumThread.IsSoftDeleted,
            CreatedAt = forumThread.CreatedAt
        };
        return Ok(result);
    }

    [HttpPost("Create/{categoryId}")]
    public async Task<IActionResult> CreateThread(int categoryId, [FromBody] CreateForumThreadDto createForumThreadDto)
    {
        var forumCategory = await _forumCategoryRepository.GetForumCategoryById(categoryId);
        if (forumCategory == null) return NotFound();
        
        var user = await _userManager.FindByNameAsync(createForumThreadDto.UserName);
        if (user == null) return NotFound();
        
        var forumThread = new ForumThread()
        {
            Title = createForumThreadDto.Title,
            CreatorId = user.Id,
            CategoryId = categoryId,
            IsPinned = false,
            IsLocked = false,
            IsSoftDeleted = false,
            CreatedAt = DateTime.Now
        };
        var resultThread = await _forumThreadRepository.CreateNewForumThread(forumThread);
        
        var forumPost = new ForumPost()
        {
            Content = createForumThreadDto.Content,
            CreatorId = user.Id,
            ThreadId = forumThread.Id,
            IsSoftDeleted = false,
            CreatedAt = DateTime.Now
        };
        var resultPost = await _forumPostRepository.CreateNewForumPost(forumPost);
        
        if (!resultThread || !resultPost) return BadRequest();
        
        return Ok();
    }

    [HttpDelete("PermaDelete/{threadId}")]
    public async Task<IActionResult> PermaDeleteSelectedForumThread(int threadId)
    {
        
        var forumThread = await _forumThreadRepository.GetForumThreadById(threadId);
        if (forumThread != null)
        {
            var result = await _forumThreadRepository.DeleteForumThread(threadId);
            if (result) return Ok();
        }
        return BadRequest();
    }

    [HttpPost("EditThread/{threadId}")]
    public async Task<IActionResult> EditThread(int threadId, [FromBody] EditThreadTitleDto editForumThreadDto)
    {
        var forumThread = await _forumThreadRepository.GetForumThreadById(threadId);
        if (forumThread == null) return NotFound();
        
        var user = await _userManager.FindByNameAsync(editForumThreadDto.UserName);
        if (user == null) return NotFound();

        forumThread.Title = editForumThreadDto.Title;
        forumThread.EditedAt = DateTime.Now;
        forumThread.EditedBy = user.Id;

        var result = await _forumThreadRepository.UpdateForumThread(forumThread);
        if (result) return Ok();
        return BadRequest();
    }
    
    [HttpDelete("SoftDelete/{threadId}")]
    public async Task<IActionResult> SoftDeleteSelectedForumThread(int threadId)
    {
        var forumThread = await _forumThreadRepository.GetForumThreadById(threadId);
        if (forumThread == null) return BadRequest();
        
        
        forumThread.IsSoftDeleted = true;
        await _forumThreadRepository.UpdateForumThread(forumThread);
        
        //Mark all posts as soft deleted
        var forumPosts = _forumPostRepository.GetAllForumPostsByThreadId(threadId).Result;
        foreach (var forumPost in forumPosts)
        {
            forumPost.IsSoftDeleted = true;
            
            // Not checking if soft deleting post is successful. Just continue to next post.
            await _forumPostRepository.UpdateForumPost(forumPost);
        }
        
        return Ok();
    }
    
    
    [HttpDelete("UnSoftDelete/{threadId}")]
    public async Task<IActionResult> UnSoftDeleteSelectedForumThread(int threadId)
    {
        var forumThread = await _forumThreadRepository.GetForumThreadById(threadId);
        if (forumThread == null) return BadRequest();
        
        
        forumThread.IsSoftDeleted = false;
        await _forumThreadRepository.UpdateForumThread(forumThread);
        
        //Mark all posts as soft deleted
        var forumPosts = _forumPostRepository.GetAllForumPostsByThreadId(threadId).Result;
        foreach (var forumPost in forumPosts)
        {
            forumPost.IsSoftDeleted = false;
            
            // Not checking if soft deleting post is successful. Just continue to next post.
            await _forumPostRepository.UpdateForumPost(forumPost);
        }
        return Ok();
    }

    [HttpGet("PinThread/{threadId}")]
    public async Task<IActionResult> PinSelectedForumThread(int threadId)
    {
        var forumThread = await _forumThreadRepository.GetForumThreadById(threadId);
        if (forumThread == null) return NotFound();

        forumThread.IsPinned = true;
        var result = await _forumThreadRepository.UpdateForumThread(forumThread);
        if (result) return Ok();
        return BadRequest();
    }
    
    [HttpGet("UnPinThread/{threadId}")]
    public async Task<IActionResult> UnPinSelectedForumThread(int threadId)
    {
        var forumThread = await _forumThreadRepository.GetForumThreadById(threadId);
        if (forumThread == null) return NotFound();

        forumThread.IsPinned = false;
        var result = await _forumThreadRepository.UpdateForumThread(forumThread);
        if (result) return Ok();
        return BadRequest();
    }
    
    
    [HttpGet("LockThread/{threadId}")]
    public async Task<IActionResult> LockSelectedForumThread(int threadId)
    {
        var forumThread = await _forumThreadRepository.GetForumThreadById(threadId);
        if (forumThread == null) return NotFound();

        forumThread.IsLocked = true;
        var result = await _forumThreadRepository.UpdateForumThread(forumThread);
        if (result) return Ok();
        return BadRequest();
    }
    
    [HttpGet("UnLockThread/{threadId}")]
    public async Task<IActionResult> UnLockSelectedForumThread(int threadId)
    {
        var forumThread = await _forumThreadRepository.GetForumThreadById(threadId);
        if (forumThread == null) return NotFound();

        forumThread.IsLocked = false;
        var result = await _forumThreadRepository.UpdateForumThread(forumThread);
        if (result) return Ok();
        return BadRequest();
    }
}