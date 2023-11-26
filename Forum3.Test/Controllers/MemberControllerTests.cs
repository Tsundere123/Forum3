using Forum3.Controllers;
using Forum3.DTOs;
using Forum3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Forum3.Test.Controllers;

public class MemberControllerTests
{
    [Fact]
    public async Task IndexPositive()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required

        var controller = new MemberController(userManagerMock.Object);
        
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
        var okResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsAssignableFrom<List<LookupUserDto>>(okResult.Value);
        
        Assert.Equal(7, model.Count);
        Assert.Equal("User1", model.ElementAt(0).UserName);
        Assert.Equal("User7", model.ElementAt(6).UserName);
    }
}