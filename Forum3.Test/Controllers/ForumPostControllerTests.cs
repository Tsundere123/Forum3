using Forum3.Controllers;
using Forum3.DAL;
using Forum3.DTOs.ForumPost;
using Forum3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Forum3.Test.Controllers;

public class ForumPostControllerTests
{
    // CREATE
    [Fact]
    public Task CreatePost_Positive()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var forumThreadRepositoryMock = new Mock<IForumThreadRepository>();
        var forumPostRepositoryMock = new Mock<IForumPostRepository>();
        
        var threadId = 1;
        var user = new ApplicationUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "Alice",
            Avatar = "default.png",
            CreatedAt = DateTime.Now
        };
        userManagerMock.Setup(x => x.FindByNameAsync(user.UserName)).ReturnsAsync(user);
        
        var forumThread = new ForumThread()
        {
            Id = threadId
        };
        forumThreadRepositoryMock.Setup(x => x.GetForumThreadById(threadId)).ReturnsAsync(forumThread);
        
        var createForumPostDto = new CreateForumPostDto()
        {
            Content = "Content",
            UserName = user.UserName
        };
        
        forumPostRepositoryMock.Setup(x => x.CreateNewForumPost(It.IsAny<ForumPost>())).ReturnsAsync(true);
        
        var controller = new ForumPostController(forumThreadRepositoryMock.Object, forumPostRepositoryMock.Object, userManagerMock.Object);

        // Act
        var result = controller.CreatePost(threadId, createForumPostDto);
        
        // Assert
        Assert.IsType<OkResult>(result.Result);
       
        return Task.CompletedTask;
    }
    
    [Fact]
    public Task CreatePost_ThreadNotFound()
    {
        // Arrange
        var forumThreadRepositoryMock = new Mock<IForumThreadRepository>();
        
        var threadId = 1;
        forumThreadRepositoryMock.Setup(x => x.GetForumThreadById(threadId)).ReturnsAsync((ForumThread?)null);
        
        var createForumPostDto = new CreateForumPostDto()
        {
            Content = "Content",
            UserName = "Alice"
        };

        var controller = new ForumPostController(forumThreadRepositoryMock.Object, null!, null!);

        // Act
        var result = controller.CreatePost(threadId, createForumPostDto);
        
        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
       
        return Task.CompletedTask;
    }
    
    [Fact]
    public Task CreatePost_UserNotFound()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var forumThreadRepositoryMock = new Mock<IForumThreadRepository>();
        
        var threadId = 1;
        userManagerMock.Setup(x => x.FindByNameAsync("Bob"))!.ReturnsAsync(null as ApplicationUser);
        
        var forumThread = new ForumThread()
        {
            Id = threadId
        };
        forumThreadRepositoryMock.Setup(x => x.GetForumThreadById(threadId)).ReturnsAsync(forumThread);
        
        var createForumPostDto = new CreateForumPostDto()
        {
            Content = "Content",
            UserName = "Bob"
        };
        
        var controller = new ForumPostController(forumThreadRepositoryMock.Object, null!, userManagerMock.Object);

        // Act
        var result = controller.CreatePost(threadId, createForumPostDto);
        
        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
       
        return Task.CompletedTask;
    }
    
    [Fact]
    public Task CreatePost_DtoFailure()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var forumThreadRepositoryMock = new Mock<IForumThreadRepository>();
        var forumPostRepositoryMock = new Mock<IForumPostRepository>();
        
        var threadId = 1;
        var user = new ApplicationUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "Alice",
            Avatar = "default.png",
            CreatedAt = DateTime.Now
        };
        userManagerMock.Setup(x => x.FindByNameAsync(user.UserName)).ReturnsAsync(user);
        
        var forumThread = new ForumThread()
        {
            Id = threadId
        };
        forumThreadRepositoryMock.Setup(x => x.GetForumThreadById(threadId)).ReturnsAsync(forumThread);
        
        var createForumPostDtoNoContent = new CreateForumPostDto()
        {
            UserName = "Alice",
            Content = ""
        };
        
        var createForumPostDtoNoUser = new CreateForumPostDto()
        {
            UserName = "",
            Content = "Content",
        };
        
        var createForumPostDtoNullContent = new CreateForumPostDto()
        {
            UserName = "Alice",
        };
        
        var createForumPostDtoNullUser = new CreateForumPostDto()
        {
            Content = "Content",
        };
        
        forumPostRepositoryMock.Setup(x => x.CreateNewForumPost(It.IsAny<ForumPost>())).ReturnsAsync(true);
        
        var controller = new ForumPostController(forumThreadRepositoryMock.Object, forumPostRepositoryMock.Object, userManagerMock.Object);

        // Act
        var result = controller.CreatePost(threadId, createForumPostDtoNoContent);
        var result2 = controller.CreatePost(threadId, createForumPostDtoNoUser);
        var result3 = controller.CreatePost(threadId, createForumPostDtoNullContent);
        var result4 = controller.CreatePost(threadId, createForumPostDtoNullUser);
        
        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
        Assert.IsType<BadRequestResult>(result2.Result);
        Assert.IsType<BadRequestResult>(result3.Result);
        Assert.IsType<BadRequestResult>(result4.Result);
       
        return Task.CompletedTask;
    }
    
    [Fact]
    public Task CreatePost_RepoFalse()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var forumThreadRepositoryMock = new Mock<IForumThreadRepository>();
        var forumPostRepositoryMock = new Mock<IForumPostRepository>();
        
        var threadId = 1;
        var user = new ApplicationUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "Alice",
            Avatar = "default.png",
            CreatedAt = DateTime.Now
        };
        userManagerMock.Setup(x => x.FindByNameAsync(user.UserName)).ReturnsAsync(user);
        
        var forumThread = new ForumThread()
        {
            Id = threadId
        };
        forumThreadRepositoryMock.Setup(x => x.GetForumThreadById(threadId)).ReturnsAsync(forumThread);
        
        var createForumPostDto = new CreateForumPostDto()
        {
            Content = "Content",
            UserName = user.UserName
        };
        
        forumPostRepositoryMock.Setup(x => x.CreateNewForumPost(It.IsAny<ForumPost>())).ReturnsAsync(false);
        
        var controller = new ForumPostController(forumThreadRepositoryMock.Object, forumPostRepositoryMock.Object, userManagerMock.Object);

        // Act
        var result = controller.CreatePost(threadId, createForumPostDto);
        
        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
       
        return Task.CompletedTask;
    }
    
    // READ
    [Fact]
    public Task ForumPostView_WithPosts()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var forumPostRepositoryMock = new Mock<IForumPostRepository>();
        
        var threadId = 1;

        var user = new ApplicationUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "Alice",
            Avatar = "default.png",
            CreatedAt = DateTime.Now
        };
        userManagerMock.Setup(x => x.FindByIdAsync(user.Id)).ReturnsAsync(user);

        var forumPosts = new List<ForumPost>()
        {
            new()
            {
                Id = 1,
                Content = "Content",
                IsSoftDeleted = false,
                CreatedAt = DateTime.Now,
                CreatorId = user.Id,
                EditedBy = user.Id,
                EditedAt = DateTime.Now
            },
            new ()
            {
                Id = 2,
                Content = "Content",
                IsSoftDeleted = false,
                CreatedAt = DateTime.Now,
                CreatorId = user.Id,
                EditedBy = "",
                EditedAt = DateTime.MinValue
            }
        };
        forumPostRepositoryMock.Setup(x => x.GetAllForumPostsByThreadId(threadId)).ReturnsAsync(forumPosts);
        
        var controller = new ForumPostController(null!, forumPostRepositoryMock.Object, userManagerMock.Object);
        
        // Act
        var result = controller.ForumPostView(threadId);
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<IEnumerable<ForumPostDto>>(okResult.Value);

        var postDtos = model.ToList();
        Assert.Equal(2, postDtos.Count);
        
        var post = postDtos.First();
        Assert.Equal(1, post.Id);
        Assert.NotNull(post.Creator);
        Assert.NotNull(post.EditedAt);
        Assert.NotNull(post.EditedBy);
        
        var lastPost = postDtos.Last();
        Assert.Equal(2, lastPost.Id);
        Assert.NotNull(lastPost.Creator);
        Assert.Null(lastPost.EditedAt);
        Assert.Null(lastPost.EditedBy);
        
        return Task.CompletedTask;
    }
    
    [Fact]
    public Task ForumPostView_WithoutPosts()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var forumPostRepositoryMock = new Mock<IForumPostRepository>();
        
        var threadId = 1;

        var user = new ApplicationUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "Alice",
            Avatar = "default.png",
            CreatedAt = DateTime.Now
        };
        userManagerMock.Setup(x => x.FindByIdAsync(user.Id)).ReturnsAsync(user);

        var forumPosts = new List<ForumPost>();
        forumPostRepositoryMock.Setup(x => x.GetAllForumPostsByThreadId(threadId)).ReturnsAsync(forumPosts);
        
        var controller = new ForumPostController(null!, forumPostRepositoryMock.Object, userManagerMock.Object);
        
        // Act
        var result = controller.ForumPostView(threadId);
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<IEnumerable<ForumPostDto>>(okResult.Value);
        
        var postDtos = model.ToList();
        Assert.Empty(postDtos);
        
        return Task.CompletedTask;
    }
    
    // UPDATE
    [Fact]
    public Task EditPost_Positive()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var forumPostRepositoryMock = new Mock<IForumPostRepository>();
        
        var postId = 1;
        var user = new ApplicationUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "Alice",
            Avatar = "default.png",
            CreatedAt = DateTime.Now
        };
        userManagerMock.Setup(x => x.FindByNameAsync(user.UserName)).ReturnsAsync(user);
        
        var createForumPostDto = new CreateForumPostDto()
        {
            Content = "Content",
            UserName = user.UserName
        };
        
        var forumPost = new ForumPost()
        {
            Id = postId,
            Content = "Content",
            CreatorId = user.Id,
            EditedBy = "",
            EditedAt = DateTime.MinValue
        };
        forumPostRepositoryMock.Setup(x => x.GetForumPostById(postId)).ReturnsAsync(forumPost);
        forumPostRepositoryMock.Setup(x => x.UpdateForumPost(It.IsAny<ForumPost>())).ReturnsAsync(true);
        
        var controller = new ForumPostController(null!, forumPostRepositoryMock.Object, userManagerMock.Object);

        // Act
        var result = controller.EditPost(postId, createForumPostDto);
        
        // Assert
        Assert.IsType<OkResult>(result.Result);
       
        return Task.CompletedTask;
    }
    
    [Fact]
    public Task EditPost_PostNotFound()
    {
        // Arrange
        var forumPostRepositoryMock = new Mock<IForumPostRepository>();
        
        var postId = 1;
        var createForumPostDto = new CreateForumPostDto()
        {
            Content = "Content",
            UserName = "Alice"
        };
        forumPostRepositoryMock.Setup(x => x.GetForumPostById(postId)).ReturnsAsync(null as ForumPost);
        
        var controller = new ForumPostController(null!, forumPostRepositoryMock.Object, null!);

        // Act
        var result = controller.EditPost(postId, createForumPostDto);
        
        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
       
        return Task.CompletedTask;
    }
    
    [Fact]
    public Task EditPost_UserNotFound()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var forumPostRepositoryMock = new Mock<IForumPostRepository>();
        
        var postId = 1;
        userManagerMock.Setup(x => x.FindByNameAsync("Alice"))!.ReturnsAsync(null as ApplicationUser);
        
        var createForumPostDto = new CreateForumPostDto()
        {
            Content = "Content",
            UserName = "Alice"
        };
        
        forumPostRepositoryMock.Setup(x => x.GetForumPostById(postId)).ReturnsAsync(null as ForumPost);
        
        var controller = new ForumPostController(null!, forumPostRepositoryMock.Object, userManagerMock.Object);

        // Act
        var result = controller.EditPost(postId, createForumPostDto);
        
        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
       
        return Task.CompletedTask;
    }
    
    [Fact]
    public Task EditPost_RepoFail()
    {
        // Arrange
        var userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), 
            null, null, null, null, null, null, null, null); // Only IUserStore is required
        var forumPostRepositoryMock = new Mock<IForumPostRepository>();
        
        var postId = 1;
        var user = new ApplicationUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "Alice",
            Avatar = "default.png",
            CreatedAt = DateTime.Now
        };
        userManagerMock.Setup(x => x.FindByNameAsync(user.UserName)).ReturnsAsync(user);
        
        var createForumPostDto = new CreateForumPostDto()
        {
            Content = "Content",
            UserName = user.UserName
        };
        
        var forumPost = new ForumPost()
        {
            Id = postId,
            Content = "Content",
            CreatorId = user.Id,
            EditedBy = "",
            EditedAt = DateTime.MinValue
        };
        forumPostRepositoryMock.Setup(x => x.GetForumPostById(postId)).ReturnsAsync(forumPost);
        forumPostRepositoryMock.Setup(x => x.UpdateForumPost(It.IsAny<ForumPost>())).ReturnsAsync(false);
        
        var controller = new ForumPostController(null!, forumPostRepositoryMock.Object, userManagerMock.Object);

        // Act
        var result = controller.EditPost(postId, createForumPostDto);
        
        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
       
        return Task.CompletedTask;
    }
    
    // DELETE (Permanent deletion)
    [Fact]
    public Task DeletePost_Positive()
    {
        // Arrange
        var forumPostRepositoryMock = new Mock<IForumPostRepository>();
        
        var postId = 1;
        
        var forumPost = new ForumPost()
        {
            Id = postId,
            Content = "Content",
            CreatorId = Guid.NewGuid().ToString(),
            EditedBy = "",
            EditedAt = DateTime.MinValue
        };
        forumPostRepositoryMock.Setup(x => x.GetForumPostById(postId)).ReturnsAsync(forumPost);
        forumPostRepositoryMock.Setup(x => x.DeleteForumPost(postId)).ReturnsAsync(true);
        
        var controller = new ForumPostController(null!, forumPostRepositoryMock.Object, null!);

        // Act
        var result = controller.PermaDeleteSelectedForumPost(postId);
        
        // Assert
        Assert.IsType<OkResult>(result.Result);
       
        return Task.CompletedTask;
    }
    
    [Fact]
    public Task DeletePost_PostNotFound()
    {
        // Arrange
        var forumPostRepositoryMock = new Mock<IForumPostRepository>();
        
        var postId = 1;
        
        forumPostRepositoryMock.Setup(x => x.GetForumPostById(postId)).ReturnsAsync(null as ForumPost);
        
        var controller = new ForumPostController(null!, forumPostRepositoryMock.Object, null!);

        // Act
        var result = controller.PermaDeleteSelectedForumPost(postId);
        
        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
       
        return Task.CompletedTask;
    }
    
    [Fact]
    public Task DeletePost_RepoFail()
    {
        // Arrange
        var forumPostRepositoryMock = new Mock<IForumPostRepository>();
        
        var postId = 1;
        
        var forumPost = new ForumPost()
        {
            Id = postId,
            Content = "Content",
            CreatorId = Guid.NewGuid().ToString(),
            EditedBy = "",
            EditedAt = DateTime.MinValue
        };
        forumPostRepositoryMock.Setup(x => x.GetForumPostById(postId)).ReturnsAsync(forumPost);
        forumPostRepositoryMock.Setup(x => x.DeleteForumPost(postId)).ReturnsAsync(false);
        
        var controller = new ForumPostController(null!, forumPostRepositoryMock.Object, null!);

        // Act
        var result = controller.PermaDeleteSelectedForumPost(postId);
        
        // Assert
        Assert.IsType<BadRequestResult>(result.Result);
       
        return Task.CompletedTask;
    }
}