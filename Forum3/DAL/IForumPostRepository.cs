﻿using Forum3.Models;

namespace Forum3.DAL;

public interface IForumPostRepository
{
  Task<IEnumerable<ForumPost>?> GetAll();
  Task<ForumPost?> GetForumPostById(int id);
  Task<IEnumerable<ForumPost>?> GetAllForumPostsByThreadId(int threadId);
  Task<ForumPost?> GetLatestPostInThread(int threadId);
  Task<IEnumerable<ForumPost>?> GetAllForumPostsByAccountId(string accountId);

  Task<bool> CreateNewForumPost(ForumPost forumPost);
  Task<bool> UpdateForumPost(ForumPost forumPost);
  Task<bool> DeleteForumPost(int forumPostId);

}
