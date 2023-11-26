using Forum3.Controllers;
using Forum3.DAL;
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

        var threads = new List<ForumThread>
        {
            new() { Id = 1, Title = "Thread 1", CreatedAt = DateTime.Now, Category = new ForumCategory() { Name = "Category" }, CreatorId = "userId1" },
            new() { Id = 1, Title = "Thread 2", CreatedAt = DateTime.Now, Category = new ForumCategory() { Name = "Category" }, CreatorId = "userId2" },
            new() { Id = 1, Title = "Thread 3", CreatedAt = DateTime.Now, Category = new ForumCategory() { Name = "Category" }, CreatorId = "userId3" },
            new() { Id = 1, Title = "Thread 4", CreatedAt = DateTime.Now, Category = new ForumCategory() { Name = "Category" }, CreatorId = "userId4" },
            new() { Id = 1, Title = "Thread 5", CreatedAt = DateTime.Now, Category = new ForumCategory() { Name = "Category" }, CreatorId = "userId5" },
            new() { Id = 1, Title = "Thread 6", CreatedAt = DateTime.Now, Category = new ForumCategory() { Name = "Category" }, CreatorId = "userId6" },
            new() { Id = 1, Title = "Thread 7", CreatedAt = DateTime.Now, Category = new ForumCategory() { Name = "Category" }, CreatorId = "userId7" },
        };
        forumThreadRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(threads);

        var posts = new List<ForumPost>
        {
            new() { Id = 1, Content = "Post 1", CreatedAt = DateTime.Now, Thread = threads[0], CreatorId = "userId1" },
            new() { Id = 1, Content = "Post 2", CreatedAt = DateTime.Now, Thread = threads[0], CreatorId = "userId2" },
            new() { Id = 1, Content = "Post 3", CreatedAt = DateTime.Now, Thread = threads[0], CreatorId = "userId3" },
            new() { Id = 1, Content = "Post 4", CreatedAt = DateTime.Now, Thread = threads[0], CreatorId = "userId4" },
            new() { Id = 1, Content = "Post 5", CreatedAt = DateTime.Now, Thread = threads[0], CreatorId = "userId5" },
            new() { Id = 1, Content = "Post 6", CreatedAt = DateTime.Now, Thread = threads[0], CreatorId = "userId6" },
            new() { Id = 1, Content = "Post 7", CreatedAt = DateTime.Now, Thread = threads[0], CreatorId = "userId7" },
        };
        forumPostRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(posts);

        var users = new List<ApplicationUser>
        {
            new() { Id = "userId1", UserName = "User1", Avatar = "default.png", CreatedAt = DateTime.Now },
            new() { Id = "userId2", UserName = "User2", Avatar = "default.png", CreatedAt = DateTime.Now },
            new() { Id = "userId3", UserName = "User3", Avatar = "default.png", CreatedAt = DateTime.Now },
            new() { Id = "userId4", UserName = "User4", Avatar = "default.png", CreatedAt = DateTime.Now },
            new() { Id = "userId5", UserName = "User5", Avatar = "default.png", CreatedAt = DateTime.Now },
            new() { Id = "userId6", UserName = "User6", Avatar = "default.png", CreatedAt = DateTime.Now },
            new() { Id = "userId7", UserName = "User7", Avatar = "default.png", CreatedAt = DateTime.Now },
        };
        userManagerMock.Setup(manager => manager.Users).Returns(users.AsQueryable());

        // Act
        var result = await controller.Index();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<JsonResult>(result);
    }
}