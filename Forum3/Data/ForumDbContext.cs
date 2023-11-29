using Microsoft.EntityFrameworkCore;
using Forum3.Models;

namespace Forum3.Data;

public class ForumDbContext : DbContext
{
    public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<ForumCategory> ForumCategory { get; set; }
    public DbSet<ForumThread> ForumThread { get; set; }
    public DbSet<ForumPost> ForumPost { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
}