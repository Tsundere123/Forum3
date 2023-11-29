using Forum3.DAL;
using Forum3.DTOs;
using Forum3.DTOs.Lookup;
using Forum3.Models;
using Forum3.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IForumThreadRepository _forumThreadRepository;
    private readonly IForumPostRepository _forumPostRepository;

    public HomeController(
        UserManager<ApplicationUser> userManager, 
        IForumThreadRepository forumThreadRepository, 
        IForumPostRepository forumPostRepository)
    {
        _userManager = userManager;
        _forumThreadRepository = forumThreadRepository;
        _forumPostRepository = forumPostRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var threads = await _forumThreadRepository.GetAll() ?? new List<ForumThread>();

        var threadList = threads.ToList();
        var latestThreads = threadList
            .Where(t => t.IsSoftDeleted == false)
            .OrderByDescending(t => t.CreatedAt)
            .Take(6)
            .Select(t => new LookupThreadDto()
            {
                Id = t.Id,
                Title = t.Title,
                CreatedAt = t.CreatedAt,
                Category = t.Category!.Name,
                Creator = DtoUtilities.GetUserDto(_userManager.Users.FirstOrDefault(u => u.Id == t.CreatorId)!)
            })
            .ToList();
        
        var posts = await _forumPostRepository.GetAll() ?? new List<ForumPost>();
        var postList = posts.ToList();
        var latestPosts = postList
            .Where(p => p.IsSoftDeleted == false)
            .OrderByDescending(p => p.CreatedAt)
            .Take(6)
            .Select(p => new LookupPostDto()
            {
                Id = p.Id,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                ThreadTitle = p.Thread.Title,
                ThreadId = p.Thread.Id,
                Creator = DtoUtilities.GetUserDto(_userManager.Users.FirstOrDefault(u => u.Id == p.CreatorId)!)
            })
            .ToList();
        
        var members = _userManager.Users
            .OrderByDescending(u => u.CreatedAt)
            .Take(6)
            .Select(u => DtoUtilities.GetUserDto(u))
            .ToList();

        var dto = new HomeDto
        {
            Threads = latestThreads,
            Posts = latestPosts,
            Members = members
        };

        return Ok(dto);
    }
}