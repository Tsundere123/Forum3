using Forum3.DAL;
using Forum3.DTOs;
using Forum3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ForumCategoryController : Controller
{
    private readonly IForumCategoryRepository _forumCategoryRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public ForumCategoryController(
        IForumCategoryRepository forumCategoryRepository, 
        UserManager<ApplicationUser> userManager)
    {
        _forumCategoryRepository = forumCategoryRepository;
        _userManager = userManager;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _forumCategoryRepository.GetAll();
        var categoriesList = categories.ToList();
        var categoriesResult = categoriesList.Select(c => new CategoryDto()
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            LatestThread = c.Threads.Any() ? new LookupThreadDto()
            {
                Id = c.Threads.LastOrDefault()?.Id,
                Title = c.Threads.LastOrDefault()?.Title,
                CreatedAt = c.Threads.LastOrDefault()?.CreatedAt,
                Category = c.Threads.LastOrDefault()?.Category.Name,
                Creator = new LookupUserDto()
                {
                    UserName = _userManager.Users.FirstOrDefault(u => u.Id == c.Threads.LastOrDefault()!.CreatorId)?.UserName,
                    Avatar = _userManager.Users.FirstOrDefault(u => u.Id == c.Threads.LastOrDefault()!.CreatorId)?.Avatar,
                    CreatedAt = _userManager.Users.FirstOrDefault(u => u.Id == c.Threads.LastOrDefault()!.CreatorId)?.CreatedAt
                }
            } : null,
            ThreadCount = c.Threads.Count,
            PostCount = c.Threads.Sum(t => t.Posts.Count)
        }).ToList();
     
        return Ok(categoriesResult);
    }
}