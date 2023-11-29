using Forum3.Models;

namespace Forum3.DAL;

public interface IForumThreadRepository
{
  Task<IEnumerable<ForumThread>?> GetAll();
  Task<ForumThread?> GetForumThreadById(int id);
  Task<List<ForumThread>?> GetForumThreadsByCategoryId(int id);
  Task<bool> CreateNewForumThread(ForumThread forumThread);
  Task<bool> UpdateForumThread(ForumThread forumThread);
  Task<bool> DeleteForumThread(int id);
}
