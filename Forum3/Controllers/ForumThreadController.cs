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
    

    // [HttpGet("{forumCategoryId}/{page?}")]
    // public async Task<IActionResult> ForumThreadsOfCategory1(int forumCategoryId, int? page)
    // {
    //     //The category of choice
    //     var forumCategory = await _forumCategoryRepository.GetForumCategoryById(forumCategoryId);
    //     if (forumCategory == null) return NotFound();
    //     
    //     //List of all threads in the category of choice
    //     var forumThreads = await _forumThreadRepository.GetForumThreadsByCategoryId(forumCategoryId);
    //     if (forumThreads == null) return NotFound();
    //     
    //     if (!User.IsInRole("Administrator"))
    //     {
    //         // Remove soft deleted threads
    //         forumThreads = forumThreads.Where(x => x.IsSoftDeleted == false);
    //     }
    //     
    //     // Pinned threads
    //     var threadList = forumThreads.ToList();
    //     var pinnedThreads = threadList.Where(t => t.IsPinned).ToList();
    //     
    //     // Sort threads by last post
    //     var sortedThreads = threadList
    //         .Where(t => t.IsPinned == false)
    //         .Select(t => new
    //         {
    //             ForumThread = t,
    //             LastPost = t.Posts!.Any() ? t.Posts!.Max(p => p.CreatedAt) : t.CreatedAt
    //         })
    //         .OrderByDescending(t => t.LastPost)
    //         .Select(t => t.ForumThread);
    //     
    //     //Newest thread
    //     var latestThread = sortedThreads.LastOrDefault();
    //
    //     // Prepare pagination
    //     const int perPage = 10;
    //     var pageList = sortedThreads.ToList();
    //     var totalPages = (int)Math.Ceiling((double)pageList.Count / perPage);
    //     var currentPage = page ?? 1;
    //     
    //     var forumThreadsOfCategory = pageList.Skip((currentPage - 1) * perPage).Take(perPage).ToList();
    //     
    //     // Post counter
    //     
    //     return Json(new{forumCategory = forumCategory, pinnedThreads = pinnedThreads, forumThreads = forumThreadsOfCategory, currentPage = currentPage, totalPages = totalPages, latestThread = latestThread});
    // }

    [HttpGet("{forumCategoryId}/{page?}")]
    public async Task<IActionResult> ForumThreadsOfCategory(int forumCategoryId, int? page)
    {
        //All threads in category
        var threads = await _forumThreadRepository.GetForumThreadsByCategoryId(forumCategoryId);
        var threadsList = threads.ToList();
        Console.WriteLine(threadsList.ToJson());
        var result = threadsList.Select(t => new ThreadDto()
        {
            Id = t.Id,
            Title = t.Title,
            PostCount = t.Posts.Count,
            LatestPost = new LookupPostDto()
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
            },
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

    [HttpGet("CategoryDetails/{forumCategoryId}")]
    public async Task<IActionResult> GetCategoryDetails(int categoryId)
    {
        var forumCategory = await _forumCategoryRepository.GetForumCategoryById(categoryId);
        var result = new CategoryDetailsDto()
        {
            Id = forumCategory.Id,
            Name = forumCategory.Name,
            Description = forumCategory.Description
        };
        return Ok(result);
    }

    
}