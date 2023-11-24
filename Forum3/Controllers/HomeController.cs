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

    public HomeController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        // Get latest 6 threads
        
        // Get latest 6 posts
        
        // Get username and avatar of latest 6 members
        var members = await _userManager.Users
            .OrderByDescending(u => u.CreatedAt)
            .Take(6)
            .Select(u => new
            {
                u.UserName,
                u.Avatar
            })
            .ToListAsync();
        
        return Ok(members);
    }
}