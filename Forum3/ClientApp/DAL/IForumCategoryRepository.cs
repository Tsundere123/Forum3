using Forum3.Models;

namespace Forum3.ClientApp.DAL;

public interface IForumCategoryRepository
{
  Task<List<ForumCategory>?> GetAll();
  Task<ForumCategory?> GetForumCategoryById(int id);
  Task<bool> CreateForumCategory(ForumCategory forumCategory);
  Task<bool> UpdateForumCategory(ForumCategory forumCategory);
  Task<bool> DeleteForumCategory(ForumCategory forumCategory);
}
