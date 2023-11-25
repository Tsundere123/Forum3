using Forum3.DAL;
using Forum3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IForumThreadRepository _forumThreadRepository;
    private readonly IForumPostRepository _forumPostRepository;

    public SearchController(UserManager<ApplicationUser> userManager, IForumThreadRepository forumThreadRepository, IForumPostRepository forumPostRepository)
    {
        _userManager = userManager;
        _forumThreadRepository = forumThreadRepository;
        _forumPostRepository = forumPostRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? query)
    {
        if (query == null)
        {
            return BadRequest();
        }
        
        var threads = await _forumThreadRepository.GetAll();
        var threadList = threads.ToList();
        var threadResults = threadList
            .Where(t => t.IsSoftDeleted == false)
            .Where(t => t.Title.ToUpper().Contains(query.ToUpper()))
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
        var postResults = postList
            .Where(p => p.IsSoftDeleted == false)
            .Where(p => p.Content.ToUpper().Contains(query.ToUpper()))
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
        
        var membersResults = await _userManager.Users
            .Where(u => u.UserName.ToUpper().Contains(query.ToUpper()))
            .OrderByDescending(u => u.CreatedAt)
            .Take(6)
            .Select(u => new
            {
                u.UserName,
                u.Avatar,
                u.CreatedAt
            })
            .ToListAsync();

        return Ok(new
        {
            threads = threadResults,
            posts = postResults,
            members = membersResults
        });
    }
    
    [HttpGet("Threads")]
    public async Task<IActionResult> SearchThreads(string? query)
    {
        if (query == null)
        {
            return BadRequest();
        }
        
        var threads = await _forumThreadRepository.GetAll();
        var threadList = threads.ToList();
        var threadResults = threadList
            .Where(t => t.IsSoftDeleted == false)
            .Where(t => t.Title.ToUpper().Contains(query.ToUpper()))
            .OrderByDescending(t => t.CreatedAt)
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

        return Ok(threadResults);
    }
    
    [HttpGet("Posts")]
    public async Task<IActionResult> SearchPosts(string? query)
    {
        if (query == null)
        {
            return BadRequest();
        }
        
        var posts = await _forumPostRepository.GetAll();
        var postList = posts.ToList();
        var postResults = postList
            .Where(p => p.IsSoftDeleted == false)
            .Where(p => p.Content.ToUpper().Contains(query.ToUpper()))
            .OrderByDescending(p => p.CreatedAt)
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

        return Ok(postResults);
    }
    
    [HttpGet("Members")]
    public async Task<IActionResult> SearchMembers(string? query)
    {
        if (query == null)
        {
            return BadRequest();
        }
        
        var membersResults = await _userManager.Users
            .Where(u => u.UserName.ToUpper().Contains(query.ToUpper()))
            .OrderByDescending(u => u.CreatedAt)
            .Select(u => new
            {
                u.UserName,
                u.Avatar,
                u.CreatedAt
            })
            .ToListAsync();

        return Ok(membersResults);
    }
}