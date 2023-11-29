using Forum3.Controllers;
using Forum3.DAL;
using Forum3.DTOs;
using Forum3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Forum3.Test.Controllers;

public class HomeControllerTests
{
    [Fact]
    public async Task IndexPositive()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var forumThreadRepositoryMock = new Mock<IForumThreadRepository>();
        var forumPostRepositoryMock = new Mock<IForumPostRepository>();

        var controller = new HomeController(
            userManagerMock.Object, 
            forumThreadRepositoryMock.Object, 
            forumPostRepositoryMock.Object);

        forumThreadRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(Constants.ForumThreads);
        forumPostRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(Constants.ForumPosts);
        userManagerMock.Setup(manager => manager.Users).Returns(Constants.Users.AsQueryable());

        // Act
        var result = await controller.Index();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<HomeDto>(okResult.Value);
        
        Assert.Equal(6, model.Threads.Count);
        Assert.Equal(6, model.Posts.Count);
        Assert.Equal(6, model.Members.Count);
    }
}