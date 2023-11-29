using Forum3.Models;
using Forum3.Utilities;
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
            .Select(u => DtoUtilities.GetUserDto(u))
            .ToList();

        return Task.FromResult<IActionResult>(Ok(userLookup));
    }
}