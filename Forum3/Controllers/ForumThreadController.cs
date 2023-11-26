using System.Diagnostics;
using Duende.IdentityServer.Events;
using Forum3.DAL;
using Forum3.Data;
using Forum3.DTOs;
using Forum3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

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
        //All threads in category
        var threads = await _forumThreadRepository.GetForumThreadsByCategoryId(forumCategoryId);
        if (threads == null) return NotFound();
        
        var threadsList = threads.ToList();
        var result = threadsList.Select(t => new ThreadDto()
        {
            Id = t.Id,
            Title = t.Title,
            PostCount = t.Posts.Count,
            IsPinned = t.IsPinned,
            LatestPost = t.Posts.Any() ? new LookupPostDto()
            {
                Id = t.Posts.LastOrDefault().Id,
                Content = t.Posts.LastOrDefault().Content,
                CreatedAt = t.Posts.LastOrDefault().CreatedAt,
                ThreadTitle = t.Title,
                ThreadId = t.Id,
                Creator = new LookupUserDto()
                {
                    // UserName = _userManager.Users.FirstOrDefault(u => u.Id == s.Posts.FirstOrDefault()!.CreatorId)?.UserName,
                    // Avatar = _userManager.Users.FirstOrDefault(u => u.Id == s.Posts.FirstOrDefault()!.CreatorId)?.Avatar,
                    // CreatedAt = _userManager.Users.FirstOrDefault(u => u.Id == s.Posts.FirstOrDefault()!.CreatorId)?.CreatedAt
                    
                    UserName = "test",
                    Avatar = "default.png",
                    CreatedAt = DateTime.Now
                }
            } : null,
            Creator = new LookupUserDto()
            {
                // UserName = _userManager.Users.FirstOrDefault(u => u.Id == s.CreatorId)?.UserName,
                // Avatar = _userManager.Users.FirstOrDefault(u => u.Id == s.CreatorId)?.Avatar,
                // CreatedAt = _userManager.Users.FirstOrDefault(u => u.Id == s.CreatorId)?.CreatedAt
                
                UserName = "test",
                Avatar = "default.png",
                CreatedAt = DateTime.Now
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

    
}