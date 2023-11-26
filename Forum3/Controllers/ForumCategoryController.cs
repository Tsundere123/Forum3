using Forum3.DAL;
using Forum3.Data;
using Forum3.DTOs;
using Forum3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ForumCategoryController : Controller
{
    private readonly IForumCategoryRepository _forumCategoryRepository;
    private readonly IForumThreadRepository _forumThreadRepository;
    private readonly IForumPostRepository _forumPostRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public ForumCategoryController(IForumCategoryRepository forumCategoryRepository, IForumThreadRepository forumThreadRepository,
        IForumPostRepository forumPostRepository, UserManager<ApplicationUser> userManager)
    {
        _forumCategoryRepository = forumCategoryRepository;
        _forumThreadRepository = forumThreadRepository;
        _userManager = userManager;
        _forumPostRepository = forumPostRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _forumCategoryRepository.GetAll();
        
        return Ok(categories);
    }
    
    [HttpGet("Test")]
    public async Task<IActionResult> GetAllCategoriesTest()
    {
        var categories = await _forumCategoryRepository.GetAll();
        var categoriesList = categories.ToList();
        var categoriesResult = categoriesList.Select(c => new CategoryDto()
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            LatestThread = new LookupThreadDto()
            {
                Id = c.Threads.LastOrDefault()?.Id,
                Title = c.Threads.LastOrDefault()?.Title,
                CreatedAt = c.Threads.LastOrDefault()?.CreatedAt,
                Category = c.Threads.LastOrDefault()?.Category.Name,
                Creator = new LookupUserDto()
                {
                    UserName = "test",
                    Avatar = "default.png",
                    CreatedAt = DateTime.Now
                }
            },
            ThreadCount = c.Threads.Count,
            PostCount = c.Threads.Sum(t => t.Posts.Count)
        }).ToList();
     
        return Ok(categoriesResult);
    }
    
    // [HttpGet]
    // public async Task<IActionResult> CountNonSoftDeletedThreadsOfCategory(int categoryId)
    // {
    //     var threads = await _forumThreadRepository.GetForumThreadsByCategoryId(categoryId);
    //     var count = 0;
    //     foreach (var thread in threads)
    //     {
    //         if (!thread.IsSoftDeleted)
    //         {
    //             count++;
    //         }
    //     }
    //     return Ok(count);
    // }

}