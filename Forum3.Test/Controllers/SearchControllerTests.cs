using Forum3.Controllers;
using Forum3.DAL;
using Forum3.DTOs;
using Forum3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Forum3.Test.Controllers;

public class SearchControllerTests
{
    [Fact]
    public async Task IndexPositive()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var forumThreadRepositoryMock = new Mock<IForumThreadRepository>();
        var forumPostRepositoryMock = new Mock<IForumPostRepository>();

        var controller = new SearchController(
            userManagerMock.Object,
            forumThreadRepositoryMock.Object,
            forumPostRepositoryMock.Object);
        
        forumThreadRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(Constants.ForumThreads);
        forumPostRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(Constants.ForumPosts);
        userManagerMock.Setup(manager => manager.Users).Returns(Constants.Users.AsQueryable());
        
        // Act
        var result = await controller.Index("r");
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<SearchDto>(okResult.Value);
        
        Assert.Equal(6, model.threads.Count);
        Assert.Equal(6, model.members.Count);
        Assert.Empty(model.posts);
    }

    [Fact]
    public async Task ThreadsPositive()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var forumThreadRepositoryMock = new Mock<IForumThreadRepository>();
        var forumPostRepositoryMock = new Mock<IForumPostRepository>();

        var controller = new SearchController(
            userManagerMock.Object,
            forumThreadRepositoryMock.Object,
            forumPostRepositoryMock.Object);

        forumThreadRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(Constants.ForumThreads);
        
        // Act
        var result = await controller.SearchThreads("1");
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<List<LookupThreadDto>>(okResult.Value);
        
        Assert.Single(model);
    }
    
    [Fact]
    public async Task PostsPositive()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var forumThreadRepositoryMock = new Mock<IForumThreadRepository>();
        var forumPostRepositoryMock = new Mock<IForumPostRepository>();

        var controller = new SearchController(
            userManagerMock.Object,
            forumThreadRepositoryMock.Object,
            forumPostRepositoryMock.Object);

        forumPostRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(Constants.ForumPosts);
        
        // Act
        var result = await controller.SearchPosts("1");
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<List<LookupPostDto>>(okResult.Value);
        
        Assert.Single(model);
    }
    
    [Fact]
    public async Task MembersPositive()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var forumThreadRepositoryMock = new Mock<IForumThreadRepository>();
        var forumPostRepositoryMock = new Mock<IForumPostRepository>();

        var controller = new SearchController(
            userManagerMock.Object,
            forumThreadRepositoryMock.Object,
            forumPostRepositoryMock.Object);

        userManagerMock.Setup(manager => manager.Users).Returns(Constants.Users.AsQueryable());
        
        // Act
        var result = await controller.SearchMembers("1");
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<List<LookupUserDto>>(okResult.Value);
        
        Assert.Single(model);
    }
}