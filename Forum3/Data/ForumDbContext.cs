using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using Forum3.Models;

namespace Forum3.Data;

public class ForumDbContext
{
    public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
    {
        // Database.EnsureCreated();
    }
    public DbSet<ForumCategory> ForumCategory { get; set; }
    public DbSet<ForumThread> ForumThread { get; set; }
    public DbSet<ForumPost> ForumPost { get; set; }
    
    // Profile Wall
    public DbSet<WallPost> WallPost { get; set; }
    public DbSet<WallPostReply> WallPostReply { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
}