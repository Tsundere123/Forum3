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

        var controller = new ProfileController(userManagerMock.Object, null, null, wallPostRepositoryMock.Object, null);

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
        
        var controller = new ProfileController(userManagerMock.Object, null, null, wallPostRepositoryMock.Object, null);
        
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
        
        var controller = new ProfileController(userManagerMock.Object, null, null, null, null);
        
        // Act
        var result = await controller.GetWallPosts(userName);
        
        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task CreateWallPost()
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

        var controller = new ProfileController(userManagerMock.Object, null, null, wallPostRepositoryMock.Object, null)
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
        var result = await controller.CreateWallPost(userName, wallPostCreateDto);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
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

        var controller = new ProfileController(userManagerMock.Object, null, null, wallPostRepositoryMock.Object, null);

        // Act
        var result = await controller.DeleteWallPost(userName, postId);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task CreateWallPostReply()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var wallPostRepositoryMock = new Mock<IWallPostRepository>();
        var wallPostReplyRepositoryMock = new Mock<IWallPostReplyRepository>();
        
        var userName = "validUser";
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

        var controller = new ProfileController(userManagerMock.Object, null, null, wallPostRepositoryMock.Object, wallPostReplyRepositoryMock.Object)
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

    [Fact]
    public async Task DeleteWallPostReply()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var wallPostRepositoryMock = new Mock<IWallPostRepository>();
        var wallPostReplyRepositoryMock = new Mock<IWallPostReplyRepository>();
        
        var userName = "validUser";
        var postId = 1;
        var replyId = 2;

        var user = new ApplicationUser
        {
            Id = "userId",
            UserName = userName
        };
        userManagerMock.Setup(m => m.FindByNameAsync(userName)).ReturnsAsync(user);

        var wallPost = new WallPost
        {
            Id = postId,
            Content = "Post content"
        };
        wallPostRepositoryMock.Setup(m => m.GetById(postId)).ReturnsAsync(wallPost);

        var wallPostReply = new WallPostReply
        {
            Id = replyId,
            Content = "Reply content"
        };
        wallPostReplyRepositoryMock.Setup(m => m.GetById(replyId)).ReturnsAsync(wallPostReply);

        wallPostReplyRepositoryMock.Setup(m => m.Delete(replyId)).ReturnsAsync(true);

        var controller = new ProfileController(userManagerMock.Object, null, null, wallPostRepositoryMock.Object, wallPostReplyRepositoryMock.Object);

        // Act
        var result = await controller.DeleteWallPostReply(userName, postId, replyId);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }
}