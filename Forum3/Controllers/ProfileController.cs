using Forum3.DAL;
using Forum3.DTOs;
using Forum3.DTOs.Profile;
using Forum3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IForumThreadRepository _forumThreadRepository;
    private readonly IForumPostRepository _forumPostRepository;
    private readonly IWallPostRepository _wallPostRepository;
    private readonly IWallPostReplyRepository _wallPostReplyRepository;
    
    public ProfileController(
        UserManager<ApplicationUser> userManager, 
        IForumThreadRepository forumThreadRepository, 
        IForumPostRepository forumPostRepository, 
        IWallPostRepository wallPostRepository, 
        IWallPostReplyRepository wallPostReplyRepository)
    {
        _userManager = userManager;
        _forumThreadRepository = forumThreadRepository;
        _forumPostRepository = forumPostRepository;
        _wallPostRepository = wallPostRepository;
        _wallPostReplyRepository = wallPostReplyRepository;
    }
    
    // Wall
    [HttpGet("{userName}/wall")]
    public async Task<IActionResult> GetWallPosts(string userName)
    {
        if (string.IsNullOrEmpty(userName)) return NotFound();
        
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) return NotFound();
        
        var wallPosts = await _wallPostRepository.GetAllByProfile(user.Id);
        var wallPostDto = new List<WallPostDto>();
        if (wallPosts != null)
        {
            var wallPostsList = wallPosts.ToList();
            wallPostDto = wallPostsList.Select(p => new WallPostDto()
            {
                Id = p.Id,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                Author = new LookupUserDto()
                {
                    UserName = _userManager.FindByIdAsync(p.AuthorId).Result.UserName,
                    Avatar = _userManager.FindByIdAsync(p.AuthorId).Result.Avatar,
                    CreatedAt = _userManager.FindByIdAsync(p.AuthorId).Result.CreatedAt
                },
                Replies = p.Replies.Select(r => new WallPostReplyDto()
                {
                    Id = r.Id,
                    Content = r.Content,
                    CreatedAt = r.CreatedAt,
                    Author = new LookupUserDto()
                    {
                        UserName = _userManager.FindByIdAsync(r.AuthorId).Result.UserName,
                        Avatar = _userManager.FindByIdAsync(r.AuthorId).Result.Avatar,
                        CreatedAt = _userManager.FindByIdAsync(r.AuthorId).Result.CreatedAt
                    }
                }).ToList()
            }).ToList();
        }

        var profileWallDto = new ProfileWallDto()
        {
            User = new LookupUserDto()
            {
                UserName = user.UserName,
                Avatar = user.Avatar,
                CreatedAt = user.CreatedAt
            },
            WallPosts = wallPostDto
        };

        return Ok(profileWallDto);
    }
    
    [HttpPost("{userName}/wall")]
    public async Task<IActionResult> CreateWallPost(string userName, [FromBody] WallPostCreateDto wallPostCreateDto)
    {
        if (string.IsNullOrEmpty(userName)) return NotFound();
        
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) return NotFound();

        var author = await _userManager.GetUserAsync(HttpContext.User);
        if (author == null) return BadRequest();
        
        var wallPost = new WallPost()
        {
            Content = wallPostCreateDto.Content,
            CreatedAt = DateTime.Now,
            AuthorId = author.Id,
            ProfileId = user.Id
        };

        var result = await _wallPostRepository.Create(wallPost);
        if (result == false) return BadRequest();
        
        return Ok();
    }
    
    [HttpDelete("{userName}/wall/{id}")]
    public async Task<IActionResult> DeleteWallPost(string userName, int id)
    {
        if (string.IsNullOrEmpty(userName)) return NotFound();
        
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) return NotFound();

        var wallPost = await _wallPostRepository.GetById(id);
        if (wallPost == null) return NotFound();
        
        var result = await _wallPostRepository.Delete(id);
        if (result == false) return BadRequest();
        
        return Ok();
    }
    
    [HttpPost("{userName}/wall/{id}")]
    public async Task<IActionResult> CreateWallPostReply(string userName, int id, [FromBody] WallPostCreateDto dto)
    {
        if (string.IsNullOrEmpty(userName)) return NotFound();
        
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) return NotFound();

        var author = await _userManager.GetUserAsync(HttpContext.User);
        if (author == null) return BadRequest();
        
        var wallPost = await _wallPostRepository.GetById(id);
        if (wallPost == null) return NotFound();
        
        var wallPostReply = new WallPostReply()
        {
            Content = dto.Content,
            CreatedAt = DateTime.Now,
            AuthorId = author.Id,
            WallPostId = id
        };

        var result = await _wallPostReplyRepository.Create(wallPostReply);
        if (result == false) return BadRequest();
        
        return Ok();
    }
    
    [HttpDelete("{userName}/wall/{id}/{replyId}")]
    public async Task<IActionResult> DeleteWallPostReply(string userName, int id, int replyId)
    {
        if (string.IsNullOrEmpty(userName)) return NotFound();
        
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) return NotFound();

        var wallPost = await _wallPostRepository.GetById(id);
        if (wallPost == null) return NotFound();
        
        var wallPostReply = await _wallPostReplyRepository.GetById(replyId);
        if (wallPostReply == null) return NotFound();
        
        var result = await _wallPostReplyRepository.Delete(replyId);
        if (result == false) return BadRequest();
        
        return Ok();
    }
    
    // Threads
    [HttpGet("{userName}/threads")]
    public async Task<IActionResult> GetThreads(string userName)
    {
        if (string.IsNullOrEmpty(userName)) return NotFound();
        
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) return NotFound();

        var threads = await _forumThreadRepository.GetForumThreadsByAccountId(user.Id);
        var threadDto = new List<LookupThreadDto>();
        if (threads != null)
        {
            var threadsList = threads.ToList();
            threadDto = threadsList.Where(t => t.IsSoftDeleted == false)
                .Select(t => new LookupThreadDto() 
                {
                    Id = t.Id,
                    Title = t.Title,
                    CreatedAt = t.CreatedAt,
                    Category = t.Category!.Name,
                    Creator = new LookupUserDto()
                    {
                        UserName = _userManager.FindByIdAsync(t.CreatorId).Result.UserName,
                        Avatar = _userManager.FindByIdAsync(t.CreatorId).Result.Avatar,
                        CreatedAt = _userManager.FindByIdAsync(t.CreatorId).Result.CreatedAt
                    }
                }).ToList();
        }

        var profileThreadsDto = new ProfileThreadsDto()
        {
            User = new LookupUserDto()
            {
                UserName = user.UserName,
                Avatar = user.Avatar,
                CreatedAt = user.CreatedAt
            },
            Threads = threadDto
        };

        return Ok(profileThreadsDto);
    }
    
    // Posts
    [HttpGet("{userName}/posts")]
    public async Task<IActionResult> GetPosts(string userName)
    {
        if (string.IsNullOrEmpty(userName)) return NotFound();
        
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) return NotFound();

        var posts = await _forumPostRepository.GetAllForumPostsByAccountId(user.Id);
        var postDto = new List<LookupPostDto>();
        if (posts != null)
        {
            var postsList = posts.ToList();
            postDto = postsList.Where(p => p.IsSoftDeleted == false)
                .Select(p => new LookupPostDto()
                {
                    Id = p.Id,
                    Content = p.Content,
                    CreatedAt = p.CreatedAt,
                    ThreadTitle = p.Thread!.Title,
                    ThreadId = p.ThreadId,
                    Creator = new LookupUserDto()
                    {
                        UserName = _userManager.FindByIdAsync(p.CreatorId).Result.UserName,
                        Avatar = _userManager.FindByIdAsync(p.CreatorId).Result.Avatar,
                        CreatedAt = _userManager.FindByIdAsync(p.CreatorId).Result.CreatedAt
                    }
                }).ToList();
        }

        var profilePostsDto = new ProfilePostsDto()
        {
            User = new LookupUserDto()
            {
                UserName = user.UserName,
                Avatar = user.Avatar,
                CreatedAt = user.CreatedAt
            },
            Posts = postDto
        };

        return Ok(profilePostsDto);
    }
}