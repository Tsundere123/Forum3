using Forum3.Controllers;
using Forum3.DAL;
using Forum3.DTOs.Profile;
using Forum3.Models;
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
}