using Forum3.Models;

namespace Forum3.ClientApp.DAL;

public interface IForumThreadRepository
{
  Task<IEnumerable<ForumThread>?> GetAll();
  Task<ForumThread?> GetForumThreadById(int id);
  Task<IEnumerable<ForumThread>?> GetForumThreadsByCategoryId(int id);
  Task<IEnumerable<ForumThread>?> GetForumThreadsByAccountId(string accountId);
  Task<bool> CreateNewForumThread(ForumThread forumThread);
  Task<bool> UpdateForumThread(ForumThread forumThread);
  Task<bool> DeleteForumThread(int id);
}
