﻿using Forum3.Data;
using Forum3.Models;
using Microsoft.EntityFrameworkCore;

namespace Forum3.DAL;

public class ForumCategoryRepository : IForumCategoryRepository
{
  private readonly ForumDbContext _db;
  private readonly ILogger<ForumCategoryRepository> _logger;

  public ForumCategoryRepository(ForumDbContext db, ILogger<ForumCategoryRepository> logger)
  {
    _db = db;
    _logger = logger;
  }

  public async Task<List<ForumCategory>?> GetAll()
  {
    try
    {
      return await _db.ForumCategory.ToListAsync();
    }
    catch (Exception e)
    {
      _logger.LogError(e, "[ForumCategoryRepository] ForumCategory GetAll failed, error message: {E}", e.Message);
      return null;
    }
  }

  public async Task<ForumCategory?> GetForumCategoryById(int id)
  {
    try
    {
      return await _db.ForumCategory.FindAsync(id);
    }
    catch (Exception e)
    {
      _logger.LogError(e, "[ForumCategoryRepository] ForumCategory GetForumCategoryById failed, error message: {E}", e.Message);
      return null;
    }
  }
}
