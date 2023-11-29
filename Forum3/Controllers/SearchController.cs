using Forum3.DAL;
using Forum3.DTOs;
using Forum3.DTOs.Lookup;
using Forum3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IForumThreadRepository _forumThreadRepository;
    private readonly IForumPostRepository _forumPostRepository;

    public SearchController(
        UserManager<ApplicationUser> userManager, 
        IForumThreadRepository forumThreadRepository, 
        IForumPostRepository forumPostRepository)
    {
        _userManager = userManager;
        _forumThreadRepository = forumThreadRepository;
        _forumPostRepository = forumPostRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? query)
    {
        if (query == null) return BadRequest();
        
        var threads = await _forumThreadRepository.GetAll();
        var threadList = threads.ToList();
        var threadResults = threadList
            .Where(t => t.IsSoftDeleted == false)
            .Where(t => t.Title.ToUpper().Contains(query.ToUpper()))
            .OrderByDescending(t => t.CreatedAt)
            .Take(6)
            .Select(t => new LookupThreadDto()
            {
                Id = t.Id,
                Title = t.Title,
                CreatedAt = t.CreatedAt,
                Category = t.Category.Name,
                Creator = new LookupUserDto()
                {
                    UserName = _userManager.Users.FirstOrDefault(u => u.Id == t.CreatorId)?.UserName,
                    Avatar = _userManager.Users.FirstOrDefault(u => u.Id == t.CreatorId)?.Avatar,
                    CreatedAt = _userManager.Users.FirstOrDefault(u => u.Id == t.CreatorId)?.CreatedAt
                }
            })
            .ToList();
        
        var posts = await _forumPostRepository.GetAll();
        var postList = posts.ToList();
        var postResults = postList
            .Where(p => p.IsSoftDeleted == false)
            .Where(p => p.Content.ToUpper().Contains(query.ToUpper()))
            .OrderByDescending(p => p.CreatedAt)
            .Take(6)
            .Select(p => new LookupPostDto()
            {
                Id = p.Id,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                ThreadTitle = p.Thread.Title,
                ThreadId = p.Thread.Id,
                Creator = new LookupUserDto()
                {
                    UserName = _userManager.Users.FirstOrDefault(u => u.Id == p.CreatorId)?.UserName,
                    Avatar = _userManager.Users.FirstOrDefault(u => u.Id == p.CreatorId)?.Avatar,
                    CreatedAt = _userManager.Users.FirstOrDefault(u => u.Id == p.CreatorId)?.CreatedAt
                }
            })
            .ToList();
        
        var membersResults =  _userManager.Users
            .Where(u => u.UserName.ToUpper().Contains(query.ToUpper()))
            .OrderByDescending(u => u.CreatedAt)
            .Take(6)
            .Select(u => new LookupUserDto()
            {
                UserName = u.UserName,
                Avatar = u.Avatar,
                CreatedAt = u.CreatedAt
            })
            .ToList();

        var dto = new SearchDto()
        {
            threads = threadResults,
            posts = postResults,
            members = membersResults
        };

        return Ok(dto);
    }
    
    [HttpGet("Threads")]
    public async Task<IActionResult> SearchThreads(string? query)
    {
        if (query == null) return BadRequest();
        
        var threads = await _forumThreadRepository.GetAll();
        var threadList = threads.ToList();
        var threadResults = threadList
            .Where(t => t.IsSoftDeleted == false)
            .Where(t => t.Title.ToUpper().Contains(query.ToUpper()))
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new LookupThreadDto()
            {
                Id = t.Id,
                Title = t.Title,
                CreatedAt = t.CreatedAt,
                Category = t.Category.Name,
                Creator = new LookupUserDto()
                {
                    UserName = _userManager.Users.FirstOrDefault(u => u.Id == t.CreatorId)?.UserName,
                    Avatar = _userManager.Users.FirstOrDefault(u => u.Id == t.CreatorId)?.Avatar,
                    CreatedAt = _userManager.Users.FirstOrDefault(u => u.Id == t.CreatorId)?.CreatedAt
                }
            })
            .ToList();

        return Ok(threadResults);
    }
    
    [HttpGet("Posts")]
    public async Task<IActionResult> SearchPosts(string? query)
    {
        if (query == null) return BadRequest();
        
        var posts = await _forumPostRepository.GetAll();
        var postList = posts.ToList();
        var postResults = postList
            .Where(p => p.IsSoftDeleted == false)
            .Where(p => p.Content.ToUpper().Contains(query.ToUpper()))
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => new LookupPostDto()
            {
                Id = p.Id,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                ThreadTitle = p.Thread.Title,
                ThreadId = p.Thread.Id,
                Creator = new LookupUserDto()
                {
                    UserName = _userManager.Users.FirstOrDefault(u => u.Id == p.CreatorId)?.UserName,
                    Avatar = _userManager.Users.FirstOrDefault(u => u.Id == p.CreatorId)?.Avatar,
                    CreatedAt = _userManager.Users.FirstOrDefault(u => u.Id == p.CreatorId)?.CreatedAt
                }
            })
            .ToList();

        return Ok(postResults);
    }
    
    [HttpGet("Members")]
    public Task<IActionResult> SearchMembers(string? query)
    {
        if (query == null) return Task.FromResult<IActionResult>(BadRequest());
        
        var membersResults = _userManager.Users
            .Where(u => u.UserName.ToUpper().Contains(query.ToUpper()))
            .OrderByDescending(u => u.CreatedAt)
            .Select(u => new LookupUserDto()
            {
                UserName = u.UserName,
                Avatar = u.Avatar,
                CreatedAt = u.CreatedAt
            })
            .ToList();

        return Task.FromResult<IActionResult>(Ok(membersResults));
    }
}