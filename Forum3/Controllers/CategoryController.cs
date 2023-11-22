using Forum3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : Controller
{
    private readonly ForumDbContext _forumDbContext;

    public CategoryController(ForumDbContext forumDbContext)
    {
        _forumDbContext = forumDbContext;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _forumDbContext.ForumCategory.ToListAsync();
        var count = 0;
        foreach (var category in categories)
        {
            count = category.Threads.Count;
        }

        var responseData = new { categories = categories, count = count };
        return Ok(responseData);
    }

}