using Forum3.DAL;
using Forum3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IForumThreadRepository _forumThreadRepository;
    private readonly IForumPostRepository _forumPostRepository;

    public HomeController(UserManager<ApplicationUser> userManager, IForumThreadRepository forumThreadRepository, IForumPostRepository forumPostRepository)
    {
        _userManager = userManager;
        _forumThreadRepository = forumThreadRepository;
        _forumPostRepository = forumPostRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var threads = await _forumThreadRepository.GetAll();
        var threadList = threads.ToList();
        var latestThreads = threadList
            .Where(t => t.IsSoftDeleted == false)
            .OrderByDescending(t => t.CreatedAt)
            .Take(6)
            .Select(t => new
            {
                t.Id,
                t.Title,
                t.CreatedAt,
                Category = t.Category.Name,
                CreatorName = _userManager.Users.FirstOrDefault(u => u.Id == t.CreatorId)?.UserName,
                CreatorAvatar = _userManager.Users.FirstOrDefault(u => u.Id == t.CreatorId)?.Avatar
            })
            .ToList();
        
        var posts = await _forumPostRepository.GetAll();
        var postList = posts.ToList();
        var latestPosts = postList
            .Where(p => p.IsSoftDeleted == false)
            .OrderByDescending(p => p.CreatedAt)
            .Take(6)
            .Select(p => new
            {
                p.Id,
                p.Content,
                p.CreatedAt,
                ThreadTitle = p.Thread.Title,
                ThreadId = p.Thread.Id,
                CreatorName = _userManager.Users.FirstOrDefault(u => u.Id == p.CreatorId)?.UserName,
                CreatorAvatar = _userManager.Users.FirstOrDefault(u => u.Id == p.CreatorId)?.Avatar
            })
            .ToList();
        
        // Get username and avatar of latest 6 members
        var members = _userManager.Users
            .OrderByDescending(u => u.CreatedAt)
            .Take(6)
            .Select(u => new
            {
                u.UserName,
                u.Avatar,
                u.CreatedAt
            })
            .ToList();
        //return Ok(new { threads = latestThreads, posts = latestPosts, members });
        
        //return json
        return Json(new { threads = latestThreads, posts = latestPosts, members });
    }
}