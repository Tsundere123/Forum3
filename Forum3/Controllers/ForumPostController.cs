using Forum3.ClientApp.DAL;
using Forum3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum3.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ForumPostController : Controller
{
    private readonly IForumThreadRepository _forumThreadRepository;
    private readonly IForumPostRepository _forumPostRepository;
    private readonly UserManager<ApplicationUser> _userManager;   
    
    public ForumPostController(IForumThreadRepository forumThreadRepository,IForumPostRepository forumPostRepository, UserManager<ApplicationUser> userManager)
    {
        _forumThreadRepository = forumThreadRepository;
        _forumPostRepository = forumPostRepository;
        _userManager = userManager;
    }
    
    private const int PageSize = 10;

    [HttpGet("{forumThreadId}/{page?}")]
    public async Task<IActionResult> ForumPostView(int forumThreadId, int? page)
    {
        var forumPosts = await _forumPostRepository.GetAllForumPostsByThreadId(forumThreadId);
        return Ok(forumPosts);
    }
}