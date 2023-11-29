﻿using Forum3.DAL;
using Forum3.Data;
using Forum3.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Forum3.DAL;

public class ForumPostRepository : IForumPostRepository
{
    private readonly ForumDbContext _db;
    private readonly ILogger<ForumPostRepository> _logger;

    public ForumPostRepository(ForumDbContext db, ILogger<ForumPostRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<IEnumerable<ForumPost>?> GetAll()
    {
        try
        {
            return await _db.ForumPost.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "[ForumPostRepository] ForumPost GetAll failed, error message: {E}", e.Message);
            return null;
        }
    }

    public async Task<ForumPost?> GetForumPostById(int id)
    {
        try
        {
            return await _db.ForumPost.FindAsync(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "[ForumPostRepository] ForumPost GetForumPostById failed, error message: {E}", e.Message);
            return null;
        }
    }

    public async Task<IEnumerable<ForumPost>?> GetAllForumPostsByThreadId(int threadId)
    {
        try
        {
            return await _db.ForumPost.Where(t => t.ThreadId == threadId).ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "[ForumPostRepository] ForumPost GetAllForumPostsByThreadId failed, error message: {E}", e.Message);
            return null;
        }
    }

    public async Task<ForumPost?> GetLatestPostInThread(int threadId)
    {
        try
        {
            return await _db.ForumPost.Where(t => t.ThreadId == threadId).LastOrDefaultAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "[ForumPostRepository] ForumPost GetAllForumPostsByThreadId failed, error message: {E}", e.Message);
            return null;
        }
    }

    public async Task<IEnumerable<ForumPost>?> GetAllForumPostsByAccountId(string accountId)
    {
        try {
            return await _db.ForumPost.Where(p => p.CreatorId == accountId).ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "[ForumPostRepository] ForumPost GetAllForumPostsByAccountId failed, error message: {E}", e.Message);
            return null;
        }
    }

    public async Task<bool> CreateNewForumPost(ForumPost forumPost)
    {
        try
        {
            _db.ForumPost.Add(forumPost);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "[ForumPostRepository] ForumPost CreateNewForumPost failed, error message: {E}", e.Message);
            return false;
        }
    }

    public async Task<bool> UpdateForumPost(ForumPost forumPost)
    {
        try
        {
            _db.ForumPost.Update(forumPost);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "[ForumPostRepository] ForumPost UpdateForumPost failed, error message: {E}", e.Message);
            return false;
        }
    }

    public async Task<bool> DeleteForumPost(int forumPostId)
    {
        try
        {
            var forumPost = await _db.ForumPost.FindAsync(forumPostId);
            if (forumPost == null)
            {
                _logger.LogError("[ForumPostRepository] ForumPost DeleteForumPost failed, forumPost with id {ID} not found", forumPostId);
                return false;
            }

            _db.ForumPost.Remove(forumPost);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "[ForumPostRepository] ForumPost DeleteForumPost failed, error message: {E}", e.Message);
            return false;
        }
    }
}
