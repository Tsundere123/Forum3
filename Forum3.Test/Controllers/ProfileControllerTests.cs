using System.Security.Claims;
using Forum3.Controllers;
using Forum3.DAL;
using Forum3.DTOs.Profile;
using Forum3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Forum3.Test.Controllers;

public class ProfileControllerTests
{
    /*
        GetWallPosts
    */
    [Fact]
    public Task GetWallPosts_UserNameNullOrEmpty()
    {
        // Arrange
        var controller = new ProfileController(null!, null!, null!, null!, null!);
        
        // Act
        var resultEmpty = controller.GetWallPosts("");
        var resultNull = controller.GetWallPosts(null!);

        // Assert
        Assert.IsType<NotFoundResult>(resultEmpty.Result);
        Assert.IsType<NotFoundResult>(resultNull.Result);
        
        return Task.CompletedTask;
    }
    
    [Fact]
    public async Task GetWallPosts_ValidUser_WithWallPosts()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var wallPostRepositoryMock = new Mock<IWallPostRepository>();
        
        var userName = "Alice";
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = userName,
            Avatar = "default.png",
            CreatedAt = DateTime.UtcNow
        };
        userManagerMock.Setup(m => m.FindByNameAsync(userName)).ReturnsAsync(user);
        userManagerMock.Setup(m => m.FindByIdAsync(user.Id)).ReturnsAsync(user);

        var wallPosts = new List<WallPost>
        {
            new WallPost
            {
                Id = 1,
                Content = "Post content",
                CreatedAt = DateTime.UtcNow,
                AuthorId = user.Id,
                Replies = new List<WallPostReply>
                {
                    new WallPostReply
                    {
                        Id = 1,
                        Content = "Reply content",
                        CreatedAt = DateTime.UtcNow,
                        AuthorId = user.Id
                    }
                }
            }
        };
        wallPostRepositoryMock.Setup(m => m.GetAllByProfile(user.Id)).ReturnsAsync(wallPosts);

        var controller = new ProfileController(userManagerMock.Object, null!, null!, wallPostRepositoryMock.Object, null!);

        // Act
        var result = await controller.GetWallPosts(userName);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var profileWallDto = Assert.IsType<ProfileWallDto>(okResult.Value);

        Assert.Equal(user.UserName, profileWallDto.User.UserName);
        Assert.Equal(user.Avatar, profileWallDto.User.Avatar);
        Assert.Equal(user.CreatedAt, profileWallDto.User.CreatedAt);

        Assert.Equal(wallPosts.Count, profileWallDto.WallPosts!.Count);
        
        foreach (var wallPostDto in profileWallDto.WallPosts)
        {
            var correspondingWallPost = wallPosts.Single(wp => wp.Id == wallPostDto.Id);
            Assert.Equal(correspondingWallPost.Content, wallPostDto.Content);
            Assert.Equal(correspondingWallPost.CreatedAt, wallPostDto.CreatedAt);

            Assert.Equal(user.UserName, wallPostDto.Author.UserName);
            Assert.Equal(user.Avatar, wallPostDto.Author.Avatar);
            Assert.Equal(user.CreatedAt, wallPostDto.Author.CreatedAt);

            Assert.Equal(correspondingWallPost.Replies!.Count, wallPostDto.Replies!.Count);
            foreach (var replyDto in wallPostDto.Replies)
            {
                var correspondingReply = correspondingWallPost.Replies.Single(r => r.Id == replyDto.Id);
                Assert.Equal(correspondingReply.Content, replyDto.Content);
                Assert.Equal(correspondingReply.CreatedAt, replyDto.CreatedAt);

                Assert.Equal(user.UserName, replyDto.Author.UserName);
                Assert.Equal(user.Avatar, replyDto.Author.Avatar);
                Assert.Equal(user.CreatedAt, replyDto.Author.CreatedAt);
            }
        }
    }

    [Fact]
    public async Task GetWallPosts_ValidUser_WithoutWallPosts()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var wallPostRepositoryMock = new Mock<IWallPostRepository>();
        
        var userName = "Alice";
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = userName,
            Avatar = "default.png",
            CreatedAt = DateTime.UtcNow
        };
        userManagerMock.Setup(m => m.FindByNameAsync(userName)).ReturnsAsync(user);
        userManagerMock.Setup(m => m.FindByIdAsync(user.Id)).ReturnsAsync(user);
        wallPostRepositoryMock.Setup(m => m.GetAllByProfile(user.Id)).ReturnsAsync((List<WallPost>?) null);
        
        var controller = new ProfileController(userManagerMock.Object, null!, null!, wallPostRepositoryMock.Object, null!);
        
        // Act
        var result = await controller.GetWallPosts(userName);
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var profileWallDto = Assert.IsType<ProfileWallDto>(okResult.Value);
        
        Assert.Equal(user.UserName, profileWallDto.User.UserName);
        Assert.Equal(user.Avatar, profileWallDto.User.Avatar);
        Assert.Equal(user.CreatedAt, profileWallDto.User.CreatedAt);
        
        Assert.Empty(profileWallDto.WallPosts!);
    }

    [Fact]
    public async Task GetWallPosts_InvalidUser()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        
        var userName = "Alice";
        userManagerMock.Setup(m => m.FindByNameAsync(userName))!.ReturnsAsync((ApplicationUser?) null);
        
        var controller = new ProfileController(userManagerMock.Object, null!, null!, null!, null!);
        
        // Act
        var result = await controller.GetWallPosts(userName);
        
        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
    
    /*
        CreateWallPost
    */
    [Fact]
    public Task CreateWallPost_UserNameNullOrEmpty()
    {
        // Arrange
        var controller = new ProfileController(null!, null!, null!, null!, null!);
        
        // Act
        var resultEmpty = controller.CreateWallPost("", null!);
        var resultNull = controller.CreateWallPost(null!, null!);

        // Assert
        Assert.IsType<NotFoundResult>(resultEmpty.Result);
        Assert.IsType<NotFoundResult>(resultNull.Result);
        
        return Task.CompletedTask;
    }

    [Fact]
    public Task CreateWallPost_ProfileUserNotFound()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        
        userManagerMock.Setup(m => m.FindByNameAsync("Alice")).ReturnsAsync((ApplicationUser) null!);
        
        var controller = new ProfileController(userManagerMock.Object, null!, null!, null!, null!);
        
        // Act
        var result = controller.CreateWallPost("Alice", null!);
        
        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
        
        return Task.CompletedTask;
    }

    [Fact]
    public Task CreateWallPost_AuthorNotFound()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        
        var user = new ApplicationUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "Alice"
        };  
        
        userManagerMock.Setup(m => m.FindByNameAsync(user.UserName)).ReturnsAsync(user);
        
        var controller = new ProfileController(userManagerMock.Object, null!, null!, null!, null!)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = null! // No user is logged in
                }
            }
        };
        
        // Act
        var result = controller.CreateWallPost(user.UserName, null!);
        
        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
        
        return Task.CompletedTask;
    }
    
    [Fact]
    public async Task CreateWallPost_Positive()
    {
        // Arrange
        var userName = "Alice";
        var wallPostCreateDto = new WallPostCreateDto
        {
            Content = "Post content"
        };

        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var wallPostRepositoryMock = new Mock<IWallPostRepository>();

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = userName
        };
        userManagerMock.Setup(m => m.FindByNameAsync(userName)).ReturnsAsync(user);

        var author = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString()
        };
        userManagerMock.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(author);

        wallPostRepositoryMock.Setup(m => m.Create(It.IsAny<WallPost>())).ReturnsAsync(true);

        var controller = new ProfileController(userManagerMock.Object, null!, null!, wallPostRepositoryMock.Object, null!)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity()) // User is logged in
                }
            }
        };

        // Act
        var result = await controller.CreateWallPost(userName, wallPostCreateDto);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }
    
    [Fact]
    public async Task CreateWallPost_Negative()
    {
        // Arrange
        var userName = "Alice";
        var wallPostCreateDto = new WallPostCreateDto
        {
            Content = "Post content"
        };

        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var wallPostRepositoryMock = new Mock<IWallPostRepository>();

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = userName
        };
        userManagerMock.Setup(m => m.FindByNameAsync(userName)).ReturnsAsync(user);

        var author = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString()
        };
        userManagerMock.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(author);

        wallPostRepositoryMock.Setup(m => m.Create(It.IsAny<WallPost>())).ReturnsAsync(false);

        var controller = new ProfileController(userManagerMock.Object, null!, null!, wallPostRepositoryMock.Object, null!)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity()) // User is logged in
                }
            }
        };

        // Act
        var result = await controller.CreateWallPost(userName, wallPostCreateDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }

    /*
        DeleteWallPost
    */
    [Fact]
    public Task DeleteWallPost_UserNameNullOrEmpty()
    {
        // Arrange
        var controller = new ProfileController(null!, null!, null!, null!, null!);
        
        // Act
        var resultEmpty = controller.DeleteWallPost("", 0);
        var resultNull = controller.DeleteWallPost(null!, 0);

        // Assert
        Assert.IsType<NotFoundResult>(resultEmpty.Result);
        Assert.IsType<NotFoundResult>(resultNull.Result);
        
        return Task.CompletedTask;
    }
    
    [Fact]
    public async Task DeleteWallPost()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var wallPostRepositoryMock = new Mock<IWallPostRepository>();
        
        var userName = "Alice";
        var postId = 1;

        var user = new ApplicationUser
        {
            Id = "userId",
            UserName = userName
        };
        userManagerMock.Setup(m => m.FindByNameAsync(userName)).ReturnsAsync(user);

        var wallPost = new WallPost
        {
            Id = postId
        };
        wallPostRepositoryMock.Setup(m => m.GetById(postId)).ReturnsAsync(wallPost);

        wallPostRepositoryMock.Setup(m => m.Delete(postId)).ReturnsAsync(true);

        var controller = new ProfileController(userManagerMock.Object, null!, null!, wallPostRepositoryMock.Object, null!);

        // Act
        var result = await controller.DeleteWallPost(userName, postId);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    /*
        CreateWallPostReply
    */
    [Fact]
    public async Task CreateWallPostReply()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var wallPostRepositoryMock = new Mock<IWallPostRepository>();
        var wallPostReplyRepositoryMock = new Mock<IWallPostReplyRepository>();
        
        var userName = "Alice";
        var postId = 1;
        var wallPostCreateDto = new WallPostCreateDto
        {
            Content = "Reply content"
        };

        var user = new ApplicationUser
        {
            Id = "userId",
            UserName = userName
        };
        userManagerMock.Setup(m => m.FindByNameAsync(userName)).ReturnsAsync(user);

        var author = new ApplicationUser
        {
            Id = "authorId"
        };
        userManagerMock.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(author);

        var wallPost = new WallPost
        {
            Id = postId,
            Content = "Post content"
        };
        wallPostRepositoryMock.Setup(m => m.GetById(postId)).ReturnsAsync(wallPost);

        wallPostReplyRepositoryMock.Setup(m => m.Create(It.IsAny<WallPostReply>())).ReturnsAsync(true);

        var controller = new ProfileController(userManagerMock.Object, null!, null!, wallPostRepositoryMock.Object, wallPostReplyRepositoryMock.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity())
                }
            }
        };

        // Act
        var result = await controller.CreateWallPostReply(userName, postId, wallPostCreateDto);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    /*
        DeleteWallPostReply
    */
    [Fact]
    public async Task DeleteWallPostReply()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var wallPostRepositoryMock = new Mock<IWallPostRepository>();
        var wallPostReplyRepositoryMock = new Mock<IWallPostReplyRepository>();
        
        var userName = "Alice";
        var postId = 1;
        var replyId = 2;

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = userName
        };
        userManagerMock.Setup(m => m.FindByNameAsync(userName)).ReturnsAsync(user);

        var wallPost = new WallPost
        {
            Id = 1,
            Content = "Post content"
        };
        wallPostRepositoryMock.Setup(m => m.GetById(postId)).ReturnsAsync(wallPost);

        var wallPostReply = new WallPostReply
        {
            Id = 1,
            Content = "Reply content"
        };
        wallPostReplyRepositoryMock.Setup(m => m.GetById(replyId)).ReturnsAsync(wallPostReply);

        wallPostReplyRepositoryMock.Setup(m => m.Delete(replyId)).ReturnsAsync(true);

        var controller = new ProfileController(userManagerMock.Object, null!, null!, wallPostRepositoryMock.Object, wallPostReplyRepositoryMock.Object);

        // Act
        var result = await controller.DeleteWallPostReply(userName, postId, replyId);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }
    
    /*
        GetThreads
    */
    [Fact]
    public async Task GetThreads_ValidUser_WithThreads()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var forumThreadRepositoryMock = new Mock<IForumThreadRepository>();
        
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "Alice",
            Avatar = "default.png",
            CreatedAt = DateTime.UtcNow
        };
        userManagerMock.Setup(m => m.FindByNameAsync(user.UserName)).ReturnsAsync(user);
        userManagerMock.Setup(m => m.FindByIdAsync(user.Id)).ReturnsAsync(user);

        var threads = new List<ForumThread>()
        {
            new() { Id = 1, Title = "Thread 1", CreatedAt = DateTime.Now, Category = new ForumCategory() { Name = "Category" }, CreatorId = user.Id },
            new() { Id = 2, Title = "Thread 2", CreatedAt = DateTime.Now, Category = new ForumCategory() { Name = "Category" }, CreatorId = user.Id },
        };
        forumThreadRepositoryMock.Setup(m => m.GetForumThreadsByAccountId(user.Id)).ReturnsAsync(threads);

        var controller = new ProfileController(userManagerMock.Object, forumThreadRepositoryMock.Object!, null!, null!, null!);

        // Act
        var result = await controller.GetThreads(user.UserName);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var dto = Assert.IsType<ProfileThreadsDto>(okResult.Value);

        Assert.Equal(user.UserName, dto.User.UserName);
        Assert.Equal(user.Avatar, dto.User.Avatar);
        Assert.Equal(user.CreatedAt, dto.User.CreatedAt);
        
        Assert.Equal(threads.Count, dto.Threads!.Count);
        foreach (var profileThreadDto in dto.Threads)
        {
            var correspondingThread = threads.Single(t => t.Id == profileThreadDto.Id);
            Assert.Equal(correspondingThread.Title, profileThreadDto.Title);
            Assert.Equal(correspondingThread.CreatedAt, profileThreadDto.CreatedAt);
            Assert.Equal(correspondingThread.Category!.Name, profileThreadDto.Category);
            
            Assert.Equal(user.UserName, profileThreadDto.Creator!.UserName);
            Assert.Equal(user.Avatar, profileThreadDto.Creator.Avatar);
            Assert.Equal(user.CreatedAt, profileThreadDto.Creator.CreatedAt);
        }
    }
    
    /*
        GetPosts
    */
    [Fact]
    public async Task GetPosts_ValidUser_WithPosts()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var forumPostRepositoryMock = new Mock<IForumPostRepository>();
        
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "Alice",
            Avatar = "default.png",
            CreatedAt = DateTime.UtcNow
        };
        userManagerMock.Setup(m => m.FindByNameAsync(user.UserName)).ReturnsAsync(user);
        userManagerMock.Setup(m => m.FindByIdAsync(user.Id)).ReturnsAsync(user);

        var threads = new List<ForumThread>()
        {
            new() { Id = 1, Title = "Thread 1", CreatedAt = DateTime.Now, Category = new ForumCategory() { Name = "Category" }, CreatorId = user.Id },
            new() { Id = 2, Title = "Thread 2", CreatedAt = DateTime.Now, Category = new ForumCategory() { Name = "Category" }, CreatorId = user.Id },
        };
        
        var posts = new List<ForumPost>()
        {
            new() { Id = 1, Content = "Post 1", CreatedAt = DateTime.Now, Thread = threads[0], CreatorId = user.Id },
            new() { Id = 2, Content = "Post 2", CreatedAt = DateTime.Now, Thread = threads[1], CreatorId = user.Id },
        };
        forumPostRepositoryMock.Setup(m => m.GetAllForumPostsByAccountId(user.Id)).ReturnsAsync(posts);

        var controller = new ProfileController(userManagerMock.Object, null!, forumPostRepositoryMock.Object, null!, null!);

        // Act
        var result = await controller.GetPosts(user.UserName);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var dto = Assert.IsType<ProfilePostsDto>(okResult.Value);

        Assert.Equal(user.UserName, dto.User.UserName);
        Assert.Equal(user.Avatar, dto.User.Avatar);
        Assert.Equal(user.CreatedAt, dto.User.CreatedAt);
        
        Assert.Equal(posts.Count, dto.Posts!.Count);
        foreach (var profilePostDto in dto.Posts)
        {
            var correspondingPost = posts.Single(p => p.Id == profilePostDto.Id);
            Assert.Equal(correspondingPost.Content, profilePostDto.Content);
            Assert.Equal(correspondingPost.CreatedAt, profilePostDto.CreatedAt);
            Assert.Equal(correspondingPost.Thread.Title, profilePostDto.ThreadTitle);
            Assert.Equal(correspondingPost.ThreadId, profilePostDto.ThreadId);
            
            Assert.Equal(user.UserName, profilePostDto.Creator.UserName);
            Assert.Equal(user.Avatar, profilePostDto.Creator.Avatar);
            Assert.Equal(user.CreatedAt, profilePostDto.Creator.CreatedAt);
        }
    }
}