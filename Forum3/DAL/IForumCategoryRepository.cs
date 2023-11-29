using Forum3.Models;

namespace Forum3.DAL;

public interface IForumCategoryRepository
{
  Task<List<ForumCategory>?> GetAll();
  Task<ForumCategory?> GetForumCategoryById(int id);
}
