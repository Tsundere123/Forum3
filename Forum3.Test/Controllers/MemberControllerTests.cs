using Forum3.Controllers;
using Forum3.DTOs.Lookup;
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
        
        userManagerMock.Setup(manager => manager.Users).Returns(Constants.Users.AsQueryable());
        
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