using System.Diagnostics;
using Forum3.ClientApp.DAL;
using Forum3.Data;
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
        // var forumCategory = await _forumCategoryRepository.GetForumCategoryById(forumCategoryId);
        // if (forumCategory == null) return NotFound();
        //
        // var forumThreads = await _forumThreadRepository.GetForumThreadsByCategoryId(forumCategoryId);
        // if (forumThreads == null) return NotFound();
        //
        // if (!User.IsInRole("Administrator"))
        // {
        //     // Remove soft deleted threads
        //     forumThreads = forumThreads.Where(x => x.IsSoftDeleted == false);
        // }
        //
        // // Pinned threads
        // var threadList = forumThreads.ToList();
        // var pinnedThreads = threadList.Where(t => t.IsPinned).ToList();
        //
        // // Sort threads by last post
        // var sortedThreads = threadList
        //     .Where(t => t.IsPinned == false)
        //     .Select(t => new
        //     {
        //         ForumThread = t,
        //         LastPost = t.Posts!.Any() ? t.Posts!.Max(p => p.CreatedAt) : t.CreatedAt
        //     })
        //     .OrderByDescending(t => t.LastPost)
        //     .Select(t => t.ForumThread);
        //
        // // Prepare pagination
        // const int perPage = 10;
        // var pageList = sortedThreads.ToList();
        // var totalPages = (int)Math.Ceiling((double)pageList.Count / perPage);
        // var currentPage = page ?? 1;
        //
        // var forumThreadsOfCategory = pageList.Skip((currentPage - 1) * perPage).Take(perPage).ToList();
        //
        // return Ok(forumThreadsOfCategory);
        Console.WriteLine(forumCategoryId);
        var forumThreads = await _forumThreadRepository.GetForumThreadsByCategoryId(forumCategoryId);

        
        return Ok(forumThreads);
    }
}