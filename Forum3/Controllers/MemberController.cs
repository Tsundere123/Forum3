using Forum3.DTOs.Lookup;
using Forum3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Forum3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MemberController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public MemberController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    
    [HttpGet]
    public Task<IActionResult> Index()
    {
        var userLookup = _userManager.Users
            .Select(u => new LookupUserDto()
            {
                UserName = u.UserName,
                Avatar = u.Avatar,
                CreatedAt = u.CreatedAt
            })
            .ToList();

        return Task.FromResult<IActionResult>(Ok(userLookup));
    }
}