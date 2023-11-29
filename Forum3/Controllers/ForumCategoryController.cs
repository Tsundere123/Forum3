using Forum3.DAL;
using Forum3.DTOs.ForumCategory;
using Forum3.DTOs.Lookup;
using Forum3.Models;
using Forum3.Utilities;
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
        if (categories == null) return NotFound();
        
        var categoriesList = categories.ToList();
        var categoriesResult = categoriesList.Select(c => new ForumCategoryDto()
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
                Creator = DtoUtilities.GetUserDto(_userManager.Users.FirstOrDefault(u => u.Id == c.Threads.LastOrDefault()!.CreatorId)!)
            } : null,
            ThreadCount = c.Threads.Count,
            PostCount = c.Threads.Sum(t => t.Posts.Count)
        }).ToList();
     
        return Ok(categoriesResult);
    }
}